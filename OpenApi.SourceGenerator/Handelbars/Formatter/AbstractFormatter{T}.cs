using HandlebarsDotNet;
using HandlebarsDotNet.IO;
using System;

namespace OpenApi.SourceGenerator.Handelbars.Formatter
{
    internal abstract class AbstractFormatter<T> : IFormatter, IFormatterProvider
    {
        protected readonly OpenApiFormatterOptions m_options;

        protected AbstractFormatter(OpenApiFormatterOptions options)
        {
            m_options = options;
        }

        /// <summary>
        /// Used for format the value to use
        /// </summary>
        /// <param name="value">The value to format</param>
        /// <param name="writer">The writer to append</param>
        public abstract void Format(T value, EncodedTextWriter writer);


        /// <inheritdoc cref="IFormatProvider"/>
        void IFormatter.Format<T1>(T1 value, in EncodedTextWriter writer)
        {
            if (value is T asT)
            {
                Format(asT, writer);
                return;
            }
            throw new ArgumentException($"The AbstractFormatter {GetType().FullName} can only convert {typeof(T).FullName}.");
        }

        /// <inheritdoc cref="IFormatter"/>
        bool IFormatterProvider.TryCreateFormatter(Type type, out IFormatter formatter)
        {
            formatter = null;
            if (type == typeof(T))
            {
                formatter = this;
                return true;
            }
            return false;
        }
    }
}
