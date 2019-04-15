///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

using System;

using com.espertech.esper.client;
using com.espertech.esper.codegen.core;
using com.espertech.esper.codegen.model.expression;

using static com.espertech.esper.codegen.model.expression.CodegenExpressionBuilder;

namespace com.espertech.esper.events.arr
{
    /// <summary>Getter for array events. </summary>
    public class ObjectArrayEventBeanArrayIndexedPropertyGetter : ObjectArrayEventPropertyGetter
    {
        private readonly int _propertyIndex;
        private readonly int _index;

        /// <summary>Ctor. </summary>
        /// <param name="propertyIndex">property index</param>
        /// <param name="index">array index</param>
        public ObjectArrayEventBeanArrayIndexedPropertyGetter(int propertyIndex, int index)
        {
            _propertyIndex = propertyIndex;
            _index = index;
        }

        public Object GetObjectArray(Object[] array)
        {
            // If the map does not contain the key, this is allowed and represented as null
            var wrapper = (EventBean[])array[_propertyIndex];
            return BaseNestableEventUtil.GetBNArrayPropertyUnderlying(wrapper, _index);
        }

        private String GetObjectArrayCodegen(ICodegenContext context)
        {
            return context.AddMethod(typeof(Object), typeof(Object[]), "array", this.GetType())
                .DeclareVar(typeof(EventBean[]), "wrapper",
                    Cast(typeof(EventBean[]),
                        ArrayAtIndex(Ref("array"),
                            Constant(_propertyIndex))))
                .MethodReturn(StaticMethod(typeof(BaseNestableEventUtil),
                    "GetBNArrayPropertyUnderlying", Ref("wrapper"), Constant(_index)));
        }

        public bool IsObjectArrayExistsProperty(Object[] array)
        {
            return true;
        }

        public Object Get(EventBean eventBean)
        {
            Object[] array = BaseNestableEventUtil.CheckedCastUnderlyingObjectArray(eventBean);
            return GetObjectArray(array);
        }

        public bool IsExistsProperty(EventBean eventBean)
        {
            return true; // Property exists as the property is not dynamic (unchecked)
        }

        public Object GetFragment(EventBean obj)
        {
            Object[] array = BaseNestableEventUtil.CheckedCastUnderlyingObjectArray(obj);
            EventBean[] wrapper = (EventBean[])array[_propertyIndex];
            return BaseNestableEventUtil.GetBNArrayPropertyBean(wrapper, _index);
        }

        private String GetFragmentCodegen(ICodegenContext context)
        {
            return context.AddMethod(typeof(Object), typeof(Object[]), "array", GetType())
                .DeclareVar(typeof(EventBean[]), "wrapper", 
                    Cast(typeof(EventBean[]), 
                    ArrayAtIndex(
                        Ref ("array"), 
                        Constant(_propertyIndex))))
                .MethodReturn(
                    StaticMethod(typeof(BaseNestableEventUtil), "GetBNArrayPropertyBean",
                    Ref("wrapper"), 
                    Constant(_index)));
        }

        public ICodegenExpression CodegenEventBeanGet(ICodegenExpression beanExpression, ICodegenContext context)
        {
            return CodegenUnderlyingGet(CastUnderlying(typeof(Object[]), beanExpression), context);
        }

        public ICodegenExpression CodegenEventBeanExists(ICodegenExpression beanExpression, ICodegenContext context)
        {
            return ConstantTrue();
        }

        public ICodegenExpression CodegenEventBeanFragment(ICodegenExpression beanExpression, ICodegenContext context)
        {
            return CodegenUnderlyingFragment(CastUnderlying(typeof(Object[]), beanExpression), context);
        }

        public ICodegenExpression CodegenUnderlyingGet(ICodegenExpression underlyingExpression, ICodegenContext context)
        {
            return LocalMethod(GetObjectArrayCodegen(context), underlyingExpression);
        }

        public ICodegenExpression CodegenUnderlyingExists(ICodegenExpression underlyingExpression, ICodegenContext context)
        {
            return ConstantTrue();
        }

        public ICodegenExpression CodegenUnderlyingFragment(ICodegenExpression underlyingExpression, ICodegenContext context)
        {
            return LocalMethod(GetFragmentCodegen(context), underlyingExpression);
        }
    }
}