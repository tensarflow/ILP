///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;

using com.espertech.esper.epl.expression.core;

namespace com.espertech.esper.epl.spec
{
    /// <summary>Specification for the merge statement insert/Update/delete-part. </summary>
    [Serializable]
    public class OnTriggerMergeMatched
    {
        public OnTriggerMergeMatched(bool matchedUnmatched, ExprNode optionalMatchCond, IList<OnTriggerMergeAction> actions)
        {
            IsMatchedUnmatched = matchedUnmatched;
            OptionalMatchCond = optionalMatchCond;
            Actions = actions;
        }

        public ExprNode OptionalMatchCond { get; set; }

        public bool IsMatchedUnmatched { get; private set; }

        public IList<OnTriggerMergeAction> Actions { get; private set; }
    }
    
}
