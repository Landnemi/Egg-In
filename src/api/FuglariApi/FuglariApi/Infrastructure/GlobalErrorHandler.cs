using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FuglariApi.Infrastructure
{
    public static class GlobalErrorHandler
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {

            app.UseExceptionHandler(appError => {

                appError.Run(async context => {

                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature != null)
                    {

                        var error = contextFeature.Error;

                        if (error.GetType() == typeof(ErrorCodeException))
                        {
                            var exception = (ErrorCodeException)error;
                            context.Response.StatusCode = exception.statusCode;
                            await context.Response.WriteAsync(exception.Serialize());
                        }
                        else
                        {
                            await context.Response.WriteAsync(error.GetBaseException().ToString());
                        }
                    }
                });
            });
        }
    }
}
