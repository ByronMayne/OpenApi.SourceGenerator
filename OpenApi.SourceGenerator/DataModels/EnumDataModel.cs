using OpenApi.SourceGenerator.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenApi.SourceGenerator.DataModels
{
	internal class EnumDataModel : ClassDataModel
	{
		public EnumDataModel(string view, params DataModel[] dataModels) : base(view)
		{
		}
	}
}
