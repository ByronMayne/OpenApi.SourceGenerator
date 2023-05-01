using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenApi.SourceGenerator.DataModels
{
    internal class OperationParameter
    {
        public string Name { get; }

        public OpenApiSchema Schema { get; }

        public OperationParameter(OpenApiParameter parameter)
        {
            Name = NamingConventionFormat.ToParameterName(parameter.Name);
            Schema = parameter.Schema;
        }
    }
}
