///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;

using com.espertech.esper.client;
using com.espertech.esper.compat.collections;
using com.espertech.esper.core.service;
using com.espertech.esper.epl.@join.table;

namespace com.espertech.esper.epl.join.pollindex
{
    /// <summary>
    /// Strategy for building an index out of poll-results knowing the properties to base the index on.
    /// </summary>
    public class PollResultIndexingStrategyIndexSingleArray : PollResultIndexingStrategy
    {
        private readonly int _streamNum;
        private readonly EventType _eventType;
        private readonly String[] _propertyNames;
    
        /// <summary>Ctor. </summary>
        /// <param name="streamNum">is the stream number of the indexed stream</param>
        /// <param name="eventType">is the event type of the indexed stream</param>
        /// <param name="propertyNames">is the property names to be indexed</param>
        public PollResultIndexingStrategyIndexSingleArray(int streamNum, EventType eventType, String[] propertyNames)
        {
            _streamNum = streamNum;
            _eventType = eventType;
            _propertyNames = propertyNames;
        }
    
        public EventTable[] Index(IList<EventBean> pollResult, bool isActiveCache, StatementContext statementContext)
        {
            if (!isActiveCache)
            {
                return new EventTable[] {new UnindexedEventTableList(pollResult, _streamNum)};
            }
            var tables = new EventTable[_propertyNames.Length];
            var evaluatorContextStatement = new ExprEvaluatorContextStatement(statementContext, false);
            for (var i = 0; i < _propertyNames.Length; i++) {
                var factory = new PropertyIndexedEventTableSingleFactory(_streamNum, _eventType, _propertyNames[i], false, null);
                tables[i] = factory.MakeEventTables(new EventTableFactoryTableIdentStmt(statementContext), evaluatorContextStatement)[0];
                tables[i].Add(pollResult.ToArray(), evaluatorContextStatement);
            }
            return tables;
        }
    
        public String ToQueryPlan()
        {
            return GetType().Name + " properties " + _propertyNames.Render();
        }
    }
}
