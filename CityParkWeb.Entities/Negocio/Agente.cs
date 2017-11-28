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

        [Required(ErrorMessage = "Debe intoducir el nombre")]
        [StringLength(50, ErrorMessage =
            "El nombre del agente no puede contener más de {1} caracteres, y menos de {2} caracteres",
            MinimumLength = 3)]
        [Display(Name ="Nombres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage ="Debe intoducir el apellido")]
        [StringLength(50, ErrorMessage =
            "El nombre del agente no puede contener más de {1} caracteres, y menos de {2} caracteres",
            MinimumLength = 3)]
        [Display(Name = "Apellidos")]
        public string Apellido { get; set; }

        public string Contrasena { get; set; }

        public int EmpresaId { get; set; }

        [Range(1, Double.MaxValue, ErrorMessage = "Debe seleccionar un sector")]
        public int SectorId { get; set; }

        public virtual Empresa Empresa { get; set; }

        public virtual Sector Sector { get; set; }
    }
}
