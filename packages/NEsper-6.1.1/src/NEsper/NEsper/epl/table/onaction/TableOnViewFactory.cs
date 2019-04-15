///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

using System;

using com.espertech.esper.compat;
using com.espertech.esper.compat.collections;
using com.espertech.esper.core.context.util;
using com.espertech.esper.epl.core;
using com.espertech.esper.epl.lookup;
using com.espertech.esper.epl.table.mgmt;

namespace com.espertech.esper.epl.table.onaction
{
    public interface TableOnViewFactory
    {
        TableOnViewBase Make(SubordWMatchExprLookupStrategy lookupStrategy,
                             TableStateInstance tableState,
                             AgentInstanceContext agentInstanceContext,
                             ResultSetProcessor resultSetProcessor);
    }
}
