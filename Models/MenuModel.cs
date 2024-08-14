namespace Restaurante.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Data;
    using Restaurante.Config;

    class MenuModel
    {
        public int MenuId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public bool Disponible { get; set; }

        List<MenuModel> listaMenus = new List<MenuModel>();
        private Conexion conexion = new Conexion();
        SqlCommand cmd = new SqlCommand();

        public List<MenuModel> todos()
        {
            string cadena = "SELECT * FROM Menus";
            SqlDataAdapter adapter = new SqlDataAdapter(cadena, conexion.AbrirConexion());
            DataTable tabla = new DataTable();
            adapter.Fill(tabla);
            foreach (DataRow menu in tabla.Rows)
            {
                MenuModel nuevoMenu = new MenuModel
                {
                    MenuId = Convert.ToInt32(menu["MenuId"]),
                    Nombre = menu["Nombre"].ToString(),
                    Descripcion = menu["Descripcion"].ToString(),
                    Precio = Convert.ToDecimal(menu["Precio"]),
                    Disponible = Convert.ToBoolean(menu["Disponible"])
                };
                listaMenus.Add(nuevoMenu);
            }

            conexion.CerrarConexion();
            return listaMenus;
        }

        public MenuModel uno(MenuModel menu)
        {
            string cadena = "SELECT * FROM Menus WHERE MenuId=" + menu.MenuId;
            cmd = new SqlCommand(cadena, conexion.AbrirConexion());
            SqlDataReader lector = cmd.ExecuteReader();

            lector.Read();
            MenuModel menuRegresa = new MenuModel
            {
                MenuId = Convert.ToInt32(lector["MenuId"]),
                Nombre = lector["Nombre"].ToString(),
                Descripcion = lector["Descripcion"].ToString(),
                Precio = Convert.ToDecimal(lector["Precio"]),
                Disponible = Convert.ToBoolean(lector["Disponible"])
            };
            conexion.CerrarConexion();
            return menuRegresa;
        }

        public string insertar(MenuModel menu)
        {
            try
            {
                cmd.Connection = conexion.AbrirConexion();
                cmd.CommandText = "INSERT INTO Menus(Nombre, Descripcion, Precio, Disponible) VALUES('" + menu.Nombre + "', '" + menu.Descripcion + "', " + menu.Precio + ", '" + menu.Disponible + "')";
                cmd.ExecuteNonQuery();
                return "ok";
            }
            catch (Exception ex)
            {
                return "error: " + ex.Message;
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }

        public string actualizar(MenuModel menu)
        {
            try
            {
                cmd.Connection = conexion.AbrirConexion();
                cmd.CommandText = "UPDATE Menus SET Nombre='" + menu.Nombre + "', Descripcion='" + menu.Descripcion + "', Precio=" + menu.Precio + ", Disponible='" + menu.Disponible + "' WHERE MenuId=" + menu.MenuId;
                cmd.ExecuteNonQuery();
                return "ok";
            }
            catch (Exception ex)
            {
                return "error: " + ex.Message;
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }

        public string eliminar(MenuModel menu)
        {
            try
            {
                cmd.Connection = conexion.AbrirConexion();
                cmd.CommandText = "DELETE FROM Menus WHERE MenuId=" + menu.MenuId;
                cmd.ExecuteNonQuery();
                return "ok";
            }
            catch (Exception ex)
            {
                return "error: " + ex.Message;
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }
    }
}
