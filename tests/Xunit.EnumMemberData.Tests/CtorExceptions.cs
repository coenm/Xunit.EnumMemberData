namespace XunitEnumMemberData.Tests;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using FluentAssertions.Types;
using Xunit;
using XunitEnumMemberData.Tests.Misc;

public class CtorExceptions
{
    [Fact]
    public void Ctor_ShouldThrow_WhenTypeIsNull()
    {
        Action act = () => _ = new EnumMemberDataAttribute((Type) null);

        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Ctor_ShouldThrow_WhenTypeIsNotAnEnum()
    {
       Action act = () => _ = new EnumMemberDataAttribute(typeof(NotEnumClass));

       act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Ctor_ShouldNotThrow_WhenTypeIsEnum()
    {
        Action act = () => _ = new EnumMemberDataAttribute(typeof(Gender));

        act.Should().NotThrow();
    }
}