using Shouldly;
using Xunit;

namespace Infrastructure.Tests.Core
{
    public class NullConversionTests
    {
        [Fact]
        public void OrNull_IsSome_ReturnsValue()
        {
            var sut = Option.Some("1");

            var actual = sut.OrNull();

            actual.ShouldBe("1");
        }

        [Fact]
        public void OrNull_IsNone_ReturnsNull()
        {
            var sut = Option.None<string>();

            var actual = sut.OrNull();

            actual.ShouldBeNull();
        }
    }
}
