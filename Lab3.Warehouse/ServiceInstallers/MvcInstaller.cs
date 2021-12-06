﻿using Lab3.Shared.ServiceInstaller;
using Lab3.Warehouse.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace Lab3.Warehouse.ServiceInstallers {
    public class MvcInstaller : IServiceInstaller {
        public void AddServices(IServiceCollection services, IConfiguration configuration) {
            services.AddControllers(options => { options.Filters.Add<RequestExceptionFilter>(); })
                    .AddNewtonsoftJson(options => options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore)
                    .AddXmlSerializerFormatters()
                    .AddXmlDataContractSerializerFormatters();

            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "Lab3.Warehouse", Version = "v1" }); });
        }
    }
}
