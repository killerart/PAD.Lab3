using System;
using Lab3.Shared.ServiceInstaller.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Lab3.Warehouse {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddSwaggerGen(options => options.SwaggerDoc("v1", new OpenApiInfo { Title = "Lab3.Warehouse", Version = "v1" }));
            services.AddInstallersFromAssemblyContaining<IWarehouseMarker>(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger) {
            app.Use(async (context, next) => {
                logger.LogInformation("[{Now}] {Method} {Scheme}://{Host}{Path}",
                                      DateTime.Now,
                                      context.Request.Method,
                                      context.Request.Scheme,
                                      context.Request.Host,
                                      context.Request.Path);
                await next.Invoke();
            });
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Lab3.Warehouse v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
