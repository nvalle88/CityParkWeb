using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityParkWeb.ClassUtils
{
   public class LivePositionRequest
    {
        public int EmpresaId { get; set; }
        public int AgenteId { get; set; }
        public float Lat { get; set; }
        public float Lon { get; set; }
        public DateTime fecha { get; set; }

    }
}
