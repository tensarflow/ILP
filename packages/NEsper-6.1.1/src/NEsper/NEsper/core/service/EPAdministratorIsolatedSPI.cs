///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

using com.espertech.esper.client;

namespace com.espertech.esper.core.service
{
    /// <summary>
    /// Implementation for the admin interface.
    /// </summary>
    public interface EPAdministratorIsolatedSPI : EPAdministratorIsolated
    {
        /// <summary>
        /// Add a statement name to the list of statements held by the isolated service provider.
        /// </summary>
        /// <param name="name">to add</param>
        void AddStatement(string name);

        EPStatement CreateEPLStatementId(
            string eplStatement,
            string statementName,
            object userObject,
            int? optionalStatementId);
    }
}
