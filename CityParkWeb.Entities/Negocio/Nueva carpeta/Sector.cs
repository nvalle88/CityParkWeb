using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityParkWeb.Entities.Negocio
{
  public class Sector
    {
        public int SectorId { get; set; }

        public string NombreSector { get; set; }

        public Nullable<int> EmpresaId { get; set; }

        public virtual Empresa Empresa { get; set; }

        public virtual ICollection<Agente> Agente { get; set; }

        public virtual ICollection<PuntoSector> PuntoSector { get; set; }


    }
}
