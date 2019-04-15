///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;

using com.espertech.esper.collection;
using com.espertech.esper.compat.collections;

namespace com.espertech.esper.view.ext
{
    /// <summary>
    /// GetEnumerator for use by <seealso cref="com.espertech.esper.view.ext.RankWindowView" />.
    /// </summary>
    public sealed class RankWindowEnumerator : MixedEventBeanAndCollectionEnumeratorBase
    {
        private readonly IDictionary<Object, Object> _window;

        /// <summary>Ctor. </summary>
        /// <param name="window">sorted map with events</param>
        public RankWindowEnumerator(IDictionary<Object, Object> window)
            : base(window.Keys)
        {
            _window = window;
        }

        protected override Object GetValue(Object keyValue)
        {
            return _window.Get(keyValue);
        }
    }
}