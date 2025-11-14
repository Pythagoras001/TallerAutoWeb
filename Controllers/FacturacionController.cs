using BibTaller.Clases;
using BibTaller.Clases.EstrucPago;
using Microsoft.AspNetCore.Mvc;
using TallerAutoWeb.Services;

namespace TallerAutoWeb.Controllers {
    public class FacturacionController : Controller {

        private Taller _taller;
        private FacturacionService _listaFacturas;


        public FacturacionController(Taller taller) {

            _taller = taller;
            _listaFacturas = new FacturacionService(_taller);

        }

        [HttpGet]
        public IActionResult MenuFacturacion(string tipoPago, string clienteId) {
            var facturas = _listaFacturas.ObtenerFacturas();

            if (!string.IsNullOrEmpty(tipoPago)) {

                EstrategiaPago.tipoPago tipoPagoEnum;
                if (Enum.TryParse(tipoPago, out tipoPagoEnum)) {
                    facturas = _listaFacturas.FiltrarPorMetodoPago(tipoPagoEnum);
                }
            }

            if(!string.IsNullOrEmpty(clienteId)) {
                facturas = _listaFacturas.FiltrarPorCliente(ulong.Parse(clienteId));
            }

            return View(facturas);
        }


    }
}
