using System.Net;
using System.Net.Http.Json;
using ModularAuth.Api.Common.Responses;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace ModularAuth.Api.Tests.Integration;

/// <summary>
/// Tests related to Correlation ID behavior across the request pipeline.
/// Ensures that a single CorrelationId is generated per request
/// and propagated correctly to the API response metadata.
/// </summary>
public class CorrelationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public CorrelationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    /// <summary>
    /// Verifies that each request returns a CorrelationId in metadata.
    /// </summary>
    [Fact]
    public async Task Should_Return_CorrelationId_In_Response_Metadata()
    {
        var response = await _client.GetAsync("/test/success");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<string>>();

        Assert.NotNull(apiResponse);
        Assert.NotNull(apiResponse!.Metadata);
        Assert.False(string.IsNullOrWhiteSpace(apiResponse.Metadata!.CorrelationId));
    }

    /// <summary>
    /// Verifies that each request generates a different CorrelationId.
    /// </summary>
    [Fact]
    public async Task Should_Generate_Different_CorrelationId_Per_Request()
    {
        var response1 = await _client.GetFromJsonAsync<ApiResponse<string>>("/test/success");
        var response2 = await _client.GetFromJsonAsync<ApiResponse<string>>("/test/success");

        Assert.NotNull(response1);
        Assert.NotNull(response2);

        Assert.NotEqual(
            response1!.Metadata!.CorrelationId,
            response2!.Metadata!.CorrelationId
        );
    }
}