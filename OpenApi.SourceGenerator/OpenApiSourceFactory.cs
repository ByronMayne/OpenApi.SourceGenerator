using HandlebarsDotNet;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using OpenApi.SourceGenerator.Components;
using OpenApi.SourceGenerator.DataModels;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace OpenApi.SourceGenerator
{
    internal class OpenApiSourceFactory
    {
        private readonly SourceProductionContext m_sourceProductionContext;
        private readonly HandlebarsRenderer m_handelbars;
        private readonly IDictionary<string, object> m_globals;

        public OpenApiSourceFactory(SourceProductionContext sourceProduction)
        {
            m_sourceProductionContext = sourceProduction;
            m_handelbars = new HandlebarsRenderer();
            m_globals = new Dictionary<string, object>();
        }

        public OpenApiSourceFactory SetGlobal<T>(string name, T value)
        {
            m_globals[name] = value;
            return this;
        }


        public OpenApiSourceFactory Generate(View view, string hintPath)
            => Generate<object>(view, hintPath, null);

        public OpenApiSourceFactory Generate<T>(View view, string hintPath, T model)
        {
            var context = CreateContext(model);
            string classDefinitionTemplate = m_handelbars.Compile(view.ViewPath, context);

            SourceText sourceText = SourceText.From(classDefinitionTemplate, Encoding.UTF8);
            m_sourceProductionContext.AddSource(hintPath, sourceText);
            return this;
        }

        private IDictionary<string, object> CreateContext(object model)
        {
            Dictionary<string, object> mergedValues = new Dictionary<string, object>();
            mergedValues["Globals"] = m_globals;

            if (model != null)
            {
                mergedValues["Model"] = model;
            }
            return mergedValues;
        }
    }
}