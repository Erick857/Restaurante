using Restaurante.Controllers;
using Restaurante.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Restaurante
{
    public partial class frm_Cliente : Form
    {
        private List<ClientesModel> clientesList; 

        public frm_Cliente()
        {
            InitializeComponent();
        }

        private void frm_Cliente_Load(object sender, EventArgs e)
        {
            LoadClientes();
        }

        private void LoadClientes()
        {
            try
            {
                ClientesController controller = new ClientesController();
                clientesList = controller.obtenerTodos(); 

                lst_Clientes.Items.Clear();

                foreach (var cliente in clientesList)
                {
                    lst_Clientes.Items.Add($"{cliente.cliente_id}: {cliente.Nombre} {cliente.Apellido} - {cliente.Email}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar clientes: {ex.Message}");
            }
        }

        private void lst_Clientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lst_Clientes.SelectedItem != null) 
            {
                
                int selectedIndex = lst_Clientes.SelectedIndex;

                ClientesModel cliente = clientesList[selectedIndex];
                
                txt_clienteId.Text = cliente.cliente_id.ToString();
                txt_nombre.Text = cliente.Nombre;
                txt_apellido.Text = cliente.Apellido;
                txt_email.Text = cliente.Email;
                txt_telefono.Text = cliente.Telefono; 
            }
        }

        private void btn_agregar_Click(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(txt_nombre.Text) ||
                    string.IsNullOrWhiteSpace(txt_apellido.Text) ||
                    string.IsNullOrWhiteSpace(txt_email.Text) ||
                    string.IsNullOrWhiteSpace(txt_telefono.Text)) 
                {
                    MessageBox.Show("Por favor, completa todos los campos correctamente.");
                    return; 
                }

                ClientesModel cliente = new ClientesModel
                {

                    Nombre = txt_nombre.Text,
                    Apellido = txt_apellido.Text,
                    Email = txt_email.Text,
                    Telefono = txt_telefono.Text 
                };

                ClientesController controller = new ClientesController();
                string result = controller.agregar(cliente);

                if (result == "ok")
                {
                    MessageBox.Show("Cliente agregado con éxito.");
                    LoadClientes(); 
                }
                else
                {
                    MessageBox.Show(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar cliente: {ex.Message}");
            }
        }

        private void btn_actualizar_Click(object sender, EventArgs e)
        {
            try
            {
             
                if (string.IsNullOrWhiteSpace(txt_nombre.Text) ||
                    string.IsNullOrWhiteSpace(txt_apellido.Text) ||
                    string.IsNullOrWhiteSpace(txt_email.Text) ||
                    string.IsNullOrWhiteSpace(txt_telefono.Text)) 
                {
                    MessageBox.Show("Por favor, completa todos los campos correctamente.");
                    return;
                }

                
                ClientesModel cliente = new ClientesModel
                {
                    cliente_id = Convert.ToInt32(txt_clienteId.Text), 
                    Nombre = txt_nombre.Text,
                    Apellido = txt_apellido.Text,
                    Email = txt_email.Text,
                    Telefono = txt_telefono.Text 
                };

                
                ClientesController controller = new ClientesController();
                string result = controller.actualizar(cliente);

                if (result == "ok")
                {
                    MessageBox.Show("Cliente actualizado con éxito.");
                    LoadClientes();
                }
                else
                {
                    MessageBox.Show(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar cliente: {ex.Message}");
            }
        }

        private void btn_eliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txt_clienteId.Text))
                {
                    MessageBox.Show("Por favor, ingresa el ID del cliente a eliminar.");
                    return;
                }

                
                ClientesModel cliente = new ClientesModel
                {
                    cliente_id = Convert.ToInt32(txt_clienteId.Text) 
                };

                
                ClientesController controller = new ClientesController();
                string result = controller.eliminar(cliente);

                if (result == "ok")
                {
                    MessageBox.Show("Cliente eliminado con éxito.");
                    LoadClientes(); 
                }
                else
                {
                    MessageBox.Show(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar cliente: {ex.Message}");
            }
        }
    }
}

