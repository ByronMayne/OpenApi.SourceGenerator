using HandlebarsDotNet;
using Microsoft.OpenApi.Models;

namespace OpenApi.SourceGenerator.Handelbars.Formatter.Implementations
{
    internal class OpenApiParameterFormatter : HandlebarsFormatter<OpenApiParameter>
    {
        private readonly OpenApiSchemaFormatter m_schemaFormatter;

        public OpenApiParameterFormatter(OpenApiFormatterOptions options)
        {
            m_schemaFormatter = new OpenApiSchemaFormatter(options);
        }

        public override void Format(ref FormatContext context, OpenApiParameter value, in EncodedTextWriter writer)
        {
            m_schemaFormatter.Format(ref context, value.Schema, writer);
            writer.WriteSafeString(" ");
            writer.WriteSafeString(NamingConventionFormat.ToParameterName(value.Name));

        }

    }
}
