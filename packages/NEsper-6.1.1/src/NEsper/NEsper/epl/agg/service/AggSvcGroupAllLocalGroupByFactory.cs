///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

using com.espertech.esper.core.context.util;
using com.espertech.esper.epl.agg.util;
using com.espertech.esper.epl.core;

namespace com.espertech.esper.epl.agg.service
{
    /// <summary>
    ///     Implementation for handling aggregation with grouping by group-keys.
    /// </summary>
    public class AggSvcGroupAllLocalGroupByFactory : AggregationServiceFactory
    {
        protected internal readonly bool IsJoin;
        private readonly AggregationLocalGroupByPlan _localGroupByPlan;

        public AggSvcGroupAllLocalGroupByFactory(bool @join, AggregationLocalGroupByPlan localGroupByPlan)
        {
            IsJoin = join;
            _localGroupByPlan = localGroupByPlan;
        }

        public AggregationService MakeService(AgentInstanceContext agentInstanceContext, EngineImportService engineImportService, bool isSubquery, int? subqueryNumber)
        {
            return new AggSvcGroupAllLocalGroupBy(IsJoin, _localGroupByPlan);
        }
    }
} // end of namespace