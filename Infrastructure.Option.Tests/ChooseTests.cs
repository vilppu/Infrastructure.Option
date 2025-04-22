using System.Collections.Immutable;
using Shouldly;
using Xunit;

namespace Infrastructure.Tests.Core;

record ExampleType(string ExampleProperty);

public class ChooseTests
{
    [Fact]
    public void Choose_underlying_value() =>
        Option.Some(new ExampleType("Example value"))
            .Choose(example => example.ExampleProperty)
            .Equals("Example value")
            .ShouldBeTrue();

    [Fact]
    public void Choose_underlying_values() =>
        ImmutableList
            .Create("1", Option<string>.None, "2", Option<string>.None, "3")
            .Choose()
            .ShouldBe(["1", "2", "3"]);

    [Fact]
    public void Choose_first_underlying_value() =>
        ImmutableList
            .Create("1", Option<string>.None, "2", Option<string>.None, "3")
            .ChooseFirst()
            .ShouldBe("1");

    [Fact]
    public void Choose_single_underlying_value() =>
        ImmutableList
            .Create(Option<string>.None, "2", Option<string>.None)
            .ChooseSingle()
            .ShouldBe("2");

    [Fact]
    public void Choose_first_underlying_value_matching_the_filter() =>
        ImmutableList
            .Create(Option<string>.None, "1", Option<string>.None, "2", Option<string>.None, "3")
            .ChooseFirst()
            .ShouldBe("1");

    [Fact]
    public void Choose_single_underlying_value_matching_the_filter() =>
        ImmutableList
            .Create("1", Option<string>.None, "2", Option<string>.None)
            .ChooseSingle(value => value == "2")
            .ShouldBe("2");

    [Fact]
    public void Choosing_from_underlying_values() =>
        ImmutableList
            .Create(new ExampleType("1"), Option<ExampleType>.None, new ExampleType("2"), Option<ExampleType>.None, new ExampleType("3"))
            .Choose(example => example.ExampleProperty)
            .ShouldBe(["1", "2", "3"]);

    [Fact]
    public void Choosing_from_nothing_results_to_nothing() =>
        Option.None<ExampleType>()
            .Choose(example => example.ExampleProperty)
            .ShouldBeOfType<None<string>>();
}