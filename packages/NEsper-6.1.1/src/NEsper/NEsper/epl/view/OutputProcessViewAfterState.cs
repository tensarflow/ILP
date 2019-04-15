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
using com.espertech.esper.core.service;

namespace com.espertech.esper.epl.view
{
    public interface OutputProcessViewAfterState
	{
	    bool CheckUpdateAfterCondition(EventBean[] newEvents, StatementContext statementContext);
	    bool CheckUpdateAfterCondition(ISet<MultiKey<EventBean>> newEvents, StatementContext statementContext);
	    bool CheckUpdateAfterCondition(UniformPair<EventBean[]> newOldEvents, StatementContext statementContext);
	    void Destroy();
	}
} // end of namespace
