///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;

using com.espertech.esper.compat.collections;

namespace com.espertech.esper.events.map
{
    public class MapEventBeanPropertyWriterIndexedProp : MapEventBeanPropertyWriter
    {
        private readonly int _index;

        public MapEventBeanPropertyWriterIndexedProp(String propertyName, int index)
            : base(propertyName)
        {
            _index = index;
        }
    
        public override void Write(Object value, IDictionary<String, Object> map)
        {
            var arrayEntry = map.Get(PropertyName) as Array;
            if (arrayEntry != null && arrayEntry.Length > _index) {
                arrayEntry.SetValue(value, _index);
            }
        }
    }
}
