using BibTaller.Aspectos;
using BibTaller.Clases;
using BibTaller.Clases.ClasesEventArgs;
using BibTaller.Clases.EstrucPago;
using BibTaller.Clases.EstrucReparcion;

namespace TallerAutoWeb.Services {
    public class FacturacionService {


        private List<Factura> listaFacturas;
        private Taller _taller;


        public FacturacionService(Taller taller) {

            _taller = taller;
            listaFacturas = _taller.AdministradorPago._facturaRecorder.GetListaFacturas();

        }


        public List<Factura> ObtenerFacturas() {
            return listaFacturas;
        }


        public List<Factura> FiltrarPorMetodoPago(EstrategiaPago.tipoPago metodoDePago) {
            return listaFacturas.Where(f => f.TipoPago == metodoDePago).ToList();
        }


        public List<Factura> FiltrarPorCliente(ulong idCliente) {
            return listaFacturas.Where(f => 
            f.OrdenPagada.Reparacion.Carro.Propietario?.Id == idCliente).ToList();
        }


    }
}
