///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Reflection;

using com.espertech.esper.client;
using com.espertech.esper.compat.logging;
using com.espertech.esper.events;
using com.espertech.esper.util;
using com.espertech.esper.view;

namespace com.espertech.esper.core.service
{
    /// <summary>
    /// View for use with pre-processing statement such as "Update istream" for indicating previous and current event.
    /// </summary>
    public class InternalRoutePreprocessView : ViewSupport
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly EventType _eventType;
        private readonly StatementResultService _statementResultService;

        /// <summary>Ctor. </summary>
        /// <param name="eventType">the type of event to indicator</param>
        /// <param name="statementResultService">determines whether listeners or subscribers are attached.</param>
        public InternalRoutePreprocessView(EventType eventType, StatementResultService statementResultService)
        {
            _eventType = eventType;
            _statementResultService = statementResultService;
        }

        public override void Update(EventBean[] newData, EventBean[] oldData)
        {
            if ((ExecutionPathDebugLog.IsEnabled) && (Log.IsDebugEnabled))
            {
                Log.Debug(".Update Received Update, " +
                        "  newData.Length==" + ((newData == null) ? 0 : newData.Length) +
                        "  oldData.Length==" + ((oldData == null) ? 0 : oldData.Length));
            }
        }

        public override EventType EventType
        {
            get { return _eventType; }
        }

        public override IEnumerator<EventBean> GetEnumerator()
        {
            return CollectionUtil.NULL_EVENT_ITERATOR;
        }

        /// <summary>Returns true if a subscriber or listener is attached. </summary>
        /// <value>indicator</value>
        public bool IsIndicate
        {
            get { return (_statementResultService.IsMakeNatural || _statementResultService.IsMakeSynthetic); }
        }

        /// <summary>Indicate an modifed event and its previous version. </summary>
        /// <param name="newEvent">modified event</param>
        /// <param name="oldEvent">previous version event</param>
        public void Indicate(EventBean newEvent, EventBean oldEvent)
        {
            try
            {
                if (_statementResultService.IsMakeNatural)
                {
                    var natural = new NaturalEventBean(_eventType, new Object[] { newEvent.Underlying }, newEvent);
                    var naturalOld = new NaturalEventBean(_eventType, new Object[] { oldEvent.Underlying }, oldEvent);
                    UpdateChildren(new NaturalEventBean[] { natural }, new NaturalEventBean[] { naturalOld });
                }
                else
                {
                    UpdateChildren(new EventBean[] { newEvent }, new EventBean[] { oldEvent });
                }
            }
            catch (Exception ex)
            {
                Log.Error("Unexpected error updating child view: " + ex.Message);
            }
        }

        public StatementResultService StatementResultService
        {
            get { return _statementResultService; }
        }
    }
}
