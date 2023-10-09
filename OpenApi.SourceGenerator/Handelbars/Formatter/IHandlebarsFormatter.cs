using HandlebarsDotNet;
using System;

namespace OpenApi.SourceGenerator.Handelbars.Formatter
{
	internal interface IHandlebarsFormatter
	{
		/// <summary>
		/// Gets the type that the formatter handles
		/// </summary>
		Type Type { get; }

		void Format(ref FormatContext context, object value, in EncodedTextWriter writer);
	}
}
