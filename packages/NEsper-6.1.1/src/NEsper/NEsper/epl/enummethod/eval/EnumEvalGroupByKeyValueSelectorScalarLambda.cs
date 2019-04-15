///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;

using com.espertech.esper.client;
using com.espertech.esper.compat.collections;
using com.espertech.esper.epl.expression.core;
using com.espertech.esper.epl.expression;
using com.espertech.esper.events.arr;

namespace com.espertech.esper.epl.enummethod.eval
{
    public class EnumEvalGroupByKeyValueSelectorScalarLambda 
        : EnumEvalBase
        , EnumEval
    {
        private readonly ExprEvaluator _secondExpression;
        private readonly ObjectArrayEventType _resultEventType;
    
        public EnumEvalGroupByKeyValueSelectorScalarLambda(ExprEvaluator innerExpression, int streamCountIncoming, ExprEvaluator secondExpression, ObjectArrayEventType resultEventType)
                    : base(innerExpression, streamCountIncoming)
        {
            _secondExpression = secondExpression;
            _resultEventType = resultEventType;
        }
    
        public Object EvaluateEnumMethod(EventBean[] eventsLambda, ICollection<object> target, bool isNewData, ExprEvaluatorContext context) {
            var result = new LinkedHashMap<object, ICollection<object>>();
    
            var resultEvent = new ObjectArrayEventBean(new Object[1], _resultEventType);
            var values = target;
            foreach (Object next in values) {
    
                resultEvent.Properties[0] = next;
                eventsLambda[StreamNumLambda] = resultEvent;
                var key = InnerExpression.Evaluate(new EvaluateParams(eventsLambda, isNewData, context));
    
                resultEvent.Properties[0] = next;
                var entry = _secondExpression.Evaluate(new EvaluateParams(eventsLambda, isNewData, context));
    
                var value = result.Get(key);
                if (value == null) {
                    value = new List<object>();
                    result.Put(key, value);
                }
                value.Add(entry);
            }
    
            return result;
        }
    }
}
