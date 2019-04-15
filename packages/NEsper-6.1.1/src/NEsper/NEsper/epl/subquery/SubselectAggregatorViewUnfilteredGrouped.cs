///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

using System;

using com.espertech.esper.client;
using com.espertech.esper.epl.agg.service;
using com.espertech.esper.epl.expression.core;
using com.espertech.esper.epl.expression;
using com.espertech.esper.metrics.instrumentation;

namespace com.espertech.esper.epl.subquery
{
    public class SubselectAggregatorViewUnfilteredGrouped : SubselectAggregatorViewBase
    {
        public SubselectAggregatorViewUnfilteredGrouped(
            AggregationService aggregationService,
            ExprEvaluator optionalFilterExpr,
            ExprEvaluatorContext exprEvaluatorContext,
            ExprEvaluator[] groupKeys)
            : base(aggregationService, optionalFilterExpr, exprEvaluatorContext, groupKeys)
        {
        }

        public override void Update(EventBean[] newData, EventBean[] oldData)
        {
            if (InstrumentationHelper.ENABLED)
            {
                InstrumentationHelper.Get().QSubselectAggregation(null);
            }
            if (newData != null)
            {
                foreach (EventBean theEvent in newData)
                {
                    EventsPerStream[0] = theEvent;
                    Object groupKey = GenerateGroupKey(true);
                    AggregationService.ApplyEnter(EventsPerStream, groupKey, ExprEvaluatorContext);
                }
            }

            if (oldData != null)
            {
                foreach (EventBean theEvent in oldData)
                {
                    EventsPerStream[0] = theEvent;
                    Object groupKey = GenerateGroupKey(false);
                    AggregationService.ApplyLeave(EventsPerStream, groupKey, ExprEvaluatorContext);
                }
            }
            if (InstrumentationHelper.ENABLED)
            {
                InstrumentationHelper.Get().ASubselectAggregation();
            }
        }
    }
}