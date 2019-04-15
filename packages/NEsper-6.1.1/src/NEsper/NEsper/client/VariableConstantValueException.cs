///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

using System;

namespace com.espertech.esper.client
{
    /// <summary>Indicates that a variable cannot be set. </summary>
    public class VariableConstantValueException : EPException
    {
        /// <summary>Ctor. </summary>
        /// <param name="message">supplies exception details</param>
        public VariableConstantValueException(String message)
            : base(message)
        {
        }
    }
}