///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

using System.Collections.Generic;

using com.espertech.esper.epl.expression.core;
using com.espertech.esper.epl.expression.prev;
using com.espertech.esper.epl.expression.prior;

namespace com.espertech.esper.epl.expression.visitor
{
    /// <summary>
    /// Visitor that collects expression nodes that require view resources.
    /// </summary>
    public class ExprNodeViewResourceVisitor : ExprNodeVisitor
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        public ExprNodeViewResourceVisitor()
        {
            ExprNodes = new List<ExprNode>();
        }
    
        public bool IsVisit(ExprNode exprNode)
        {
            return true;
        }

        /// <summary>
        /// Returns the list of expression nodes requiring view resources.
        /// </summary>
        /// <value>expr nodes such as 'prior' or 'prev'</value>
        public IList<ExprNode> ExprNodes { get; private set; }

        public void Visit(ExprNode exprNode)
        {
            if (exprNode is ExprPreviousNode || exprNode is ExprPriorNode)
            {
                ExprNodes.Add(exprNode);
            }
        }
    }
}
