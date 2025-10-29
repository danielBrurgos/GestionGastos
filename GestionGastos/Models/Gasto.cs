using System;

namespace GestionGastos.Models
{
    public class Gasto
    {
        // Propiedades que usa el controlador para el INSERT
        public int id_usuario { get; set; }
        public int id_categoria { get; set; }
        public int id_tipoMovimiento { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }
        public DateTime fechaGasto { get; set; }
        public string MetodoPago { get; set; }

        // Propiedad Id (aunque no se usa en el 'Crear', es buena práctica)
        public int id_gasto { get; set; }
    }
}