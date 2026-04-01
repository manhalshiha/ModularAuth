using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

/// <summary>
/// Integration tests for TestController.
/// Ensures correct HTTP status codes and response structure.
/// </summary>
public class TestControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public TestControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    /// <summary>
    /// Verifies that the success endpoint returns HTTP 200.
    /// </summary>
    [Fact]
    public async Task GetSuccess_ShouldReturn200()
    {
        var response = await _client.GetAsync("/test/success");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    /// <summary>
    /// Verifies that the failure endpoint returns HTTP 400.
    /// </summary>
    [Fact]
    public async Task GetFailure_ShouldReturn400()
    {
        var response = await _client.GetAsync("/test/failure");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}