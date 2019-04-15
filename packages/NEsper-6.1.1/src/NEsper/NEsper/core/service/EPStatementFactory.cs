///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

using com.espertech.esper.client;
using com.espertech.esper.core.context.factory;
using com.espertech.esper.dispatch;
using com.espertech.esper.timer;
using com.espertech.esper.util;

namespace com.espertech.esper.core.service
{
    public interface EPStatementFactory
    {
        EPStatementSPI Make(
            string expressionNoAnnotations,
            bool isPattern,
            DispatchService dispatchService,
            StatementLifecycleSvcImpl statementLifecycleSvc,
            long timeLastStateChange,
            bool preserveDispatchOrder,
            bool isSpinLocks,
            long blockingTimeout,
            TimeSourceService timeSource,
            StatementMetadata statementMetadata,
            object statementUserObject,
            StatementContext statementContext,
            bool isFailed,
            bool nameProvided);

        StopCallback MakeStopMethod(StatementAgentInstanceFactoryResult startResult);
    }
} // end of namespace
