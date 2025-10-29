using Serilog;

namespace StockMode.IDP;

internal static class HostingExtensions
{
    const string MyReactAppCorsPolicy = "_myReactCorsPolicy";
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddRazorPages();

        builder.Services.AddIdentityServer(options =>
            {
                options.EmitStaticAudienceClaim = true;
                // Allow HTTP for Kubernetes/production deployment behind ingress
                options.Authentication.CookieSameSiteMode = Microsoft.AspNetCore.Http.SameSiteMode.Lax;
            })
            .AddInMemoryIdentityResources(Config.IdentityResources)
            .AddInMemoryApiScopes(Config.ApiScopes)
            .AddInMemoryApiResources(Config.ApiResources)
            .AddInMemoryClients(Config.Clients)
            .AddTestUsers(TestUsers.Users);

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: MyReactAppCorsPolicy,
                              policy =>
                              {
                                  policy.SetIsOriginAllowed(origin =>
                                  {
                                      // Allow localhost and Azure domain
                                      if (origin == null) return true;
                                      if (origin.StartsWith("http://localhost")) return true;
                                      if (origin.StartsWith("https://localhost")) return true;
                                      if (origin.Contains("cloudapp.azure.com")) return true;
                                      if (origin.Contains("stockmode")) return true;
                                      return false;
                                  })
                                        .AllowAnyHeader()
                                        .AllowAnyMethod()
                                        .AllowCredentials();
                              });
        });

        return builder.Build();
    }
    
    public static WebApplication ConfigurePipeline(this WebApplication app)
    { 
        app.UseSerilogRequestLogging();

        // Configure path base for Kubernetes ingress
        var pathBase = app.Configuration["PathBase"] ?? Environment.GetEnvironmentVariable("PATH_BASE");
        if (!string.IsNullOrEmpty(pathBase))
        {
            app.UsePathBase(pathBase);
        }

        // Configure forwarded headers for reverse proxy
        app.UseForwardedHeaders(new Microsoft.AspNetCore.Builder.ForwardedHeadersOptions
        {
            ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedFor | 
                             Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedProto
        });
    
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseStaticFiles();
        app.UseRouting();

        app.UseCors(MyReactAppCorsPolicy);

        app.UseIdentityServer();

        app.UseAuthorization();
        app.MapRazorPages();

        return app;
    }
}
