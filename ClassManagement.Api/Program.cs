using ClassManagement.Api.Common.Exceptions;
using ClassManagement.Api.Configurations;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

//DB Context
DatabaseConfig.ConfigureDatabase(builder.Services, builder.Configuration);

DatabaseConfig.ConfigureContext(builder.Services);

//DI
DIConfig.ConfigureDI(builder.Services);

//AutoMapper
AutoMapperConfig.ConfigureAutoMapper(builder.Services);

//FluentAPI
FluentApiConfig.ConfigureFluentApi(builder.Services);

//Configure Identity
IdentityConfig.ConfigureIdentity(builder.Services, builder.Configuration);

//Cofigure the Authentication 
IdentityConfig.ConfigureAuthenticate(builder.Services, builder.Configuration);

//Configure the SwaggerGen
SwaggerApiConfig.ConfigureSwagger(builder.Services, builder.Configuration);

//Configure the Serilog
SerilogConfig.ConfigurSerilog(builder.Host, builder.Environment);

//Configuration the EmailSender
EmailConfig.ConfigureEmail(builder.Services, builder.Configuration);

//Configuration the Quartz
QuartzConfig.ConfigureQuartz(builder.Services);

//Configuration the Rate Limiting
RateLimiterConfig.ConfigureRateLimiter(builder.Services);

var app = builder.Build();

//Declare DB if not exist
DatabaseConfig.ConfigureDeclareDb(app.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

//    app.UseSwagger();

//    app.UseSwaggerUI();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Class Management Api v1");
});

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/swagger/index.html");

        return;
    }
    await next();
});

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.UseMiddleware<ExceptionHandler>();

app.UseForwardedHeaders();

app.UseRateLimiter();

app.MapControllers();

app.UseSerilogRequestLogging();

app.Run();

public partial class Program { }
