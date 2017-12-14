using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityParkWeb.Entities.Negocio
{
   public class Administrador
    {
        public int AdministradorId { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Contrasela { get; set; }

        public int EmpresaId { get; set; }

        public virtual Empresa Empresa { get; set; }
    }
}
