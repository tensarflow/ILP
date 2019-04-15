///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

using com.espertech.esper.core.context.util;
using com.espertech.esper.epl.agg.service;

namespace com.espertech.esper.epl.core
{
    /// <summary>
    /// A processor for ordering output events according to the order specified in the order-by clause.
    /// </summary>
    public interface OrderByProcessorFactory
    {
        OrderByProcessor Instantiate(AggregationService aggregationService, AgentInstanceContext agentInstanceContext);
    }
}
