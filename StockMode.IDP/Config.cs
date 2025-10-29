using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace StockMode.IDP;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        { 
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
            {
                new ApiScope(name: "stockmodeapi", displayName: "StockMode API")
            };


    public static IEnumerable<ApiResource> ApiResources =>
       new ApiResource[]
       {
            new ApiResource("stockmodeapi", "StockMode API")
            {
                Scopes = { "stockmodeapi" }
            }
       };

    public static IEnumerable<Client> Clients =>
        new Client[] 
            {
                new Client
                {
                    ClientName = "StockMode React Client",
                    ClientId = "stockmodeclient",
                    AllowedGrantTypes = GrantTypes.Code,

                    RedirectUris =
                    {
                        "http://localhost:5173/signin-oidc",
                        "https://localhost:5173/signin-oidc",
                        "http://localhost/signin-oidc",
                        "https://localhost/signin-oidc",
                        "http://localhost:80/signin-oidc",
                        "http://localhost:3000/signin-oidc",
                        "http://localhost:8080/signin-oidc",
                        "http://127.0.0.1:5173/signin-oidc",
                        "http://127.0.0.1:3000/signin-oidc",
                        "http://127.0.0.1:8080/signin-oidc",
                        "http://stockmode-app.westus2.cloudapp.azure.com/signin-oidc",
                        "https://stockmode-app.westus2.cloudapp.azure.com/signin-oidc",
                    },
                    PostLogoutRedirectUris =
                    {
                        "http://localhost:5173/",
                        "https://localhost:5173/",
                        "http://localhost/",
                        "https://localhost/",
                        "http://localhost:80/",
                        "http://localhost:3000/",
                        "http://localhost:8080/",
                        "http://127.0.0.1:5173/",
                        "http://127.0.0.1:3000/",
                        "http://127.0.0.1:8080/",
                        "http://stockmode-app.westus2.cloudapp.azure.com/",
                        "https://stockmode-app.westus2.cloudapp.azure.com/",
                    },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "stockmodeapi"
                    },
                    RequireConsent = false,
                    AllowOfflineAccess = true,
                    RequireClientSecret = false,
                }
            };
}