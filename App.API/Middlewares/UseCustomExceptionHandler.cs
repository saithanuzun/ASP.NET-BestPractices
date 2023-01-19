using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using App.Core.DTOs;
using App.Service.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace App.API.Middlewares
{
    public static class UseCustomExceptionHandler
    {
        public static void UseCustomException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(config=>
            {
                config.Run(async (context) =>
                {
                    context.Response.ContentType = "application/json";
                    var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var statusCode = exceptionFeature.Error switch
                    {
                        ClientSideException => 400,
                        NotFoundException => 404,
                        _ => 500
                    };
                    context.Response.StatusCode = statusCode;

                    var Response = CustomResponseDto<NoContentDto>.Fail(statusCode,exceptionFeature.Error.Message);
                    await context.Response.WriteAsync(JsonSerializer.Serialize(Response));

                });
            });
        }
    }
}