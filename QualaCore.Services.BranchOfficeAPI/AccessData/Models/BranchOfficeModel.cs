using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QualaCore.Services.BranchOfficeAPI.AccessData.Models
{
    public class BranchOfficeModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCodigo { get; set; }

        [Required]
        [StringLength(500)]
        public string Descripcion { get; set; } = string.Empty;

        [Required]
        [StringLength(250)]
        public string Direccion { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Identificacion { get; set; } = string.Empty;

        [Required]
        public DateTime FechaDeCreacion { get; set; } = DateTime.Now;

        [Required]
        public int MonedaId { get; set; } 
        
        
    }
}
