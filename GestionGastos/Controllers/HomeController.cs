using GestionGastos.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using GestionGastos.Repositorios; // <-- IMPORTANTE

namespace GestionGastos.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepositorioReportes _repositorioReportes; // <-- AGREGADO

        // Modificado para inyectar el repositorio
        public HomeController(ILogger<HomeController> logger, IRepositorioReportes repositorioReportes)
        {
            _logger = logger;
            _repositorioReportes = repositorioReportes; // <-- AGREGADO
        }

        // Modificado para ser async y llamar al SP
        public async Task<IActionResult> Index()
        {
            int idUsuarioPrueba = 1;

            var saldoVM = await _repositorioReportes.ObtenerSaldoActual(idUsuarioPrueba);

            return View(saldoVM);
        }

        // Tu acción de ProbarConexion (si aún la tienes)
        public IActionResult ProbarConexion()
        {
            try
            {
                ConexionDB conexion = new ConexionDB();
                bool resultado = conexion.ProbarConexion();

                if (resultado)
                {
                    ViewBag.Mensaje = "✓ Conexión exitosa a la base de datos 'control_gastos'";
                    ViewBag.TipoMensaje = "success";
                }
                else
                {
                    ViewBag.Mensaje = "✗ Error al conectar";
                    ViewBag.TipoMensaje = "danger";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = $"✗ Error: {ex.Message}";
                ViewBag.TipoMensaje = "danger";
            }

            // Redirige al Index, que ahora recargará los datos del saldo
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}