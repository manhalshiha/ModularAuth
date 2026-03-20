using ModularAuth.Api.Middleware;

namespace ModularAuth.Api.Middleware.Extensions;

/// <summary>
/// Provides extension methods for configuring the middleware pipeline.
/// Keeps Program.cs clean and focused on composition.
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Adds global exception handling middleware to the pipeline.
    /// </summary>
    /// <param name="app">The application builder.</param>
    /// <returns>The application builder for chaining.</returns>
    public static IApplicationBuilder UseGlobalExceptionHandling(this IApplicationBuilder app)
    {
        return app.UseMiddleware<GlobalExceptionMiddleware>();
    }
}