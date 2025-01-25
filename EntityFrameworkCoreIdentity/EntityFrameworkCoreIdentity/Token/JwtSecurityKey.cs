using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EntityFrameworkCoreIdentity.Token;

public class JwtSecurityKey
{
    public static SymmetricSecurityKey Create(string secret) => new(Encoding.ASCII.GetBytes(secret));
}
