///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

using System;

using com.espertech.esper.epl.agg.service;
using com.espertech.esper.type;

namespace com.espertech.esper.epl.agg.aggregator
{
    /// <summary>
    /// Min/max aggregator for all values, not considering events leaving the aggregation (i.e. ever).
    /// </summary>
    public class AggregatorMinMaxEverFilter : AggregatorMinMaxEver
    {
        public AggregatorMinMaxEverFilter(MinMaxTypeEnum minMaxTypeEnum)
            : base(minMaxTypeEnum)
        {
        }
    
        public override void Enter(Object parameters)
        {
            var paramArray = (Object[]) parameters;
            if (!AggregatorUtil.CheckFilter(paramArray)) {
                return;
            }
            base.Enter(paramArray[0]);
        }
    
        public override void Leave(Object parameters)
        {
            var paramArray = (Object[]) parameters;
            if (!AggregatorUtil.CheckFilter(paramArray)) {
                return;
            }
            base.Leave(paramArray[0]);
        }
    }
}
