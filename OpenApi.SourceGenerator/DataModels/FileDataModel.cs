using OpenApi.SourceGenerator.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenApi.SourceGenerator.DataModels
{
    internal abstract class FileDataModel : DataModel
    {
        /// <summary>
        ///  Gets the hint path used to created the 
        /// </summary>
        public abstract string HintName { get; }

        protected FileDataModel(string view) : base(view)
        {
        }

        protected FileDataModel(string view, params DataModel[] dataModels) : base(view, dataModels)
        {
        }
    }
}
