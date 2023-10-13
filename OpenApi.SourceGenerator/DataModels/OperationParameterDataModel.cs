#nullable enable

using Microsoft.OpenApi.Models;
using OpenApi.SourceGenerator.DataModels.Visitors;
using OpenApi.SourceGenerator.Handelbars.Formatter.Implementations;
using OpenApi.SourceGenerator.Types;
using System;
using System.Linq;

namespace OpenApi.SourceGenerator.DataModels
{
	internal class OperationParameterDataModel
	{
		public string Name { get; }

		public OpenApiSchema Schema { get; }

		/// <summary>
		/// Gets the enum model if the schema is a local refernece
		/// </summary>
		public EnumDataModel? EnumModel { get; }

		public OperationParameterDataModel(OpenApiParameter parameter)
		{
			Name = NamingConventionFormat.ToParameterName(parameter.Name);
			Schema = parameter.Schema;
			// Single Enum
			if (Schema.Enum.Any())
			{
				EnumModel = new EnumDataModel("Enum.hbs")
				{
					RootNamespace = null,
					AccessModifier = AccessModifier.Public,
					TypeName = $"{NamingConventionFormat.ToTypeName(parameter.Name)}Enum"
				};
				Schema.Type = EnumModel.TypeName;
			}
			// Multi Enum
			if(Schema.Items != null && Schema.Items.Enum.Any())
			{
				EnumModel = new EnumDataModel("Enum.hbs")
				{
					RootNamespace = null,
					AccessModifier = AccessModifier.Public,
					TypeName = $"{NamingConventionFormat.ToTypeName(parameter.Name)}Enum"
				};
				Schema.Items.Type = EnumModel.TypeName;
			}
		}

		public void Visit(IModelVisitor visitor)
		{
			if (EnumModel != null)
			{
				visitor.Visit(EnumModel);
			}
		}

	}
}
