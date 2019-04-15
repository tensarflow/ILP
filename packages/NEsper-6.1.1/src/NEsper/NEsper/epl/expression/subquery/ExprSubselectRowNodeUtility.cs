///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

using System.Collections.Generic;

using com.espertech.esper.client;
using com.espertech.esper.compat.logging;
using com.espertech.esper.epl.expression.core;

namespace com.espertech.esper.epl.expression.subquery
{
    public class ExprSubselectRowNodeUtility
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static EventBean EvaluateFilterExpectSingleMatch(
            EventBean[] eventsZeroSubselect,
            bool newData,
            ICollection<EventBean> matchingEvents,
            ExprEvaluatorContext exprEvaluatorContext,
            ExprSubselectRowNode parent)
        {
            var evaluateParams = new EvaluateParams(eventsZeroSubselect, newData, exprEvaluatorContext);

            EventBean subSelectResult = null;
            foreach (EventBean subselectEvent in matchingEvents)
            {
                // Prepare filter expression event list
                eventsZeroSubselect[0] = subselectEvent;

                var pass = parent.FilterExpr.Evaluate(evaluateParams);
                if ((pass != null) && true.Equals(pass))
                {
                    if (subSelectResult != null)
                    {
                        Log.Warn(parent.GetMultirowMessage());
                        return null;
                    }
                    subSelectResult = subselectEvent;
                }
            }

            return subSelectResult;
        }
    }
} // end of namespace
