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
    
    public partial class UsuarioTarjetaPrepago
    {
        public int UsuarioTarjetaPrepagoId { get; set; }
        public int UsuarioId { get; set; }
        public int TarjetaPrepagoId { get; set; }
    
        public virtual TarjetaPrepago TarjetaPrepago { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
