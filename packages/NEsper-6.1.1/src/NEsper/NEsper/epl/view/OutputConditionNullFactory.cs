///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

using com.espertech.esper.core.context.util;


namespace com.espertech.esper.epl.view
{
    public class OutputConditionNullFactory : OutputConditionFactory {
    
        public OutputCondition Make(AgentInstanceContext agentInstanceContext, OutputCallback outputCallback) {
            return new OutputConditionNull(outputCallback);
        }
    }
}
