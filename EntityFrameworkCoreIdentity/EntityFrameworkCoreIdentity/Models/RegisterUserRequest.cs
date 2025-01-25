namespace EntityFrameworkCoreIdentity.Models;

public class RegisterUserRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string RG { get; set; } = string.Empty;
}
