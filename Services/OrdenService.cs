using BibTaller.Clases;
using BibTaller.Clases.EstrucPersona;
using BibTaller.Clases.EstrucReparcion;

namespace TallerAutoWeb.Services
{
    public class OrdenService
    {
        // Atributos
        private List<OrdenReparacion> listaOrdenes;
        private Taller _taller;

        // Constructor
        public OrdenService(Taller taller)
        {
            _taller = taller;
            listaOrdenes = _taller.AdministradorOrden.ObtenerOrdenes();
        }

     
        // MÉTODOS DE CONSULTA
 

        public List<OrdenReparacion> ObtenerTodas()
        {
            return listaOrdenes;
        }

        public OrdenReparacion ObtenerPorId(ulong id)
        {
            return listaOrdenes.Find(o => o.IdOrden == id);
        }

       
        // MÉTODOS DE MODIFICACIÓN
    

        public void ModificarOrden(ulong id, OrdenReparacion newOrden)
        {
            _taller.AdministradorOrden.ModificarOrden(id, newOrden);
        }

    
        // PROCESO DE LOS EVENTOS DE LA ORDEN
      

        public string AgregarOrden(OrdenReparacion orden)
        {
            string mensaje = _taller.PublicadorProcesoDeReparacion.IngresarVehiculo(orden);
            return mensaje;
        }

        public string RepararOrden(OrdenReparacion orden)
        {
            string mensaje = _taller.PublicadorProcesoDeReparacion.RepararVehiculo(orden);
            
            return mensaje;
        }

        public string PagarOrden(OrdenReparacion orden)
        {
            string mensaje = _taller.PublicadorProcesoDeReparacion.PagarRepVehiculo(orden);
            return mensaje;
        }

        public string EntregarVehiculo(OrdenReparacion orden)
        {
            string mensaje = _taller.PublicadorProcesoDeReparacion.EntregarVehiculoReparado(orden);
            return mensaje;
        }
    }
}
