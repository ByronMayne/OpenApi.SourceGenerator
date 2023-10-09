using HandlebarsDotNet;
using OpenApi.SourceGenerator.Types;

namespace OpenApi.SourceGenerator.Handelbars.Formatter.Implementations
{
    internal class AccessModifierFormatter : HandlebarsFormatter<AccessModifier>
    {
        private readonly OpenApiFormatterOptions m_options;

        public AccessModifierFormatter(OpenApiFormatterOptions options)
        {
            m_options = options;
        }

        public override void Format(ref FormatContext context, AccessModifier value, in EncodedTextWriter writer)
        {
            switch (value)
            {
                case AccessModifier.Public:
                    writer.Write("public");
                    break;
                case AccessModifier.Private:
                    writer.Write("private");
                    break;
                case AccessModifier.PrivateProtected:
                    writer.Write("private protected");
                    break;
                case AccessModifier.Protected:
                    writer.Write("protected");
                    break;
                case AccessModifier.ProtectedInternal:
                    writer.Write("protected internal");
                    break;
                case AccessModifier.Internal:
                    writer.Write("internal");
                    break;
            }
        }
    }
}
