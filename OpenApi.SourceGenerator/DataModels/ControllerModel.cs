using System.Collections.Generic;

namespace OpenApi.SourceGenerator.DataModels
{

    internal class ControllerModel : ClassDataModel
    {
        private readonly List<ControllerOperation> m_operations;

        public IReadOnlyList<ControllerOperation> Operations
        {
            get => Get<List<ControllerOperation>>();
            private set => Set(value);
        }


        public override string HintName => $"Controllers_{TypeName}Controller.hbs";

        public ControllerModel(string name) : base("Controllers/Controller.hbs")
        {
            TypeName = NamingConventionFormat.ToTypeName(name);
            m_operations = new List<ControllerOperation>();
            Operations = m_operations;
        }

        public ControllerModel(string name, IEnumerable<ControllerOperation> operations) : this(name)
        {
            TypeName = name;
            m_operations.AddRange(operations);
        }

        public void Add(ControllerOperation operation)
        {
            m_operations.Add(operation);
        }

    }
}
