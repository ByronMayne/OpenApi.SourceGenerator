using Microsoft.OpenApi.Models;
using System.Collections.Generic;

namespace OpenApi.SourceGenerator.DataModels
{
    internal class ControllerOperation
    {
        private readonly List<OperationParameter> m_parameters;

        public string Path { get; }

        public string MethodName { get; }

        public OperationType Type { get; }

        public string Summary { get; }

        public bool IsDeprecated { get; }

        public IReadOnlyList<OperationParameter> Parameters
            => m_parameters;

        public ControllerOperation(string path, OperationType type, OpenApiOperation apiOption)
        {
            Path = path;
            Type = type;
            MethodName = NamingConventionFormat.ToMethodName(apiOption.OperationId);
            Summary = apiOption.Summary ?? apiOption.Description;
            IsDeprecated = apiOption.Deprecated;
            m_parameters = new List<OperationParameter>();

            foreach (OpenApiParameter parameter in apiOption.Parameters)
            {
                m_parameters.Add(new OperationParameter(parameter));

            }
        }
    }
}
