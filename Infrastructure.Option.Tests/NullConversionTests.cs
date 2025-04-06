using Shouldly;
using Xunit;

namespace Infrastructure.Tests.Core;

public class NullConversionTests
{
    [Fact]
    public void Some_can_be_treated_as_nullable_with_value() => 
        Option.Some("1").OrNull().ShouldBe("1");

    [Fact]
    public void None_can_be_treated_as_null() => 
        Option.None<string>().OrNull().ShouldBeNull();
}