///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

using com.espertech.esper.epl.join.plan;

namespace com.espertech.esper.epl.join.util
{
    public interface QueryPlanIndexHook
    {
        void Subquery(QueryPlanIndexDescSubquery subquery);
        void InfraOnExpr(QueryPlanIndexDescOnExpr onexpr);
        void FireAndForget(QueryPlanIndexDescFAF queryPlanIndexDescFAF);
        void Join(QueryPlan join);
        void Historical(QueryPlanIndexDescHistorical historical);
    }
}