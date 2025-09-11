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
                        "https://localhost:5173/signin-oidc",
                        "http://localhost/signin-oidc",
                    },
                    PostLogoutRedirectUris =
                    {
                        "https://localhost:5173/",
                        "http://localhost/",
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