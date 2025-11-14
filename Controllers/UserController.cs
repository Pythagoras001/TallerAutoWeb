using BibTaller.Clases;
using BibTaller.Clases.EstrucPersona;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TallerAutoWeb.Services;

namespace TallerAutoWeb.Controllers
{
    public class UserController : Controller
    {

        // Atributos
        private Taller _taller;
        private UserService _userService;

        // Constructor
        public UserController(Taller taller)
        {
            _taller = taller;
            _userService = new UserService(_taller);
        }

        // Métodos HttpGet
        [HttpGet]
        public IActionResult Index()
        {
            var personas = _userService.ObtenerTodas();
            return View(personas);
        }

        public IActionResult Clientes()
        {
            var clientes = _userService.ClientesRegistrados();
            return View("ListaClientes", clientes);
        }

        public IActionResult Mecanicos()
        {
            var personas = _userService.MecanicosRegistrados();
            return View("ListaMecanicos", personas);
        }

        public IActionResult Admins()
        {
            var personas = _userService.AdminsRegistrados();
            return View("ListaAdmins", personas);
        }

        // Métodos HttpPost (por agregar)
    }
}
