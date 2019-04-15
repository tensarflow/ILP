///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Threading;

using com.espertech.esper.core.context.util;

namespace com.espertech.esper.epl.named
{
    public abstract class NamedWindowConsumerLatch
    {
        protected NamedWindowConsumerLatch(NamedWindowDeltaData deltaData, IDictionary<EPStatementAgentInstanceHandle, IList<NamedWindowConsumerView>> dispatchTo)
        {
#if DEBUG && DEVELOPMENT
            Id = Guid.NewGuid();
            AllocThread = Thread.CurrentThread;
#endif
            DeltaData = deltaData;
            DispatchTo = dispatchTo;
        }

#if DEBUG && DEVELOPMENT
        public Guid Id { get; }
        public Thread AllocThread { get;  }
#endif

        public abstract NamedWindowConsumerLatch Earlier { get; }
        public abstract void Await();
        public abstract void Done();

        public NamedWindowDeltaData DeltaData { get; private set; }

        public IDictionary<EPStatementAgentInstanceHandle, IList<NamedWindowConsumerView>> DispatchTo { get; private set; }
    }
} // end of namespace