using System;
using System.Collections.Immutable;
using Shouldly;
using Xunit;

namespace Infrastructure.Tests.Core;

public class ChooseSingularValueFromSequenceTests
{
    [Fact]
    public void Choose_first_value() =>
        ImmutableList
            .Create("1", "2", "3")
            .FirstOrNone()
            .ShouldBe("1");
    [Fact]
    public void Choose_first_value_from_empty_sequence() =>
        ImmutableList<string>
            .Empty
            .FirstOrNone()
            .ShouldBeOfType<None<string>>();
    [Fact]
    public void Choose_single_value() =>
        ImmutableList
            .Create("1")
            .SingleOrNone()
            .ShouldBe("1");
    [Fact]
    public void Choose_single_value_from_empty_sequence() =>
        ImmutableList<string>
            .Empty
            .SingleOrNone()
            .ShouldBeOfType<None<string>>();

    [Fact]
    public void Choosing_single_value_from_sequence_with_multiple_elements_is_considered_an_error()
    {
        var act = () => ImmutableList
            .Create("1", "2", "3")
            .SingleOrNone();

        act.ShouldThrow<InvalidOperationException>();
    }

    [Fact]
    public void Choose_first_value_matching_the_filter() =>
        ImmutableList
            .Create("1", "2", "2")
            .FirstOrNone(element => element == "2")
            .ShouldBe("2");
    [Fact]
    public void Choose_first_value_matching_the_filter_from_empty_sequence() =>
        ImmutableList<string>
            .Empty
            .FirstOrNone(element => element == "2")
            .ShouldBeOfType<None<string>>();
    [Fact]
    public void Choose_single_value_matching_the_filter() =>
        ImmutableList
            .Create("1", "2", "3")
            .SingleOrNone(element => element == "2")
            .ShouldBe("2");
    [Fact]
    public void Choose_single_value_matching_the_filter_from_empty_sequence() =>
        ImmutableList<string>
            .Empty
            .SingleOrNone(element => element == "2")
            .ShouldBeOfType<None<string>>();

    [Fact]
    public void Choosing_single_value_sequence_is_considered_an_error_when_multiple_elements_match_the_filter()
    {
        var act = () => ImmutableList
            .Create("1", "2", "2")
            .SingleOrNone(element => element == "2");

        act.ShouldThrow<InvalidOperationException>();
    }
}