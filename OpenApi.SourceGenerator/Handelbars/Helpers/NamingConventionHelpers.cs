#nullable enable

using HandlebarsDotNet;
using Humanizer;
using System;
using System.Diagnostics;
using System.Text;

namespace OpenApi.SourceGenerator.Handelbars.Helpers
{
    internal class NamingConventionHelpers : AbstractHelper
    {
        public NamingConventionHelpers(HandlebarsConfiguration configuration) : base("NamingConvention", configuration)
        {
            RegisterStringFormatterHelper("Type", NamingConventionFormat.ToTypeName);
            RegisterStringFormatterHelper("Method", NamingConventionFormat.ToMethodName);
            RegisterStringFormatterHelper("Parameter", NamingConventionFormat.ToParameterName);
        }

    
    }
}
