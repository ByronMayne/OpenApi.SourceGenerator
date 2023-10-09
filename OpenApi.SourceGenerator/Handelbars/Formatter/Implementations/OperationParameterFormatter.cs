using HandlebarsDotNet;
using OpenApi.SourceGenerator.DataModels;

namespace OpenApi.SourceGenerator.Handelbars.Formatter.Implementations
{
    internal class OperationParameterFormatter : HandlebarsFormatter<OperationParameterDataModel>
    {
        public OperationParameterFormatter(OpenApiFormatterOptions options)
        { }

        public override void Format(ref FormatContext context, OperationParameterDataModel value, in EncodedTextWriter writer)
        {
            writer.WriteSafeString(NamingConventionFormat.ToParameterName(value.Name));
        }
    }
}
