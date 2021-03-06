//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CityParkWeb.Entities.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Agente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Agente()
        {
            this.Multa = new HashSet<Multa>();
        }

        public int AgenteId { get; set; }

        [Required(ErrorMessage = "Debe intoducir el nombre")]
        [StringLength(50, ErrorMessage =
             "El nombre del agente no puede contener más de {1} caracteres, y menos de {2} caracteres",
             MinimumLength = 3)]
        [Display(Name = "Nombres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Debe intoducir el apellido")]
        [StringLength(50, ErrorMessage =
            "El nombre del agente no puede contener más de {1} caracteres, y menos de {2} caracteres",
            MinimumLength = 3)]
        [Display(Name = "Apellidos")]
        public string Apellido { get; set; }
        public string Contrasena { get; set; }
        public Nullable<int> EmpresaId { get; set; }
        public Nullable<int> SectorId { get; set; }
    
        public virtual Empresa Empresa { get; set; }
        public virtual Sector Sector { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Multa> Multa { get; set; }
    }
}
