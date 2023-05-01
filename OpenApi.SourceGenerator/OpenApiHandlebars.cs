using HandlebarsDotNet;
using OpenApi.SourceGenerator.Handelbars;
using System.Collections.Generic;

namespace OpenApi.SourceGenerator
{
    internal class HandlebarsRenderer
    {
        private readonly IHandlebars m_handlebars;
        private readonly IDictionary<string, HandlebarsTemplate<object, object>> m_templateCache;

        public HandlebarsRenderer()
        {
            HandlebarsConfiguration configuration = new HandlebarsConfiguration()
                .AddEmbeddedFileSystem()
                .AddOpenApiFormatters()
                .AddOpenApiHelpers();

            m_handlebars = Handlebars.Create(configuration);
            m_templateCache = new Dictionary<string, HandlebarsTemplate<object, object>>();
        }


        public string Compile<T>(string viewName, T context)
        {
            if (!m_templateCache.TryGetValue(viewName, out HandlebarsTemplate<object, object> template))
            {
                template = m_handlebars.CompileView(viewName);
                m_templateCache[viewName] = template;
            }

            return template(context);

        }
    }
}