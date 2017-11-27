using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityParkWeb.Entities.Negocio
{
   public class Empresa
    {
        public int EmpresaId { get; set; }

        [Required]
        [Display(Name = "Razón social")]
        public string RazonSocial { get; set; }

        public string Ruc { get; set; }

        [Required]
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }

        public string Telefono { get; set; }

        public string PersonaDeContacto { get; set; }


        public virtual ICollection<Administrador> Administrador { get; set; }

        public virtual ICollection<Agente> Agente { get; set; }

        public virtual ICollection<Vendedor> Vendedor { get; set; }
    }
}
