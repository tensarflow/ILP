///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

using System;
using System.IO;

using com.espertech.esper.compat;
using com.espertech.esper.compat.collections;

namespace com.espertech.esper.client.soda
{
    /// <summary>
    /// Mean deviation of the (distinct) values returned by an expression.
    /// </summary>
    [Serializable]
    public class AvedevProjectionExpression : ExpressionBase
    {
        private bool distinct;
    
        /// <summary>
        /// Ctor.
        /// </summary>
        public AvedevProjectionExpression() {
        }
    
        /// <summary>
        /// Ctor - for use to create an expression tree, without inner expression.
        /// </summary>
        /// <param name="isDistinct">true if distinct</param>
        public AvedevProjectionExpression(bool isDistinct)
        {
            this.distinct = isDistinct;
        }
    
        /// <summary>
        /// Ctor - adds the expression to project.
        /// </summary>
        /// <param name="expression">returning values to project</param>
        /// <param name="isDistinct">true if distinct</param>
        public AvedevProjectionExpression(Expression expression, bool isDistinct)
        {
            this.distinct = isDistinct;
            this.Children.Add(expression);
        }

        public override ExpressionPrecedenceEnum Precedence
        {
            get { return ExpressionPrecedenceEnum.UNARY; }
        }

        public override void ToPrecedenceFreeEPL(TextWriter writer)
        {
            ExpressionBase.RenderAggregation(writer, "avedev", distinct, this.Children);
        }

        /// <summary>
        /// Returns true if the projection considers distinct values only.
        /// </summary>
        /// <value>true if distinct</value>
        public bool IsDistinct
        {
            get { return distinct; }
            set { distinct = value; }
        }
    }
}
