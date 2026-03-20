using System.Net;
using System.Net.Http.Json;
using ModularAuth.Api;
using ModularAuth.Api.Common.Responses;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Modular.Api.Tests.Integration;

/// <summary>
/// Integration tests for global exception handling middleware.
/// Verifies that unhandled exceptions are properly caught and transformed
/// into standardized API responses.
/// </summary>
public class ExceptionHandlingTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    /// <summary>
    /// Initializes the test server and HTTP client.
    /// </summary>
    /// <param name="factory">
    /// The web application factory used to create an in-memory test server.
    /// </param>
    public ExceptionHandlingTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    /// <summary>
    /// Verifies that when an exception is thrown,
    /// the middleware returns a standardized API response.
    /// </summary>
    [Fact]
    public async Task Should_Return_Standardized_Error_Response_When_Exception_Is_Thrown()
    {
        // Arrange
        var url = "/test/exception"; // endpoint سننشئه

        // Act
        var response = await _client.GetAsync(url);

        // Assert
        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);

        var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();

        Assert.NotNull(apiResponse);
        Assert.False(apiResponse!.Success);

        Assert.NotNull(apiResponse.Error);
        Assert.Equal("SYSTEM_ERROR", apiResponse.Error!.Code);
    }
}