/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurante.Controllers
{
    class ClientesController
    {

    }
}*/
using Restaurante.Models;
using System.Collections.Generic;

namespace Restaurante.Controllers
{
    public class ClientesController
    {
        private ClientesModel modeloCliente = new ClientesModel();

        public string agregar(ClientesModel cliente)
        {
            return modeloCliente.insertar(cliente);
        }

        public string actualizar(ClientesModel cliente)
        {
            return modeloCliente.actualizar(cliente);
        }

        public string eliminar(ClientesModel cliente)
        {
            return modeloCliente.eliminar(cliente);
        }

        public List<ClientesModel> obtenerTodos()
        {
            return modeloCliente.todos(); 
        }
    }
}