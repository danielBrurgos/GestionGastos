using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GestionGastos.Models
{
    public class PresupuestoViewModel
    {
        // --- Propiedades para el FORMULARIO ---

        [Required(ErrorMessage = "El campo Mes es obligatorio")]
        [Range(1, 12, ErrorMessage = "El mes debe estar entre 1 y 12")]
        public int Mes { get; set; } = DateTime.Now.Month; // Valor por defecto

        [Required(ErrorMessage = "El campo Año es obligatorio")]
        [Range(2020, 2030, ErrorMessage = "El año debe ser válido")]
        public int Anio { get; set; } = DateTime.Now.Year; // Valor por defecto

        [Required(ErrorMessage = "Debe seleccionar una categoría")]
        [Display(Name = "Categoría")]
        public int CategoriaId { get; set; }

        // --- Propiedades para el RESULTADO del SP ---
        public string Categoria { get; set; }
        public decimal LimitePresupuesto { get; set; }
        public decimal Gastado { get; set; }
        public string PorcentajeConsumido { get; set; }
        public decimal Diferencia { get; set; }

        // --- Propiedad para el DropDownList ---
        public IEnumerable<SelectListItem> Categorias { get; set; }

        // --- Propiedad para saber si mostrar resultados ---
        public bool MostrarResultados { get; set; } = false;
    }
}