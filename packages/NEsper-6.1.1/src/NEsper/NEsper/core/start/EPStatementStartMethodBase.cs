///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

using com.espertech.esper.compat.logging;
using com.espertech.esper.core.context.util;
using com.espertech.esper.core.service;
using com.espertech.esper.epl.expression.core;
using com.espertech.esper.epl.spec;
using com.espertech.esper.util;

namespace com.espertech.esper.core.start
{
    /// <summary>
    /// Starts and provides the stop method for EPL statements.
    /// </summary>
    public abstract class EPStatementStartMethodBase : EPStatementStartMethod
    {
        private static readonly ILog QUERY_PLAN_LOG = LogManager.GetLogger(AuditPath.QUERYPLAN_LOG);

        internal readonly StatementSpecCompiled _statementSpec;

        protected EPStatementStartMethodBase(StatementSpecCompiled statementSpec)
        {
            _statementSpec = statementSpec;
        }

        public StatementSpecCompiled StatementSpec
        {
            get { return _statementSpec; }
        }

        public abstract EPStatementStartResult StartInternal(
            EPServicesContext services,
            StatementContext statementContext,
            bool isNewStatement,
            bool isRecoveringStatement,
            bool isRecoveringResilient);

        public EPStatementStartResult Start(EPServicesContext services, StatementContext statementContext, bool isNewStatement, bool isRecoveringStatement, bool isRecoveringResilient)
        {
            statementContext.VariableService.SetLocalVersion();    // get current version of variables

            bool queryPlanLogging = services.ConfigSnapshot.EngineDefaults.Logging.IsEnableQueryPlan;
            if (queryPlanLogging && QUERY_PLAN_LOG.IsInfoEnabled)
            {
                QUERY_PLAN_LOG.Info("Query plans for statement '" + statementContext.StatementName + "' expression '" + statementContext.Expression + "'");
            }

            // validate context - may not exist
            if (_statementSpec.OptionalContextName != null && statementContext.ContextDescriptor == null)
            {
                throw new ExprValidationException("Context by name '" + _statementSpec.OptionalContextName + "' has not been declared");
            }

            return StartInternal(services, statementContext, isNewStatement, isRecoveringStatement, isRecoveringResilient);
        }

        protected EPStatementAgentInstanceHandle GetDefaultAgentInstanceHandle(StatementContext statementContext)
        {
            return new EPStatementAgentInstanceHandle(statementContext.EpStatementHandle, statementContext.DefaultAgentInstanceLock, -1, new StatementAgentInstanceFilterVersion(), statementContext.FilterFaultHandlerFactory);
        }

        protected AgentInstanceContext GetDefaultAgentInstanceContext(StatementContext statementContext)
        {
            EPStatementAgentInstanceHandle handle = GetDefaultAgentInstanceHandle(statementContext);
            return new AgentInstanceContext(statementContext, handle, EPStatementStartMethodConst.DEFAULT_AGENT_INSTANCE_ID, null, null, statementContext.DefaultAgentInstanceScriptContext);
        }

        protected bool IsQueryPlanLogging(EPServicesContext services)
        {
            return services.ConfigSnapshot.EngineDefaults.Logging.IsEnableQueryPlan;
        }
    }
}
