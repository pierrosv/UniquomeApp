using System.Text;
using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;

namespace UniquomeApp.WebApi.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void UseFluentValidationExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(x =>
        {
            x.Run(async context =>
            {
                var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (errorFeature == null)
                    throw new Exception("Unknown Exception");

                var exception = errorFeature.Error;
                if (!(exception is ValidationException validationException))
                {
                    throw exception;
                }

                var errors = validationException.Errors.Select(err => new
                {
                    err.PropertyName,
                    err.ErrorMessage
                });

                var errorText = JsonSerializer.Serialize(errors);
                context.Response.StatusCode = 400;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(errorText, Encoding.UTF8);
            });
        });
    }
}