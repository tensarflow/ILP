///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;

using com.espertech.esper.client.dataflow;
using com.espertech.esper.compat.collections;

namespace com.espertech.esper.dataflow.util
{
    public class DefaultSupportGraphOpProviderByOpName : EPDataFlowOperatorProvider
    {
        private readonly IDictionary<String, Object> _names;

        public DefaultSupportGraphOpProviderByOpName(IDictionary<String, Object> names)
        {
            _names = names;
        }

        public Object Provide(EPDataFlowOperatorProviderContext context)
        {
            return _names.Get(context.OperatorName);
        }
    }
}
