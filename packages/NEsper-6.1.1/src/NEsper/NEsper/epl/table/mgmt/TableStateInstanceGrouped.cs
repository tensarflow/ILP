///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

using System.Collections.Generic;

using com.espertech.esper.compat.threading;
using com.espertech.esper.epl.expression.core;
using com.espertech.esper.epl.lookup;
using com.espertech.esper.events;

namespace com.espertech.esper.epl.table.mgmt
{
    public interface TableStateInstanceGrouped
    {
	    IReaderWriterLock TableLevelRWLock { get; }
	    ObjectArrayBackedEventBean GetCreateRowIntoTable(object groupByKey, ExprEvaluatorContext exprEvaluatorContext);
	    void HandleRowUpdated(ObjectArrayBackedEventBean row);
	    ObjectArrayBackedEventBean GetRowForGroupKey(object groupKey);
	    ICollection<object> GroupKeys { get; }
	    void Clear();
	    EventTableIndexRepository IndexRepository { get; }
    }
} // end of namespace
