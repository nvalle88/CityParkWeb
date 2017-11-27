using CityParkWeb.Entities.Negocio;
using System.Web.Security;

namespace CityParkWeb.Seguridad
{
    public class UsuarioMembership:MembershipUser
    {
        public int AdministradorId { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Contrasela { get; set; }

        public int EmpresaId { get; set; }

        public UsuarioMembership(Administrador us)
        {
            AdministradorId = us.AdministradorId;
            Nombre = us.Nombre;
            Contrasela = Contrasela;
            EmpresaId = us.EmpresaId;
            Apellido = us.Apellido;
           
        }

        public UsuarioMembership()
        {

        }
    }
}
