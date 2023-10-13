using Humanizer;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;
using OpenApi.SourceGenerator.DataModels;
using OpenApi.SourceGenerator.OpenApi;
using SGF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace OpenApi.SourceGenerator
{
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

            OpenApiSourceFactory factory = new OpenApiSourceFactory(sourceContext)
                .SetGlobal("RootNamespace", defaultNamespace)
                .Generate(View.SwaggerController, "SwaggerController.cs")
                .Generate(View.OpenApiSettings, "OpenApiSettings.cs");


            using FileStream fileStream = File.OpenRead(additionalText.Path);
            OpenApiStreamReader reader = new();
            OpenApiDocument apiDocument = reader.Read(fileStream, out OpenApiDiagnostic diagnostic);
            _ = apiDocument.ResolveReferences();

            // Apply vistors 
            InlineModelVisitor.Run(apiDocument);



			List<ControllerOperationDataModel> operations = new List<ControllerOperationDataModel>();

            // Setup
            OpenApiTag defaultTag = new() { Name = "Default" };
            Dictionary<OpenApiTag, ControllerDataModel> controllerMap = new();
            foreach (KeyValuePair<string, OpenApiPathItem> pathProperty in apiDocument.Paths)
            {
                foreach (KeyValuePair<OperationType, OpenApiOperation> operationProperty in pathProperty.Value.Operations)
                {
                    OpenApiTag? tag = operationProperty.Value.Tags.FirstOrDefault() ?? defaultTag;

                    if (!controllerMap.TryGetValue(tag, out ControllerDataModel model))
                    {
                        model = new ControllerDataModel(tag.Name)
                        {
                            RootNamespace = defaultNamespace,
                        };
                        controllerMap[tag] = model;
                    }

                    ControllerOperationDataModel controllerOperation = new ControllerOperationDataModel(pathProperty.Key, operationProperty.Key, operationProperty.Value);
                    model.Add(controllerOperation);
                    operations.Add(controllerOperation);
                }
            }

            // Generate: Controllers
            foreach (ControllerDataModel controller in controllerMap.Values)
            {
				Logger.Information("Controller: {Name}", controller.TypeName);
				factory.Generate(View.Controller, $"Controllers.{controller.TypeName}.cs", controller);
            }

            // Generate: Commands
            foreach (ControllerOperationDataModel operation in operations)
            {
                Logger.Information("Command: {Name}", operation.MethodName);
                factory.Generate(View.Command, $"Commands.{operation.MethodName}Command.cs", operation);
            }

            // Generate Extension 
            factory.Generate(View.ServiceCollectionExtensions, "ServiceCollectionExtensions.cs", new Dictionary<string, object>()
            {
                ["Operations"] = operations
            });
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