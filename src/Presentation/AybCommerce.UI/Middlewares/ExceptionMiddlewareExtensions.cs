using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using NLog;

namespace AybCommerce.UI.Middlewares
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, Logger logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        logger.Error(contextFeature.Error, "Global exception : " + contextFeature.Error.Message);
                    }

                    context.Response.Redirect("/Home/Error?statuscode=" + context.Response.StatusCode);
                });

                //appError.UseExceptionHandler("/Home/Error/{0}");
            });
        }
    }
}
