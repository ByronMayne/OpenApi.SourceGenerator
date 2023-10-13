using System;
using System.Collections.Generic;
using System.Text;

namespace OpenApi.SourceGenerator.DataModels.Visitors
{
    internal interface IModelVisitor
    {
        void Visit(ClassDataModel classModel);
        void Visit(EnumDataModel enumModel);
        void Visit(ControllerDataModel controllerModel);
        void Visit(OperationParameterDataModel operationParameterDataModel);
    }
}
