using ModularAuth.Api.Common.Responses;
using Xunit;

namespace ModularAuth.Api.Tests.Api;

/// <summary>
/// Tests for <see cref="ApiResponse{T}"/> and <see cref="ApiMeta"/>.
/// </summary>
public class ApiResponseTests
{
    [Fact]
    public void SuccessResponse_ShouldIncludeMetadata_WhenProvided()
    {
        var meta = new ApiMeta
        {
            CorrelationId = "test-correlation",
            Timestamp = DateTime.UtcNow
        };

        var response = ApiResponse<string>.SuccessResponse("data", meta);

        Assert.True(response.Success);
        Assert.NotNull(response.Metadata);
        Assert.Equal("test-correlation", response.Metadata!.CorrelationId);
    }

    [Fact]
    public void FailureResponse_ShouldIncludeMetadata_WhenProvided()
    {
        var meta = new ApiMeta
        {
            CorrelationId = "error-correlation"
        };

        var error = new ApiError
        {
            Code = "ERR",
            Message = "error",
            Type = "Failure"
        };

        var response = ApiResponse<string>.FailureResponse(error, meta);

        Assert.False(response.Success);
        Assert.NotNull(response.Metadata);
        Assert.Equal("error-correlation", response.Metadata!.CorrelationId);
    }
}