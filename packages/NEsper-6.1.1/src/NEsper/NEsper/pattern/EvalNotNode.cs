///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

using com.espertech.esper.compat.logging;

namespace com.espertech.esper.pattern
{
    /// <summary>
    /// This class represents an 'not' operator in the evaluation tree representing any event expressions.
    /// </summary>
    public class EvalNotNode : EvalNodeBase
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly EvalNotFactoryNode _factoryNode;
        private readonly EvalNode _childNode;
    
        public EvalNotNode(PatternAgentInstanceContext context, EvalNotFactoryNode factoryNode, EvalNode childNode)
            : base(context)
        {
            _factoryNode = factoryNode;
            _childNode = childNode;
        }

        public EvalNotFactoryNode FactoryNode
        {
            get { return _factoryNode; }
        }

        public EvalNode ChildNode
        {
            get { return _childNode; }
        }

        public override EvalStateNode NewState(
            Evaluator parentNode,
            EvalStateNodeNumber stateNodeNumber,
            long stateNodeId)
        {
            return new EvalNotStateNode(parentNode, this);
        }
    }
} // end of namespace
