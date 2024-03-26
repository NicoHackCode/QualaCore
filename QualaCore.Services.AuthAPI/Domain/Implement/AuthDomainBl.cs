using Microsoft.AspNetCore.Identity;
using QualaCore.Services.AuthAPI.AccesData.DTO;
using QualaCore.Services.AuthAPI.AccesData.Implement;
using QualaCore.Services.AuthAPI.AccesData.Models;
using QualaCore.Services.AuthAPI.Domain.Interface;

namespace QualaCore.Services.AuthAPI.Domain.Implement
{
    /// <summary>
    /// Clase de implementación de la lógica de negocio de autenticación.
    /// </summary>
    public class AuthDomainBl : IAuthDomainBl
    {
        private readonly AuthDbContext _authDbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        /// <summary>
        /// Constructor de la clase AuthDomainBl.
        /// </summary>
        /// <param name="authDbContext">Contexto de base de datos de autenticación.</param>
        /// <param name="userManager">Administrador de usuarios.</param>
        /// <param name="roleManager">Administrador de roles.</param>
        /// <param name="jwtTokenGenerator">Generador de tokens JWT.</param>
        public AuthDomainBl(AuthDbContext authDbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            _authDbContext = authDbContext;
            _roleManager = roleManager;
            _userManager = userManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        /// <summary>
        /// Método para realizar el inicio de sesión.
        /// </summary>
        /// <param name="loginRequestDTO">Datos de solicitud de inicio de sesión.</param>
        /// <returns>Objeto LoginResponseDTO que contiene el usuario y el token de acceso.</returns>
        public async Task<LoginResponseDTO> LoginBl(LoginRequestDTO loginRequestDTO)
        {
            var user = _authDbContext.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDTO.UserName.ToLower());

            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);

            if (user == null || isValid == false)
            {
                return new LoginResponseDTO() { User = null, Token = "" };
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenGenerator.GenerateToken(user, roles);

            UserDTO userDTO = new UserDTO()
            {
                Email = user.Email,
                ID = user.Id,
                PhoneNumber = user.PhoneNumber,
                Name = user.Name,
            };

            LoginResponseDTO loginResponseDTO = new LoginResponseDTO()
            {
                User = userDTO,
                Token = token
            };

            return loginResponseDTO;
        }

        /// <summary>
        /// Método para registrar un nuevo usuario.
        /// </summary>
        /// <param name="registerRequestDTO">Datos de solicitud de registro.</param>
        /// <returns>Un mensaje de error en caso de fallo, o una cadena vacía en caso de éxito.</returns>
        public async Task<string> RegisterBl(RegisterRequestDTO registerRequestDTO)
        {
            ApplicationUser user = new()
            {
                UserName = registerRequestDTO.Email,
                Email = registerRequestDTO.Email,
                NormalizedEmail = registerRequestDTO.Email.ToUpper(),
                Name = registerRequestDTO.Name,
                PhoneNumber = registerRequestDTO.PhoneNumber
            };

            try
            {
                var result = await _userManager.CreateAsync(user, registerRequestDTO.Password);
                if (result.Succeeded)
                {
                    var userToReturn = _authDbContext.ApplicationUsers.First(r => r.UserName == registerRequestDTO.Email);
                    UserDTO userDTO = new()
                    {
                        Email = userToReturn.Email,
                        ID = userToReturn.Id,
                        Name = userToReturn.Name,
                        PhoneNumber = userToReturn.PhoneNumber
                    };

                    return "";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception ex)
            {

            }
            return "Error al registrar usuario";
        }

        /// <summary>
        /// Método para asignar un rol a un usuario.
        /// </summary>
        /// <param name="email">Correo electrónico del usuario.</param>
        /// <param name="roleName">Nombre del rol a asignar.</param>
        /// <returns>true si se asignó el rol correctamente, false en caso contrario.</returns>
        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = _authDbContext.ApplicationUsers.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
            if (user != null)
            {
                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }

                await _userManager.AddToRoleAsync(user, roleName);
                return true;
            }

            return false;
        }
    }
}
