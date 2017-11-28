using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityParkWeb.Entities.Negocio
{
   public class Vendedor
    {
        public int VendedorId { get; set; }

        [Required(ErrorMessage = "Debe intoducir el nombre")]
        [StringLength(50, ErrorMessage =
           "El nombre del vendedor no puede contener más de {1} caracteres, y menos de {2} caracteres",
           MinimumLength = 3)]
        [Display(Name = "Nombres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Debe intoducir el apellido")]
        [StringLength(50, ErrorMessage =
          "El nombre del vendedor no puede contener más de {1} caracteres, y menos de {2} caracteres",
          MinimumLength = 3)]
        [Display(Name = "Apellidos")]
        public string Apellido { get; set; }

        public string Contrasena { get; set; }

        public int EmpresaId { get; set; }

        public virtual ICollection<Transaccion> Transaccion { get; set; }

        public virtual Empresa Empresa { get; set; }
    }
}
