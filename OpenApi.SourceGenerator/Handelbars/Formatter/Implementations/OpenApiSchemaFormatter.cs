using HandlebarsDotNet;
using Microsoft.OpenApi.Models;
using System;
using System.Diagnostics;
using System.Linq;

namespace OpenApi.SourceGenerator.Handelbars.Formatter.Implementations
{
    internal class OpenApiSchemaFormatter : HandlebarsFormatter<OpenApiSchema>
    {
        public OpenApiSchemaFormatter(OpenApiFormatterOptions options)
        { }


        public override void Format(ref FormatContext context, OpenApiSchema value, in EncodedTextWriter writer)
        {
            writer.WriteSafeString(FormatSchema(value));
        }

        private string FormatSchema(OpenApiSchema schema)
        {
            string typeName = "";

            switch (schema.Type)
            {
                case "number":
                    typeName = FormatNumber(schema);
                    break;
                case "integer":
                    typeName = FormatInteger(schema);
                    break;
                case "string":
                    typeName = FormatString(schema);
                    break;
                case "boolean":
                    typeName = "bool";
                    break;
                case "array":
                    typeName = FormatArray(schema);
                    break;
            }

            return typeName;
        }


        private string FormatNumber(OpenApiSchema schema)
        {
            string format = schema.Format switch
            {
                "float" => "float",
                "double" => "double",
                _ => "float",
            };
            return schema.Nullable
                ? $"{format}?"
                : format;
        }

        private string FormatInteger(OpenApiSchema schema)
        {
            string format = schema.Format switch
            {
                "int32" => "int",
                "int64" => "long",
                _ => "int",
            };
            return schema.Nullable
              ? $"{format}?"
              : format;
        }

        private string FormatString(OpenApiSchema schema)
        {
            return schema.Format switch
            {
                "date-time" or "date" => schema.Nullable ? "DateTime?" : "DateTime",
                "byte" or "binary" => "byte[]",
                _ => "string",
            };
        }

        private string FormatArray(OpenApiSchema schema)
        {
            string itemType = FormatSchema(schema.Items);
            return $"IList<{itemType}>";
        }
    }
}
