using BibTaller.Clases;
using BibTaller.Clases.EstrucPersona;
using BibTaller.Clases.EstrucReparcion;
using BibTaller.Clases.EstrucVehiculos;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using TallerAutoWeb.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TallerAutoWeb.Controllers  // Asegúrate que el namespace sea correcto
{
    public class OrdenController : Controller  // Debe heredar de Controller
    {
        private Taller _taller;
        private OrdenService _ordenService;
        private UserService _usuarioService;

        public OrdenController(Taller taller)
        {
            _taller = taller;
            _ordenService = new OrdenService(taller);
            _usuarioService = new UserService(taller);
        }

        // ---------------------- MÉTODOS GET ----------------------

        public IActionResult Index()
        {
            ViewBag.Ordenes = _ordenService.ObtenerTodas();
            ViewBag.Clientes = _usuarioService.ClientesRegistrados();
            ViewBag.EstadosVehiculo = Enum.GetNames(typeof(OrdenReparacion.estadoOrden)).ToList();

            return View();
        }

        public IActionResult Crear()
        {
            ViewBag.TiposReparacion = new List<string> {
                "Cambio de Aceite",
                "Cambio de Bujías",
                "Puesta a Punto"
            };

            ViewBag.Clientes = _usuarioService.ClientesRegistrados();
            ViewBag.Mecanicos = _usuarioService.MecanicosRegistrados();
            ViewBag.TiposVehiculo = new List<string> { "Hibrido", "Electrico", "Carburado" };
            ViewBag.MensajeError = TempData["MensajeError"];
            return View();
        }

        public IActionResult AsignarRespusto(ulong id)
        {
            var orden = _ordenService.ObtenerPorId(id);

            ViewBag.RespuestosAplicados = orden.Reparacion.RepuestosUtilizados;
            ViewBag.IdOrden = id;

            ViewBag.MensajeEvento = TempData["MensajeEvento"];
            ViewBag.MensajeError = TempData["MensajeError"];

            return View("VistaRepuesto");
        }

        public IActionResult AsigMecanicos(ulong id)
        {
            var orden = _ordenService.ObtenerPorId(id);
            ViewBag.MecanicosEncargados = orden.Reparacion.MecanicosEncargados;
            ViewBag.IdOrden = id;
            ViewBag.Mecanicos = _usuarioService.MecanicosRegistrados();

            ViewBag.MensajeError = TempData["MensajeError"];
            ViewBag.MensajeEvento = TempData["MensajeEvento"];

            return View("VistaAsigMecanico");
        }

        public IActionResult ProcesarOrden(ulong id)
        {
            ViewBag.Orden = _ordenService.ObtenerPorId(id);
            
            ViewBag.MensajeEvento = TempData["MensajeEvento"];

            return View("ProcesamientoOrden");
        }

        public IActionResult ActualizarEstado(ulong IdOrden, string NuevoEstado)
        {
            
            string mensaje = "";

            if (NuevoEstado == "Pendiente Por Pago")
                mensaje = _ordenService.RepararOrden(_ordenService.ObtenerPorId(IdOrden));
            else if (NuevoEstado == "Listo Para Entrega")
                mensaje = _ordenService.PagarOrden(_ordenService.ObtenerPorId(IdOrden));
            else if (NuevoEstado == "Completado")
                mensaje = _ordenService.EntregarVehiculo(_ordenService.ObtenerPorId(IdOrden));

            TempData["MensajeEvento"] = mensaje;
            return RedirectToAction("ProcesarOrden", new { id = IdOrden });
        }

        // ---------------------- MÉTODOS POST ----------------------

        [HttpPost]
        public IActionResult CrearOrden(
        string TipoReparacion,
        ulong CostoManoObra,
        string TipoVehiculo,
        ulong PropietarioId,
        int DatoVehiculo,
        string Placa,
        string Marca,
        string Modelo,
        ushort Anio)
        {
            try
            {
                Reparacion reparacion;
                Carro carro;
                Cliente cliente = _usuarioService.ObtenerPorId(PropietarioId) as Cliente;
                OrdenReparacion ordenRep;

                if (cliente == null)
                    throw new Exception("El cliente seleccionado no existe.");

                // --- Vehículo ---
                switch (TipoVehiculo)
                {
                    case "Hibrido":
                        carro = new CarHibrido(Placa, Marca, Modelo, Anio, cliente, (byte)DatoVehiculo);
                        break;

                    case "Electrico":
                        carro = new CarElectrico(Placa, Marca, Modelo, Anio, cliente, (ushort)DatoVehiculo);
                        break;

                    case "Carburado":
                        carro = new CarCarburado(Placa, Marca, Modelo, Anio, cliente, (byte)DatoVehiculo);
                        break;

                    default:
                        throw new Exception("Tipo de vehículo no reconocido.");
                }

                // --- Reparación ---
                switch (TipoReparacion)
                {
                    case "Cambio de Aceite":
                        reparacion = new RepCambioAceite(CostoManoObra, new Mecanico[] { }, carro, new Repuesto[] { });
                        break;

                    case "Cambio de Bujías":
                        reparacion = new RepCambioBujia(CostoManoObra, new Mecanico[] { }, carro, new Repuesto[] { });
                        break;

                    case "Puesta a Punto":
                        reparacion = new RepPuestaPunto(CostoManoObra, new Mecanico[] { }, carro, new Repuesto[] { });
                        break;

                    default:
                        throw new Exception("Tipo de reparación no válido.");
                }

                ordenRep = new OrdenReparacion(reparacion);

                // Publicamos el evento
                TempData["MensajeEvento"] = _taller.PublicadorProcesoDeReparacion.IngresarVehiculo(ordenRep);

                return RedirectToAction("AsignarRespusto", new { id = ordenRep.IdOrden });
            }
            catch (Exception ex)
            {

                // Mostramos el error temporalmente
                TempData["MensajeError"] = $"❌ Ocurrió un error al crear la orden: {ex.Message}";

                // Redirigimos a la vista de creación para mostrar el mensaje
                return RedirectToAction("Crear");
            }
        }


        [HttpPost]
        public IActionResult Asignar(string Nombre, string Proveedor, decimal Costo, ulong Id)
        {
            try
            {
                // Validaciones básicas antes de procesar
                if (string.IsNullOrWhiteSpace(Nombre))
                    throw new Exception("El nombre del repuesto no puede estar vacío.");

                if (string.IsNullOrWhiteSpace(Proveedor))
                    throw new Exception("Debe especificar el proveedor del repuesto.");

                if (Costo <= 0)
                    throw new Exception("El costo del repuesto debe ser mayor que cero.");

                var orden = _ordenService.ObtenerPorId(Id);
                if (orden == null)
                    throw new Exception($"No se encontró una orden con el ID {Id}.");

                // Crear el repuesto
                Repuesto repuesto = new Repuesto(Nombre, Proveedor, (ulong)Costo);

                // Agregar el repuesto a la orden
                orden.Reparacion.RepuestosUtilizados = orden.Reparacion.RepuestosUtilizados
                    .Concat(new Repuesto[] { repuesto })
                    .ToArray();

                // Guardar los cambios
                _ordenService.ModificarOrden(Id, orden);

                // Mensaje de éxito
                TempData["MensajeEvento"] = $"✅ El repuesto \"{Nombre}\" fue asignado correctamente.";

                // Redirigir a la vista de repuestos
                return RedirectToAction("AsignarRespusto", new { Id });
            }
            catch (Exception ex)
            {
                // Mensaje de error temporal
                TempData["MensajeError"] = $"❌ Error al asignar repuesto: {ex.Message}";

                // Redirigir de nuevo a la vista de asignación de repuestos
                return RedirectToAction("AsignarRespusto", new { Id });
            }
        }



        [HttpPost]
        public IActionResult AsignarMecanicos(ulong IdOrdenAsig, ulong MecanicoId)
        {
            try
            {
                var orden = _ordenService.ObtenerPorId(IdOrdenAsig);
                if (orden == null)
                    throw new Exception($"No se encontró una orden con el ID {IdOrdenAsig}.");

                var mecanico = _usuarioService.ObtenerPorId(MecanicoId) as Mecanico;
                if (mecanico == null)
                    throw new Exception("El mecánico especificado no existe.");

                // Validar si ya está asignado
                bool yaAsignado = orden.Reparacion.MecanicosEncargados.Any(m => m.Id == mecanico.Id);
                if (yaAsignado)
                    throw new Exception($"El mecánico \"{mecanico.Nombre}\" ya está asignado a esta orden.");

                // Agregar el mecánico
                orden.Reparacion.MecanicosEncargados = orden.Reparacion.MecanicosEncargados
                    .Concat(new Mecanico[] { mecanico })
                    .ToArray();

                _ordenService.ModificarOrden(IdOrdenAsig, orden);

                // Mensaje de éxito
                TempData["MensajeEvento"] = $"✅ El mecánico \"{mecanico.Nombre}\" fue asignado correctamente.";

                return RedirectToAction("AsigMecanicos", new { id = IdOrdenAsig });
            }
            catch (Exception ex)
            {

                // Mostrar mensaje de error temporal
                TempData["MensajeError"] = $"❌ {ex.Message}";


                return RedirectToAction("AsigMecanicos", new { id = IdOrdenAsig });
            }
        }

    }
}
