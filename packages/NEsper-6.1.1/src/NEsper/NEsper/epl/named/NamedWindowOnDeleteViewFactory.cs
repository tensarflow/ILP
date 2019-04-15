///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

using com.espertech.esper.client;
using com.espertech.esper.core.context.util;
using com.espertech.esper.core.service;
using com.espertech.esper.epl.core;
using com.espertech.esper.epl.lookup;

namespace com.espertech.esper.epl.named
{
    /// <summary>View for the on-delete statement that handles removing events from a named window. </summary>
    public class NamedWindowOnDeleteViewFactory : NamedWindowOnExprBaseViewFactory
    {
        public NamedWindowOnDeleteViewFactory(EventType namedWindowEventType, StatementResultService statementResultService)
            : base(namedWindowEventType)
        {
            StatementResultService = statementResultService;
        }

        public StatementResultService StatementResultService { get; private set; }

        public override NamedWindowOnExprBaseView Make(SubordWMatchExprLookupStrategy lookupStrategy, NamedWindowRootViewInstance namedWindowRootViewInstance, AgentInstanceContext agentInstanceContext, ResultSetProcessor resultSetProcessor)
        {
            return new NamedWindowOnDeleteView(lookupStrategy, namedWindowRootViewInstance, agentInstanceContext, this);
        }
    }
}
