using System.Collections.Immutable;
using System.Threading.Tasks;
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
    public async Task Choose_underlying_value_using_async_mapping() =>
        (await Option.Some(new ExampleType("Example value"))
            .Choose(async example => await Task.FromResult(example.ExampleProperty)))
        .Equals("Example value")
        .ShouldBeTrue();

    [Fact]
    public async Task Choose_underlying_async_value() =>
        (await Task.FromResult<Option<ExampleType>>(Option.Some(new ExampleType("Example value")))
            .Choose(example => example.ExampleProperty))
            .Equals("Example value")
            .ShouldBeTrue();
    [Fact]
    public async Task Choose_underlying_async_value_using_async_mapping() =>
        (await Task.FromResult<Option<ExampleType>>(Option.Some(new ExampleType("Example value")))
            .Choose(async example => await Task.FromResult(example.ExampleProperty)))
        .Equals("Example value")
        .ShouldBeTrue();

    [Fact]
    public void Unwrap_underlying_value() =>
        Option.Some<Option<string>>(Option.Some("Example value"))
            .Choose()
            .Equals("Example value")
            .ShouldBeTrue();

    [Fact]
    public void Choose_underlying_value_with_mapping_to_optional_value() =>
        Option.Some(new ExampleType("Example value"))
            .Choose(example => (Option<string>)Option.Some(example.ExampleProperty))
            .Equals("Example value")
            .ShouldBeTrue();
    [Fact]
    public async Task Choose_underlying_value_using_async_mapping_to_optional_value() =>
        (await Option.Some(new ExampleType("Example value"))
            .Choose(async example => await Task.FromResult<Option<string>>(Option.Some(example.ExampleProperty))))
        .Equals("Example value")
        .ShouldBeTrue();

    [Fact]
    public async Task Choose_underlying_async_value_with_mapping_to_optional_value() =>
        (await Task.FromResult<Option<ExampleType>>(Option.Some(new ExampleType("Example value")))
            .Choose(example => (Option<string>)Option.Some(example.ExampleProperty)))
        .Equals("Example value")
        .ShouldBeTrue();
    [Fact]
    public async Task Choose_underlying_async_value_using_async_mapping_to_optional_value() =>
        (await Task.FromResult<Option<ExampleType>>(Option.Some(new ExampleType("Example value")))
            .Choose(async example => await Task.FromResult<Option<string>>(Option.Some(example.ExampleProperty))))
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
    public async Task Choosing_from_underlying_values_using_async_mapping() =>
        (await ImmutableList
            .Create(new ExampleType("1"), Option<ExampleType>.None, new ExampleType("2"), Option<ExampleType>.None,
                new ExampleType("3"))
            .Choose(async example => await Task.FromResult(example.ExampleProperty)))
        .ShouldBe(["1", "2", "3"]);

    [Fact]
    public void Choosing_from_any_by_mapping_to_optional() =>
        ImmutableList
            .Create("1", "2", "3")
            .Choose(example => example == "2" ? Option.None<string>() : Option.Some(example))
            .ShouldBe(["1", "3"]);

    [Fact]
    public async Task Choosing_from_any_by_asynchronously_mapping_to_optional() =>
        (await ImmutableList
            .Create("1", "2", "3")
            .Choose(async example => example == "2"
                ? await Task.FromResult(Option.None<string>())
                : await Task.FromResult(Option.Some(example))))
            .ShouldBe(["1", "3"]);

    [Fact]
    public void Choosing_from_nothing_results_to_nothing() =>
        Option.None<ExampleType>()
            .Choose(example => example.ExampleProperty)
            .ShouldBeOfType<None<string>>();
}