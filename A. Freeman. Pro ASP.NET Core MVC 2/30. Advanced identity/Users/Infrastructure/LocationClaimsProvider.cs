namespace Users.Infrastructure
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authentication;

    
    
    public class LocationClaimsProvider : IClaimsTransformation
    {
        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            if (principal?.HasClaim(c => c.Type == ClaimTypes.PostalCode) is false)
            {
                var identity = principal.Identity as ClaimsIdentity;

                if (identity?.IsAuthenticated is true && identity.Name != null)
                {
                    if (identity.Name.ToLower() is "alice")
                    {
                        identity.AddClaims(new []
                        {
                            new Claim(ClaimTypes.PostalCode, "DC 20500", "RemoteClaims"),
                            new Claim(ClaimTypes.StateOrProvince, "DC", "RemoteClaims"),
                        });
                    }
                    else
                    {
                        identity.AddClaims(new []
                        {
                            new Claim(ClaimTypes.PostalCode, "NY 10036", "RemoteClaims"),
                            new Claim(ClaimTypes.StateOrProvince, "NY", "RemoteClaims"),
                        });
                    }
                }
            }

            return await Task.FromResult(principal);
        }
    }
}