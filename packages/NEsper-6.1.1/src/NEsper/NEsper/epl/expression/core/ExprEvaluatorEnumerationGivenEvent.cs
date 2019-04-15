///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

using System.Collections.Generic;

using com.espertech.esper.client;

namespace com.espertech.esper.epl.expression.core
{
	/// <summary>
	/// Interface for evaluating of an event re. enumeration.
	/// </summary>
	public interface ExprEvaluatorEnumerationGivenEvent
	{
	    ICollection<EventBean> EvaluateEventGetROCollectionEvents(EventBean @event, ExprEvaluatorContext context);

	    ICollection<object> EvaluateEventGetROCollectionScalar(EventBean @event, ExprEvaluatorContext context);

	    EventBean EvaluateEventGetEventBean(EventBean @event, ExprEvaluatorContext context);
	}
} // end of namespace
