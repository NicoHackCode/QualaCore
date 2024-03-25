using AutoMapper;
using QualaCore.Services.BranchOfficeAPI.AccessData.DTO;
using QualaCore.Services.BranchOfficeAPI.AccessData.Models;

namespace QualaCore.Services.BranchOfficeAPI.Domain.Mappers
{
    public class MappingBranchOffice: Profile
    {
        public MappingBranchOffice()
        {
            CreateMap<BranchOfficeModel, BranchOfficeDTO>();            
            CreateMap<BranchOfficeDTO, BranchOfficeModel>();
        }
    }
}
