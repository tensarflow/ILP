///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Reflection;

namespace com.espertech.esper.epl.core
{
    /// <summary>Exception for resolution of a method failed. </summary>
    public class EngineNoSuchMethodException : Exception
    {
        [NonSerialized]
        private readonly MethodInfo _nearestMissMethod;
        /// <summary>Ctor. </summary>
        /// <param name="message">message</param>
        /// <param name="nearestMissMethod">best-match method</param>
        public EngineNoSuchMethodException(String message, MethodInfo nearestMissMethod)

                    : base(message)
        {
            _nearestMissMethod = nearestMissMethod;
        }

        /// <summary>Returns the best-match method. </summary>
        /// <value>method</value>
        public MethodInfo NearestMissMethod
        {
            get { return _nearestMissMethod; }
        }
    }
}
