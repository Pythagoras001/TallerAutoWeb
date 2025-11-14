using BibTaller.Clases;
using BibTaller.Clases.EstrucPersona;
using Microsoft.AspNetCore.Mvc;
using TallerAutoWeb.Services;

namespace TallerAutoWeb.Controllers
{
    public class CLienteController : Controller
    {
        // Atributos
        private Taller _taller;
        private UserService _userService;

        // Constructor
        public CLienteController(Taller taller)
        {
            _taller = taller;
            _userService = new UserService(_taller);
        }

        // Métodos HttpGet
        [HttpGet]
        public IActionResult Clientes()
        {
            var clientes = _userService.ClientesRegistrados();
            return View("ListaClientes", clientes);
        }

        // Métodos HttpPost
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("User/CrearNuevoUsuarioAjax")]
        public IActionResult CrearNuevoUsuarioAjax(ulong id, string nombre, ulong telefono, bool tieneCredito)
        {
            try
            {
                // Validaciones básicas
                if (string.IsNullOrWhiteSpace(nombre))
                    throw new Exception("El nombre del usuario no puede estar vacío.");

                if (telefono == 0)
                    throw new Exception("Debe ingresar un número de teléfono válido.");

                // Intentar registrar el usuario
                var cliente = new Cliente(id, nombre, telefono, tieneCredito);
                string mensaje = _userService.RegistrarUsuario(cliente);

                // Respuesta exitosa en formato JSON
                return Json(new
                {
                    success = true,
                    mensajeEvento = mensaje,
                    cliente = new
                    {
                        id = cliente.Id,
                        nombre = cliente.Nombre,
                        telefono = cliente.Telefono,
                        saldo = cliente.SaldoADeber,
                        tieneSaldo = cliente.TieneSaldo
                    }
                });
            }
            catch (Exception ex)
            {
                // Registrar el error en el log o consola
                Console.WriteLine($"Error al crear usuario: {ex.Message}");

                // Enviar el mensaje de error al cliente AJAX
                return Json(new
                {
                    success = false,
                    mensajeError = $"❌ Error al crear el usuario: {ex.Message}"
                });
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("User/ActualizarClienteAjax")]
        public IActionResult ActualizarClienteAjax(ulong id, string nombre, ulong telefono, bool tieneCredito)
        {
            // Actualiza en el servicio/repo
            var cliente = new Cliente(id, nombre, telefono, tieneCredito);
            // Si tu UserService tiene otro método de actualización, ajústalo
            _userService.ActualizarUsuario(id, cliente);

            return Json(new
            {
                success = true,
                cliente = new
                {
                    id = cliente.Id,
                    nombre = cliente.Nombre,
                    telefono = cliente.Telefono,
                    saldo = cliente.SaldoADeber,
                    tieneSaldo = cliente.TieneSaldo
                }
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("User/EliminarClienteAjax")]
        public IActionResult EliminarClienteAjax(ulong id)
        {
            var cliente = _userService.ObtenerPorId(id) as Cliente;

            // Elimina del servicio/repo
            string mensaje = _userService.EliminarUsuario(cliente);

            ViewBag.MensajeEvento = mensaje;

            return Json(new { success = true, cliente });
        }
    }
}
