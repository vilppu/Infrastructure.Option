using Shouldly;
using Xunit;

namespace Infrastructure.Tests.Core
{
    public class NoneTests
    {
        [Fact]
        public void Equals_SameTypeNone_True()
        {
            var sut = Option.None<int>();

            var actual = sut.Equals(Option.None<int>());

            actual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_DifferentTypeNone_False()
        {
            var sut = Option.None<int>();

            var actual = sut.Equals(Option.None<string>());

            actual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_Some_False()
        {
            var sut = Option.None<int>();

            var actual = sut.Equals(Option.Some(0));

            actual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_ObjectThatIsNotNone_False()
        {
            var sut = Option.None<int>();

            var actual = sut.Equals(0);

            actual.ShouldBeFalse();
        }

        [Fact]
        public void ToString_Empty()
        {
            var sut = Option.None<string>();

            var actual = sut.ToString();

            actual.ShouldBeEmpty();
        }

        [Fact]
        public void PatternMath_None_IsMatch()
        {
            Option<string> sut = Option.None<string>();

            var actual = sut is None<string> ? true : false;

            actual.ShouldBeTrue();
        }

        [Fact]
        public void PatternMath_Some_IsNotMatch()
        {
            Option<string> sut = Option.None<string>();

            var actual = sut is Some<string> ? true : false;

            actual.ShouldBeFalse();
        }

        [Fact]
        public void IsNone_True()
        {
            var sut = Option.None<string>();

            var actual = sut.IsNone();

            actual.ShouldBeTrue();
        }

        [Fact]
        public void Or_WithValueFallback_ReturnsFallbackValue()
        {
            var expected = "Fallback value";
            var sut = Option.None<string>();

            var actual = sut.Or(expected);

            actual.ShouldBe(expected);
        }

        [Fact]
        public void Otherwise_WithValueAnother_AnotherIsSome_ReturnsAnother()
        {
            var expected = Option.Some("Another value");
            var sut = Option.None<string>();

            var actual = sut.Otherwise((Option<string>)expected);

            actual.ShouldBe(expected);
        }

        [Fact]
        public void Otherwise_ReturnsAnother()
        {
            var expected = "Another value";
            var sut = Option.None<string>();

            var actual = sut.Otherwise(expected);

            actual.ShouldBe(expected);
        }

        [Fact]
        public void OtherwiseWithLambda_ReturnsAnother()
        {
            var expected = "Another value";
            var sut = Option.None<string>();

            var actual = sut.Otherwise(() => expected);

            actual.ShouldBe(expected);
        }

        [Fact]
        public void Or_WithLambdaFallback_ReturnsFallbackResult()
        {
            var expected = "Fallback value";
            var sut = Option.None<string>();

            var actual = sut.Or(() => expected);

            actual.ShouldBe(expected);
        }

        [Fact]
        public void Otherwise_WithLambdaAnother_AnotherResultIsSome_ReturnsResult()
        {
            var expected = Option.Some("Another value");
            var sut = Option.None<string>();

            var actual = sut.Otherwise((System.Func<Option<string>>)(() => expected));

            actual.ShouldBe(expected);
        }

        [Fact]
        public void Otherwise_AnotherIsNone_ReturnsNone()
        {
            var expected = Option.None<string>();
            var sut = Option.None<string>();

            var actual = sut.Otherwise(expected);

            actual.ShouldBe(expected);
        }

        [Fact]
        public void Otherwise_WithLambdaAnother_AnotherReturnsNone_ReturnsNone()
        {
            var expected = Option.None<string>();
            var sut = Option.None<string>();

            var actual = sut.Otherwise(() => expected);

            actual.ShouldBe(expected);
        }
    }
}
