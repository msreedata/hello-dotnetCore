// Modified 2017 Peter Burkholder, cloud.gov
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace HelloWeb
{
    public class Data
    {
        public int version { get; set; }
    }
    public class Startup
    {
        private Data d = new Data();
        private string some = "someData";

        public void Configure(IApplicationBuilder app)
        {
             app.Use((context, next) =>
        {
            var cultureQuery = context.Request.Query["culture"];
            if (!string.IsNullOrWhiteSpace(cultureQuery))
            {
                var culture = new CultureInfo(cultureQuery);

                CultureInfo.CurrentCulture = culture;
                CultureInfo.CurrentUICulture = culture;
            }

            d.version = 1;


            // Call the next delegate/middleware in the pipeline
            return next();
        });

        app.Run(async (context) =>
        {
            await context.Response.WriteAsync(
                $"Hello from .Net Core ( App. Version : {d.version} )");
        });


            // app.Run(context =>
            // {
            //     return context.Response.WriteAsync("Hello World from .Net Core 2!\ndo do do");
            // });
        }
    }
}
