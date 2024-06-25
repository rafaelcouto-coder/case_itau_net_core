using CaseItau.API.Middleware;
using Microsoft.AspNetCore.Builder;

namespace CaseItau.API.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void UseCustomExceptionHandler(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
    }

}