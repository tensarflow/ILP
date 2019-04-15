///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

using com.espertech.esper.client.soda;
using com.espertech.esper.epl.spec;

namespace com.espertech.esper.core.service
{
    /// <summary>
    /// Statement metadata factory context.
    /// </summary>
    public class StatementMetadataFactoryContext
    {
        public StatementMetadataFactoryContext(
            string statementName,
            int statementId,
            StatementContext statementContext,
            StatementSpecRaw statementSpec,
            string expression,
            bool pattern,
            EPStatementObjectModel optionalModel)
        {
            StatementName = statementName;
            StatementId = statementId;
            StatementContext = statementContext;
            StatementSpec = statementSpec;
            Expression = expression;
            IsPattern = pattern;
            OptionalModel = optionalModel;
        }

        public string StatementName { get; private set; }

        public int StatementId { get; private set; }

        public StatementContext StatementContext { get; private set; }

        public StatementSpecRaw StatementSpec { get; private set; }

        public bool IsPattern { get; private set; }

        public string Expression { get; private set; }

        public EPStatementObjectModel OptionalModel { get; private set; }
    }
}