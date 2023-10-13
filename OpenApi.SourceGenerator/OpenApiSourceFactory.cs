using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using OpenApi.SourceGenerator.Components;
using System;
using System.Collections.Generic;
using System.Linq;
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

		public OpenApiSourceFactory Generate<T>(View view, string hintPath, T dataModel) where T : DataModel
			=> Generate(view, hintPath, dataModel.ToDataContext());

		public OpenApiSourceFactory Generate(View view, string hintPath) 
			=> Generate(view, hintPath, new Dictionary<string, object>());

		public OpenApiSourceFactory Generate(View view, string hintPath, IDictionary<string, object> model)
		{
			var context = CreateContext(model);
			string classDefinitionTemplate = m_handelbars.Compile(view.ViewPath, context);

			SourceText sourceText = SourceText.From(classDefinitionTemplate, Encoding.UTF8);
			m_sourceProductionContext.AddSource(hintPath, sourceText);
			return this;
		}

		private IDictionary<string, object> CreateContext(IDictionary<string, object> dataContext)
		{
			dataContext["Globals"] = m_globals;
			return dataContext;
		}
	}
}