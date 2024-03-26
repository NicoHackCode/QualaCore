using QualaCore.Services.BranchOfficeAPI.AccessData.DTO;

namespace QualaCore.Services.BranchOfficeAPI.Domain.Interface
{
    /// <summary>
    /// Interfaz para el negocio de sucursales.
    /// </summary>
    public interface IBranchOfficeBl
    {
        /// <summary>
        /// Obtiene todas las sucursales de forma asincrónica.
        /// </summary>
        /// <returns>Una tarea que representa la operación asincrónica. El resultado contiene una colección de objetos BranchOfficeDTO.</returns>
        Task<IEnumerable<BranchOfficeDTO>> GetAllBranchOfficesAsync();

        /// <summary>
        /// Obtiene una sucursal por su código de forma asincrónica.
        /// </summary>
        /// <param name="id">El código de la sucursal.</param>
        /// <returns>Una tarea que representa la operación asincrónica. El resultado contiene un objeto BranchOfficeDTO.</returns>
        Task<BranchOfficeDTO> GetBranchOfficeByCodeAsync(int id);

        /// <summary>
        /// Crea una nueva sucursal de forma asincrónica.
        /// </summary>
        /// <param name="branchOfficeDTO">El objeto BranchOfficeDTO que representa la sucursal a crear.</param>
        /// <returns>Una tarea que representa la operación asincrónica. El resultado contiene el objeto BranchOfficeDTO creado.</returns>
        Task<BranchOfficeDTO> CreateBranchOfficeAsync(BranchOfficeDTO branchOfficeDTO);

        /// <summary>
        /// Actualiza una sucursal existente de forma asincrónica.
        /// </summary>
        /// <param name="id">El código de la sucursal a actualizar.</param>
        /// <param name="branchOfficeDTO">El objeto BranchOfficeDTO que contiene los datos actualizados de la sucursal.</param>
        /// <returns>Una tarea que representa la operación asincrónica. El resultado contiene el objeto BranchOfficeDTO actualizado.</returns>
        Task<BranchOfficeDTO> UpdateBranchOfficeAsync(int id, BranchOfficeDTO branchOfficeDTO);

        /// <summary>
        /// Elimina una sucursal de forma asincrónica.
        /// </summary>
        /// <param name="id">El código de la sucursal a eliminar.</param>
        /// <returns>Una tarea que representa la operación asincrónica.</returns>
        Task DeleteBranchOfficeAsync(int id);
    }
}
