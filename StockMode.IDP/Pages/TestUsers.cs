// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.

using Duende.IdentityServer.Test;
using IdentityModel;
using System.Security.Claims;

namespace StockMode.IDP;

public static class TestUsers
{
    public static List<TestUser> Users
    {
        get
        {
            var address = new
            {
                street_address = "One Hacker Way",
                locality = "Heidelberg",
                postal_code = "69118",
                country = "Germany"
            };

            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "289e69ce-2b15-4477-b9fa-c363ef7e4625",
                    Username = "GoodCompany",
                    Password = "password",
                    IsActive = true,
                    Claims =
                    {
                        new Claim(JwtClaimTypes.GivenName, "Company"),
                        new Claim(JwtClaimTypes.FamilyName, "Smith"),
                        new Claim(JwtClaimTypes.Email, "igorsantanamedeiros17@gmail.com"),
                    }
                },
                new TestUser
                {
                    SubjectId = "1ad47718-6086-43f5-a7db-f7c895556053",
                    Username = "CoolCompany",
                    Password = "password",
                    IsActive = true,
                    Claims =
                    {
                        new Claim(JwtClaimTypes.GivenName, "Kompany"),
                        new Claim(JwtClaimTypes.FamilyName, "Smith"),
                        new Claim(JwtClaimTypes.Email, "igorsantanamedeiros@outlook.com"),
                    }
                }
            };
        }
    }
}