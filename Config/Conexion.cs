using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Using
using System.Data.SqlClient;
using System.Data;
namespace Restaurante.Config
{
    class Conexion
    {
        private SqlConnection conexion;

        public Conexion()
        {
            string cadenaConexion = "Server=DESKTOP-BUUJ9LF\\SQLEXPRESS01;Database=RestauranteDB;User Id=sa;Password=erick;";
            conexion = new SqlConnection(cadenaConexion);
        }

        public SqlConnection AbrirConexion()
        {
            if (conexion.State == System.Data.ConnectionState.Closed)
                conexion.Open();
            return conexion;
        }

        
        public void CerrarConexion()
        {
            if (conexion.State == System.Data.ConnectionState.Open)
                conexion.Close();
        }
    }
}
