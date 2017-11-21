using System;
using System.Collections.Generic;
using System.Text;

namespace CityParkWeb.entidades.entidades.Negocio
{
  public  class Administrador
   {
        public int AdministradorId { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Contrasela { get; set; }

        public int EmpresaId { get; set; }
    }
}
