using Humanizer;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;
using OpenApi.SourceGenerator.DataModels;
using SGF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace OpenApi.SourceGenerator
{
    public struct View
    {
        public static readonly View SwaggerController = new View();
        public static readonly View ServiceCollectionExtensions = new View();
        public static readonly View OpenApiSettings;
        public static readonly View Controller;
        public static readonly View ICommand;
        public static readonly View Command;

        public readonly string ViewPath;

        public View(string viewPath)
        {
            ViewPath = viewPath;
        }

        static View()
        {
            SwaggerController = new View("Controllers\\SwaggerController.hbs");
            ServiceCollectionExtensions = new View("ServiceCollectionExtensions.hbs");
            OpenApiSettings = new View("OpenApiSettings.hbs");
            Controller = new View("Controllers\\Controller.hbs");
            ICommand = new View("Commands/ICommand.hbs");
            Command = new View("Commands/Command.hbs");
        }
    }


    [Generator]
    internal class OpenApiSourceGenerator : IncrementalGenerator
    {
        static OpenApiSourceGenerator()
        {
            Environment.CurrentDirectory = Path.GetTempPath();
        }


        public OpenApiSourceGenerator()
        {
        }


        protected override void OnInitialize(IncrementalGeneratorInitializationContext context)
        {
            IncrementalValuesProvider<AdditionalText> schemaProvider = context.AdditionalTextsProvider.Where(IsOpenApi);
            IncrementalValueProvider<AnalyzerConfigOptionsProvider> configOptionsProvider = context.AnalyzerConfigOptionsProvider;
            IncrementalValuesProvider<(AdditionalText Left, AnalyzerConfigOptionsProvider Right)> combined = schemaProvider.Combine(configOptionsProvider);

            context.RegisterSourceOutput(combined, (spc, nameAndContent) => GenerateOpenApiAsync(spc, nameAndContent.Left, nameAndContent.Right));
        }

        public record OperationRecord();


        private void GenerateOpenApiAsync(SourceProductionContext sourceContext, AdditionalText additionalText, AnalyzerConfigOptionsProvider configOptions)
        {
            _ = configOptions.GlobalOptions.TryGetValue("build_property.rootnamespace", out string defaultNamespace);

            ClassDataModel baseModel = new("")
            {
                RootNamespace = defaultNamespace
            };
            OpenApiSourceFactory factory = new OpenApiSourceFactory(sourceContext)
                .SetGlobal("RootNamespace", defaultNamespace)
                .Generate(View.SwaggerController, "SwaggerController.cs")
                .Generate(View.OpenApiSettings, "OpenApiSettings.cs");


            using FileStream fileStream = File.OpenRead(additionalText.Path);
            OpenApiStreamReader reader = new();
            OpenApiDocument apiDocument = reader.Read(fileStream, out OpenApiDiagnostic diagnostic);
            _ = apiDocument.ResolveReferences();






            List<ControllerOperation> operations = new List<ControllerOperation>();

            // Setup
            OpenApiTag defaultTag = new() { Name = "Default" };
            Dictionary<OpenApiTag, ControllerModel> controllerMap = new();
            foreach (KeyValuePair<string, OpenApiPathItem> pathProperty in apiDocument.Paths)
            {
                foreach (KeyValuePair<OperationType, OpenApiOperation> operationProperty in pathProperty.Value.Operations)
                {
                    OpenApiTag? tag = operationProperty.Value.Tags.FirstOrDefault() ?? defaultTag;

                    if (!controllerMap.TryGetValue(tag, out ControllerModel model))
                    {
                        model = new ControllerModel(tag.Name)
                        {
                            RootNamespace = defaultNamespace,
                        };
                        controllerMap[tag] = model;
                    }

                    ControllerOperation controllerOperation = new ControllerOperation(pathProperty.Key, operationProperty.Key, operationProperty.Value);
                    model.Add(controllerOperation);
                    operations.Add(controllerOperation);
                }
            }

            // Generate: Controllers
            foreach (ControllerModel controller in controllerMap.Values)
            {
                factory.Generate(View.Controller, $"Controllers_{controller.TypeName}.cs", controller);
            }

            // Generate: Commands
            foreach (ControllerOperation operation in operations)
            {
                factory.Generate(View.ICommand, $"Commands_I{operation.MethodName}Command.cs", operation);
                factory.Generate(View.Command, $"Commands_{operation.MethodName}Command.cs", operation);
            }

            // Generate Extension 
            factory.Generate(View.ServiceCollectionExtensions, "ServiceCollectionExtensions.cs", new Dictionary<string, object>()
            {
                ["Operations"] = operations
            });

            //List<OperationModel> records = new();

            //foreach (KeyValuePair<string, OpenApiPathItem> pathProperty in apiDocument.Paths)
            //{
            //    foreach (KeyValuePair<OperationType, OpenApiOperation> operationProperty in pathProperty.Value.Operations)
            //    {
            //        records.Generate(new OperationModel(pathProperty.Key, operationProperty.Key, operationProperty.Value));
            //    }
            //}


            //OperationMapModel map = OperationMapModel.Create(records);
            //foreach (OperationGroup operationGroup in map)
            //{
            //    string tag = operationGroup.Tag;

            //    if (operationGroup.Operations.Count == 0)
            //    {
            //        continue;
            //    }

            //    factory.Generate(new ControllerModel(tag, operationGroup.Operations.Select()

            ////    string content = m_handelbars.Compile("View/service.hbs", operationGroup);
            //  //  sourceContext.AddSource($"{NamingConventionFormat.ToTypeName(tag)}Service.cs", SourceText.From(content, Encoding.UTF8));

            //    foreach (OperationModel operationProperty in operationGroup.Operations)
            //    {
            //        string commandTemplate = m_handelbars.Compile("View/command.hbs", operationProperty);
            //        string hintName = $"Commands_I{NamingConventionFormat.ToTypeName(operationProperty.Spec.OperationId)}Comamnd.cs";
            //        sourceContext.AddSource(hintName, SourceText.From(commandTemplate, Encoding.UTF8));
            //    }
            //}
        }

        private static bool IsOpenApi(AdditionalText additionalText)
        {

            string fileName = Path.GetFileName(additionalText.Path).ToLower();
            return fileName switch
            {
                "openapi.json" or "openapi.yaml" or "openapi.yml" => true,
                _ => false,
            };
        }
    }
}