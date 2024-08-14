using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Restaurante.Config;

namespace Restaurante.Models
{
    public class ClientesModel
    {
        public int cliente_id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }

        private Conexion conexion = new Conexion();
        SqlCommand cmd = new SqlCommand();

        // Método para obtener todos los clientes
        public List<ClientesModel> todos()
        {
            List<ClientesModel> listaClientes = new List<ClientesModel>();

            try
            {
                cmd.Connection = conexion.AbrirConexion();
                cmd.CommandText = "SELECT * FROM Clientes";
                SqlDataReader lector = cmd.ExecuteReader();

                while (lector.Read())
                {
                    ClientesModel cliente = new ClientesModel
                    {
                        cliente_id = Convert.ToInt32(lector["cliente_id"]), //aqui
                        Nombre = lector["Nombre"].ToString(),
                        Apellido = lector["Apellido"].ToString(),
                        Email = lector["Email"].ToString(),
                        Telefono = lector["Telefono"].ToString()
                    };
                    listaClientes.Add(cliente);
                }
                lector.Close();
            }
            catch (Exception ex)
            {
               
                throw new Exception("Error al obtener clientes: " + ex.Message);
            }
            finally
            {
                conexion.CerrarConexion();
            }

            return listaClientes;
        }

        public string insertar(ClientesModel cliente)
        {
            try
            {
                cmd.Connection = conexion.AbrirConexion();
                cmd.CommandText = "INSERT INTO Clientes (Nombre, Apellido, Email, Telefono) VALUES (@Nombre, @Apellido, @Email, @Telefono)";
                cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", cliente.Apellido);
                cmd.Parameters.AddWithValue("@Email", cliente.Email);
                cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono);
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

        public string actualizar(ClientesModel cliente)
        {
            try
            {
                cmd.Connection = conexion.AbrirConexion();
                cmd.CommandText = "UPDATE Clientes SET Nombre = @Nombre, Apellido = @Apellido, Email = @Email, Telefono = @Telefono WHERE cliente_id = @ClienteId"; // Cambia ClienteId por cliente_id
                cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", cliente.Apellido);
                cmd.Parameters.AddWithValue("@Email", cliente.Email);
                cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                cmd.Parameters.AddWithValue("@ClienteId", cliente.cliente_id); 
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

        public string eliminar(ClientesModel cliente)
        {
            try
            {
                cmd.Connection = conexion.AbrirConexion();
                cmd.CommandText = "DELETE FROM Clientes WHERE cliente_id = @ClienteId"; 
                cmd.Parameters.AddWithValue("@ClienteId", cliente.cliente_id); 
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
        public ClientesModel obtenerPorId(int clienteId)
        {
            ClientesModel cliente = null;

            try
            {
                cmd.Connection = conexion.AbrirConexion();
                cmd.CommandText = "SELECT * FROM Clientes WHERE cliente_id = @ClienteId";
                cmd.Parameters.AddWithValue("@ClienteId", clienteId);
                SqlDataReader lector = cmd.ExecuteReader();

                if (lector.Read())
                {
                    cliente = new ClientesModel
                    {
                        cliente_id = Convert.ToInt32(lector["cliente_id"]),
                        Nombre = lector["Nombre"].ToString(),
                        Apellido = lector["Apellido"].ToString(),
                        Email = lector["Email"].ToString(),
                        Telefono = lector["Telefono"].ToString()
                    };
                }
                lector.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener cliente: " + ex.Message);
            }
            finally
            {
                conexion.CerrarConexion();
            }

            return cliente;
        }
    }
}

