using Shouldly;
using Xunit;

namespace Infrastructure.Tests.Core
{
    public class SomeTests
    {
        [Fact]
        public void Equals_EqualValue_True()
        {
            const string value = "Example value";

            var sut = Option.Some(value);

            var actual = sut.Equals(value);

            actual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_SomeWithEqualValue_True()
        {
            const string value = "Example value";

            var sut = Option.Some(value);
            var someWithEqualValue = Option.Some(value);

            var actual = sut.Equals(someWithEqualValue);

            actual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_SomeWithUnequalValue_False()
        {
            const string value = "Example value";

            var sut = Option.Some(value);
            var someWithUnequalValue = Option.Some("Another value");

            var actual = sut.Equals(someWithUnequalValue);

            actual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_NoneOfSameType_False()
        {
            const string value = "Example value";

            var sut = Option.Some(value);
            var noneOfSameType = Option.None<string>();

            var actual = sut.Equals(noneOfSameType);

            actual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_NoneOfDifferentType_False()
        {
            const string value = "Example value";

            var sut = Option.Some(value);
            var noneOfDifferentType = Option.None<int>();

            var actual = sut.Equals(noneOfDifferentType);

            actual.ShouldBeFalse();
        }

        [Fact]
        public void ToString_IsValueToString()
        {
            const string expected = "Example value";

            var sut = Option.Some(expected);

            var actual = sut.ToString();

            actual.ShouldBe(expected);
        }

        [Fact]
        public void ImplicitConversionToValue_UnwrapsValue()
        {
            const string expected = "Example value";

            var sut = Option.Some(expected);

            string actual = sut;

            actual.ShouldBe(expected);
        }

        [Fact]
        public void ExplicitConversionToValue_UnwrapsValue()
        {
            const string expected = "Example value";

            var sut = Option.Some(expected);
            var actual = (string)sut;

            actual.ShouldBe(expected);
        }

        [Fact]
        public void ImplicitConversionToOption_WrapsValue()
        {
            const string value = "Example value";

            var sut = value;
            var expected = Option.Some(value);

            Some<string> actual = sut;


            actual.ShouldBe(expected);
        }

        [Fact]
        public void ExplicitConversionToOption_WrapsValue()
        {
            const string value = "Example value";

            var sut = value;
            var expected = Option.Some(value);

            var actual = (Some<string>)sut;

            actual.ShouldBe(expected);
        }

        [Fact]
        public void PatternMath_Some_IsMatch()
        {
            const string expected = "Example value";
            const string unexpected = "Another value";

            Option<string> sut = Option.Some(expected);

            var actual = sut is Some<string> some ? some.Value : unexpected;

            actual.ShouldBe(expected);
        }

        [Fact]
        public void PatternMath_None_IsNotMatch()
        {
            const string value = "Example value";

            Option<string> sut = Option.Some(value);
            var actual = sut is None<string> ? true : false;

            actual.ShouldBeFalse();
        }

        [Fact]
        public void IsSome_True()
        {
            var sut = Option.Some("Example value");

            var actual = sut.IsSome();

            actual.ShouldBeTrue();
        }

        [Fact]
        public void IsNone_False()
        {
            var sut = Option.Some("Example value");

            var actual = sut.IsNone();

            actual.ShouldBeFalse();
        }

        [Fact]
        public void Or_WithValueFallback_ReturnsOriginalValue()
        {
            var expected = "Original value";
            var fallback = "Fallback value";
            var sut = Option.Some(expected);

            var actual = sut.Or(fallback);

            actual.ShouldBe(expected);
        }

        [Fact]
        public void Or_WithLambdaFallback_ReturnsOriginalValue()
        {
            var expected = "Original value";
            var fallback = "Fallback value";
            var sut = Option.Some(expected);

            var actual = sut.Or(() => fallback);

            actual.ShouldBe(expected);
        }

        [Fact]
        public void Or_WithLambdaFallback_FallbackIsNotInvoked()
        {
            var fallbackInvoked = false;
            var sut = Option.Some("Original value");

            var _ = sut.Or(() => { fallbackInvoked = true; return "Fallback value"; });

            fallbackInvoked.ShouldBeFalse();
        }

        [Fact]
        public void Otherwise_WithValueAnother_ReturnsOriginalValue()
        {
            var expected = Option.Some("Original value");
            var another = Option.Some("Another value");
            var sut = expected;

            var actual = sut.Otherwise((Option<string>)another);

            actual.ShouldBe(expected);
        }

        [Fact]
        public void Otherwise_WithLambdaAnother_ReturnsOriginalValue()
        {
            var expected = Option.Some("Original value");
            var another = Option.Some("Another value");
            var sut = expected;

            var actual = sut.Otherwise((System.Func<Option<string>>)(() => another));

            actual.ShouldBe(expected);
        }

        [Fact]
        public void Otherwise_WithLambdaAnother_FallbackIsNotInvoked()
        {
            var fallbackInvoked = false;
            var sut = Option.Some("Original value");

            var _ = sut.Otherwise((System.Func<Option<string>>)(() => { fallbackInvoked = true; return Option.Some("Fallback value"); }));

            fallbackInvoked.ShouldBeFalse();
        }

        [Fact]
        public void Otherwise_ReturnsOriginal()
        {
            var sut = Option.Some("Original value");

            var actual = sut.Otherwise("Another value");

            actual.ShouldBe("Original value");
        }

        [Fact]
        public void OtherwiseWithLambda_ReturnsOriginal()
        {
            var sut = Option.Some("Original value");

            var actual = sut.Otherwise(() => "Another value");

            actual.ShouldBe("Original value");
        }
    }
}
