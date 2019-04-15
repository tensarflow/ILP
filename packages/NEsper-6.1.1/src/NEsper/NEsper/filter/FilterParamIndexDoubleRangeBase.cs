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
using com.espertech.esper.compat.threading;

namespace com.espertech.esper.filter
{
    /// <summary>
    /// MapIndex for filter parameter constants for the range operators (range open/closed/half). 
    /// The implementation is based on the SortedMap implementation of TreeMap and stores only
    /// expression parameter values of type DoubleRange.
    /// </summary>
    public abstract class FilterParamIndexDoubleRangeBase : FilterParamIndexLookupableBase
    {
        private readonly IDictionary<DoubleRange, EventEvaluator> _rangesNullEndpoints;
        private readonly IReaderWriterLock _rangesRwLock;

        protected readonly OrderedDictionary<DoubleRange, EventEvaluator> Ranges;
        protected double LargestRangeValueDouble = Double.MinValue;

        protected FilterParamIndexDoubleRangeBase(FilterSpecLookupable lookupable, IReaderWriterLock readWriteLock, FilterOperator filterOperator)
            : base(filterOperator, lookupable)
        {
            Ranges = new OrderedDictionary<DoubleRange, EventEvaluator>(new DoubleRangeComparator());
            _rangesNullEndpoints = new Dictionary<DoubleRange, EventEvaluator>();
            _rangesRwLock = readWriteLock;
        }

        public override EventEvaluator Get(Object expressionValue)
        {
            if (!(expressionValue is DoubleRange))
            {
                throw new ArgumentException("Supplied expressionValue must be of type DoubleRange");
            }

            var range = (DoubleRange)expressionValue;
            if ((range.Max == null) || (range.Min == null))
            {
                return _rangesNullEndpoints.Get(range);
            }

            return Ranges.Get(range);
        }

        public override void Put(Object expressionValue, EventEvaluator matcher)
        {
            if (!(expressionValue is DoubleRange))
            {
                throw new ArgumentException("Supplied expressionValue must be of type DoubleRange");
            }

            var range = (DoubleRange)expressionValue;
            if ((range.Max == null) || (range.Min == null))
            {
                _rangesNullEndpoints.Put(range, matcher);     // endpoints null - we don't enter
                return;
            }

            if (Math.Abs(range.Max.Value - range.Min.Value) > LargestRangeValueDouble)
            {
                LargestRangeValueDouble = Math.Abs(range.Max.Value - range.Min.Value);
            }

            Ranges.Put(range, matcher);
        }

        public override void Remove(Object filterConstant)
        {
            var range = (DoubleRange)filterConstant;

            if ((range.Max == null) || (range.Min == null))
            {
                _rangesNullEndpoints.Delete(range);
            }
            else
            {
                Ranges.Delete(range);
            }
        }

        public override int Count
        {
            get { return Ranges.Count; }
        }

        public override bool IsEmpty
        {
            get { return Ranges.IsEmpty(); }
        }

        public override IReaderWriterLock ReadWriteLock
        {
            get { return _rangesRwLock; }
        }
    }
}
