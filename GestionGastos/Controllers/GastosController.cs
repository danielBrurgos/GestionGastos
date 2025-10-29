using GestionGastos.Models;
using GestionGastos.Repositorios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestionGastos.Controllers
{
    public class GastosController : Controller
    {
        private readonly IRepositorioGastos _repositorioGastos;

        public GastosController(IRepositorioGastos repositorioGastos)
        {
            _repositorioGastos = repositorioGastos;
        }

        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            var modelo = new GastoCreacionViewModel();
            modelo.Categorias = await ObtenerCategoriasSelect();
            modelo.TiposMovimiento = await ObtenerTiposMovimientoSelect();
            return View(modelo);
        }

        // --- CÓDIGO LIMPIO Y CORRECTO ---
        [HttpPost]
        public async Task<IActionResult> Crear(GastoCreacionViewModel modelo)
        {
            // Este 'if' ahora SÓLO revisará los campos reales
            if (!ModelState.IsValid)
            {
                modelo.Categorias = await ObtenerCategoriasSelect();
                modelo.TiposMovimiento = await ObtenerTiposMovimientoSelect();
                return View(modelo);
            }

            int idUsuarioPrueba = 1;

            var gasto = new Gasto
            {
                id_usuario = idUsuarioPrueba,
                id_categoria = modelo.id_categoria,
                id_tipoMovimiento = modelo.id_tipoMovimiento,
                Descripcion = modelo.Descripcion,
                Monto = modelo.Monto,
                MetodoPago = modelo.MetodoPago,
                fechaGasto = DateTime.Now
            };

            try
            {
                await _repositorioGastos.Crear(gasto);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Ocurrió un error al guardar: {ex.Message}");
                modelo.Categorias = await ObtenerCategoriasSelect();
                modelo.TiposMovimiento = await ObtenerTiposMovimientoSelect();
                return View(modelo);
            }

            return RedirectToAction("Index", "Home");
        }


        // --- Métodos privados ---
        private async Task<IEnumerable<SelectListItem>> ObtenerCategoriasSelect()
        {
            var categorias = await _repositorioGastos.ObtenerCategoriasGasto();
            return categorias.Select(c => new SelectListItem(c.nombre, c.id_categoria.ToString()));
        }

        private async Task<IEnumerable<SelectListItem>> ObtenerTiposMovimientoSelect()
        {
            var tipos = await _repositorioGastos.ObtenerTiposMovimiento();
            return tipos.Select(t => new SelectListItem(t.tipo_nombre, t.id_tipo.ToString()));
        }
    }
}