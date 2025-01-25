using System.IdentityModel.Tokens.Jwt;

namespace EntityFrameworkCoreIdentity.Token;

public class TokenJwt
{
    private readonly JwtSecurityToken token;

    internal TokenJwt(JwtSecurityToken token)
    {
        this.token = token;
        this.value = new JwtSecurityTokenHandler().WriteToken(token);
    }

    public DateTime Valid => token.ValidTo;
    public string value { get; }
}
