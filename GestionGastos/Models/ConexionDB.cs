using System;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace GestionGastos.Models
{
    public class ConexionDB
    {
        // Cadena de conexión configurada para tu laptop
        private string cadenaConexion = @"Data Source=LAPTOP-NVUR2PBQ\SQLEXPRESS;Initial Catalog=control_gastos;Integrated Security=True";

        private SqlConnection conexion;

        public ConexionDB()
        {
            conexion = new SqlConnection(cadenaConexion);
        }

        // Método para probar la conexión
        public bool ProbarConexion()
        {
            try
            {
                conexion.Open();
                conexion.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al conectar: {ex.Message}");
            }
        }

        // Método para obtener la conexión
        public SqlConnection ObtenerConexion()
        {
            return conexion;
        }

        // Método para abrir conexión
        public void Abrir()
        {
            if (conexion.State == System.Data.ConnectionState.Closed)
            {
                conexion.Open();
            }
        }

        // Método para cerrar conexión
        public void Cerrar()
        {
            if (conexion.State == System.Data.ConnectionState.Open)
            {
                conexion.Close();
            }
        }
    }
}