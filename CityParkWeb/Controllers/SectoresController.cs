using CityParkWeb.Entities.ModeloTranferencia;
using CityParkWeb.Entities.Negocio;
using CityParkWeb.Entities.Utils;
using CityParkWeb.Seguridad;
using CityParkWeb.Services.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CityParkWeb.Controllers
{
    public class Posiciones
    {
        public string latitud { get; set; }
        public string longitud { get; set; }
    }

    public class SectoresController : Controller
    {
        // GET: Sectores
        public async Task<ActionResult> Index()
        {

            IdentityPersonalizado ci = (IdentityPersonalizado)HttpContext.User.Identity;
            string nombreUsuario = ci.Identity.Name;
            var administrador = new Administrador { Nombre = nombreUsuario };
            administrador = await ProveedorAutenticacion.GetUser(administrador);

            var empresa = new Empresa { EmpresaId = administrador.EmpresaId };

            var response = await ApiServicio.Listar<Sector>(empresa,
                                                             new Uri(WebApp.BaseAddress),
                                                             "api/Sectors/GetSectoresPorEmpresa");
            if (response == null)
            {
                return View(new List<Sector>());
            }
            return View(response);
        }

        public ActionResult Create()
        {
            return View();
        }


        public async Task<JsonResult> InsertarSector(string nombreSector, List<Posiciones> arreglo)
        {

            if (string.IsNullOrEmpty(nombreSector)|| arreglo.Count<=2)
            {
                return Json(false);
            }

            IdentityPersonalizado ci = (IdentityPersonalizado)HttpContext.User.Identity;
            string nombreUsuario = ci.Identity.Name;
            var administrador = new Administrador { Nombre = nombreUsuario };
            administrador = await ProveedorAutenticacion.GetUser(administrador);

            var lista = new List<PuntoSector>();

            foreach (var item in arreglo)
            {
                item.latitud=item.latitud.Replace(".", ",");
                item.longitud=item.longitud.Replace(".", ",");
                lista.Add(new PuntoSector {Latitud=Convert.ToDouble(item.latitud),Longitud=Convert.ToDouble(item.longitud)});

            }

            var sector = new SectorViewModel
            {
                Sector=new Sector {NombreSector=nombreSector,EmpresaId=administrador.EmpresaId },
                PuntoSector=lista,
            };


            var response=  await ApiServicio.InsertarAsync(sector,new Uri( WebApp.BaseAddress), "api/Sectors/InsertarSector");
            if (!response.IsSuccess)
            {
                return Json(false);
            }
            return Json(true);
        }
    }
}