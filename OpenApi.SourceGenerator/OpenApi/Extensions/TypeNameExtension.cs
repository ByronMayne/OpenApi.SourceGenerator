using Microsoft.OpenApi;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Writers;

namespace OpenApi.SourceGenerator.OpenApi.Extensions
{
    internal class TypeNameExtension : IOpenApiExtension
    {
        public const string PropertyName = "x-type-name";

        public string Name { get; }

        public TypeNameExtension(string name)
        {
            Name = name;
        }

        public void Write(IOpenApiWriter writer, OpenApiSpecVersion specVersion)
        {
            writer.WriteProperty(PropertyName, Name);
        }
    }
}
