using ModularAuth.Api.Common.Abstractions;
using ModularAuth.Api.Common.Responses;
using ModularAuth.Domain.Results;

namespace ModularAuth.Api.Common.Mappers;

/// <summary>
/// Provides mapping functionality between domain Result objects
/// and API-level ApiResponse objects.
///
/// This implementation relies on <see cref="IApiMetadataProvider"/>
/// to ensure consistent metadata generation across the system.
/// </summary>
public class ResultToApiResponseMapper
{
    private readonly IApiMetadataProvider _metadataProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="ResultToApiResponseMapper"/> class.
    /// </summary>
    /// <param name="metadataProvider">
    /// The metadata provider used to generate response metadata.
    /// </param>
    public ResultToApiResponseMapper(IApiMetadataProvider metadataProvider)
    {
        _metadataProvider = metadataProvider;
    }

    public ApiResponse<object?> ToResponse(Result result)
    {
        var metadata = _metadataProvider.Create();

        if (result.IsSuccess)
        {
            return ApiResponse<object?>.SuccessResponse(null, metadata);
        }

        var apiError = ApiErrorMapper.Map(result.Error!);

        return ApiResponse<object?>.FailureResponse(apiError, metadata);
    }

    public ApiResponse<T> ToResponse<T>(Result<T> result)
    {
        var metadata = _metadataProvider.Create();

        if (result.IsSuccess)
        {
            return ApiResponse<T>.SuccessResponse(result.Value, metadata);
        }

        var apiError = ApiErrorMapper.Map(result.Error!);

        return ApiResponse<T>.FailureResponse(apiError, metadata);
    }
}