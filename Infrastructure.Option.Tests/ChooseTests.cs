using System.Collections.Immutable;
using Shouldly;
using Xunit;

namespace Infrastructure.Tests.Core;

record ExampleType(string ExampleProperty);

public class ChooseTests
{
    [Fact]
    public void Can_choose_property_of_some() =>
        Option.Some(new ExampleType("Example value"))
            .Choose(example => example.ExampleProperty)
            .Equals("Example value")
            .ShouldBeTrue();

    [Fact]
    public void Can_choose_property_of_none() =>
        Option.None<ExampleType>()
            .Choose(example => example.ExampleProperty)
            .ShouldBeOfType<None<string>>();

    [Fact]
    public void Can_choose_all_values() =>
        ImmutableList
            .Create("1", Option<string>.None, "2", Option<string>.None, "3")
            .Choose()
            .ShouldBe(["1", "2", "3"]);

    [Fact]
    public void Can_choose_all_property_values() =>
        ImmutableList
            .Create(new ExampleType("1"), Option<ExampleType>.None, new ExampleType("2"), Option<ExampleType>.None, new ExampleType("3"))
            .Choose(example => example.ExampleProperty)
            .ShouldBe(["1", "2", "3"]);
}