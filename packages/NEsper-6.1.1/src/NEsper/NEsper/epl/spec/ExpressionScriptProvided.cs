///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;

namespace com.espertech.esper.epl.spec
{
    [Serializable]
    public class ExpressionScriptProvided 
    {
        [NonSerialized] private ExpressionScriptCompiled _compiled;

        public ExpressionScriptProvided(
            string name,
            string expression,
            IList<string> parameterNames,
            string optionalReturnTypeName,
            bool optionalReturnTypeIsArray,
            string optionalEventTypeName,
            string optionalDialect)
        {
            Name = name;
            Expression = expression;
            ParameterNames = parameterNames;
            OptionalReturnTypeName = optionalReturnTypeName;
            IsOptionalReturnTypeIsArray = optionalReturnTypeIsArray;
            OptionalEventTypeName = optionalEventTypeName;
            OptionalDialect = optionalDialect;
    
            if (expression == null) {
                throw new ArgumentException("Invalid null expression received");
            }
        }

        public string Name { get; private set; }

        public string Expression { get; private set; }

        public IList<string> ParameterNames { get; private set; }

        public string OptionalReturnTypeName { get; private set; }

        public string OptionalDialect { get; private set; }

        public ExpressionScriptCompiled Compiled
        {
            get { return _compiled; }
            set { _compiled = value; }
        }

        public bool IsOptionalReturnTypeIsArray { get; private set; }

        public string OptionalEventTypeName { get; private set; }
    }
} // end of namespace
