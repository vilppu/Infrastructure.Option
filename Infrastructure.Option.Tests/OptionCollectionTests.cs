using System;
using Shouldly;
using Xunit;

namespace Infrastructure.Tests.Core;

public class OptionCollectionTests
{
    [Fact]
    public void Select_unwrapped_values() =>
        new[]
        {
            Option.None<string>(),
            Option.Some("1"),
            Option.None<string>(),
            Option.None<string>(),
            Option.Some("2"),
            Option.Some("3"),
            Option.Some("4"),
            Option.None<string>(),
            Option.Some("5"),
            Option.None<string>(),
        }.Values().ShouldBe(["1", "2", "3", "4", "5"]);

    [Fact]
    public void Select_first_value() =>
        new[]
        {
            Option.None<string>(),
            Option.None<string>(),
            Option.Some("1"),
            Option.Some("2")
        }.FirstOrNone().ShouldBe(Option.Some("1"));

    [Fact]
    public void Selecting_first_when_there_are_no_values() =>
        new[]
        {
            Option.None<string>(),
            Option.None<string>(),
            Option.None<string>()
        }.FirstOrNone().ShouldBe(Option.None<string>());

    [Fact]
    public void Select_single_value() =>
        new[]
        {
            Option.None<string>(),
            Option.Some("Example"),
            Option.None<string>()
        }.SingleOrNone().ShouldBe(Option.Some("Example"));

    [Fact]
    public void Selecting_single_when_there_are_no_values() =>
        new[]
        {
            Option.None<string>(),
            Option.None<string>()
        }.SingleOrNone().ShouldBe(Option.None<string>());

    [Fact]
    public void Getting_value_the_are_multiple_values_throws_an_exception()
    {
        var act = () => new[]
        {
            Option.None<string>(),
            Option.Some("Example"),
            Option.Some("Example2"),
        }.SingleOrNone();

        act.ShouldThrow<InvalidOperationException>();
    }

    [Fact]
    public void Get_first_match_as_optional_value() => 
        new[] { "1", "2", "3" }
            .FirstOrNone(value => value == "2")
            .ShouldBe(Option.Some("2"));

    [Fact]
    public void Getting_first_match_when_there_is_no_match()
    {
        new[] { "1", "2", "3" }
            .FirstOrNone(value => value == "4")
            .ShouldBe(Option<string>.None);
    }

    [Fact]
    public void Get_single_match_as_optional_value() => 
        new[] { "1", "2", "3" }
            .SingleOrNone(value => value == "2")
            .ShouldBe(Option.Some("2"));

    [Fact]
    public void Getting_single_match_when_there_is_no_match() => 
        new[] { "1", "2", "3" }
            .SingleOrNone(value => value == "4")
            .ShouldBe(Option<string>.None);

    [Fact]
    public void Getting_single_match_when_there_are_multiple_matches()
    {
        var act = () => new[] { "2", "2", "3" }.SingleOrNone(value => value == "2");

        act.ShouldThrow<InvalidOperationException>();
    }
}