using BibTaller.Clases;
using BibTaller.Clases.EstrucPersona;
using Microsoft.AspNetCore.Mvc;
using TallerAutoWeb.Services;

namespace TallerAutoWeb.Controllers
{
    public class MecanicoController : Controller
    {
        private readonly Taller _taller;
        private readonly UserService _userService;

        public MecanicoController(Taller taller)
        {
            _taller = taller;
            _userService = new UserService(_taller);
        }



        // Métodos HttpGet


        [HttpGet]
        public IActionResult ListaMecanicos()
        {
            var mecanicos = _userService.MecanicosRegistrados();
            return View(mecanicos);
        }



        // Métodos HttpPost


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Mecanico/CrearMecanicoAjax")]
        public IActionResult CrearMecanicoAjax(ulong id, string nombre, ulong telefono, string especialidad)
        {
            try
            {
                var mecanico = new Mecanico(id, nombre, telefono, especialidad);

                string mensaje = _userService.RegistrarUsuario(mecanico);
                if (string.IsNullOrEmpty(mensaje))
                    mensaje = "Mecánico registrado correctamente.";

                return Json(new
                {
                    success = true,
                    mensajeEvento = mensaje,
                    mecanico = new
                    {
                        id = mecanico.Id,
                        nombre = mecanico.Nombre,
                        telefono = mecanico.Telefono,
                        especialidad = mecanico.Especialidad
                    }
                });
            }
            catch (Exception ex)
            {
                // Cualquier otro error inesperado
                return Json(new
                {
                    success = false,
                    mensajeEvento = $"Ocurrió un error inesperado: {ex.Message}"
                });
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Mecanico/ActualizarMecanicoAjax")]
        public IActionResult ActualizarMecanicoAjax(ulong id, string nombre, ulong telefono, string especialidad)
        {
            var mecanico = new Mecanico(id, nombre, telefono, especialidad);
            _userService.ActualizarUsuario(id, mecanico); // o método equivalente
            return Json(new
            {
                success = true,
                mecanico = new
                {
                    id = mecanico.Id,
                    nombre = mecanico.Nombre,
                    telefono = mecanico.Telefono,
                    especialidad = mecanico.Especialidad
                }
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Mecanico/EliminarMecanicoAjax")]
        public IActionResult EliminarMecanicoAjax(ulong id)
        {
            var mecanico = _userService.ObtenerPorId(id) as Mecanico;
            _userService.EliminarUsuario(mecanico);
            return Json(new { success = true, id });
        }
    }
}
