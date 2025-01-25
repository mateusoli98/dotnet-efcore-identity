using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkCoreIdentity.Entities;

public class ApplicationUser : IdentityUser
{
    [Column("USER_RG")]  
    public string RG { get; set; } = string.Empty;
}
