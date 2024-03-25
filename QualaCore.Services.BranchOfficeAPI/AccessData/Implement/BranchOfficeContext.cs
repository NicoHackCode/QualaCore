using Microsoft.EntityFrameworkCore;
using QualaCore.Services.BranchOfficeAPI.AccessData.Interface;
using QualaCore.Services.BranchOfficeAPI.AccessData.Models;

namespace QualaCore.Services.BranchOfficeAPI.AccessData.Implement
{
    public class BranchOfficeContext : DbContext, IBranchOfficeContext
    {
        public BranchOfficeContext(DbContextOptions<BranchOfficeContext> options) : base(options)
        {

        }

        public DbSet<BranchOfficeModel> BranchOffices { get; set; }

        public async Task<List<BranchOfficeModel>> GetAllBranchOfficesAsync()
        {
            return await BranchOffices.ToListAsync();
        }

        public async Task<BranchOfficeModel?> GetBranchOfficeByIdAsync(int id)
        {
            return await BranchOffices.FirstOrDefaultAsync(r => r.IdCodigo == id);
        }

        public async Task<BranchOfficeModel> CreateBranchOfficeAsync(BranchOfficeModel branchOffice)
        {
            await BranchOffices.AddAsync(branchOffice);
            await SaveChangesAsync();
            return branchOffice;
        }

        public async Task<BranchOfficeModel> UpdateBranchOfficeAsync(BranchOfficeModel branchOffice)
        {
            BranchOffices.Update(branchOffice);
            await SaveChangesAsync();
            return branchOffice;
        }

        public async Task DeleteBranchOfficeAsync(BranchOfficeModel branchOffice)
        {
            BranchOffices.Remove(branchOffice);
            await SaveChangesAsync();
        }
    }
}
