using Microsoft.EntityFrameworkCore;
using QualaCore.Services.BranchOfficeAPI.AccessData.Models;

namespace QualaCore.Services.BranchOfficeAPI.AccessData.Interface
{
    public interface IBranchOfficeContext
    {
        Task<List<BranchOfficeModel>> GetAllBranchOfficesAsync();
        Task<BranchOfficeModel?> GetBranchOfficeByIdAsync(int id);
        Task<BranchOfficeModel> CreateBranchOfficeAsync(BranchOfficeModel branchOffice);
        Task<BranchOfficeModel> UpdateBranchOfficeAsync(BranchOfficeModel branchOffice);
        Task DeleteBranchOfficeAsync(BranchOfficeModel branchOffice);

    }
}
