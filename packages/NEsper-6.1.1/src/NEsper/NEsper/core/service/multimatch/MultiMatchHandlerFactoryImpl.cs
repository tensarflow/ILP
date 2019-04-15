///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

namespace com.espertech.esper.core.service.multimatch
{
    public class MultiMatchHandlerFactoryImpl : MultiMatchHandlerFactory
    {
        public MultiMatchHandler GetDefaultHandler()
        {
            return MultiMatchHandlerSubqueryPreevalNoDedup.INSTANCE;
        }

        public MultiMatchHandler MakeNoDedupNoSubq()
        {
            return MultiMatchHandlerNoSubqueryNoDedup.INSTANCE;
        }

        public MultiMatchHandler MakeNoDedupSubselectPreval()
        {
            return MultiMatchHandlerSubqueryPreevalNoDedup.INSTANCE;
        }

        public MultiMatchHandler MakeNoDedupSubselectPosteval()
        {
            return MultiMatchHandlerSubqueryPostevalNoDedup.INSTANCE;
        }

        public MultiMatchHandler MakeDedupNoSubq()
        {
            return MultiMatchHandlerNoSubqueryWDedup.INSTANCE;
        }

        public MultiMatchHandler MakeDedupSubq(bool isSubselectPreeval)
        {
            return new MultiMatchHandlerSubqueryWDedup(isSubselectPreeval);
        }
    }
} // end of namespace
