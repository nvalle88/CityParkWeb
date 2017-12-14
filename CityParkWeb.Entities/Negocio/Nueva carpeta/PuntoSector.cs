using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityParkWeb.Entities.Negocio
{
   public class PuntoSector
    {
        public int PuntoSectorId { get; set; }

        public Nullable<double> Latitud { get; set; }

        public Nullable<double> Longitud { get; set; }

        public Nullable<int> SectorId { get; set; }

        public virtual Sector Sector { get; set; }
    }
}
