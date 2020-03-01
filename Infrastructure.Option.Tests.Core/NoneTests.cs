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
    }
}
