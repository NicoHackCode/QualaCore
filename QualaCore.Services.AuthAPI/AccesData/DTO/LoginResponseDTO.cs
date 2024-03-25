namespace QualaCore.Services.AuthAPI.AccesData.DTO
{
    public class LoginResponseDTO
    {
        public required UserDTO User { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
