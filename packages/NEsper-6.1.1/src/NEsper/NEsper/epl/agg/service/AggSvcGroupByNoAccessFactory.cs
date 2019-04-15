///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

using com.espertech.esper.core.context.util;
using com.espertech.esper.epl.core;
using com.espertech.esper.epl.expression.core;

namespace com.espertech.esper.epl.agg.service
{
    /// <summary>
    /// Implementation for handling aggregation with grouping by group-keys.
    /// </summary>
    public class AggSvcGroupByNoAccessFactory : AggregationServiceFactoryBase
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="evaluators">- evaluate the sub-expression within the aggregate function (ie. Sum(4*myNum))</param>
        /// <param name="prototypes">- collect the aggregation state that evaluators evaluate to, act as prototypes for new aggregations</param>
        public AggSvcGroupByNoAccessFactory(ExprEvaluator[] evaluators, AggregationMethodFactory[] prototypes)
            : base(evaluators, prototypes)
        {
        }

        public override AggregationService MakeService(
            AgentInstanceContext agentInstanceContext,
            EngineImportService engineImportService,
            bool isSubquery,
            int? subqueryNumber)
        {
            return new AggSvcGroupByNoAccessImpl(Evaluators, Aggregators);
        }
    }
} // end of namespace
