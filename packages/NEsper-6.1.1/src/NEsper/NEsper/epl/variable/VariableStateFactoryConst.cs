///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

using System;

namespace com.espertech.esper.epl.variable
{
    public class VariableStateFactoryConst : VariableStateFactory
    {
        public VariableStateFactoryConst(Object initialState)
        {
            InitialState = initialState;
        }

        public object InitialState { get; private set; }
    }
}