///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;

using com.espertech.esper.client;
using com.espertech.esper.compat.collections;
using com.espertech.esper.epl.expression.core;
using com.espertech.esper.epl.spec;
using com.espertech.esper.events;

namespace com.espertech.esper.epl.expression.subquery
{
    /// <summary>Represents a subselect in an expression tree.</summary>
    [Serializable]
    public class ExprSubselectInNode : ExprSubselectNode
    {
        private readonly bool _isNotIn;
        [NonSerialized] private SubselectEvalStrategyNR _subselectEvalStrategyNr;
    
        public ExprSubselectInNode(StatementSpecRaw statementSpec, bool isNotIn)
            : base(statementSpec)
        {
            _isNotIn = isNotIn;
        }

        public override Type ReturnType
        {
            get { return typeof (bool?); }
        }

        /// <summary>
        /// Returns true for not-in, or false for in.
        /// </summary>
        /// <value>true for not-in</value>
        public bool IsNotIn
        {
            get { return _isNotIn; }
        }

        public override void ValidateSubquery(ExprValidationContext validationContext) {
            _subselectEvalStrategyNr = SubselectEvalStrategyNRFactory.CreateStrategyAnyAllIn(this, _isNotIn, false, false, null);
        }
    
        public override Object Evaluate(EventBean[] eventsPerStream, bool isNewData, ICollection<EventBean> matchingEvents, ExprEvaluatorContext exprEvaluatorContext) {
            return _subselectEvalStrategyNr.Evaluate(eventsPerStream, isNewData, matchingEvents, exprEvaluatorContext, SubselectAggregationService);
        }

        public override LinkedHashMap<string, object> TypableGetRowProperties
        {
            get { return null; }
        }

        public override Object[] EvaluateTypableSingle(EventBean[] eventsPerStream, bool isNewData, ICollection<EventBean> matchingEvents, ExprEvaluatorContext exprEvaluatorContext) {
            return null;
        }
    
        public override Object[][] EvaluateTypableMulti(EventBean[] eventsPerStream, bool isNewData, ICollection<EventBean> matchingEvents, ExprEvaluatorContext exprEvaluatorContext) {
            return null;
        }
    
        public override ICollection<EventBean> EvaluateGetCollEvents(EventBean[] eventsPerStream, bool isNewData, ICollection<EventBean> matchingEvents, ExprEvaluatorContext context) {
            return null;
        }
    
        public override EventType GetEventTypeCollection(EventAdapterService eventAdapterService, int statementId) {
            return null;
        }

        public override Type ComponentTypeCollection
        {
            get { return null; }
        }

        public override EventBean EvaluateGetEventBean(EventBean[] eventsPerStream, bool isNewData, ICollection<EventBean> matchingEvents, ExprEvaluatorContext exprEvaluatorContext) {
            return null;
        }
    
        public override ICollection<object> EvaluateGetCollScalar(EventBean[] eventsPerStream, bool isNewData, ICollection<EventBean> matchingEvents, ExprEvaluatorContext exprEvaluatorContext) {
            return null;
        }

        public override bool IsAllowMultiColumnSelect
        {
            get { return false; }
        }

        public override EventType GetEventTypeSingle(EventAdapterService eventAdapterService, int statementId) {
            return null;
        }
    
        public override EventBean EvaluateGetEventBean(EvaluateParams evaluateParams) {
            return null;
        }
    }
} // end of namespace
