﻿///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

using System;

using com.espertech.esper.epl.expression.core;
using com.espertech.esper.util;

namespace com.espertech.esper.epl.expression.funcs.cast
{
    /// <summary>Casting and parsing computer.</summary>
    public class NonnumericCasterComputer : CasterParserComputer
    {
        private readonly SimpleTypeCaster _caster;

        public NonnumericCasterComputer(SimpleTypeCaster numericTypeCaster)
        {
            _caster = numericTypeCaster;
        }

        public Object Compute(Object input, EvaluateParams evaluateParams)
        {
            return _caster.Invoke(input);
        }

        public bool IsConstantForConstInput
        {
            get { return true; }
        }
    }
}
