using ModularAuth.Api.Common.Mappers;
using ModularAuth.Domain.Errors;
using System.Net;
using Xunit;

/// <summary>
/// Tests for <see cref="HttpStatusCodeMapper"/>.
/// Ensures correct mapping between domain error types and HTTP status codes.
/// </summary>
public class HttpStatusCodeMapperTests
{
    [Fact]
    public void Map_ShouldReturnBadRequest_ForValidation()
    {
        var result = HttpStatusCodeMapper.Map(ErrorType.Validation);

        Assert.Equal(HttpStatusCode.BadRequest, result);
    }

    [Fact]
    public void Map_ShouldReturnNotFound_ForNotFound()
    {
        var result = HttpStatusCodeMapper.Map(ErrorType.NotFound);

        Assert.Equal(HttpStatusCode.NotFound, result);
    }

    [Fact]
    public void Map_ShouldReturnUnauthorized_ForUnauthorized()
    {
        var result = HttpStatusCodeMapper.Map(ErrorType.Unauthorized);

        Assert.Equal(HttpStatusCode.Unauthorized, result);
    }

    [Fact]
    public void Map_ShouldReturnConflict_ForBusinessRule()
    {
        var result = HttpStatusCodeMapper.Map(ErrorType.BusinessRule);

        Assert.Equal(HttpStatusCode.Conflict, result);
    }
}