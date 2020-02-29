using Shouldly;
using Xunit;

namespace Arado.Option.Tests.Core
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

            static string Unwrap(string some) => some;

            var actual = sut is Some<string> some ? Unwrap(some) : unexpected;

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
    }
}
