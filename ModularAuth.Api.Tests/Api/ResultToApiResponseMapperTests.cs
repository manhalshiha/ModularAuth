using ModularAuth.Api.Common.Abstractions;
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
    private readonly ResultToApiResponseMapper _mapper;

    public ResultToApiResponseMapperTests()
    {
        // Inject fake metadata provider to ensure deterministic tests
        var metadataProvider = new FakeMetadataProvider();
        _mapper = new ResultToApiResponseMapper(metadataProvider);
    }

    /// <summary>
    /// Fake implementation of <see cref="IApiMetadataProvider"/>
    /// used to remove randomness (GUID/time) from tests.
    /// </summary>
    private class FakeMetadataProvider : IApiMetadataProvider
    {
        public ApiMeta Create()
        {
            return new ApiMeta
            {
                CorrelationId = "test-correlation-id",
                Timestamp = new DateTime(2026, 01, 01)
            };
        }
    }

    [Fact]
    public void ToResponse_ShouldReturnSuccess_WhenResultIsSuccess()
    {
        var result = Result.Success();

        var response = _mapper.ToResponse(result);

        Assert.True(response.Success);
        Assert.Null(response.Error);
        Assert.Null(response.Data);
    }

    [Fact]
    public void ToResponse_ShouldReturnFailure_WhenResultIsFailure()
    {
        var error = Error.Validation("ERR001", "Invalid input");
        var result = Result.Failure(error);

        var response = _mapper.ToResponse(result);

        Assert.False(response.Success);
        Assert.NotNull(response.Error);

        Assert.Equal("ERR001", response.Error!.Code);
        Assert.Equal("Invalid input", response.Error.Message);
        Assert.Equal(error.Type.ToString(), response.Error.Type);
    }

    [Fact]
    public void ToResponse_Generic_ShouldReturnSuccess_WithValue()
    {
        var result = Result<string>.Success("test-value");

        var response = _mapper.ToResponse(result);

        Assert.True(response.Success);
        Assert.Equal("test-value", response.Data);
        Assert.Null(response.Error);
    }

    [Fact]
    public void ToResponse_Generic_ShouldReturnFailure_WhenResultIsFailure()
    {
        var error = Error.NotFound("ERR404", "Resource not found");
        var result = Result<string>.Failure(error);

        var response = _mapper.ToResponse(result);

        Assert.False(response.Success);
        Assert.NotNull(response.Error);

        Assert.Equal("ERR404", response.Error!.Code);
        Assert.Equal("Resource not found", response.Error.Message);
        Assert.Equal(error.Type.ToString(), response.Error.Type);

        Assert.Null(response.Data);
    }

    [Fact]
    public void ToResponse_ShouldIncludeMetadata_WhenCalled()
    {
        var result = Result.Success();

        var response = _mapper.ToResponse(result);

        Assert.NotNull(response.Metadata);
        Assert.Equal("test-correlation-id", response.Metadata!.CorrelationId);
        Assert.Equal(new DateTime(2026, 01, 01), response.Metadata.Timestamp);
    }

    [Fact]
    public void ToResponse_Generic_ShouldIncludeMetadata_WhenCalled()
    {
        var result = Result<string>.Success("value");

        var response = _mapper.ToResponse(result);

        Assert.NotNull(response.Metadata);
        Assert.Equal("test-correlation-id", response.Metadata!.CorrelationId);
    }
}