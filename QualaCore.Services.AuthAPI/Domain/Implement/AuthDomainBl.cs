using Microsoft.AspNetCore.Identity;
using QualaCore.Services.AuthAPI.AccesData.DTO;
using QualaCore.Services.AuthAPI.AccesData.Implement;
using QualaCore.Services.AuthAPI.AccesData.Models;
using QualaCore.Services.AuthAPI.Domain.Interface;

namespace QualaCore.Services.AuthAPI.Domain.Implement
{
    public class AuthDomainBl: IAuthDomainBl
    {
        private readonly AuthDbContext _authDbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthDomainBl(AuthDbContext authDbContext, UserManager <ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            _authDbContext = authDbContext;
            _roleManager = roleManager;
            _userManager = userManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }



        public async Task<LoginResponseDTO> LoginBl(LoginRequestDTO loginRequestDTO)
        {
            var user = _authDbContext.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDTO.UserName.ToLower());

            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);

            if(user == null || isValid == false)
            {
                return new LoginResponseDTO() { User = null, Token ="" };
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenGenerator.GenerateToken(user,roles);

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
            catch(Exception ex)
            {
                
            }
            return "Error al registrar usuario";
        }

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
