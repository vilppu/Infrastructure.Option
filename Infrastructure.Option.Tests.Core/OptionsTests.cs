using Shouldly;
using Xunit;

namespace Infrastructure.Tests.Core
{
    public class OptionsTests
    {
        [Fact]
        public void Values_ReturnsWrappedValues()
        {
            var sut = new Option<string>[]
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
                };

            var actual = sut.Values();

            actual.ShouldBe(new[] { "1", "2", "3", "4", "5" });
        }
    }
}
