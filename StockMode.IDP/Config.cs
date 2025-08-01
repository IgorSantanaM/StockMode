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
            { };

    public static IEnumerable<Client> Clients =>
        new Client[] 
            {
                new Client
                {
                    ClientName = "StockMode Client",
                    ClientId = "stockmodeclient",
                    AllowedGrantTypes = GrantTypes.Code,
                    //RedirectUris =
                    //{
                    //    "https://localhost:5000/signin-oidc",
                    //},
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    },
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    }
                }
            };
}