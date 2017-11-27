using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityParkWeb.Entities.Negocio
{
   public class Vendedor
    {
        public int VendedorId { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Contrasena { get; set; }

        public int EmpresaId { get; set; }

        public virtual ICollection<Transaccion> Transaccion { get; set; }

        public virtual Empresa Empresa { get; set; }
    }
}
