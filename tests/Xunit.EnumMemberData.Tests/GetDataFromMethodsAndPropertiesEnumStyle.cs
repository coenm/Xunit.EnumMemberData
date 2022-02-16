namespace XunitEnumMemberData.Tests;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Xunit;
using XunitEnumMemberData.Tests.Misc;

public class GetDataFromMethodsAndPropertiesEnumStyle

{
    [Fact]
    public void GetData_ShouldReturnElementsFromStaticParameterlessMethod_EnumStyle()
    {
        // arrange
        MethodInfo methodInfo = MethodInfoHelper.GetMethodInfo<GetDataFromMethodsAndPropertiesEnumStyle>(x => x.GetData_ShouldReturnElementsFromStaticParameterlessMethod_EnumStyle());
        
        //act
        var sut = new EnumMemberDataAttribute(nameof(EnumParameterlessStaticMethod));
        IEnumerable<object[]> result = sut.GetData(methodInfo);

        // assert
        result.Should().BeEquivalentTo(new List<object[]>
            {
                new object[] { Gender.Male, },
                new object[] { Gender.Female, },
            });
    }

    public static IEnumerable<Gender> EnumParameterlessStaticMethod()
    {
        yield return Gender.Female;
        yield return Gender.Male;
    }

    [Fact]
    public void GetData_ShouldReturnElementsFromStaticParameterMethod_EnumStyle()
    {
        // arrange
        MethodInfo methodInfo = MethodInfoHelper.GetMethodInfo<GetDataFromMethodsAndPropertiesEnumStyle>(x => x.GetData_ShouldReturnElementsFromStaticParameterMethod_EnumStyle());

        //act
        var sut = new EnumMemberDataAttribute(nameof(EnumParameterStaticMethod), true);
        IEnumerable<object[]> result = sut.GetData(methodInfo);

        // assert
        result.Should().BeEquivalentTo(new List<object[]>
            {
                new object[] { Gender.Male, },
            });
    }

    public static IEnumerable<Gender> EnumParameterStaticMethod(bool onlyMale)
    {
        if (!onlyMale)
        {
            yield return Gender.Female;
        }

        yield return Gender.Male;
    }

    [Fact]
    public void GetData_ShouldReturnElementsFromStaticProperty_EnumStyle()
    {
        // arrange
        MethodInfo methodInfo = MethodInfoHelper.GetMethodInfo<GetDataFromMethodsAndPropertiesEnumStyle>(x => x.GetData_ShouldReturnElementsFromStaticProperty_EnumStyle());

        //act
        var sut = new EnumMemberDataAttribute(nameof(EnumProperty));
        IEnumerable<object[]> result = sut.GetData(methodInfo);

        // assert
        result.Should().BeEquivalentTo(new List<object[]>
            {
                new object[] { Gender.Male, },
            });
    }

    public static IEnumerable<Gender> EnumProperty
    {
        get
        {
#if NETCOREAPP2_1 || NETCOREAPP3_1
            return Enum.GetValues(typeof(Gender)).Cast<Gender>().Where(x => x != Gender.Female);
#else
            return Enum.GetValues<Gender>().Where(x => x != Gender.Female);
#endif
        }
    }
}