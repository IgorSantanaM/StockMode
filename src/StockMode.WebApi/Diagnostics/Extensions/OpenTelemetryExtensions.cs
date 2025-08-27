using Npgsql;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Diagnostics;
using System.Reflection;

namespace StockMode.WebApi.Diagnostics.Extensions
{
    public static class OpenTelemetryExtensions
    {
        public static WebApplicationBuilder AddOpenTelemetry(this WebApplicationBuilder builder)
        {
            const string serviceName = "StockMode.WebApi";
            var otlpEndpoint = new Uri(builder.Configuration.GetValue<string>("OTLP_Endpoint")!);

            builder.Services.AddOpenTelemetry()
                .ConfigureResource(resource =>
                {
                    resource
                        .AddService(serviceName)
                        .AddAttributes(new[]
                            {
                                new KeyValuePair<string, object>("service.version",
                                    Assembly.GetExecutingAssembly().GetName().Version!.ToString())
                            });
                })
                .WithTracing(tracing =>
                    tracing
                        .AddAspNetCoreInstrumentation()
                        .AddHttpClientInstrumentation()
                        .AddNpgsql()
                        .AddOtlpExporter(opt =>
                                opt.Endpoint = otlpEndpoint)
                        .AddConsoleExporter()
                )
                .WithMetrics(metrics => 
                    metrics
                        .AddAspNetCoreInstrumentation()
                        .AddHttpClientInstrumentation()
                        .AddMeter("Microsoft.AspNetCore.Hosting")
                        .AddMeter("Microsoft.AspNetCore.Server.Kestrel")
                        .AddMeter(ApplicationDiagnostics.Meter.Name)
                        .AddOtlpExporter(opt =>
                                opt.Endpoint = otlpEndpoint)
                );

            return builder;
        }
    }
}
