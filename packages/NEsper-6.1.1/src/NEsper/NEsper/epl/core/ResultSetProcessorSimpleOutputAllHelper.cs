///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

using System.Collections.Generic;

using com.espertech.esper.client;
using com.espertech.esper.collection;

namespace com.espertech.esper.epl.core
{
	public interface ResultSetProcessorSimpleOutputAllHelper
        : ResultSetProcessorOutputHelper
	{
	    void ProcessView(EventBean[] newData, EventBean[] oldData);
	    void ProcessJoin(ISet<MultiKey<EventBean>> newEvents, ISet<MultiKey<EventBean>> oldEvents);
	    UniformPair<EventBean[]> OutputView(bool isSynthesize);
	    UniformPair<EventBean[]> OutputJoin(bool isSynthesize);
	    void Destroy();
	}
} // end of namespace
