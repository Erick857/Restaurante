using Restaurante.Controllers;
using Restaurante.Models;
using System;
using System.Data;
using System.Windows.Forms;

namespace Restaurante
{
    public partial class frm_Menu : Form
    {
        public frm_Menu()
        {
            InitializeComponent();
        }

        private void frm_Menu_Load(object sender, EventArgs e)
        {
            LoadMenus(); 
        }

        private void LoadMenus()
        {
            try
            {
                MenusController controller = new MenusController();
                DataTable menus = controller.GetMenus(); 
                lst_Menu.Items.Clear(); 

                foreach (DataRow row in menus.Rows)
                {
                    lst_Menu.Items.Add($"{row["menu_id"]}: {row["Nombre"]} - {row["Precio"]} USD");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar menús: {ex.Message}");
            }
        }

        private void btn_agregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txt_nombre.Text) ||
                    string.IsNullOrWhiteSpace(txt_descripcion.Text) ||
                    !decimal.TryParse(txt_precio.Text, out decimal precio))
                {
                    MessageBox.Show("Por favor, completa todos los campos correctamente.");
                    return;
                }

                MenusController controller = new MenusController();
                MenuModel menu = new MenuModel
                {
                    Nombre = txt_nombre.Text,
                    Descripcion = txt_descripcion.Text,
                    Precio = precio
                };
                controller.agregar(menu); 
                LoadMenus();
                MessageBox.Show("Menú agregado con éxito.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar menú: {ex.Message}");
            }
        }

        private void btn_actualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txt_menu.Text) ||
                    !int.TryParse(txt_menu.Text, out int menuId) ||
                    string.IsNullOrWhiteSpace(txt_nombre.Text) ||
                    string.IsNullOrWhiteSpace(txt_descripcion.Text) ||
                    !decimal.TryParse(txt_precio.Text, out decimal precio))
                {
                    MessageBox.Show("Por favor, completa todos los campos correctamente.");
                    return;
                }

                MenusController controller = new MenusController();
                MenuModel menu = new MenuModel
                {
                    MenuId = menuId,
                    Nombre = txt_nombre.Text,
                    Descripcion = txt_descripcion.Text,
                    Precio = precio
                };
                controller.actualizar(menu); 
                LoadMenus();
                MessageBox.Show("Menú actualizado con éxito.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar menú: {ex.Message}");
            }
        }

        private void btn_eliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txt_menu.Text) ||
                    !int.TryParse(txt_menu.Text, out int menuId))
                {
                    MessageBox.Show("Por favor, seleccione un menú para eliminar.");
                    return;
                }

                var confirmResult = MessageBox.Show("¿Está seguro de que desea eliminar este menú?", "Confirmar eliminación", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    MenusController controller = new MenusController();
                    controller.eliminar(menuId); 
                    LoadMenus();
                    MessageBox.Show("Menú eliminado con éxito.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar menú: {ex.Message}");
            }
        }
    }
}
