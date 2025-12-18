using Microsoft.AspNetCore.Identity;

namespace BookStore.Models
{
    public class AppUserModel : IdentityUser
    {
        public string? FullName { get; set; }
    }
}
