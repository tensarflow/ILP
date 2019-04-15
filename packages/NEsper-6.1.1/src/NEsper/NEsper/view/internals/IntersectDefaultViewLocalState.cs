///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

using System.Collections.Generic;

using com.espertech.esper.client;

namespace com.espertech.esper.view.internals
{
	public class IntersectDefaultViewLocalState
	{
	    public IntersectDefaultViewLocalState(EventBean[][] oldEventsPerView)
	    {
	        RemovalEvents = new HashSet<EventBean>();
	        OldEventsPerView = oldEventsPerView;
	    }

	    public EventBean[][] OldEventsPerView { get; private set; }

	    public ISet<EventBean> RemovalEvents { get; private set; }

	    public bool HasRemovestreamData { get; set; }

	    public bool IsRetainObserverEvents { get; set; }

	    public bool IsDiscardObserverEvents { get; set; }
	}
} // end of namespace
