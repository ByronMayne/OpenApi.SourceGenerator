using Microsoft.CodeAnalysis.Text;
using Microsoft.CodeAnalysis;
using OpenApi.SourceGenerator.Components;
using OpenApi.SourceGenerator.Types;
using System;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace OpenApi.SourceGenerator.DataModels
{
    internal class ClassDataModel : FileDataModel
    {

        public string TypeName
        {
            get => Get<string>();
            set => Set(value);
        }

        public string RootNamespace
        {
            get => Get(defaultValue: "OpenApi");
            set => Set(value);
        }

        public AccessModifier AccessModifier
        {
            get => Get(AccessModifier.Internal);
            set => Set(value);
        }

        public override string HintName { get; }

        public ClassDataModel(string view, params DataModel[] dataModels) : base(view, dataModels)
        {
            TypeName = Path.GetFileNameWithoutExtension(view);

            string filePath = Path.ChangeExtension(view, ".cs");
            string[] components = view.Split(new char[] { '/', '\\' }, StringSplitOptions.RemoveEmptyEntries);
            HintName = string.Join("_", components);
        }
    }
}
