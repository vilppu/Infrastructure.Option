using Shouldly;
using Xunit;

namespace Infrastructure.Tests.Core;

public class NoneTests
{
    [Fact]
    public void Nones_with_same_value_type_are_equal() => 
        Option.None<int>().Equals(Option.None<int>()).ShouldBeTrue();

    [Fact]
    public void Nones_with_different_value_type_are_not_equal() => 
        // ReSharper disable once SuspiciousTypeConversion.Global
        Option.None<int>().Equals(Option.None<string>()).ShouldBeFalse();

    [Fact]
    public void None_and_some_are_not_equal() => 
        Option.None<int>().Equals(Option.Some(0)).ShouldBeFalse();

    [Fact]
    public void None_is_never_equal_to_some_value() => 
        Option.None<int>().Equals(0).ShouldBeFalse();

    [Fact]
    public void None_string_is_empty() => 
        Option.None<string>().ToString().ShouldBeEmpty();

    [Fact]
    public void Nones_with_same_value_type_match() => 
        (Option.None<string>() is None<string>).ShouldBeTrue();

    [Fact]
    public void Nones_with_different_value_type_do_not_match() => 
        (Option.None<string>() is Some<string>).ShouldBeFalse();

    [Fact]
    public void None_value_is_none() => 
        Option.None<string>().IsNone().ShouldBeTrue();

    [Fact]
    public void None_falls_back_to_given_value() => 
        Option.None<string>().Or("Fallback value").ShouldBe("Fallback value");

    [Fact]
    public void None_falls_back_to_given_option() => 
        Option.None<string>().Otherwise(Option.Some("Another value")).ShouldBe(Option.Some("Another value"));

    [Fact]
    public void None_falls_back_to_given_option_value() => 
        Option.None<string>().Otherwise("Another value").ShouldBe("Another value");

    [Fact]
    public void None_falls_back_to_given_lambda_result() => 
        Option.None<string>().Otherwise(() => "Another value").ShouldBe("Another value");

    [Fact]
    public void None_falls_back_to_given_option_lambda_option() =>
        Option.None<string>().Otherwise(() => Option.Some("Another value")).ShouldBe(Option.Some("Another value"));

    [Fact]
    public void None_falls_back_to_given_none() => 
        Option.None<string>().Or(Option.None<string>()).ShouldBe(Option.None<string>());

    [Fact]
    public void None_falls_back_to_given_none_lambda() => 
        Option.None<string>().Or(Option.None<string>).ShouldBe(Option.None<string>());

    [Fact]
    public void Default_to_given_fallback_value() =>
        Option.None<string>().Or(() => "Fallback value").ShouldBe("Fallback value");
}