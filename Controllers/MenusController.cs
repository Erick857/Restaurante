using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Restaurante.Models;

namespace Restaurante.Controllers
{
    class MenusController
    {
        private string connectionString = "Server=DESKTOP-BUUJ9LF\\SQLEXPRESS01;Database=RestauranteDB;User Id=sa;Password=erick;";

        
        public DataTable GetMenus()
        {
            DataTable menus = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Menus", connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(menus);
                    }
                }
            }
            return menus;
        }

       
        public string agregar(MenuModel menu)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("INSERT INTO Menus (Nombre, Descripcion, Precio) VALUES (@Nombre, @Descripcion, @Precio)", connection))
                {
                    command.Parameters.AddWithValue("@Nombre", menu.Nombre);
                    command.Parameters.AddWithValue("@Descripcion", menu.Descripcion);
                    command.Parameters.AddWithValue("@Precio", menu.Precio);
                    command.ExecuteNonQuery();
                }
            }
            return "ok";
        }

        public string actualizar(MenuModel menu)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("UPDATE Menus SET Nombre=@Nombre, Descripcion=@Descripcion, Precio=@Precio WHERE menu_id=@MenuId", connection))
                {
                    command.Parameters.AddWithValue("@MenuId", menu.MenuId);
                    command.Parameters.AddWithValue("@Nombre", menu.Nombre);
                    command.Parameters.AddWithValue("@Descripcion", menu.Descripcion);
                    command.Parameters.AddWithValue("@Precio", menu.Precio);
                    command.ExecuteNonQuery();
                }
            }
            return "ok";
        }

        // Metodo para eliminar un menu
        public string eliminar(int menuId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("DELETE FROM Menus WHERE menu_id=@MenuId", connection))
                {
                    command.Parameters.AddWithValue("@MenuId", menuId);
                    command.ExecuteNonQuery();
                }
            }
            return "ok";
        }
    }
}
