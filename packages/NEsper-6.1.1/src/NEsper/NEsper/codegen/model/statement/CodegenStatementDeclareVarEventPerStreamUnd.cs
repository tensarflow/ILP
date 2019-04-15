///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using com.espertech.esper.codegen.core;

// import static com.espertech.esper.codegen.core.CodeGenerationHelper.appendClassName;

namespace com.espertech.esper.codegen.model.statement
{
    public class CodegenStatementDeclareVarEventPerStreamUnd : CodegenStatementBase
    {
        private readonly Type _clazz;
        private readonly int _streamNum;

        public CodegenStatementDeclareVarEventPerStreamUnd(Type clazz, int streamNum)
        {
            this._clazz = clazz;
            this._streamNum = streamNum;
        }

        public override void RenderStatement(TextWriter textWriter)
        {
            CodeGenerationHelper.AppendClassName(textWriter, _clazz, null);
            textWriter.Write(" s");
            textWriter.Write(_streamNum);
            textWriter.Write("=(");
            CodeGenerationHelper.AppendClassName(textWriter, _clazz, null);
            textWriter.Write(")eventsPerStream[");
            textWriter.Write(_streamNum);
            textWriter.Write("].Underlying");
        }

        public override void MergeClasses(ICollection<Type> classes)
        {
            classes.Add(_clazz);
        }
    }
} // end of namespace