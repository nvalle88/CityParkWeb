using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityParkWeb.Entities.Negocio
{
  public class Agente
    {
        public int AgenteId { get; set; }

        [Required]
        [Display(Name ="Nombres")]
        public string Nombre { get; set; }
        [Required]
        [Display(Name = "Apellidos")]
        public string Apellido { get; set; }

        public string Contrasena { get; set; }

        public int EmpresaId { get; set; }

        public virtual Empresa Empresa { get; set; }
    }
}
