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
using com.espertech.esper.compat;
using com.espertech.esper.compat.collections;
using com.espertech.esper.epl.agg.service;
using com.espertech.esper.epl.expression.core;

namespace com.espertech.esper.core.context.stmt
{
    public class AIRegistryAggregationMap : AIRegistryAggregation {
        private readonly IDictionary<int, AggregationService> _services;
    
        public AIRegistryAggregationMap() {
            _services = new Dictionary<int, AggregationService>();
        }
    
        public void AssignService(int serviceId, AggregationService aggregationService) {
            _services.Put(serviceId, aggregationService);
        }
    
        public void DeassignService(int serviceId) {
            _services.Remove(serviceId);
        }

        public int InstanceCount
        {
            get { return _services.Count; }
        }

        public void ApplyEnter(EventBean[] eventsPerStream, Object optionalGroupKeyPerRow, ExprEvaluatorContext exprEvaluatorContext) {
            _services.Get(exprEvaluatorContext.AgentInstanceId).ApplyEnter(eventsPerStream, optionalGroupKeyPerRow, exprEvaluatorContext);
        }
    
        public void ApplyLeave(EventBean[] eventsPerStream, Object optionalGroupKeyPerRow, ExprEvaluatorContext exprEvaluatorContext) {
            _services.Get(exprEvaluatorContext.AgentInstanceId).ApplyLeave(eventsPerStream, optionalGroupKeyPerRow, exprEvaluatorContext);
        }
    
        public void SetCurrentAccess(Object groupKey, int agentInstanceId, AggregationGroupByRollupLevel rollupLevel) {
            _services.Get(agentInstanceId).SetCurrentAccess(groupKey, agentInstanceId, null);
        }
    
        public AggregationService GetContextPartitionAggregationService(int agentInstanceId) {
            return _services.Get(agentInstanceId);
        }
    
        public void ClearResults(ExprEvaluatorContext exprEvaluatorContext) {
            _services.Get(exprEvaluatorContext.AgentInstanceId).ClearResults(exprEvaluatorContext);
        }
    
        public object GetValue(int column, int agentInstanceId, EvaluateParams evaluateParams) {
            return _services.Get(agentInstanceId).GetValue(column, agentInstanceId, evaluateParams);
        }
    
        public ICollection<EventBean> GetCollectionOfEvents(int column, EvaluateParams evaluateParams) {
            return _services.Get(evaluateParams.ExprEvaluatorContext.AgentInstanceId).GetCollectionOfEvents(column, evaluateParams);
        }
    
        public ICollection<object> GetCollectionScalar(int column, EvaluateParams evaluateParams) {
            return _services.Get(evaluateParams.ExprEvaluatorContext.AgentInstanceId).GetCollectionScalar(column, evaluateParams);
        }
    
        public EventBean GetEventBean(int column, EvaluateParams evaluateParams) {
            return _services.Get(evaluateParams.ExprEvaluatorContext.AgentInstanceId).GetEventBean(column, evaluateParams);
        }
    
        public void SetRemovedCallback(AggregationRowRemovedCallback callback) {
            // not applicable
        }
    
        public void Accept(AggregationServiceVisitor visitor) {
            throw new UnsupportedOperationException("Not applicable");
        }
    
        public void AcceptGroupDetail(AggregationServiceVisitorWGroupDetail visitor) {
            throw new UnsupportedOperationException("Not applicable");
        }

        public bool IsGrouped
        {
            get { throw new UnsupportedOperationException("Not applicable"); }
        }

        public Object GetGroupKey(int agentInstanceId) {
            return _services.Get(agentInstanceId).GetGroupKey(agentInstanceId);
        }
    
        public ICollection<Object> GetGroupKeys(ExprEvaluatorContext exprEvaluatorContext) {
            return _services.Get(exprEvaluatorContext.AgentInstanceId).GetGroupKeys(exprEvaluatorContext);
        }
    
        public void Stop() {
        }
    }
} // end of namespace
