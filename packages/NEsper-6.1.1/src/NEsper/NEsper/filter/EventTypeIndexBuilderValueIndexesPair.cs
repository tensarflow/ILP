///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

namespace com.espertech.esper.filter
{
    public class EventTypeIndexBuilderValueIndexesPair : FilterServiceEntry
    {
        public EventTypeIndexBuilderValueIndexesPair(
            FilterValueSet filterValueSet,
            EventTypeIndexBuilderIndexLookupablePair[][] indexPairs)
        {
            FilterValueSet = filterValueSet;
            IndexPairs = indexPairs;
        }

        public FilterValueSet FilterValueSet { get; private set; }

        public EventTypeIndexBuilderIndexLookupablePair[][] IndexPairs { get; private set; }
    }
}
