using ModularAuth.Api.Common.Mappers;
using ModularAuth.Api.Common.Responses;
using ModularAuth.Domain.Errors;
using ModularAuth.Domain.Results;
using Xunit;

namespace ModularAuth.Api.Tests.Api;

/// <summary>
/// Contains unit tests for <see cref="ResultToApiResponseMapper"/>.
/// Verifies correct mapping between domain Result objects and API responses.
/// </summary>
public class ResultToApiResponseMapperTests
{
    /// <summary>
    /// Verifies that a successful non-generic Result
    /// is mapped to a successful ApiResponse.
    /// </summary>
    [Fact]
    public void ToResponse_ShouldReturnSuccess_WhenResultIsSuccess()
    {
        // Arrange
        var result = Result.Success();

        // Act
        var response = ResultToApiResponseMapper.ToResponse(result);

        // Assert
        Assert.True(response.Success);
        Assert.Null(response.Error);
        Assert.Null(response.Data);
    }

    /// <summary>
    /// Verifies that a failed non-generic Result
    /// is mapped to a failed ApiResponse with correct error details.
    /// </summary>
    [Fact]
    public void ToResponse_ShouldReturnFailure_WhenResultIsFailure()
    {
        // Arrange
        var error = Error.Validation("ERR001", "Invalid input");
        var result = Result.Failure(error);

        // Act
        var response = ResultToApiResponseMapper.ToResponse(result);

        // Assert
        Assert.False(response.Success);
        Assert.NotNull(response.Error);

        Assert.Equal("ERR001", response.Error!.Code);
        Assert.Equal("Invalid input", response.Error.Message);
        Assert.Equal(error.Type.ToString(), response.Error.Type);
    }

    /// <summary>
    /// Verifies that a successful generic Result
    /// is mapped to a successful ApiResponse containing the value.
    /// </summary>
    [Fact]
    public void ToResponse_Generic_ShouldReturnSuccess_WithValue()
    {
        // Arrange
        var result = Result<string>.Success("test-value");

        // Act
        var response = ResultToApiResponseMapper.ToResponse(result);

        // Assert
        Assert.True(response.Success);
        Assert.Equal("test-value", response.Data);
        Assert.Null(response.Error);
    }

    /// <summary>
    /// Verifies that a failed generic Result
    /// is mapped to a failed ApiResponse with correct error details.
    /// </summary>
    [Fact]
    public void ToResponse_Generic_ShouldReturnFailure_WhenResultIsFailure()
    {
        // Arrange
        var error = Error.NotFound("ERR404", "Resource not found");
        var result = Result<string>.Failure(error);

        // Act
        var response = ResultToApiResponseMapper.ToResponse(result);

        // Assert
        Assert.False(response.Success);
        Assert.NotNull(response.Error);

        Assert.Equal("ERR404", response.Error!.Code);
        Assert.Equal("Resource not found", response.Error.Message);
        Assert.Equal(error.Type.ToString(), response.Error.Type);

        Assert.Null(response.Data);
    }
    /// <summary>
    /// Verifies that when a non-generic successful <see cref="Result"/> is mapped,
    /// the resulting <see cref="ApiResponse{T}"/> includes metadata.
    /// 
    /// This ensures that all API responses—regardless of having a data payload—
    /// are enriched with infrastructure-level information such as correlation identifiers
    /// and timestamps for observability and tracing.
    /// </summary>
    [Fact]
    public void ToResponse_ShouldIncludeMetadata_WhenCalled()
    {
        var result = Result.Success();

        var response = ResultToApiResponseMapper.ToResponse(result);

        Assert.NotNull(response.Metadata);
        Assert.NotNull(response.Metadata!.CorrelationId);
        Assert.NotNull(response.Metadata.Timestamp);
    }
    /// <summary>
    /// Verifies that when a generic successful <see cref="Result{T}"/> is mapped,
    /// the resulting <see cref="ApiResponse{T}"/> includes metadata.
    /// 
    /// This ensures that responses containing data are also consistently enriched
    /// with metadata, maintaining a uniform API contract across all response types.
    /// </summary>
    [Fact]
    public void ToResponse_Generic_ShouldIncludeMetadata_WhenCalled()
    {
        var result = Result<string>.Success("value");

        var response = ResultToApiResponseMapper.ToResponse(result);

        Assert.NotNull(response.Metadata);
        Assert.NotNull(response.Metadata!.CorrelationId);
    }
}