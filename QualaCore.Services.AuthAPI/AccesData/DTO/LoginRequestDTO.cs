using System.Reflection.Metadata.Ecma335;

namespace QualaCore.Services.AuthAPI.AccesData.DTO
{
    public class LoginRequestDTO
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
