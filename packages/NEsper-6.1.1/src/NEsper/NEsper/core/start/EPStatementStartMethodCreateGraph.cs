///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

using System;

using com.espertech.esper.compat.collections;
using com.espertech.esper.core.context.factory;
using com.espertech.esper.core.service;
using com.espertech.esper.epl.spec;
using com.espertech.esper.view;

namespace com.espertech.esper.core.start
{
    /// <summary>
    /// Starts and provides the stop method for EPL statements.
    /// </summary>
    public class EPStatementStartMethodCreateGraph : EPStatementStartMethodBase
    {
        public EPStatementStartMethodCreateGraph(StatementSpecCompiled statementSpec)
            : base(statementSpec)
        {
        }

        public override EPStatementStartResult StartInternal(EPServicesContext services, StatementContext statementContext, bool isNewStatement, bool isRecoveringStatement, bool isRecoveringResilient)
        {
            var createGraphDesc = StatementSpec.CreateGraphDesc;
            var agentInstanceContext = GetDefaultAgentInstanceContext(statementContext);

            // define output event type
            var typeName = "EventType_Graph_" + createGraphDesc.GraphName;
            var resultType = services.EventAdapterService.CreateAnonymousMapType(typeName, Collections.GetEmptyMap<String, Object>(), true);

            services.DataFlowService.AddStartGraph(createGraphDesc, statementContext, services, agentInstanceContext, isNewStatement);

            var stopMethod = new ProxyEPStatementStopMethod(() =>
                services.DataFlowService.StopGraph(createGraphDesc.GraphName));

            var destroyMethod = new ProxyEPStatementDestroyMethod(() =>
                    services.DataFlowService.RemoveGraph(createGraphDesc.GraphName));

            var resultView = new ZeroDepthStreamNoIterate(resultType);
            statementContext.StatementAgentInstanceFactory = new StatementAgentInstanceFactoryNoAgentInstance(resultView);
            return new EPStatementStartResult(resultView, stopMethod, destroyMethod);
        }
    }
}
