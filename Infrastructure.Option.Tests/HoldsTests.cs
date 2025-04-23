using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Infrastructure.Tests.Core;

public class ValueMatchingTests
{
    [Fact]
    public void Truthful_predicate_holds() =>
        Option.Some("Example value").Holds(example => example == "Example value").ShouldBeTrue();

    [Fact]
    public async Task Truthful_async_predicate_holds() =>
        (await Option.Some("Example value").Holds(async example => example == await Task.FromResult("Example value"))).ShouldBeTrue();
    [Fact]
    public void False_predicate_does_not_hold() =>
        Option.Some("Example value").Holds(example => example == "Something else").ShouldBeFalse();

    [Fact]
    public async Task False_async_predicate_does_not_hold() =>
        (await Option.Some("Example value").Holds(async example => example == await Task.FromResult("Something else"))).ShouldBeFalse();
}