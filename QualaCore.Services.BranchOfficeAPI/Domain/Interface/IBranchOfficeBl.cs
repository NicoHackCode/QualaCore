using QualaCore.Services.BranchOfficeAPI.AccessData.DTO;

namespace QualaCore.Services.BranchOfficeAPI.Domain.Interface
{
    public interface IBranchOfficeBl
    {
        Task<IEnumerable<BranchOfficeDTO>> GetAllBranchOfficesAsync();
        Task<BranchOfficeDTO> GetBranchOfficeByCodeAsync(int id);
        Task<BranchOfficeDTO> CreateBranchOfficeAsync(BranchOfficeDTO branchOfficeDTO);
        Task<BranchOfficeDTO> UpdateBranchOfficeAsync(int id, BranchOfficeDTO branchOfficeDTO);
        Task DeleteBranchOfficeAsync(int id);
    }
}
