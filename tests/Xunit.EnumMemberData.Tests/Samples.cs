namespace XunitEnumMemberData.Tests;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using XunitEnumMemberData.Tests.Misc;

public class Samples
{
    private readonly ITestOutputHelper _outputWriter;

    public Samples(ITestOutputHelper outputWriter)
    {
        _outputWriter = outputWriter ?? throw new ArgumentNullException(nameof(outputWriter));
    }

    #region EnumMemberDataByType
    [Theory]
    [EnumMemberData(typeof(Gender))]
    public void TestWithAllEnumValues_UsingEnumMemberData_ByType(Gender gender)
    {
        _outputWriter.WriteLine($"Gender: {gender}");
    }
    #endregion

    #region EnumMemberDataByProperty
    [Theory]
    [EnumMemberData(nameof(Genders))]
    public void TestWithAllEnumValues_UsingEnumMemberData_ByProperty(Gender gender)
    {
        _outputWriter.WriteLine($"Gender: {gender}");
    }

    // Normally, xunit uses return values like IEnumerable<object[]>
    public static IEnumerable<Gender> Genders
    {
        get
        {
            return Enum.GetValues(typeof(Gender)).Cast<Gender>();
        }
    }

    #endregion
}