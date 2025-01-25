using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EntityFrameworkCoreIdentity.Token;

public class TokenJwtBuilder
{
    private SecurityKey securityKey;
    private string subject = string.Empty;
    private string issuer = string.Empty;
    private string audience = string.Empty;
    private Dictionary<string, string> claims = [];
    private int expiresInMinutes = 5;

    public TokenJwtBuilder AddSecurityKey(SecurityKey securityKey)
    {
        this.securityKey = securityKey;
        return this;
    }

    public TokenJwtBuilder AddSubject(string subject)
    {
        this.subject = subject;
        return this;
    }

    public TokenJwtBuilder AddIssuer(string issuer)
    {
        this.issuer = issuer;
        return this;
    }

    public TokenJwtBuilder AddAudience(string audience)
    {
        this.audience = audience;
        return this;
    }

    public TokenJwtBuilder AddClaim(string type, string value)
    {
        this.claims.Add(type, value);
        return this;
    }

    public TokenJwtBuilder AddExpiresInMinutes(int expiresInMinutes)
    {
        this.expiresInMinutes = expiresInMinutes;
        return this;
    }

    private void EnsureArguments()
    {
        if (securityKey is null)
        {
            throw new ArgumentNullException(nameof(securityKey));
        }
        if (string.IsNullOrEmpty(subject))
        {
            throw new ArgumentNullException(nameof(subject));
        }
        if (string.IsNullOrEmpty(issuer))
        {
            throw new ArgumentNullException(nameof(issuer));
        }
        if (string.IsNullOrEmpty(audience))
        {
            throw new ArgumentNullException(nameof(audience));
        }
    }

    public TokenJwt Build()
    {
        EnsureArguments();

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, subject),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        }.Union(this.claims.Select(item => new Claim(item.Key, item.Value)));

        var token = new JwtSecurityToken(
            issuer,
            audience,
            claims,
            expires: DateTime.UtcNow.AddMinutes(expiresInMinutes),
            signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.Aes128CbcHmacSha256)
        );
               
        return new TokenJwt(token);
    }
}
