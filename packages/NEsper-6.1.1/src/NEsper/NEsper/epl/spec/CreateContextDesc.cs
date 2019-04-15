///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;

using com.espertech.esper.filter;

namespace com.espertech.esper.epl.spec
{
    [Serializable]
    public class CreateContextDesc
    {
        public CreateContextDesc(String contextName, ContextDetail contextDetail) {
            ContextName = contextName;
            ContextDetail = contextDetail;
        }

        public string ContextName { get; private set; }
        public ContextDetail ContextDetail { get; private set; }

        public IList<FilterSpecCompiled> FilterSpecs
        {
            get { return ContextDetail.ContextDetailFilterSpecs; }
        }
    }
}
