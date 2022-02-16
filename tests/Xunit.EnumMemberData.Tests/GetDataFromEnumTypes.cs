namespace XunitEnumMemberData.Tests;

using System.Collections.Generic;
using System.Reflection;
using FluentAssertions;
using Xunit;
using XunitEnumMemberData.Tests.Misc;

public class GetDataFromEnumTypes
{
    [Fact]
    public void GetData_ShouldReturnAllElementsFromEnum()
    {
        // arrange
        MethodInfo methodInfo = MethodInfoHelper.GetMethodInfo<GetDataFromEnumTypes>(x => x.GetData_ShouldReturnAllElementsFromEnum());
        
        //act
        var sut = new EnumMemberDataAttribute(typeof(Gender));
        IEnumerable<object[]> result = sut.GetData(methodInfo);

        // assert
        result.Should().BeEquivalentTo(new List<object[]>
            {
                new object[] { Gender.Male, },
                new object[] { Gender.Female, },
            });
    }
}