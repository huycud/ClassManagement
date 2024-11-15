using Serilog;
using Serilog.Events;
using Utilities.Common;

namespace ClassManagement.Api.Configurations
{
    static class SerilogConfig
    {
        public static void ConfigurSerilog(IHostBuilder host, IWebHostEnvironment env)
        {

            host.UseSerilog((context, config) =>
               {
                   config.MinimumLevel.Information()

                   .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)

                   .MinimumLevel.Override("System", LogEventLevel.Warning)

                   .Enrich.FromLogContext()

                   .Enrich.WithCorrelationId()

                   .Enrich.WithEnvironmentName()

                   .WriteTo.Debug(outputTemplate: SystemConstants.OUTPUTTEMPLATE)

                   .WriteTo.Console(outputTemplate: SystemConstants.OUTPUTTEMPLATE)

                   .WriteTo.File(path: $"{env.ContentRootPath}/logs/log-.txt",

                   outputTemplate: SystemConstants.OUTPUTTEMPLATE,

                   rollingInterval: RollingInterval.Day,

                   rollOnFileSizeLimit: true)

                   .Filter.ByExcluding("@MessageTemplate like '%Executed DbCommand%'");
               });
        }
    }
}
