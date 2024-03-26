using AutoMapper;
using QualaCore.Services.BranchOfficeAPI.AccessData.DTO;
using QualaCore.Services.BranchOfficeAPI.AccessData.Interface;
using QualaCore.Services.BranchOfficeAPI.AccessData.Models;
using QualaCore.Services.BranchOfficeAPI.Domain.Interface;

namespace QualaCore.Services.BranchOfficeAPI.Domain.Implements
{
    /// <summary>
    /// Implementación de la lógica de negocio de la sucursal.
    /// </summary>
    public class BranchOfficeBl : IBranchOfficeBl
    {
        private readonly IBranchOfficeContext _branchOfficeContext;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor de la clase BranchOfficeBl.
        /// </summary>
        /// <param name="branchOfficeContext">Contexto de la sucursal.</param>
        /// <param name="mapper">Instancia de IMapper para mapear objetos.</param>
        public BranchOfficeBl(IBranchOfficeContext branchOfficeContext, IMapper mapper)
        {
            _branchOfficeContext = branchOfficeContext;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtiene todas las sucursales de forma asíncrona.
        /// </summary>
        /// <returns>Una colección de objetos BranchOfficeDTO.</returns>
        public async Task<IEnumerable<BranchOfficeDTO>> GetAllBranchOfficesAsync()
        {
            var branchOfficeModels = await _branchOfficeContext.GetAllBranchOfficesAsync();
            var branchOfficeDTOs = _mapper.Map<IEnumerable<BranchOfficeDTO>>(branchOfficeModels);

            return branchOfficeDTOs;
        }

        /// <summary>
        /// Obtiene una sucursal por su código de forma asíncrona.
        /// </summary>
        /// <param name="id">El código de la sucursal.</param>
        /// <returns>Un objeto BranchOfficeDTO.</returns>
        public async Task<BranchOfficeDTO> GetBranchOfficeByCodeAsync(int id)
        {
            var branchOfficeModel = await _branchOfficeContext.GetBranchOfficeByIdAsync(id);
            var branchOfficeDTO = _mapper.Map<BranchOfficeDTO>(branchOfficeModel);

            return branchOfficeDTO;
        }

        /// <summary>
        /// Crea una nueva sucursal de forma asíncrona.
        /// </summary>
        /// <param name="branchOfficeDTO">El objeto BranchOfficeDTO que representa la sucursal a crear.</param>
        /// <returns>El objeto BranchOfficeDTO creado.</returns>
        public async Task<BranchOfficeDTO> CreateBranchOfficeAsync(BranchOfficeDTO branchOfficeDTO)
        {
            var branchOffice = _mapper.Map<BranchOfficeModel>(branchOfficeDTO);
            var createdBranchOffice = await _branchOfficeContext.CreateBranchOfficeAsync(branchOffice);

            return _mapper.Map<BranchOfficeDTO>(createdBranchOffice);
        }

        /// <summary>
        /// Actualiza una sucursal existente de forma asíncrona.
        /// </summary>
        /// <param name="id">El código de la sucursal a actualizar.</param>
        /// <param name="branchOfficeDTO">El objeto BranchOfficeDTO que contiene los datos actualizados de la sucursal.</param>
        /// <returns>El objeto BranchOfficeDTO actualizado.</returns>
        public async Task<BranchOfficeDTO> UpdateBranchOfficeAsync(int id, BranchOfficeDTO branchOfficeDTO)
        {
            var branchOfficeModel = await _branchOfficeContext.GetBranchOfficeByIdAsync(id);
            if (branchOfficeModel == null)
            {
                // Log de errores
                throw new KeyNotFoundException("Branch office not found");
            }

            branchOfficeModel.Descripcion = branchOfficeDTO.Descripcion;
            branchOfficeModel.Direccion = branchOfficeDTO.Direccion;
            branchOfficeModel.Identificacion = branchOfficeDTO.Identificacion;
            branchOfficeModel.MonedaId = branchOfficeDTO.MonedaId;

            var updatedBranchOffice = await _branchOfficeContext.UpdateBranchOfficeAsync(branchOfficeModel);

            return _mapper.Map<BranchOfficeDTO>(updatedBranchOffice);
        }

        /// <summary>
        /// Elimina una sucursal existente de forma asíncrona.
        /// </summary>
        /// <param name="id">El código de la sucursal a eliminar.</param>
        public async Task DeleteBranchOfficeAsync(int id)
        {
            var branchOfficeModel = await _branchOfficeContext.GetBranchOfficeByIdAsync(id);
            if (branchOfficeModel == null)
            {
                throw new KeyNotFoundException("Branch office not found");
            }

            await _branchOfficeContext.DeleteBranchOfficeAsync(branchOfficeModel);
        }
    }
}
