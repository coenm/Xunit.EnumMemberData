# Xunit.EnumMemberData

Xunit.EnumMemberData is a library containing one attribute (`[EnumMemberData]`) to simplify using enums when writing theories (parameterized unittests) using xunit.

## By type of enum
<!-- snippet: EnumMemberDataByType -->
<a id='snippet-enummemberdatabytype'></a>
```cs
[Theory]
[EnumMemberData(typeof(Gender))]
public void TestWithAllEnumValues_UsingEnumMemberData_ByType(Gender gender)
{
    _outputWriter.WriteLine($"Gender: {gender}");
}
```
<sup><a href='/tests/Xunit.EnumMemberData.Tests/Samples.cs#L20-L27' title='Snippet source file'>snippet source</a> | <a href='#snippet-enummemberdatabytype' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

## By property (or method) with simple return value
<!-- snippet: EnumMemberDataByProperty -->
<a id='snippet-enummemberdatabyproperty'></a>
```cs
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
```
<sup><a href='/tests/Xunit.EnumMemberData.Tests/Samples.cs#L29-L46' title='Snippet source file'>snippet source</a> | <a href='#snippet-enummemberdatabyproperty' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->
