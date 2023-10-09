using HandlebarsDotNet;
using OpenApi.SourceGenerator.Handelbars.Components;
using OpenApi.SourceGenerator.Handelbars.Formatter;
using OpenApi.SourceGenerator.Handelbars.Helpers;
using System;
using System.Diagnostics;

namespace OpenApi.SourceGenerator.Handelbars
{
    internal static class HandlebarsConfigurationExtensions
    {
        public static HandlebarsConfiguration AddOpenApiFormatters(this HandlebarsConfiguration configuration, Action<OpenApiFormatterOptions> configure = null)
        {
            OpenApiFormatterOptions options = new OpenApiFormatterOptions();
            FormatContext context = new FormatContext();
            if (configure != null) configure(options);
            configuration.FormatterProviders.Add(new FormatterFactory(context, options));


            return configuration;
        }

        /// <summary>
        /// Adds all the open api handlers that have been defined 
        /// </summary>
        public static HandlebarsConfiguration AddOpenApiHelpers(this HandlebarsConfiguration handlebars)
        {
            _ = new ParameterLocationHelpers(handlebars);
            _ = new NamingConventionHelpers(handlebars);
#pragma warning disable CS0618 // Type or member is obsolete
            handlebars.Compatibility.RelaxedHelperNaming = true;
#pragma warning restore CS0618 // Type or member is obsolete
            return handlebars;
        }

        public static HandlebarsConfiguration AddEmbeddedFileSystem(this HandlebarsConfiguration configuration, Action<OpenApiFormatterOptions> configure = null)
        {
            configuration.FileSystem = new EmbeddedResourceFileSystem();
            return configuration;
        }
    }
}
