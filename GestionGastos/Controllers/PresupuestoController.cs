using GestionGastos.Models;
using GestionGastos.Repositorios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestionGastos.Controllers
{
    public class PresupuestoController : Controller
    {
        private readonly IRepositorioReportes _repositorioReportes;

        public PresupuestoController(IRepositorioReportes repositorioReportes)
        {
            _repositorioReportes = repositorioReportes;
        }

        [HttpGet]
        public async Task<IActionResult> Verificar()
        {
            int idUsuarioPrueba = 1;
            var modelo = new PresupuestoViewModel();
            modelo.Categorias = await ObtenerListaCategorias();
            return View(modelo);
        }

        // --- ESTA ES LA ACCIÓN CORREGIDA ---
        [HttpPost]
        public async Task<IActionResult> Verificar(PresupuestoViewModel modelo)
        {
            int idUsuarioPrueba = 1;

            if (!ModelState.IsValid)
            {
                // Si el modelo no es válido, recargamos el dropdown y mostramos errores
                modelo.Categorias = await ObtenerListaCategorias();
                return View(modelo);
            }

            // Llamamos al SP
            var resultadoSP = await _repositorioReportes.VerificarPresupuesto(
                idUsuarioPrueba,
                modelo.Mes,
                modelo.Anio,
                modelo.CategoriaId
            );

            // --- INICIO DE LA MODIFICACIÓN (CON CHEQUEO DE NULL) ---
            if (resultadoSP != null)
            {
                modelo.Categoria = resultadoSP.Categoria;
                modelo.LimitePresupuesto = resultadoSP.LimitePresupuesto;
                modelo.Gastado = resultadoSP.Gastado;
                modelo.PorcentajeConsumido = resultadoSP.PorcentajeConsumido;
                modelo.Diferencia = resultadoSP.Diferencia;
            }
            else
            {
                modelo.Categoria = "Sin datos";
                modelo.PorcentajeConsumido = "No hay presupuesto definido para esta categoría/mes.";
                modelo.LimitePresupuesto = 0;
                modelo.Gastado = 0;
            }

            modelo.MostrarResultados = true;
            // --- FIN DE LA MODIFICACIÓN ---

            modelo.Categorias = await ObtenerListaCategorias();

            return View(modelo);
        }


        // --- Método privado para reutilizar la carga de categorías ---
        private async Task<IEnumerable<SelectListItem>> ObtenerListaCategorias()
        {
            var categorias = await _repositorioReportes.ObtenerCategoriasGasto();
            return categorias.Select(c => new SelectListItem(c.nombre, c.id_categoria.ToString()));
        }
    }
}