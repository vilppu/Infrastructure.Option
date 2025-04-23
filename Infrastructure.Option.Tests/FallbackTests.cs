using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Tests.Core;

public class FallbackTests
{
    [Fact]
    public void Option_does_not_fallback_to_other_value_when_value_exists() =>
        Option.Some("Original value").Otherwise("Fallback value").ShouldBe("Original value");

    [Fact]
    public void Option_does_not_fallback_to_other_lambda_value_when_value_exists() =>
        Option.Some("Original value").Otherwise(() => "Fallback value").ShouldBe("Original value");

    [Fact]
    public void Fallback_factory_is_not_invoked_when_value_exists()
    {
        var fallbackInvoked = false;
        var sut = Option.Some("Original value");

        sut.Otherwise(() =>
        {
            fallbackInvoked = true;

            return "Fallback value";
        });

        fallbackInvoked.ShouldBeFalse();
    }

    [Fact]
    public void Option_does_not_fallback_to_other_option_when_value_exists() =>
        Option.Some("Original value").Otherwise("Fallback value").ShouldBe("Original value");

    [Fact]
    public void Option_does_not_fallback_to_option_lambda_when_value_exists() =>
        Option.Some("Original value").Otherwise((System.Func<Option<string>>)(() => Option.Some("Fallback value"))).ShouldBe(Option.Some("Original value"));

    [Fact]
    public void Fallback_option_factory_is_not_invoked_when_value_exists()
    {
        var fallbackInvoked = false;
        var sut = Option.Some("Original value");

        _ = sut.Otherwise((System.Func<Option<string>>)(() =>
        {
            fallbackInvoked = true;
            return Option.Some("Fallback value");
        }));

        fallbackInvoked.ShouldBeFalse();
    }

    [Fact]
    public void Option_does_not_fallback_to_option_lambda_value_when_value_exists() => 
        Option.Some("Original value").Otherwise(() => "Fallback value").ShouldBe("Original value");

    [Fact]
    public void None_falls_back_to_given_value() =>
        Option.None<string>().Otherwise("Fallback value").ShouldBe("Fallback value");

    [Fact]
    public void None_falls_back_to_given_option() =>
        Option.None<string>().Otherwise(Option.Some("Fallback value")).ShouldBe(Option.Some("Fallback value"));

    [Fact]
    public void None_falls_back_to_given_option_value() =>
        Option.None<string>().Otherwise("Fallback value").ShouldBe("Fallback value");

    [Fact]
    public void None_falls_back_to_given_lambda_result() =>
        Option.None<string>().Otherwise(() => "Fallback value").ShouldBe("Fallback value");

    [Fact]
    public void None_falls_back_to_given_option_lambda_option() =>
        Option.None<string>().Otherwise(() => Option.Some("Fallback value")).ShouldBe(Option.Some("Fallback value"));

    [Fact]
    public void None_falls_back_to_given_none() =>
        Option.None<string>().Otherwise(Option.None<string>()).ShouldBe(Option.None<string>());

    [Fact]
    public void None_falls_back_to_given_none_lambda() =>
        Option.None<string>().Otherwise(Option.None<string>).ShouldBe(Option.None<string>());

    [Fact]
    public void Default_to_given_fallback_value() =>
        Option.None<string>().Otherwise(() => "Fallback value").ShouldBe("Fallback value");

    [Fact]
    public async Task Option_does_not_fallback_to_async_lambda_when_value_exists() =>
        (await Option.Some("Original value").Otherwise(async () => await Task.FromResult(Option.Some("Fallback value")))).ShouldBe(Option.Some("Original value"));

    [Fact]
    public async Task Option_does_not_fallback_to_async__lambda_value_when_value_exists() =>
        (await Option.Some("Original value").Otherwise(async () => await Task.FromResult("Fallback value"))).ShouldBe("Original value");

    [Fact]
    public async Task None_falls_back_to_given_async_lambda_result() =>
        (await Option.None<string>().Otherwise(async () => await Task.FromResult("Fallback value"))).ShouldBe("Fallback value");

    [Fact]
    public async Task None_falls_back_to_given_async_lambda_option() =>
        (await Option.None<string>().Otherwise(async () => await Task.FromResult(Option.Some("Fallback value")))).ShouldBe(Option.Some("Fallback value"));

    [Fact]
    public async Task Default_to_given_async_value() =>
        (await Option.None<string>().Otherwise(async () => await Task.FromResult("Fallback value"))).ShouldBe("Fallback value");
    [Fact]
    public async Task Async_option_does_not_fallback_to_other_value_when_value_exists() =>
        (await Task.FromResult<Option<string>>(Option.Some("Original value")).Otherwise("Fallback value")).ShouldBe("Original value");

    [Fact]
    public async Task Async_option_does_not_fallback_to_other_lambda_value_when_value_exists() =>
        (await Task.FromResult<Option<string>>(Option.Some("Original value")).Otherwise(() => "Fallback value")).ShouldBe("Original value");

    [Fact]
    public async Task Async_option_does_not_fallback_to_other_option_when_value_exists() =>
        (await Task.FromResult<Option<string>>(Option.Some("Original value")).Otherwise("Fallback value")).ShouldBe("Original value");

    [Fact]
    public async Task Async_option_does_not_fallback_to_option_lambda_when_value_exists() =>
        (await Task.FromResult<Option<string>>(Option.Some("Original value")).Otherwise((System.Func<Option<string>>)(() => Option.Some("Fallback value")))).ShouldBe(Option.Some("Original value"));

    [Fact]
    public async Task Async_option_does_not_fallback_to_option_lambda_value_when_value_exists() =>
        (await Task.FromResult<Option<string>>(Option.Some("Original value")).Otherwise(() => "Fallback value")).ShouldBe("Original value");

    [Fact]
    public async Task Async_none_falls_back_to_given_value() =>
        (await Task.FromResult(Option.None<string>()).Otherwise("Fallback value")).ShouldBe("Fallback value");

    [Fact]
    public async Task Async_none_falls_back_to_given_option() =>
        (await Task.FromResult(Option.None<string>()).Otherwise(Option.Some("Fallback value"))).ShouldBe(Option.Some("Fallback value"));

    [Fact]
    public async Task Async_none_falls_back_to_given_option_value() =>
        (await Task.FromResult(Option.None<string>()).Otherwise("Fallback value")).ShouldBe("Fallback value");

    [Fact]
    public async Task Async_none_falls_back_to_given_lambda_result() =>
        (await Task.FromResult(Option.None<string>()).Otherwise(() => "Fallback value")).ShouldBe("Fallback value");

    [Fact]
    public async Task Async_none_falls_back_to_given_option_lambda_option() =>
        (await Task.FromResult(Option.None<string>()).Otherwise(() => Option.Some("Fallback value"))).ShouldBe(Option.Some("Fallback value"));

    [Fact]
    public async Task Async_none_falls_back_to_given_none() =>
        (await Task.FromResult(Option.None<string>()).Otherwise(Option.None<string>())).ShouldBe(Option.None<string>());

    [Fact]
    public async Task Async_none_falls_back_to_given_none_lambda() =>
        (await Task.FromResult(Option.None<string>()).Otherwise(Option.None<string>)).ShouldBe(Option.None<string>());

    [Fact]
    public async Task Async_default_to_given_fallback_value() =>
        (await Task.FromResult(Option.None<string>()).Otherwise(() => "Fallback value")).ShouldBe("Fallback value");

    [Fact]
    public async Task Async_option_does_not_fallback_to_async_lambda_when_value_exists() =>
        (await Task.FromResult<Option<string>>(Option.Some("Original value")).Otherwise(async () => await Task.FromResult(Option.Some("Fallback value")))).ShouldBe(Option.Some("Original value"));

    [Fact]
    public async Task Async_option_does_not_fallback_to_async__lambda_value_when_value_exists() =>
        (await Task.FromResult<Option<string>>(Option.Some("Original value")).Otherwise(async () => await Task.FromResult("Fallback value"))).ShouldBe("Original value");

    [Fact]
    public async Task Async_none_falls_back_to_given_async_lambda_result() =>
        (await Task.FromResult(Option.None<string>()).Otherwise(async () => await Task.FromResult("Fallback value"))).ShouldBe("Fallback value");

    [Fact]
    public async Task Async_none_falls_back_to_given_async_lambda_option() =>
        (await Task.FromResult(Option.None<string>()).Otherwise(async () => await Task.FromResult(Option.Some("Fallback value")))).ShouldBe(Option.Some("Fallback value"));

    [Fact]
    public async Task Async_default_to_given_async_value() =>
        (await Task.FromResult(Option.None<string>()).Otherwise(async () => await Task.FromResult("Fallback value"))).ShouldBe("Fallback value");
}