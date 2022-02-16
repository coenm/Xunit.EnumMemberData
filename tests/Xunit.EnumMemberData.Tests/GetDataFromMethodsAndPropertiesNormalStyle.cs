namespace XunitEnumMemberData.Tests;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using FluentAssertions;
using FluentAssertions.Types;
using Xunit;
using XunitEnumMemberData.Tests.Misc;

public class GetDataFromMethodsAndPropertiesNormalStyle
{
    [Fact]
    public void GetData_ShouldReturnElementsFromStaticParameterlessMethod_NormalStyle()
    {
        // arrange
        MethodInfo methodInfo = MethodInfoHelper.GetMethodInfo<GetDataFromMethodsAndPropertiesNormalStyle>(x => x.GetData_ShouldReturnElementsFromStaticParameterlessMethod_NormalStyle());
        
        //act
        var sut = new EnumMemberDataAttribute(nameof(NormalParameterlessStaticMethod));
        IEnumerable<object[]> result = sut.GetData(methodInfo);

        // assert
        result.Should().BeEquivalentTo(new List<object[]>
            {
                new object[1] { Gender.Male, },
                new object[1] { Gender.Female, },
            });
    }

    public static IEnumerable<object[]> NormalParameterlessStaticMethod()
    {
        yield return new object[] { Gender.Female, };
        yield return new object[] { Gender.Male, };
    }

    [Fact]
    public void GetData_ShouldReturnElementsFromStaticParameterMethod_NormalStyle()
    {
        // arrange
        MethodInfo methodInfo = MethodInfoHelper.GetMethodInfo<GetDataFromMethodsAndPropertiesNormalStyle>(x => x.GetData_ShouldReturnElementsFromStaticParameterMethod_NormalStyle());

        //act
        var sut = new EnumMemberDataAttribute(nameof(NormalParameterStaticMethod), true);
        IEnumerable<object[]> result = sut.GetData(methodInfo);

        // assert
        result.Should().BeEquivalentTo(new List<object[]>
            {
                new object[1] { Gender.Male, },
            });
    }

    public static IEnumerable<object[]> NormalParameterStaticMethod(bool onlyMale)
    {
        if (!onlyMale)
        {
            yield return new object[] { Gender.Female, };
        }

        yield return new object[] { Gender.Male, };
    }

    [Fact]
    public void GetData_ShouldReturnElementsFromStaticProperty_NormalStyle()
    {
        // arrange
        MethodInfo methodInfo = MethodInfoHelper.GetMethodInfo<GetDataFromMethodsAndPropertiesNormalStyle>(x => x.GetData_ShouldReturnElementsFromStaticProperty_NormalStyle());

        //act
        var sut = new EnumMemberDataAttribute(nameof(NormalProperty));
        IEnumerable<object[]> result = sut.GetData(methodInfo);

        // assert
        result.Should().BeEquivalentTo(new List<object[]>
            {
                new object[1] { Gender.Male, },
                new object[1] { Gender.Female, },
            });
    }

    public static IEnumerable<object[]> NormalProperty
    {
        get
        {
            yield return new object[] { Gender.Female, };
            yield return new object[] { Gender.Male, };
        }
    }
}