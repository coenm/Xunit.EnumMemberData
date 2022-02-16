namespace XunitEnumMemberData;

using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;
using Xunit.Sdk;

/// <summary>
/// Provides a data source for a data theory, with the data coming from one of the following sources:
/// <list type="bullet">
///   <item>A static property</item>
///   <item>A static field</item>
///   <item>A static method (with or without parameters)</item>
///   <item>All members of a given enum.</item>
/// </list>
/// The member must return something compatible with
/// <list type="bullet">
///   <item>IEnumerable&lt;object[]&gt;</item>
///   <item>IEnumerable&lt;enum type&gt;</item>
/// </list> with the test data.
/// Caution: the property is completely enumerated by .ToList() before any test is run. Hence it should return independent object sets.
/// </summary>
[DataDiscoverer("Xunit.Sdk.MemberDataDiscoverer", "xunit.core")]
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public sealed class EnumMemberDataAttribute : MemberDataAttributeBase
{
    private readonly Type? _enumType;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Xunit.EnumMemberDataAttribute" /> class.
    /// </summary>
    /// <param name="enumType">The enum type that will provide the test data</param>
    public EnumMemberDataAttribute(Type enumType)
        : base(enumType?.Name ?? string.Empty, null)
    {
        _enumType = enumType ?? throw new ArgumentNullException(nameof(enumType));

        if (!_enumType.IsEnum)
        {
            throw new ArgumentException($"Provided type ({_enumType.Name}) is not an enum.", nameof(enumType));
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Xunit.EnumMemberDataAttribute" /> class.
    /// </summary>
    /// <param name="memberName">The name of the public static member on the test class that will provide all enum values.</param>
    /// <param name="parameters">The parameters for the member (only supported for methods; ignored for everything else)</param>
    public EnumMemberDataAttribute(string memberName, params object[] parameters)
        : base(memberName, parameters)
    {
    }

    /// <inheritdoc />
    public override IEnumerable<object[]?> GetData(MethodInfo testMethod)
    {
        return _enumType != null
            ? ObjectsEnumerable(testMethod, _enumType)
            : base.GetData(testMethod);
    }

    private IEnumerable<object[]?> ObjectsEnumerable(MethodInfo testMethod, Type enumType)
    {
        foreach (var value in Enum.GetValues(enumType))
        {
            yield return ConvertDataItem(testMethod, value);
        }
    }

    /// <inheritdoc />
    protected override object[]? ConvertDataItem(MethodInfo testMethod, object? item)
    {
        if (item == null)
        {
            return null;
        }

        if (item is object[] array)
        {
            return array;
        }

        if (item is Enum)
        {
            return new object[] { item, };
        }

        throw new ArgumentException($"Property {MemberName} on {MemberType ?? testMethod.DeclaringType} yielded an item that is not an object[]");
    }
}