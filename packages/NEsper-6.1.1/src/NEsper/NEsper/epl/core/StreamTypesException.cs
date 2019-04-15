///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

using System;

using com.espertech.esper.collection;

namespace com.espertech.esper.epl.core
{
    /// <summary>Base class for stream and property name resolution errors.</summary>
    public abstract class StreamTypesException : Exception
    {
        private readonly StreamTypesExceptionSuggestionGen _optionalSuggestionGenerator;
    
        public StreamTypesException(string message, StreamTypesExceptionSuggestionGen optionalSuggestionGenerator)
            : base(message)
        {
            _optionalSuggestionGenerator = optionalSuggestionGenerator;
        }

        /// <summary>
        /// Returns the optional suggestion for a matching name.
        /// </summary>
        /// <value>suggested match</value>
        public Pair<int, string> OptionalSuggestion
        {
            get { return _optionalSuggestionGenerator != null ? _optionalSuggestionGenerator.Invoke() : null; }
        }
    }
} // end of namespace
