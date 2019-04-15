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
using com.espertech.esper.epl.agg.service;
using com.espertech.esper.epl.expression.core;

namespace com.espertech.esper.epl.expression.subquery
{
    public class SubselectEvalStrategyNRExistsAlwaysTrue : SubselectEvalStrategyNR
    {
        public static readonly SubselectEvalStrategyNRExistsAlwaysTrue INSTANCE = new SubselectEvalStrategyNRExistsAlwaysTrue();
    
        private SubselectEvalStrategyNRExistsAlwaysTrue()
        {
        }

        public Object Evaluate(
            EventBean[] eventsPerStream,
            bool isNewData,
            ICollection<EventBean> matchingEvents,
            ExprEvaluatorContext exprEvaluatorContext,
            AggregationService aggregationService)
        {
            return true;
        }
    }
} // end of namespace
