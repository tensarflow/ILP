///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

using System;

namespace com.espertech.esper.client.deploy
{
    /// <summary>
    /// Options for use in deployment of a module to control the behavior of the deploy operation.
    /// </summary>
    [Serializable]
    public class DeploymentOptions
    {
        public DeploymentOptions()
        {
            IsCompile = true;
            IsFailFast = true;
            IsRollbackOnFail = true;
            IsCompileOnly = false;
            IsolatedServiceProvider = null;
            IsValidateOnly = false;
            DeploymentLockStrategy = DeploymentLockStrategyDefault.INSTANCE;
        }

        /// <summary>
        /// Returns true (the default) to indicate that the deploy operation first performs a compile step for
        /// each statement before attempting to start a statement.
        /// </summary>
        /// <value>true for compile before start, false for start-only</value>
        public bool IsCompile { get; set; }

        /// <summary>
        /// Returns true (the default) to indicate that the first statement to fail starting will
        /// fail the complete module deployment, or set to false to indicate that the operation should attempt
        /// to start all statements regardless of any failures.
        /// </summary>
        /// <value>indicator</value>
        public bool IsFailFast { get; set; }

        /// <summary>
        /// Returns true (the default) to indicate that the engine destroys any started statement when
        /// a subsequent statement fails to start, or false if the engine should leave any started statement
        /// as-is even when exceptions occur for one or more statements.
        /// </summary>
        /// <value>indicator</value>
        public bool IsRollbackOnFail { get; set; }

        /// <summary>
        /// Returns true to indicate to compile only and not start any statements, or false (the default) to
        /// indicate that statements are started as part of the deploy.
        /// </summary>
        /// <value>indicator</value>
        public bool IsCompileOnly { get; set; }

        /// <summary>
        /// Returns the isolated service provider to deploy to, if specified.
        /// </summary>
        /// <value>isolated service provider name</value>
        public string IsolatedServiceProvider { get; set; }

        /// <summary>
        /// Returns true to validate the module syntax and EPL syntax only. Use this option to
        /// not deploy any EPL statement, performing only syntax checking.
        /// </summary>
        /// <value>validate flag</value>
        public bool IsValidateOnly { get; set; }

        /// <summary>
        /// Returns the statement name resolver.
        /// </summary>
        /// <value>statement name resolver</value>
        public StatementNameResolver StatementNameResolver { get; set; }

        /// <summary>
        /// Returns the statement user object resolver.
        /// </summary>
        /// <value>statement user object resolver</value>
        public StatementUserObjectResolver StatementUserObjectResolver { get; set; }

        /// <summary>
        /// Return the deployment lock strategy, the default is <seealso cref="DeploymentLockStrategyDefault" />
        /// </summary>
        /// <value>lock strategy</value>
        public DeploymentLockStrategy DeploymentLockStrategy { get; set; }
    }
} // end of namespace
