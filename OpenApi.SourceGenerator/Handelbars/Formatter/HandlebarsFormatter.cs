using HandlebarsDotNet;
using System;

namespace OpenApi.SourceGenerator.Handelbars.Formatter
{
	internal abstract class HandlebarsFormatter<T> : IHandlebarsFormatter
	{
		public Type Type { get; }

		public HandlebarsFormatter()
		{
			Type = typeof(T);
		}

		public abstract void Format(ref FormatContext context, T value, in EncodedTextWriter writer);

		void IHandlebarsFormatter.Format(ref FormatContext context, object value, in EncodedTextWriter writer)
			=> Format(ref context, (T)value, in writer); 
	}
}
