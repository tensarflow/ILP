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
using com.espertech.esper.compat;
using com.espertech.esper.compat.collections;
using com.espertech.esper.compat.logging;
using com.espertech.esper.epl.core;
using com.espertech.esper.epl.expression.core;
using com.espertech.esper.events.vaevent;

namespace com.espertech.esper.epl.core.eval
{
    public class EvalInsertWildcardRevisionWrapper : EvalBaseMap, SelectExprProcessor
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    
        private readonly ValueAddEventProcessor _vaeProcessor;
        private readonly EventType _wrappingEventType;
    
        public EvalInsertWildcardRevisionWrapper(SelectExprContext selectExprContext, EventType resultEventType, ValueAddEventProcessor vaeProcessor, EventType wrappingEventType)
            : base(selectExprContext, resultEventType)
        {
            _vaeProcessor = vaeProcessor;
            _wrappingEventType = wrappingEventType;
        }

        public override EventBean ProcessSpecific(
            IDictionary<string, Object> props,
            EventBean[] eventsPerStream,
            bool isNewData,
            bool isSynthesize,
            ExprEvaluatorContext exprEvaluatorContext)
        {
            EventBean underlying = eventsPerStream[0];
            EventBean wrapped = base.EventAdapterService.AdapterForTypedWrapper(underlying, props, _wrappingEventType);
            return _vaeProcessor.GetValueAddEventBean(wrapped);
        }
    }
} // end of namespace
