///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

using com.espertech.esper.client;
using com.espertech.esper.client.context;
using com.espertech.esper.core.service;

namespace com.espertech.esper.core.start
{
    /// <summary>Starts and provides the stop method for EPL statements. </summary>
    public interface EPPreparedExecuteMethod
    {
        EPPreparedQueryResult Execute(ContextPartitionSelector[] contextPartitionSelectors);

        EventType EventType { get; }
    }
}
