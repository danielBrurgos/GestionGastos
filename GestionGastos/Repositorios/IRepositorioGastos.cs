using GestionGastos.Models;

namespace GestionGastos.Repositorios
{
    public interface IRepositorioGastos
    {
        // --- Métodos para el formulario de Crear ---
        Task<IEnumerable<Categoria>> ObtenerCategoriasGasto();
        Task<IEnumerable<tipoMovimiento>> ObtenerTiposMovimiento();
        Task Crear(Gasto gasto);
    }
}