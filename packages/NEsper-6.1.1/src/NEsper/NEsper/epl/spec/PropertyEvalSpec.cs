///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;

using com.espertech.esper.util;

namespace com.espertech.esper.epl.spec
{
    /// <summary>
    /// Specification for property evaluation.
    /// </summary>
    [Serializable]
    public class PropertyEvalSpec : MetaDefItem
    {
        /// <summary>
        /// Return a list of atoms.
        /// </summary>
        /// <value>atoms</value>
        public IList<PropertyEvalAtom> Atoms { get; private set; }

        /// <summary>
        /// Ctor.
        /// </summary>
        public PropertyEvalSpec()
        {
            Atoms = new List<PropertyEvalAtom>();
        }

        /// <summary>Add an atom. </summary>
        /// <param name="atom">to add</param>
        public void Add(PropertyEvalAtom atom)
        {
            Atoms.Add(atom);
        }
    }
}
