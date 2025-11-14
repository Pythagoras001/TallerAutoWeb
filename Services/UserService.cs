using BibTaller.Clases;
using BibTaller.Clases.EstrucPersona;
using Castle.Components.DictionaryAdapter;

namespace TallerAutoWeb.Services
{
    public class UserService
    {
        // Atributos
        private List<Persona> listaUsuario;
        private Taller _taller;

        // Constructor
        public UserService(Taller taller)
        {
            _taller = taller;
            listaUsuario = _taller.AdministradorUsuario.ObtenerUsuarios();
        }

        // Métodos de obtención
        public List<Persona> ObtenerTodas()
        {
            return listaUsuario;
        }

        // Buscar Usuario Por ID
        public Persona? ObtenerPorId(ulong id)
        {
            return listaUsuario.FirstOrDefault(u => u.Id == id);
        }

        // Retornar Lista De Usuarios Por Tipo
        public List<Cliente> ClientesRegistrados()
        {
            return listaUsuario.Where(u => u is Cliente).Cast<Cliente>().ToList();
        }

        public List<Mecanico> MecanicosRegistrados()
        {
            return listaUsuario.Where(u => u is Mecanico).Cast<Mecanico>().ToList();
        }

        public List<Admin> AdminsRegistrados()
        {
            return listaUsuario.Where(u => u is Admin).Cast<Admin>().ToList();
        }

        // Métodos CRUD
        public string RegistrarUsuario(Persona usuario)
        {
            string mensaje = _taller.PublicadorRegistroDeUsuario.RegistrarUsuario(usuario);
            return mensaje;
        }

        public void ActualizarUsuario(ulong id, Persona newUsuario)
        {
            _taller.AdministradorUsuario.ModificarUser(id, newUsuario);
        }

        public string EliminarUsuario(Persona usuario)
        {
            string mensaje =_taller.PublicadorRegistroDeUsuario.EliminarUsuario(usuario);
            return mensaje;
        }
    }
}
