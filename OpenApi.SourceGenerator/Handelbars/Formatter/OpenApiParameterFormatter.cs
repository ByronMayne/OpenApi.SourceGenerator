using HandlebarsDotNet;
using Microsoft.OpenApi.Models;

namespace OpenApi.SourceGenerator.Handelbars.Formatter
{
    internal class OpenApiParameterFormatter : AbstractFormatter<OpenApiParameter>
    {
        private readonly OpenApiSchemaFormatter m_schemaFormatter;

        public OpenApiParameterFormatter(OpenApiFormatterOptions options) : base(options)
        {
            m_schemaFormatter = new OpenApiSchemaFormatter(options);
        }

        public override void Format(OpenApiParameter apiParameter, EncodedTextWriter writer)
        {
            m_schemaFormatter.Format(apiParameter.Schema, writer);
            writer.WriteSafeString(" ");
            writer.WriteSafeString(NamingConventionFormat.ToParameterName(apiParameter.Name));
        
        }
    }
}
