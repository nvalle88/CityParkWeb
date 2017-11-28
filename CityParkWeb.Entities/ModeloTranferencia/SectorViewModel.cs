using CityParkWeb.Entities.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityParkWeb.Entities.ModeloTranferencia
{
   public class SectorViewModel
    {
        public Sector Sector { get; set; }
        public List<PuntoSector> PuntoSector { get; set; }
    }
}
