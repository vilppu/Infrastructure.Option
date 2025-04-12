using Shouldly;
using Xunit;

namespace Infrastructure.Tests.Core;

public class SomeTests
{
    [Fact]
    public void Option_is_equal_to_container_value() =>
        Option.Some("Example value").Equals("Example value").ShouldBeTrue();

    [Fact]
    public void Options_with_same_value_are_equal() =>
        Option.Some("Example value").Equals(Option.Some("Example value")).ShouldBeTrue();

    [Fact]
    public void Options_with_different_values_are_not_equal() =>
        Option.Some("Example value").Equals(Option.Some("Another value")).ShouldBeFalse();

    [Fact]
    public void Option_with_value_is_not_equal_to_option_without_value() =>
        Option.Some("Example value").Equals(Option.None<string>()).ShouldBeFalse();

    [Fact]
    public void Option_with_value_is_not_equal_to_option_without_value_of_different_type() =>
        // ReSharper disable once SuspiciousTypeConversion.Global
        Option.Some("Example value").Equals(Option.None<int>()).ShouldBeFalse();

    [Fact]
    public void Option_has_same_string_presentation_as_the_value() =>
        Option.Some("Example value").ToString().ShouldBe("Example value");

    [Fact]
    public void Implicit_unwrapping_as_value() =>
        ((string)Option.Some("Example value")).ShouldBe("Example value");

    [Fact]
    public void Explicit_unwrapping_as_value() =>
        ((string)Option.Some("Example value")).ShouldBe("Example value");

    [Fact]
    public void Implicit_wrapping_as_optional() =>
        ((Some<string>)"Example value").ShouldBe(Option.Some("Example value"));

    [Fact]
    public void Explicit_wrapping_as_optional() =>
        ((Some<string>)"Example value").ShouldBe(Option.Some("Example value"));

    [Fact]
    public void Wrapper_values_are_matched() =>
        ((Option<string>)Option.Some("Example value") is Some<string> some ? some.Value : "Another value").ShouldBe("Example value");

    [Fact]
    public void Some_does_not_match_with_none() =>
        // ReSharper disable once SuspiciousTypeConversion.Global
        ((Option<string>)Option.Some("Example value") is None<string> ? true : false).ShouldBeFalse();

    [Fact]
    public void Some_is_some() =>
        Option.Some("Example value").IsSome().ShouldBeTrue();

    [Fact]
    public void Some_is_not_none() =>
        Option.Some("Example value").IsNone().ShouldBeFalse();

    [Fact]
    public void Option_does_not_fallback_to_other_value_when_value_exists() =>
        Option.Some("Original value").Or("Fallback value").ShouldBe("Original value");

    [Fact]
    public void Option_does_not_fallback_to_other_lambda_value_when_value_exists() =>
        Option.Some("Original value").Or(() => "Fallback value").ShouldBe("Original value");

    [Fact]
    public void Fallback_factory_is_not_invoked_when_value_exists()
    {
        var fallbackInvoked = false;
        var sut = Option.Some("Original value");

        sut.Or(() =>
        {
            fallbackInvoked = true;

            return "Fallback value";
        });

        fallbackInvoked.ShouldBeFalse();
    }

    [Fact]
    public void Option_does_not_fallback_to_other_option_when_value_exists() =>
        Option.Some("Original value").Otherwise("Another value").ShouldBe("Original value");

    [Fact]
    public void Option_does_not_fallback_to_option_lambda_when_value_exists() =>
        Option.Some("Original value").Or((System.Func<Option<string>>)(() => Option.Some("Another value"))).ShouldBe(Option.Some("Original value"));

    [Fact]
    public void Fallback_option_factory_is_not_invoked_when_value_exists()
    {
        var fallbackInvoked = false;
        var sut = Option.Some("Original value");

        _ = sut.Or((System.Func<Option<string>>)(() =>
        {
            fallbackInvoked = true;
            return Option.Some("Fallback value");
        }));

        fallbackInvoked.ShouldBeFalse();
    }

    [Fact]
    public void Option_does_not_fallback_to_option_lambda_value_when_value_exists() => 
        Option.Some("Original value").Otherwise(() => "Another value").ShouldBe("Original value");
}