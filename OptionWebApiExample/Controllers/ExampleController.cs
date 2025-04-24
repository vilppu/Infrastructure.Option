using System.Collections.Immutable;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace OptionWebApiExample.Controllers;

[ApiController]
public class ExampleController : ControllerBase
{
    [Route("/example")]
    [HttpGet]
    public Option<ExampleType> GetExample() => 
        new ExampleType("Hello!");

    [Route("/examples")]
    [HttpGet]
    public ImmutableList<Option<ExampleType>> GetExamples() =>
        [new ExampleType("Hello!")];

    [Route("/simple-type-example")]
    [HttpGet]
    public Option<string> GetSimpleTypeExample() =>
        "Hello!";

    [Route("/simple-type-examples")]
    [HttpGet]
    public ImmutableList<Option<string>> GetSimpleTypeExamples() =>
        ["Hello!"];
}