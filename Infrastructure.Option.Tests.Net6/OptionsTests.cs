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

        [Fact]
        public void FirstSomeOtherwiseNone_ReturnsFirstSome()
        {
            var expected = Option.Some("1");
            
            var sut = new Option<string>[]
            {
                Option.None<string>(),
                Option.None<string>(),
                expected,
                Option.Some("2")
            };

            var actual = sut.FirstSomeOtherwiseNone();

            actual.ShouldBe(expected);
        }

        [Fact]
        public void FirstSomeOtherwiseNone_OnlyNones_ReturnsNone()
        {
            var expected = Option.None<string>();

            var sut = new Option<string>[]
            {
                Option.None<string>(),
                Option.None<string>(),
                Option.None<string>()
            };

            var actual = sut.FirstSomeOtherwiseNone();

            actual.ShouldBe(expected);
        }
    }
}
