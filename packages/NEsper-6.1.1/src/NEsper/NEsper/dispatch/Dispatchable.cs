///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////


namespace com.espertech.esper.dispatch
{
	/// <summary>
	/// Implementations are executed when the DispatchService receives a dispatch invocation.
	/// </summary>
	public interface Dispatchable
	{
		/// <summary>
		/// Execute dispatch.
		/// </summary>
		void Execute();
	}
}
