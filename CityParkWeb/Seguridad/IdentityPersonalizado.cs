using CityParkWeb.Entities.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace CityParkWeb.Seguridad
{
    public class IdentityPersonalizado : IIdentity
    {
        public string Name
        {
            get { return Nombre; }
            set { }
        }

        public string AuthenticationType
        {
            get { return Identity.AuthenticationType; }
        }

        public bool IsAuthenticated
        {
            get { return Identity.IsAuthenticated; }
        }

        public int AdministradorId { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Contrasela { get; set; }

        public int EmpresaId { get; set; }

        public IIdentity Identity { get; set; }

        public async Task  InicializarAsync(IIdentity identity)
        {
            this.Identity = identity;

            var userAdmin = new Administrador
            {
                Nombre = identity.Name,
            };

            var us =await ProveedorAutenticacion.GetUser(userAdmin) ;

            AdministradorId = us.AdministradorId;
            Nombre = us.Nombre;
            Contrasela = us.Contrasela;
            Apellido = us.Apellido;
            EmpresaId = us.EmpresaId;
            Name = us.Nombre;
        }

        public IdentityPersonalizado(IIdentity identity)
        {
            InicializarAsync(identity);

        }
    }
}
