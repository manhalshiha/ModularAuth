using ModularAuth.Api.Common.Abstractions;
using ModularAuth.Api.Common.Mappers;
using ModularAuth.Api.Common.Providers;
using ModularAuth.Api.Middleware;
using ModularAuth.Api.Middleware.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IApiMetadataProvider, ApiMetadataProvider>();
builder.Services.AddScoped<ResultToApiResponseMapper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseMiddleware<CorrelationIdMiddleware>();
app.UseGlobalExceptionHandling();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
