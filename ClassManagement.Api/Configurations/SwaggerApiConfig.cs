using System.Reflection;
using Microsoft.OpenApi.Models;

namespace ClassManagement.Api.Configurations
{
    static class SwaggerApiConfig
    {
        public static void ConfigureSwagger(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc(configuration.GetSection("SwaggerConfig:DocVer").Value, new OpenApiInfo
                {
                    Version = configuration.GetSection("SwaggerConfig:DocVer").Value,

                    Title = configuration.GetSection("SwaggerConfig:DocTitle").Value,

                    Description = configuration.GetSection("SwaggerConfig:DocDesc").Value
                });

                swagger.AddSecurityDefinition(configuration.GetSection("SwaggerConfig:SecScheme").Value, new OpenApiSecurityScheme()
                {
                    Name = configuration.GetSection("SwaggerConfig:SecName").Value,

                    Type = SecuritySchemeType.ApiKey,

                    Scheme = configuration.GetSection("SwaggerConfig:SecScheme").Value,

                    BearerFormat = configuration.GetSection("SwaggerConfig:BearerFormat").Value,

                    In = ParameterLocation.Header,

                    Description = configuration.GetSection("SwaggerConfig:SecDesc").Value
                });

                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,

                                    Id = configuration.GetSection("SwaggerConfig:SecScheme").Value
                                }
                            },
                            new string[] {}
                    }
                });

                // ADD COMMENT IN SWAGGER
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

                swagger.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
        }
    }
}
