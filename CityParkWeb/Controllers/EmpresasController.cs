using CityParkWeb.Entities.Negocio;
using CityParkWeb.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CityParkWeb.Controllers
{
    public class EmpresasController : Controller
    {
        // GET: Empresas
        public async Task<JsonResult> GetIdEmpresa()
        {
            IdentityPersonalizado ci = (IdentityPersonalizado)HttpContext.User.Identity;
            string nombreUsuario = ci.Identity.Name;
            var administrador = new Administrador { Nombre = nombreUsuario };
            administrador = await ProveedorAutenticacion.GetUser(administrador);
            return Json(administrador.EmpresaId);
        }
    }
}