using System;
using System.Collections.Generic;
using System.Text;

namespace OpenApi.SourceGenerator.DataModels.Visitors
{
	internal class ModelVisitor : IModelVisitor
	{
		public void Visit(ClassDataModel classModel)
		{
			throw new NotImplementedException();
		}

		public void Visit(EnumDataModel enumModel)
		{
			throw new NotImplementedException();
		}


		public void Visit(ControllerDataModel controllerModel)
		{
			throw new NotImplementedException();
		}

		public void Visit(OperationParameterDataModel operationParameterDataModel)
		{
			throw new NotImplementedException();
		}
	}
}
