using Microsoft.AspNetCore.Identity;

namespace frontend.Areas.Identity.Data
{
   
    public class frontendUser : IdentityUser
    {
        public string? Role { get; set; }
    }
}
