using Microsoft.OpenApi.Models;
using OpenApi.SourceGenerator.Components;
using OpenApi.SourceGenerator.DataModels.Visitors;
using System.Collections.Generic;
using System.Diagnostics;

namespace OpenApi.SourceGenerator.DataModels
{
	[DebuggerDisplay("{MethodName,nq}: {Path,nq}")]
	internal class ControllerOperationDataModel : DataModel
	{
		public string Path
		{
			get => Get<string>();
			set => Set(value);
		}

		public string MethodName
		{
			get => Get<string>();
			set => Set(value);
		}

		public OperationType Type
		{
			get => Get<OperationType>();
			set => Set(value);
		}


		public string Summary
		{
			get => Get<string>();
			set => Set(value);
		}

		public bool IsDeprecated
		{
			get => Get<bool>();
			set => Set(value);
		}

		public IList<OperationParameterDataModel> Parameters
		{
			get => Get<IList<OperationParameterDataModel>>();
			set => Set(value);
		}

		public ControllerOperationDataModel(string path, OperationType type, OpenApiOperation apiOption) : base("controller_operation.hbs")
		{
			Path = path;
			Type = type;
			MethodName = NamingConventionFormat.ToMethodName(apiOption.OperationId);
			Summary = apiOption.Summary ?? apiOption.Description;
			IsDeprecated = apiOption.Deprecated;
			Parameters = new List<OperationParameterDataModel>();
			foreach (OpenApiParameter parameter in apiOption.Parameters)
			{
				Parameters.Add(new OperationParameterDataModel(parameter));

			}
		}

		/// <inheritdoc cref="DataModel"/>
		public override void Visit(IModelVisitor visitor)
		{
			foreach (OperationParameterDataModel parameter in Parameters)
			{
				visitor.Visit(parameter);
			}
		}
	}
}
