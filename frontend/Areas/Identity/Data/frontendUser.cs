using Microsoft.AspNetCore.Identity;

namespace frontend.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the frontendUser class
    public class frontendUser : IdentityUser
    {
        public string? Role { get; set; }
    }
}
