using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QualaCore.Services.AuthAPI.AccesData.DTO;
using QualaCore.Services.AuthAPI.Domain.Interface;

namespace QualaCore.Services.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {

        private readonly IAuthDomainBl _authDomainBl;
        private ResponseDTO _response;

        public AuthAPIController(IAuthDomainBl authDomainBl)
        {
            _authDomainBl = authDomainBl;
            _response = new();
        }

        // Crear handler para registro de usuarios
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO userModel)
        {
            var errorMessage = await _authDomainBl.RegisterBl(userModel);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                _response.IsSuccess = false;
                _response.Message = errorMessage;
                return BadRequest(_response);
            }
            else
            {
                _response.IsSuccess = true;
                _response.Message = "Registro completado con éxito !";
                return Ok(_response);
            }            
        }

        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginRequestDTO modelUser)
        {
            var logginResponse = await _authDomainBl.LoginBl(modelUser);
            if (logginResponse.User == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Usuario y/o Contraseña Inválidos";
                return BadRequest(_response);
            }
            else
            {
                _response.IsSuccess = true;
                _response.Message = "Login Correcto";
                _response.Result = logginResponse;
                return Ok(_response);
            }
        }

        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegisterRequestDTO modelUser)
        {
            var assignRoleIsCorrect = await _authDomainBl.AssignRole(modelUser.Email, modelUser.Role.ToUpper());
            if (!assignRoleIsCorrect)
            {
                _response.IsSuccess = false;
                _response.Message = "No se pudo asignar el rol";
                return BadRequest(_response);
            }
            else
            {
                _response.IsSuccess = true;
                _response.Message = "El rol se asignó correctamente";
                _response.Result = assignRoleIsCorrect;
                return Ok(_response);
            }
        }
    }
}
