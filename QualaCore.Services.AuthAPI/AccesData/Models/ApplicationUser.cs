using Microsoft.AspNetCore.Identity;

namespace QualaCore.Services.AuthAPI.AccesData.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string Name { get; set; }
    }
}
