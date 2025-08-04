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
            })
            .AddInMemoryIdentityResources(Config.IdentityResources)
            .AddInMemoryApiScopes(Config.ApiScopes)
            .AddInMemoryClients(Config.Clients)
            .AddTestUsers(TestUsers.Users);

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: MyReactAppCorsPolicy,
                              policy =>
                              {
                                  policy.SetIsOriginAllowed(origin =>
                                  {
                                      return origin == "https://localhost:5173" || origin == null;
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
