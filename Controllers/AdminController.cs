using BibTaller.Clases;
using BibTaller.Clases.EstrucPersona;
using Microsoft.AspNetCore.Mvc;
using TallerAutoWeb.Services;

namespace TallerAutoWeb.Controllers
{
    public class AdminController : Controller
    {
        private readonly Taller _taller;
        private readonly UserService _userService;

        public AdminController(Taller taller)
        {
            _taller = taller;
            _userService = new UserService(_taller);
        }

        // GET: lista de admins -> vista "ListaAdmins"
        [HttpGet]
        public IActionResult ListaAdmins()
        {
            var admins = _userService.AdminsRegistrados();
            return View("ListaAdmins", admins);
        }

        // POST AJAX: crear admin
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/CrearAdminAjax")]
        public IActionResult CrearAdminAjax(ulong id, string nombre, ulong telefono, string claveAcceso)
        {
            var admin = new Admin(id, nombre, telefono, claveAcceso);
            _userService.RegistrarUsuario(admin);

            return Json(new
            {
                success = true,
                admin = new
                {
                    id = admin.Id,
                    nombre = admin.Nombre,
                    telefono = admin.Telefono,
                    claveAcceso = admin.ClaveAcceso
                }
            });
        }

        // POST AJAX: actualizar admin
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/ActualizarAdminAjax")]
        public IActionResult ActualizarAdminAjax(ulong id, Admin newAdmin)
        {
            _userService.ActualizarUsuario(id, newAdmin);

            return Json(new
            {
                success = true,
                admin = new
                {
                    id = newAdmin.Id,
                    nombre = newAdmin.Nombre,
                    telefono = newAdmin.Telefono,
                    claveAcceso = newAdmin.ClaveAcceso
                }
            });
        }

        // POST AJAX: eliminar admin por id
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Admin/EliminarAdminAjax")]
        public IActionResult EliminarAdminAjax(ulong id)
        {
            var admin = _userService.ObtenerPorId(id) as Admin;
            _userService.EliminarUsuario(admin);
            return Json(new { success = true, id });
        }
    }
}