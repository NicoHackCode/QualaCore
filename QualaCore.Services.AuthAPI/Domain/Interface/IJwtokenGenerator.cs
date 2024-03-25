using QualaCore.Services.AuthAPI.AccesData.Models;

namespace QualaCore.Services.AuthAPI.Domain.Interface
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser, IEnumerable<string>roles);
    }
}
