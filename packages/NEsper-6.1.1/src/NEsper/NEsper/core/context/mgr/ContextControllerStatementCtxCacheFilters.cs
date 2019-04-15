///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

using System.Collections.Generic;
using com.espertech.esper.filter;

namespace com.espertech.esper.core.context.mgr
{
    public class ContextControllerStatementCtxCacheFilters : ContextControllerStatementCtxCache
    {
        public ContextControllerStatementCtxCacheFilters(IList<FilterSpecCompiled> filterSpecs)
        {
            FilterSpecs = filterSpecs;
        }

        public IList<FilterSpecCompiled> FilterSpecs { get; private set; }
    }
}