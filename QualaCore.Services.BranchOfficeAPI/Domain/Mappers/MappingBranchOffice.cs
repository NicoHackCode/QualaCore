using AutoMapper;
using QualaCore.Services.BranchOfficeAPI.AccessData.DTO;
using QualaCore.Services.BranchOfficeAPI.AccessData.Models;

namespace QualaCore.Services.BranchOfficeAPI.Domain.Mappers
{
    /// <summary>
    /// Clase que se encarga de mapear entre el modelo de sucursal y el DTO de sucursal.
    /// </summary>
    public class MappingBranchOffice : Profile
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="MappingBranchOffice"/>.
        /// </summary>
        public MappingBranchOffice()
        {
            CreateMap<BranchOfficeModel, BranchOfficeDTO>();
            CreateMap<BranchOfficeDTO, BranchOfficeModel>();
        }
    }
}
