///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

using System;

using com.espertech.esper.client;
using com.espertech.esper.client.scopetest;
using com.espertech.esper.compat;
using com.espertech.esper.compat.collections;
using com.espertech.esper.compat.logging;
using com.espertech.esper.supportregression.bean;
using com.espertech.esper.supportregression.execution;

using NUnit.Framework;

namespace com.espertech.esper.regression.rowrecog
{
    public class ExecRowRecogVariantStream : RegressionExecution {
    
        public override void Run(EPServiceProvider epService) {
            epService.EPAdministrator.CreateEPL("create schema S0 as " + typeof(SupportBean_S0).FullName);
            epService.EPAdministrator.CreateEPL("create schema S1 as " + typeof(SupportBean_S1).FullName);
            epService.EPAdministrator.CreateEPL("create variant schema MyVariantType as S0, S1");
    
            string[] fields = "a,b".Split(',');
            string text = "select * from MyVariantType#keepall " +
                    "match_recognize (" +
                    "  measures A.id? as a, B.id? as b" +
                    "  pattern (A B) " +
                    "  define " +
                    "    A as typeof(A) = 'S0'," +
                    "    B as typeof(B) = 'S1'" +
                    ")";
    
            EPStatement stmt = epService.EPAdministrator.CreateEPL(text);
            var listener = new SupportUpdateListener();
            stmt.Events += listener.Update;
    
            epService.EPAdministrator.CreateEPL("insert into MyVariantType select * from S0");
            epService.EPAdministrator.CreateEPL("insert into MyVariantType select * from S1");
    
            epService.EPRuntime.SendEvent(new SupportBean_S0(1, "S0"));
            epService.EPRuntime.SendEvent(new SupportBean_S1(2, "S1"));
            EPAssertionUtil.AssertPropsPerRow(listener.GetAndResetLastNewData(), fields,
                    new object[][]{new object[] {1, 2}});
            EPAssertionUtil.AssertPropsPerRow(stmt.GetEnumerator(), fields,
                    new object[][]{new object[] {1, 2}});
    
            string epl = "// Declare one sample type\n" +
                    "create schema ST0 as (col string)\n;" +
                    "// Declare second sample type\n" +
                    "create schema ST1 as (col string)\n;" +
                    "// Declare variant stream holding either type\n" +
                    "create variant schema MyVariantStream as ST0, ST1\n;" +
                    "// Populate variant stream\n" +
                    "insert into MyVariantStream select * from ST0\n;" +
                    "// Populate variant stream\n" +
                    "insert into MyVariantStream select * from ST1\n;" +
                    "// Simple pattern to match ST0 ST1 pairs\n" +
                    "select * from MyVariantType#time(1 min)\n" +
                    "match_recognize (\n" +
                    "measures A.id? as a, B.id? as b\n" +
                    "pattern (A B)\n" +
                    "define\n" +
                    "A as typeof(A) = 'ST0',\n" +
                    "B as typeof(B) = 'ST1'\n" +
                    ");";
            epService.EPAdministrator.DeploymentAdmin.ParseDeploy(epl);
        }
    }
} // end of namespace
