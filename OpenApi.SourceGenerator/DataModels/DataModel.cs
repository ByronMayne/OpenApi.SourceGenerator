using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace OpenApi.SourceGenerator.Components
{
    internal abstract class DataModel : IReadOnlyDictionary<string, object>
    {
        private readonly Dictionary<string, object> m_data;

        /// <inheritdoc cref="IReadOnlyDictionary{TKey, TValue}"/>
        public object this[string propertyName]
        {
            get
            {
                if (m_data.ContainsKey(propertyName))
                {
                    return m_data[propertyName];
                }
                return null;
            }
        }

        /// <inheritdoc cref="IReadOnlyDictionary{TKey, TValue}"/>
        public IEnumerable<string> Keys
            => m_data.Keys;

        /// <inheritdoc cref="IReadOnlyDictionary{TKey, TValue}"/>
        public IEnumerable<object> Values
            => m_data.Values;
        /// <inheritdoc cref="IReadOnlyDictionary{TKey, TValue}"/>

        /// <summary>
        /// Gets the name of the view that will be used to render this object
        /// </summary>
        public string View
        {
            get => Get<string>();
            private set => Set(value);
        }

        public int Count
            => m_data.Count;

        public DataModel(string view)
        {
            m_data = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(view))
            {
                View = view;
            }
        }

        public DataModel(string view, params DataModel[] dataModels) : this(view)
        {
            foreach (DataModel dataModel in dataModels)
            {
                Add(dataModel);
            }
        }

        public void Add(DataModel dataModel)
        {
            foreach (KeyValuePair<string, object> property in dataModel)
            {
                m_data[property.Key] = property.Value;
            }
        }

        /// <inheritdoc cref="IReadOnlyDictionary{TKey, TValue}"/>
        public bool ContainsKey(string key)
        {
            return m_data.ContainsKey(key);
        }

        /// <inheritdoc cref="IReadOnlyDictionary{TKey, TValue}"/>
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return m_data.GetEnumerator();
        }

        /// <inheritdoc cref="IReadOnlyDictionary{TKey, TValue}"/>
        public bool TryGetValue(string key, out object value)
        {
            return m_data.TryGetValue(key, out value);
        }

        /// <inheritdoc cref="IReadOnlyDictionary{TKey, TValue}"/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_data.GetEnumerator();
        }

        protected void Set<T>(T value, [CallerMemberName] string callerMemberName = "")
        {
            if (value == null)
            {
                return;
            }

            m_data[callerMemberName] = value;
        }

        protected T Get<T>(T defaultValue, [CallerMemberName] string callerMemberName = "")
        {
            if (m_data.TryGetValue(callerMemberName, out object value))
            {
                return (T)value;
            }
            return defaultValue;

        }

        protected T Get<T>([CallerMemberName] string callerMemberName = "")
        {
            if (m_data.ContainsKey(callerMemberName))
            {
                return (T)m_data[callerMemberName];
            }

            return default(T);
        }
    }
}
