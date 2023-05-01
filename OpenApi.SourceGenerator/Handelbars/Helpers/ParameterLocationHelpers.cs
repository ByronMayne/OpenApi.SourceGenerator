using HandlebarsDotNet;
using Microsoft.OpenApi.Models;
using System;

namespace OpenApi.SourceGenerator.Handelbars.Helpers
{

    internal class ParameterLocationHelpers : AbstractHelper
    {
        public ParameterLocationHelpers(HandlebarsConfiguration configuration) : base("ParameterLocation", configuration)
        {
            RegisterHelper("ifQuery", IfQueryBlock);
            RegisterHelper("ifHeader", IfHeaderBlock);
            RegisterHelper("ifIsPath", IfPathBlock);
            RegisterHelper("ifCookie", IfCookieBlock);
        }




        private void IfQueryBlock(EncodedTextWriter output, BlockHelperOptions options, Context context, Arguments arguments)
            => IfLocation(ParameterLocation.Query, output, options, context, arguments);

        private void IfHeaderBlock(EncodedTextWriter output, BlockHelperOptions options, Context context, Arguments arguments)
            => IfLocation(ParameterLocation.Header, output, options, context, arguments);

        private void IfPathBlock(EncodedTextWriter output, BlockHelperOptions options, Context context, Arguments arguments)
            => IfLocation(ParameterLocation.Path, output, options, context, arguments);

        private void IfCookieBlock(EncodedTextWriter output, BlockHelperOptions options, Context context, Arguments arguments)
            => IfLocation(ParameterLocation.Cookie, output, options, context, arguments);


        private void IfLocation(ParameterLocation location, EncodedTextWriter output, BlockHelperOptions options, Context context, Arguments arguments)
        {
            string value = arguments[0] as string;
            bool isValue = Enum.TryParse(value, true, out ParameterLocation parsedLocation) && parsedLocation == location;
            if (isValue)
            {
                options.Template(output, context);
            }
        }
    }
}
