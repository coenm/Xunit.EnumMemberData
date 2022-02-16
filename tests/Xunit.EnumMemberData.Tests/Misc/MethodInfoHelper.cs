namespace XunitEnumMemberData.Tests.Misc;

using System;
using System.Linq.Expressions;
using System.Reflection;

internal static class MethodInfoHelper
{
    // copied from https://stackoverflow.com/questions/9382216/get-methodinfo-from-a-method-reference-c-sharp
    public static MethodInfo GetMethodInfo<T>(Expression<Action<T>> expression)
    {
        if (expression.Body is MethodCallExpression member)
        {
            return member.Method;
        }

        throw new ArgumentException("Expression is not a method", "expression");
    }
}