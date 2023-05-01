using HandlebarsDotNet;
using HandlebarsDotNet.Helpers;
using HandlebarsDotNet.Helpers.BlockHelpers;
using System;

namespace OpenApi.SourceGenerator.Handelbars.Helpers
{
    internal abstract class AbstractHelper
    {
        private readonly string m_className;
        private readonly HandlebarsConfiguration m_configuration;

        protected AbstractHelper(string className, HandlebarsConfiguration configuration)
        {
            m_className = className;
            m_configuration = configuration;
        }

        protected void RegisterStringFormatterHelper(string helperName, Func<string, string> stringFormatter)
        {
            HandlebarsHelperWithOptions method = new((in EncodedTextWriter output, in HelperOptions options, in Context context, in Arguments arguments) =>
            {
                if (arguments.Length == 0)
                {
                    throw new HandlebarsException("String helpers require values to be assinged");
                }

                string value = arguments[0].ToString();

                if (value == null || value.Length == 0)
                {
                    output.Write(value);
                    return;
                }

                output.Write(stringFormatter(value));
            });
            DelegateHelperWithOptionsDescriptor descriptor = new(helperName, method);
            m_configuration.Helpers.AddOrReplace($"{m_className}.{helperName}", descriptor);
        }

        protected void RegisterHelper(string helperName, HandlebarsBlockHelper helperFunction)
        {
            string name = $"{m_className}.{helperName}";

            DelegateBlockHelperDescriptor descriptor = new(name, helperFunction);
            m_configuration.BlockHelpers.AddOrReplace(name, descriptor);
        }

        protected void RegisterHelper(string helperName, HandlebarsHelperWithOptions helperWithOptions)
        {
            string name = $"{m_className}.{helperName}";
            DelegateHelperWithOptionsDescriptor descriptor = new(name, helperWithOptions);
            m_configuration.Helpers.AddOrReplace(name, descriptor);

        }
    }
}
