using HandlebarsDotNet;
using OpenApi.SourceGenerator.Types;

namespace OpenApi.SourceGenerator.Handelbars.Formatter
{
    internal class AccessModifierFormatter : AbstractFormatter<AccessModifier>
    {
        public AccessModifierFormatter(OpenApiFormatterOptions options) : base(options)
        { }

        public override void Format(AccessModifier value, EncodedTextWriter writer)
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
