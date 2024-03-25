using AutoMapper;
using QualaCore.Services.BranchOfficeAPI.AccessData.DTO;
using QualaCore.Services.BranchOfficeAPI.AccessData.Interface;
using QualaCore.Services.BranchOfficeAPI.AccessData.Models;
using QualaCore.Services.BranchOfficeAPI.Domain.Interface;

namespace QualaCore.Services.BranchOfficeAPI.Domain.Implements
{
    public class BranchOfficeBl : IBranchOfficeBl
    {
        private readonly IBranchOfficeContext _branchOfficeContext;
        private readonly IMapper _mapper;

        public BranchOfficeBl(IBranchOfficeContext branchOfficeContext, IMapper mapper)
        {
            _branchOfficeContext = branchOfficeContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BranchOfficeDTO>> GetAllBranchOfficesAsync()
        {
            var branchOfficeModels = await _branchOfficeContext.GetAllBranchOfficesAsync();
            var branchOfficeDTOs = _mapper.Map<IEnumerable<BranchOfficeDTO>>(branchOfficeModels);

            return branchOfficeDTOs;
        }

        public async Task<BranchOfficeDTO> GetBranchOfficeByCodeAsync(int id)
        {
            var branchOfficeModel = await _branchOfficeContext.GetBranchOfficeByIdAsync(id);
            var branchOfficeDTO = _mapper.Map<BranchOfficeDTO>(branchOfficeModel);

            return branchOfficeDTO;
        }

        public async Task<BranchOfficeDTO> CreateBranchOfficeAsync(BranchOfficeDTO branchOfficeDTO)
        {
            var branchOffice = _mapper.Map<BranchOfficeModel>(branchOfficeDTO);
            var createdBranchOffice = await _branchOfficeContext.CreateBranchOfficeAsync(branchOffice);

            return _mapper.Map<BranchOfficeDTO>(createdBranchOffice);
        }

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
