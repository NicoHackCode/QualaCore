using QualaCore.Services.AuthAPI.AccesData.DTO;

namespace QualaCore.Services.AuthAPI.Domain.Interface
{
    /// <summary>
    /// Interfaz para la lógica de dominio de autenticación.
    /// </summary>
    public interface IAuthDomainBl
    {
        /// <summary>
        /// Realiza el inicio de sesión.
        /// </summary>
        /// <param name="loginRequestDTO">Los datos de inicio de sesión.</param>
        /// <returns>La respuesta del inicio de sesión.</returns>
        Task<LoginResponseDTO> LoginBl(LoginRequestDTO loginRequestDTO);

        /// <summary>
        /// Realiza el registro de un usuario.
        /// </summary>
        /// <param name="registerRequestDTO">Los datos de registro.</param>
        /// <returns>El resultado del registro.</returns>
        Task<string> RegisterBl(RegisterRequestDTO registerRequestDTO);

        /// <summary>
        /// Asigna un rol a un usuario.
        /// </summary>
        /// <param name="email">El correo electrónico del usuario.</param>
        /// <param name="roleName">El nombre del rol a asignar.</param>
        /// <returns>Un valor booleano que indica si se asignó el rol correctamente.</returns>
        Task<bool> AssignRole(string email, string roleName);
    }
}
