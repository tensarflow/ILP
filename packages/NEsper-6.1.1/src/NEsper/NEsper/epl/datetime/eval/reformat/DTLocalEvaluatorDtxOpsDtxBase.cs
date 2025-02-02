﻿///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

using System.Collections.Generic;

using com.espertech.esper.epl.datetime.calop;

namespace com.espertech.esper.epl.datetime.eval.reformat
{
    internal abstract class DTLocalEvaluatorDtxOpsDtxBase
    {
        protected readonly IList<CalendarOp> CalendarOps;

        protected DTLocalEvaluatorDtxOpsDtxBase(IList<CalendarOp> calendarOps)
        {
            CalendarOps = calendarOps;
        }
    }
}
