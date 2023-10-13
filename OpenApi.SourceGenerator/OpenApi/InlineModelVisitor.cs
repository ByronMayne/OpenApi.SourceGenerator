using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenApi.SourceGenerator.OpenApi
{
    internal class InlineModelVisitor : OpenApiVisitorBase
    {
        public Dictionary<string, OpenApiSchema> InlineModels { get; }

        public InlineModelVisitor()
        {
            InlineModels = new Dictionary<string, OpenApiSchema>();
        }

        public override void Visit(IList<OpenApiParameter> parameters)
        {
            foreach (OpenApiParameter parameter in parameters)
            {
                OpenApiSchema schema = parameter.Schema;
                string enumName = $"{NamingConventionFormat.ToTypeName(parameter.Name)}Value";

                if (schema.Enum.Any())
                {
                    parameter.Schema.TrySetTypeName(enumName, false);
                }
                else if (schema.Items != null && schema.Items.Enum.Any())
                {
                    schema.Items.TrySetTypeName(enumName, false);
                }
            }
        }   

        public static InlineModelVisitor Run(OpenApiDocument document)
        {
            InlineModelVisitor visitor = new InlineModelVisitor();
            OpenApiWalker walker = new OpenApiWalker(visitor);
            walker.Walk(document);
            return visitor;
        }
    }
}
