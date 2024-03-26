using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QualaCore.Services.BranchOfficeAPI.AccessData.DTO;
using QualaCore.Services.BranchOfficeAPI.Domain.Interface;
using static Azure.Core.HttpHeader;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QualaCore.Services.BranchOfficeAPI.Controllers
{
    /// <summary>
    /// Controlador para las operaciones relacionadas con las sucursales.
    /// </summary>
    [Route("api/branchOffice")]
    [ApiController]
    [Authorize]
    public class BranchOffice : ControllerBase
    {
        private readonly IBranchOfficeBl _blBranchOfficce;
        private ResponseDTO _response;

        /// <summary>
        /// Constructor de la clase BranchOffice.
        /// </summary>
        /// <param name="blBranchOffice">Instancia de la interfaz IBranchOfficeBl.</param>
        public BranchOffice(IBranchOfficeBl blBranchOffice)
        {
            _blBranchOfficce = blBranchOffice;
            _response = new ResponseDTO();
        }

        /// <summary>
        /// Obtiene todas las sucursales.
        /// </summary>
        /// <returns>Objeto ResponseDTO que contiene la lista de sucursales.</returns>
        [HttpGet]
        [Route("getallbranchoffice")]
        public async Task<ResponseDTO> GetAllBranchOfficces()
        {
            try
            {
                var branchOffices = await _blBranchOfficce.GetAllBranchOfficesAsync();

                // Si la lista está vacía, devuelve una respuesta 200 con una lista vacía.
                if (!branchOffices.Any())
                {
                    _response.Result = Enumerable.Empty<BranchOfficeDTO>();
                    _response.IsSuccess = true;
                    _response.Message = "No se encontraron sucursales";

                }

                // Si hay sucursales, las devuelve.
                _response.Result = branchOffices;
                _response.IsSuccess = true;
                _response.Message = "Sucursales encontradas";

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message.ToString();
            }

            return _response;
        }

        /// <summary>
        /// Obtiene una sucursal por su código.
        /// </summary>
        /// <param name="id">Código de la sucursal.</param>
        /// <returns>Objeto ResponseDTO que contiene la sucursal encontrada.</returns>
        [HttpGet]
        [Route("getbranchofficeid/{id:int}")]
        public async Task<ResponseDTO> GetBranchOfficeByCode(int id)
        {
            try
            {
                var branchOffice = await _blBranchOfficce.GetBranchOfficeByCodeAsync(id);

                if (branchOffice == null)
                {
                    _response.Result = null;
                    _response.IsSuccess = false;
                    _response.Message = "No se encontró la sucursal";
                }
                else
                {
                    _response.Result = branchOffice;
                    _response.IsSuccess = true;
                    _response.Message = "Sucursal encontrada";
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message.ToString();
            }

            return _response;
        }

        /// <summary>
        /// Crea una nueva sucursal.
        /// </summary>
        /// <param name="branchOfficeDTO">Datos de la sucursal a crear.</param>
        /// <returns>Objeto ResponseDTO que indica el resultado de la operación.</returns>
        [HttpPost]
        [Route("insertnewbranchoffice")]
        public async Task<ResponseDTO> CreateBranchOffice([FromBody] BranchOfficeDTO branchOfficeDTO)
        {

            if (!ModelState.IsValid)
            {

                _response.IsSuccess = false;
                _response.Message = "Datos Inválidos";
                _response.Result = ModelState.Values.SelectMany(v => v.Errors.Select(b => b.ErrorMessage)).ToString();

                return _response;

            }

            try
            {
                var createdBranchOffice = await _blBranchOfficce.CreateBranchOfficeAsync(branchOfficeDTO);
                if (createdBranchOffice == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "No se pudo crear la sucursal.";
                    return _response;
                }

                _response.Result = createdBranchOffice;
                _response.IsSuccess = true;
                _response.Message = "Sucursal creada exitosamente.";

                return _response;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = "Error al crear la sucursal: " + ex.Message;

                return _response;
            }
        }

        /// <summary>
        /// Elimina una sucursal por su código.
        /// </summary>
        /// <param name="id">Código de la sucursal a eliminar.</param>
        /// <returns>Objeto ResponseDTO que indica el resultado de la operación.</returns>
        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ResponseDTO> DeleteBranchOffice(int id)
        {
            try
            {
                await _blBranchOfficce.DeleteBranchOfficeAsync(id);

                _response.IsSuccess = true;
                _response.Message = "Sucursal eliminada exitosamente.";
            }
            catch (KeyNotFoundException ex)
            {
                _response.IsSuccess = false;
                _response.Message = "No se encontró la sucursal a eliminar.";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = "Error al eliminar la sucursal: " + ex.Message;
            }

            return _response;
        }

        /// <summary>
        /// Actualiza una sucursal por su código.
        /// </summary>
        /// <param name="id">Código de la sucursal a actualizar.</param>
        /// <param name="branchOfficeDTO">Datos de la sucursal actualizada.</param>
        /// <returns>Objeto ResponseDTO que indica el resultado de la operación.</returns>
        [HttpPut]
        [Route("{id:int}")]
        public async Task<ResponseDTO> UpdateBranchOffice(int id, [FromBody] BranchOfficeDTO branchOfficeDTO)
        {
            if (!ModelState.IsValid)
            {
                _response.IsSuccess = false;
                _response.Message = "Datos Inválidos";
                _response.Result = ModelState.Values.SelectMany(v => v.Errors.Select(b => b.ErrorMessage)).ToString();

                return _response;
            }

            try
            {
                var updatedBranchOffice = await _blBranchOfficce.UpdateBranchOfficeAsync(id, branchOfficeDTO);
                if (updatedBranchOffice == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "No se pudo actualizar la sucursal.";
                    return _response;
                }

                _response.Result = updatedBranchOffice;
                _response.IsSuccess = true;
                _response.Message = "Sucursal actualizada exitosamente.";

                return _response;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = "Error al actualizar la sucursal: " + ex.Message;

                return _response;
            }
        }
    }
}
