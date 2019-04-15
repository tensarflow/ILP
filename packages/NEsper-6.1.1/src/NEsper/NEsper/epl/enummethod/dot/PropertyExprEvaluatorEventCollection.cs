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
using com.espertech.esper.events;

namespace com.espertech.esper.epl.enummethod.dot
{
    public class PropertyExprEvaluatorEventCollection
        : ExprEvaluatorEnumeration
        , ExprEvaluatorEnumerationGivenEvent
    {
        private readonly string _propertyNameCache;
        private readonly int _streamId;
        private readonly EventType _fragmentType;
        private readonly EventPropertyGetter _getter;
        private readonly bool _disablePropertyExpressionEventCollCache;

        public PropertyExprEvaluatorEventCollection(
            string propertyNameCache,
            int streamId,
            EventType fragmentType,
            EventPropertyGetter getter,
            bool disablePropertyExpressionEventCollCache)
        {
            _propertyNameCache = propertyNameCache;
            _streamId = streamId;
            _fragmentType = fragmentType;
            _getter = getter;
            _disablePropertyExpressionEventCollCache = disablePropertyExpressionEventCollCache;
        }

        public ICollection<EventBean> EvaluateGetROCollectionEvents(EvaluateParams evaluateParams)
        {
            var eventInQuestion = evaluateParams.EventsPerStream[_streamId];
            if (eventInQuestion == null)
            {
                return null;
            }
            return EvaluateInternal(eventInQuestion, evaluateParams.ExprEvaluatorContext);
        }

        public ICollection<EventBean> EvaluateEventGetROCollectionEvents(EventBean @event, ExprEvaluatorContext context)
        {
            if (@event == null)
            {
                return null;
            }
            return EvaluateInternal(@event, context);
        }

        private ICollection<EventBean> EvaluateInternal(EventBean eventInQuestion, ExprEvaluatorContext context)
        {
            if (_disablePropertyExpressionEventCollCache)
            {
                var eventsX = _getter.GetFragment(eventInQuestion).UnwrapIntoArray<EventBean>();
                return (ICollection<EventBean>) eventsX;
            }

            var cache = context.ExpressionResultCacheService.AllocateUnwrapProp;
            var cacheEntry = cache.GetPropertyColl(_propertyNameCache, eventInQuestion);
            if (cacheEntry != null)
            {
                return cacheEntry.Result;
            }

            var events = _getter.GetFragment(eventInQuestion).UnwrapIntoArray<EventBean>();
            var coll = (ICollection<EventBean>) events;
            cache.SavePropertyColl(_propertyNameCache, eventInQuestion, coll);
            return coll;
        }

        public EventType GetEventTypeCollection(EventAdapterService eventAdapterService, int statementId)
        {
            return _fragmentType;
        }

        public ICollection<object> EvaluateGetROCollectionScalar(EvaluateParams evaluateParams)
        {
            return null;
        }

        public Type ComponentTypeCollection
        {
            get { return null; }
        }

        public EventType GetEventTypeSingle(EventAdapterService eventAdapterService, int statementId)
        {
            return null;
        }

        public EventBean EvaluateGetEventBean(EvaluateParams evaluateParams)
        {
            return null;
        }

        public ICollection<object> EvaluateEventGetROCollectionScalar(EventBean @event, ExprEvaluatorContext context)
        {
            return null;
        }

        public EventBean EvaluateEventGetEventBean(EventBean @event, ExprEvaluatorContext context)
        {
            return null;
        }
    }
} // end of namespace
