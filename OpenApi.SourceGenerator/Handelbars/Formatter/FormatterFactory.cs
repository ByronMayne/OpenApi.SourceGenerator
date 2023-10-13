using HandlebarsDotNet;
using HandlebarsDotNet.IO;
using OpenApi.SourceGenerator.Components;
using OpenApi.SourceGenerator.DataModels;
using OpenApi.SourceGenerator.Handelbars.Formatter.Implementations;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace OpenApi.SourceGenerator.Handelbars.Formatter
{

	internal class FormatterFactory : IFormatterProvider, IFormatter
	{
		private FormatContext m_context;
		private readonly OpenApiFormatterOptions m_options;
		private readonly List<Type> m_types;
		private readonly IList<IHandlebarsFormatter> m_formatters;


		public FormatterFactory(FormatContext context, OpenApiFormatterOptions options)
		{
			m_options = options;
			m_context = context;
			m_types = new List<Type>();
			m_formatters = new List<IHandlebarsFormatter>();
			Register(new OpenApiSchemaFormatter(options));
			Register(new AccessModifierFormatter(options));
		}

		public void Register(IHandlebarsFormatter formatter)
		{
			m_formatters.Add(formatter);
			m_types.Add(formatter.Type);
		}


		public void Format<T>(T value, in EncodedTextWriter writer)
		{
			foreach (IHandlebarsFormatter formatter in m_formatters)
			{
				if (formatter.Type.IsInstanceOfType(value))
				{
					formatter.Format(ref m_context, value, writer);
				}
			}
		}

		public bool TryCreateFormatter(Type type, out IFormatter formatter)
		{
			formatter = null;
			if (m_types.Contains(type))
			{
				formatter = this;
				return true;
			}


			return false;
		}
	}
}
