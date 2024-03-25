using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QualaCore.Services.BranchOfficeAPI.AccessData.DTO
{
    public class BranchOfficeDTO
    {
        
        public int IdCodigo { get; set; }
        
        public string Descripcion { get; set; } = string.Empty;
        
        public string Direccion { get; set; } = string.Empty;
        
        public string Identificacion { get; set; } = string.Empty;
        
        public DateTime FechaDeCreacion { get; set; } = DateTime.Now;        
        public int MonedaId { get; set; }
    }
}
