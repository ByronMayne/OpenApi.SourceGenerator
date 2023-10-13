using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using OpenApi.SourceGenerator.OpenApi.Extensions;

namespace OpenApi.SourceGenerator.OpenApi
{
    internal static class OpenApiSchemaExtensions
	{
		public const string TYPE_NAME_PROPERTY_KEY = "x-type-name";

		public static bool TryGetTypeName(this OpenApiSchema schema, out string typeName)
		{
			typeName = "";

			if (schema.Extensions.TryGetValue(TypeNameExtension.PropertyName, out IOpenApiExtension extension))
			{
				typeName = ((TypeNameExtension)extension).Name;
				return true;
			}
			return false;
		}

		/// <summary>
		/// Attempts to set the type name only if it does not already have a value
		/// </summary>
		public static bool TrySetTypeName(this OpenApiSchema schema, string typeName, bool overWrite = false)
		{
			if(schema.Extensions.TryGetValue(TypeNameExtension.PropertyName, out var extension) || overWrite)
			{
				SetTypeName(schema, typeName);
				return true;
			}
			return false;
		}

		public static void SetTypeName(this OpenApiSchema schema, string typeName)
		{
			if (!schema.Properties.TryGetValue(TypeNameExtension.PropertyName, out OpenApiSchema apiSchema))
			{
				schema.Extensions[TypeNameExtension.PropertyName] = new TypeNameExtension(typeName);
			}
		}
	}
}
