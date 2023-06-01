
using ConsoleToWebApi.Repository;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ConsoleToWebApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            // services.AddTransient<CustomMiddleware1>();
            //services.AddSingleton<IProductRespository, ProductRespository>();
            //services.AddScoped<IProductRespository, ProductRespository>();
            services.TryAddTransient<IProductRespository, ProductRespository>();
            services.TryAddTransient<IProductRespository, TestRespository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            /* app.Use(async (context, next) =>
            {        
                await context.Response.WriteAsync("hello from use \n");
                await next();
                await context.Response.WriteAsync("hello from use 2 \n");
            });

            app.UseMiddleware<CustomMiddleware1>();

            app.Map("/himanshu", customCode);

            app.Run(async context =>
            {
                await context.Response.WriteAsync("Hello from run \n");
            });
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Hello from run2");
            }); */


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void customCode(IApplicationBuilder obj)
        {
            obj.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Hello from custome code \n");
                await next();
            });
        }
    }
}
