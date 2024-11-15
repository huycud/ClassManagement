using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.AspNetCore;
using ClassManagement.Api.Common.Converter;
using ClassManagement.Api.Models.Validations.Users.Clients;
using ClassManagement.Api.Common.Exceptions;

namespace ClassManagement.Api.Configurations
{
    static class FluentApiConfig
    {
        public static void ConfigureFluentApi(IServiceCollection services)
        {
            //services.AddControllers()
            //                .AddFluentValidation(
            //                    opts => opts.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly())
            //                )
            //                .AddJsonOptions(
            //                    x =>
            //                    {
            //                        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;

            //                       x.JsonSerializerOptions.Converters.Add(new DateConverter());
            //                    }
            //                );

            services.AddControllers()

                    .AddJsonOptions(x =>
                    {
                        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;

                        x.JsonSerializerOptions.Converters.Add(new DateConverter());
                    });

            services.AddFluentValidationAutoValidation();

            services.AddFluentValidationClientsideAdapters();

            services.AddValidatorsFromAssemblyContaining<CreateClientValidator>();
        }
    }
}
