namespace GestionGastos.Models
{
    // Esta clase recibe el resultado del SP "CalcularSaldoActual"
    public class SaldoViewModel
    {
        public string Usuario { get; set; }
        public decimal SaldoInicial { get; set; }
        public decimal TotalIngresos { get; set; }
        public decimal TotalGastos { get; set; }
        public decimal SaldoActual { get; set; }
    }
}