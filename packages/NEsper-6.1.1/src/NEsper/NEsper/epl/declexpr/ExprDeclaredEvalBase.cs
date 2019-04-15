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
using com.espertech.esper.epl.spec;
using com.espertech.esper.events;
using com.espertech.esper.metrics.instrumentation;

namespace com.espertech.esper.epl.declexpr
{
    public abstract class ExprDeclaredEvalBase : ExprEvaluatorTypableReturn, ExprEvaluatorEnumeration
    {
        private readonly ExprEvaluator _innerEvaluator;
        private readonly ExprEvaluatorEnumeration _innerEvaluatorLambda;
        private readonly ExpressionDeclItem _prototype;
        private readonly bool _isCache;
    
        public abstract EventBean[] GetEventsPerStreamRewritten(EventBean[] eventsPerStream);

        protected ExprDeclaredEvalBase(ExprEvaluator innerEvaluator, ExpressionDeclItem prototype, bool isCache)
        {
            _innerEvaluator = innerEvaluator;
            _prototype = prototype;
            if (innerEvaluator is ExprEvaluatorEnumeration) {
                _innerEvaluatorLambda = (ExprEvaluatorEnumeration) innerEvaluator;
            }
            else {
                _innerEvaluatorLambda = null;
            }
            _isCache = isCache;
        }

        public ExprEvaluator InnerEvaluator
        {
            get { return _innerEvaluator; }
        }

        public Type ReturnType
        {
            get { return _innerEvaluator.ReturnType; }
        }

        public IDictionary<string, object> RowProperties
        {
            get
            {
                if (_innerEvaluator is ExprEvaluatorTypableReturn)
                {
                    return ((ExprEvaluatorTypableReturn) _innerEvaluator).RowProperties;
                }
                return null;
            }
        }

        public bool? IsMultirow
        {
            get
            {
                if (_innerEvaluator is ExprEvaluatorTypableReturn)
                {
                    return ((ExprEvaluatorTypableReturn) _innerEvaluator).IsMultirow;
                }
                return null;
            }
        }

        public Object[] EvaluateTypableSingle(EventBean[] eventsPerStream, bool isNewData, ExprEvaluatorContext context)
        {
            return ((ExprEvaluatorTypableReturn) _innerEvaluator).EvaluateTypableSingle(
                eventsPerStream, isNewData, context);
        }

        public Object[][] EvaluateTypableMulti(EventBean[] eventsPerStream, bool isNewData, ExprEvaluatorContext context)
        {
            return ((ExprEvaluatorTypableReturn) _innerEvaluator).EvaluateTypableMulti(
                eventsPerStream, isNewData, context);
        }

        public object Evaluate(EvaluateParams evaluateParams)
        {
            object[] result = { null };

            using (Instrument.With(
                i => i.QExprDeclared(_prototype),
                i => i.AExprDeclared(result[0])))
            {
                // rewrite streams
                var events = GetEventsPerStreamRewritten(evaluateParams.EventsPerStream);
                var evaluateParamsX = new EvaluateParams(events, evaluateParams.IsNewData, evaluateParams.ExprEvaluatorContext);
                var context = evaluateParams.ExprEvaluatorContext;

                if (_isCache)
                {
                    // no the same cache as for iterator
                    var cache = context.ExpressionResultCacheService.AllocateDeclaredExprLastValue;
                    var entry = cache.GetDeclaredExpressionLastValue(_prototype, events);
                    if (entry != null)
                    {
                        return entry.Result;
                    }
                    result[0] = _innerEvaluator.Evaluate(evaluateParamsX);
                    cache.SaveDeclaredExpressionLastValue(_prototype, events, result[0]);
                }
                else
                {
                    result[0] = _innerEvaluator.Evaluate(evaluateParamsX);
                }

                return result[0];
            }
        }
    
        public ICollection<EventBean> EvaluateGetROCollectionEvents(EvaluateParams evaluateParams)
        {
            // rewrite streams
            EventBean[] events = GetEventsPerStreamRewritten(evaluateParams.EventsPerStream);
    
            ICollection<EventBean> result;
            if (_isCache)
            {
                var cache = evaluateParams.ExprEvaluatorContext.ExpressionResultCacheService.AllocateDeclaredExprLastColl;
                var entry = cache.GetDeclaredExpressionLastColl(_prototype, events);
                if (entry != null)
                {
                    return entry.Result;
                }
    
                result = _innerEvaluatorLambda.EvaluateGetROCollectionEvents(evaluateParams);
                cache.SaveDeclaredExpressionLastColl(_prototype, events, result);
                return result;
            }
            else
            {
                result = _innerEvaluatorLambda.EvaluateGetROCollectionEvents(evaluateParams);
            }
    
            return result;
        }
    
        public ICollection<object> EvaluateGetROCollectionScalar(EvaluateParams evaluateParams)
        {
            // rewrite streams
            EventBean[] events = GetEventsPerStreamRewritten(evaluateParams.EventsPerStream);

            ICollection<object> result;
            if (_isCache)
            {
                var cache = evaluateParams.ExprEvaluatorContext.ExpressionResultCacheService.AllocateDeclaredExprLastColl;
                var entry = cache.GetDeclaredExpressionLastColl(_prototype, events);
                if (entry != null)
                {
                    return entry.Result.Unwrap<object>();
                }
    
                result = _innerEvaluatorLambda.EvaluateGetROCollectionScalar(evaluateParams);
                cache.SaveDeclaredExpressionLastColl(_prototype, events, result.UnwrapSafe<EventBean>());
                return result;
            }
            else {
                result = _innerEvaluatorLambda.EvaluateGetROCollectionScalar(evaluateParams);
            }
    
            return result;
        }

        public Type ComponentTypeCollection
        {
            get
            {
                return _innerEvaluatorLambda != null ? _innerEvaluatorLambda.ComponentTypeCollection : null;
            }
        }

        public EventType GetEventTypeCollection(EventAdapterService eventAdapterService, int statementId)
        {
            return _innerEvaluatorLambda != null ? _innerEvaluatorLambda.GetEventTypeCollection(eventAdapterService, statementId) : null;
        }

        public EventType GetEventTypeSingle(EventAdapterService eventAdapterService, int statementId)
        {
            return _innerEvaluatorLambda != null ? _innerEvaluatorLambda.GetEventTypeSingle(eventAdapterService, statementId) : null;
        }

        public EventBean EvaluateGetEventBean(EvaluateParams evaluateParams)
        {
            return _innerEvaluatorLambda.EvaluateGetEventBean(evaluateParams);
        }
    }
}
