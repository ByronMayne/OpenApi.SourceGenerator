using HandlebarsDotNet;
using Seed.IO;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace OpenApi.SourceGenerator.Handelbars.Components
{
    internal class EmbeddedResourceFileSystem : ViewEngineFileSystem
    {
        private readonly IReadOnlyDictionary<RelativePath, string> m_virtualFileSystemMap;

        /// <inheritdoc cref="ViewEngineFileSystem"/>
        public EmbeddedResourceFileSystem()
        {
            Dictionary<RelativePath, string> virtualFileSystem = new();

            Regex viewPattern = new("OAPI::(?<Path>.*)", RegexOptions.IgnoreCase);
            Assembly assembly = typeof(EmbeddedResourceFileSystem).Assembly;

            string[] resourceNames = assembly.GetManifestResourceNames();
            foreach (string resource in resourceNames)
            {
                Match match = viewPattern.Match(resource);
                if (match.Success)
                {
                    string path = match.Groups["Path"].Value;
                    using Stream resourceStream = assembly.GetManifestResourceStream(resource);
                    using StreamReader streamReader = new(resourceStream);
                    string content = streamReader.ReadToEnd();
                    virtualFileSystem.Add(new RelativePath(path), content);
                }
            }

            m_virtualFileSystemMap = virtualFileSystem;
        }

        /// <inheritdoc cref="ViewEngineFileSystem"/>
        public override bool FileExists(string filename)
        {
            RelativePath relativePath = new($"Views/{filename}");
            return m_virtualFileSystemMap.ContainsKey(relativePath);
        }

        /// <inheritdoc cref="ViewEngineFileSystem"/>
        public override string GetFileContent(string filename)
        {
            RelativePath relativePath = new($"Views/{filename}");
            return m_virtualFileSystemMap[relativePath];
        }

        /// <inheritdoc cref="ViewEngineFileSystem"/>
        protected override string CombinePath(string dir, string otherFileName)
        {
            return new RelativePath(dir) / otherFileName;
        }
    }
}