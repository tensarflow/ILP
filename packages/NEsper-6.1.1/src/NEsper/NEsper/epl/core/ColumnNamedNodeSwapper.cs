///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

using System;

using com.espertech.esper.compat;
using com.espertech.esper.epl.expression.core;
using com.espertech.esper.epl.expression;

namespace com.espertech.esper.epl.core
{
    /// <summary>A utility class for replacing select-clause column names with their definitions in expression node trees. </summary>
    public class ColumnNamedNodeSwapper
    {
    	/// <summary>Replace all instances of the node representing the colum name with the full expression. </summary>
    	/// <param name="exprTree">the expression node tree to make the changes in</param>
    	/// <param name="columnName">the select-clause name that is to be expanded</param>
    	/// <param name="fullExpr">the full expression that the column name represents</param>
    	/// <returns>exprTree with the appropriate swaps performed, or fullExpr,if all of exprTree needed to be swapped </returns>
    	public static ExprNode Swap(ExprNode exprTree, String columnName, ExprNode fullExpr)
    	{
    		if(fullExpr == null)
    		{
    			throw new ArgumentNullException();
    		}
    
    		if(IsColumnNameNode(exprTree, columnName))
    		{
    			return fullExpr;
    		}
    		else
    		{
    			VisitChildren(exprTree, columnName, fullExpr);
    		}
    
    		return exprTree;
    	}
    
    	/// <summary>A recursive function that works on the child nodes of a given node, replacing any instances of the node representing the name, and visiting the children of all other nodes. </summary>
    	/// <param name="node">the node whose children are to be examined for names</param>
    	/// <param name="name">the name to replace</param>
    	/// <param name="fullExpr">the full expression corresponding to the name</param>
    	private static void VisitChildren(ExprNode node, String name, ExprNode fullExpr)
    	{
    		var childNodes = node.ChildNodes;
    
    		for (int i = 0; i < childNodes.Count; i++)
    		{
    			ExprNode childNode = childNodes[i];
    			if(IsColumnNameNode(childNode, name))
    			{
    				node.SetChildNode(i, fullExpr);
    			}
    			else
    			{
    				VisitChildren(childNode, name, fullExpr);
    			}
    		}
    	}
    
    	private static bool IsColumnNameNode(ExprNode node, String name)
    	{
    		if(node is ExprIdentNode)
    		{
    			if(node.ChildNodes.Count > 0)
    			{
    				throw new IllegalStateException("Ident node has unexpected child nodes");
    			}
                ExprIdentNode identNode = (ExprIdentNode) node;
    			return identNode.UnresolvedPropertyName.Equals(name) && identNode.StreamOrPropertyName == null;
    		}
    		else
    		{
    			return false;
    		}
    	}
    }
}
