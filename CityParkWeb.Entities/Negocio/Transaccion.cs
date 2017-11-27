using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityParkWeb.Entities.Negocio
{
   public class Transaccion
    {
        public int TransaccionId { get; set; }

        public int UsuarioId { get; set; }

        public int VendedorId { get; set; }

        public double Monto { get; set; }

        public System.DateTime Fecha { get; set; }

        public int EmpresaId { get; set; }


        public virtual Empresa Empresa { get; set; }

        public virtual Vendedor Vendedor { get; set; }
    }
}
