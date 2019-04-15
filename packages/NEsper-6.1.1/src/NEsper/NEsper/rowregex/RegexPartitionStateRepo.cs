///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

using System;

using com.espertech.esper.client;

namespace com.espertech.esper.rowregex
{
    /// <summary>Service for holding partition state.</summary>
    public interface RegexPartitionStateRepo : IDisposable
    {
        /// <summary>
        /// Return state for key or create state if not found.
        /// </summary>
        /// <param name="key">to look up</param>
        /// <returns>state</returns>
        RegexPartitionState GetState(Object key);
    
        /// <summary>
        /// Return state for event or create state if not found.
        /// </summary>
        /// <param name="theEvent">to look up</param>
        /// <param name="isCollect">true if a collection of unused state can occur</param>
        /// <returns>state</returns>
        RegexPartitionState GetState(EventBean theEvent, bool isCollect);
    
        /// <summary>
        /// Remove old events from the state, applicable for "prev" function and partial NFA state.
        /// </summary>
        /// <param name="events">to remove</param>
        /// <param name="isEmpty">indicator if there are not matches</param>
        /// <param name="found">indicator if any partial matches exist to be deleted</param>
        /// <returns>number removed</returns>
        int RemoveOld(EventBean[] events, bool isEmpty, bool[] found);
    
        /// <summary>
        /// Copy state for iteration.
        /// </summary>
        /// <param name="forOutOfOrderReprocessing">indicator whether we are processing out-of-order events</param>
        /// <returns>copied state</returns>
        RegexPartitionStateRepo CopyForIterate(bool forOutOfOrderReprocessing);
    
        void RemoveState(Object partitionKey);
    
        void Accept(EventRowRegexNFAViewServiceVisitor visitor);

        bool IsPartitioned { get; }

        int StateCount { get; }

        int IncrementAndGetEventSequenceNum();

        int EventSequenceNum { set; }

        RegexPartitionStateRepoScheduleState ScheduleState { get; }
    }
} // end of namespace
