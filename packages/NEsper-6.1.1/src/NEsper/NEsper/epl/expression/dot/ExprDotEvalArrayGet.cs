///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

using System;

using com.espertech.esper.client;
using com.espertech.esper.compat;
using com.espertech.esper.compat.logging;
using com.espertech.esper.epl.expression.core;
using com.espertech.esper.epl.rettype;

namespace com.espertech.esper.epl.expression.dot
{
    public class ExprDotEvalArrayGet : ExprDotEval
    {
        private static readonly ILog Log =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly EPType _typeInfo;
        private readonly ExprEvaluator _indexExpression;

        public ExprDotEvalArrayGet(ExprEvaluator index, Type componentType)
        {
            _indexExpression = index;
            _typeInfo = EPTypeHelper.SingleValue(componentType);
        }

        public object Evaluate(object target, EvaluateParams evalParams)
        {
            if (target == null)
            {
                return null;
            }

            var index = _indexExpression.Evaluate(evalParams);
            if (index == null)
            {
                return null;
            }
            if (!index.IsInt())
            {
                return null;
            }

            var indexNum = index.AsInt();
            var targetArray = (Array) target;
            if (targetArray.Length <= indexNum)
            {
                return null;
            }
            return targetArray.GetValue(indexNum);
        }

        public EPType TypeInfo
        {
            get { return _typeInfo; }
        }

        public void Visit(ExprDotEvalVisitor visitor)
        {
            visitor.VisitArraySingleItemSource();
        }
    }
} // end of namespace
