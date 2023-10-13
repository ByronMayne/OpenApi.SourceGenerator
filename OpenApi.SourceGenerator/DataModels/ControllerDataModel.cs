using OpenApi.SourceGenerator.DataModels.Visitors;
using System.Collections.Generic;
using System.Diagnostics;

namespace OpenApi.SourceGenerator.DataModels
{

	[DebuggerDisplay("{TypeName,nq}Controller")]
	internal class ControllerDataModel : ClassDataModel
	{
		private readonly List<ControllerOperationDataModel> m_operations;

		public override string HintName => $"Controllers.{TypeName}Controller.hbs";

		public IReadOnlyList<ControllerOperationDataModel> Operations
		{
			get => Get<List<ControllerOperationDataModel>>();
			private set => Set(value);
		}

		public ControllerDataModel(string name) : base("Controllers/Controller.hbs")
		{
			TypeName = NamingConventionFormat.ToTypeName(name);
			m_operations = new List<ControllerOperationDataModel>();
			Operations = m_operations;
		}

		public ControllerDataModel(string name, IEnumerable<ControllerOperationDataModel> operations) : this(name)
		{
			TypeName = name;
			m_operations.AddRange(operations);
		}

		public void Add(ControllerOperationDataModel operation)
		{
			m_operations.Add(operation);
		}


		/// <inheritdoc cref="DataModel"/>
		public override void Visit(IModelVisitor visitor)
		{
			foreach (ControllerOperationDataModel operationModel in m_operations)
			{
				operationModel.Visit(visitor);
			}
		}
	}
}
