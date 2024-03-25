using QualaCore.Services.AuthAPI.AccesData.DTO;

namespace QualaCore.Services.AuthAPI.Domain.Interface
{
    public interface IAuthDomainBl
    {
        Task<LoginResponseDTO> LoginBl(LoginRequestDTO loginRequestDTO);
        Task<string> RegisterBl(RegisterRequestDTO registerRequestDTO);
        Task<bool> AssignRole(string email, string roleName);
    }
}
