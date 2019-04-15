///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

using System;

namespace com.espertech.esper.util
{
    /// <summary>Exception to represent a circular dependency. </summary>
    public class GraphCircularDependencyException : Exception
    {
        /// <summary>Ctor. </summary>
        /// <param name="message">supplies the detailed description</param>
        public GraphCircularDependencyException(String message)
            : base(message)
        {
        }
    
        /// <summary>Ctor. </summary>
        /// <param name="message">supplies the detailed description</param>
        /// <param name="innerException">the exception cause</param>
        public GraphCircularDependencyException(String message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
