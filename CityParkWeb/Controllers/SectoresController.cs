using CityParkWeb.Entities.ModeloTranferencia;
using CityParkWeb.Entities.Negocio;
using CityParkWeb.Entities.Utils;
using CityParkWeb.Seguridad;
using CityParkWeb.Services.Servicios;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        private async Task<List<Sector>> obtenerSectoresPorEmpresa()
        {
            IdentityPersonalizado ci = (IdentityPersonalizado)HttpContext.User.Identity;
            string nombreUsuario = ci.Identity.Name;
            var administrador = new Administrador { Nombre = nombreUsuario };
            administrador = await ProveedorAutenticacion.GetUser(administrador);

            var empresa = new Empresa { EmpresaId = administrador.EmpresaId };

            var listaSectores = await ApiServicio.Listar<Sector>(empresa,
                                                             new Uri(WebApp.BaseAddress),
                                                             "api/Sectors/GetSectoresPorEmpresa");
            return listaSectores;
        }


        public async Task<ActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var sector = new Sector { SectorId = id };

            var sectorRequest = await ApiServicio.EliminarAsync<Response>(sector,
                                                              new Uri(WebApp.BaseAddress),
                                                              "api/Sectors/DeleteSector");
            if (!sectorRequest.IsSuccess)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return RedirectToAction("Index");
        }


        public async Task<ActionResult> Edit(int id)
        {

            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var sector = new Sector { SectorId = id };

            var sectorRequest = await ApiServicio.ObtenerElementoAsync1<Response>(sector,
                                                              new Uri(WebApp.BaseAddress),
                                                              "api/Sectors/GetSector");
            if (!sectorRequest.IsSuccess)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var result = JsonConvert.DeserializeObject<Sector>(sectorRequest.Result.ToString());
            return View(result);
        }

        public async Task<ActionResult> Details(int id)
        {

            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var sector = new Sector { SectorId = id };

            var sectorRequest = await ApiServicio.ObtenerElementoAsync1<Response>(sector,
                                                              new Uri(WebApp.BaseAddress),
                                                              "api/Sectors/GetSector");
            if (!sectorRequest.IsSuccess)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var result = JsonConvert.DeserializeObject<Sector>(sectorRequest.Result.ToString());
            return View(result);
        }
        // GET: Sectores
        public async Task<ActionResult> Index()
        {

            var response = await obtenerSectoresPorEmpresa();
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

        public class PuntosRequest
        {
            public double lat { get; set; }
            public double lng { get; set; }
        }

        public async Task<JsonResult> DetalleSector(int sectorId)
        {
            if (sectorId<=0)
            {
                return Json(false);
            }

            var sector = new Sector { SectorId = sectorId };

            var response = await ApiServicio.Listar<PuntoSector>(sector, new Uri(WebApp.BaseAddress), "api/Sectors/GetPuntoSectorPorSector");

            if (response==null)
            {
                return Json(false);
            }

            var listaRequest = new List<PuntosRequest>();

            foreach (var item in response)
            {
                listaRequest.Add(new PuntosRequest {lat=(Double)item.Latitud ,lng=(Double) item.Longitud});
            }

            return Json(listaRequest);
        }




        public async Task<JsonResult> EditarSector(int sectorId,string nombreSector, List<Posiciones> arreglo)
        {

            if (string.IsNullOrEmpty(nombreSector) || arreglo.Count <= 2)
            {
                return Json(false);
            }

            var lista = new List<PuntoSector>();

            foreach (var item in arreglo)
            {
               // item.latitud = item.latitud.Replace(".", ",");
                //item.longitud = item.longitud.Replace(".", ",");
                lista.Add(new PuntoSector { Latitud = Convert.ToDouble(item.latitud), Longitud = Convert.ToDouble(item.longitud) });

            }

            var sector = new SectorViewModel
            {
                Sector = new Sector { NombreSector = nombreSector,SectorId=sectorId },
                PuntoSector = lista,
            };


            var response = await ApiServicio.InsertarAsync(sector, new Uri(WebApp.BaseAddress), "api/Sectors/EditarSector");
            if (!response.IsSuccess)
            {
                return Json(false);
            }
            return Json(true);
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
                //item.latitud=item.latitud.Replace(".", ",");
                //item.longitud=item.longitud.Replace(".", ",");
                lista.Add(new PuntoSector {Latitud=Convert.ToDouble(item.latitud),Longitud=Convert.ToDouble(item.longitud)});

            }

            var sector = new SectorViewModel
            {
                Sector=new Sector {NombreSector=nombreSector,EmpresaId=administrador.EmpresaId },
                PuntoSector=lista,
            };

            

            var response=  await ApiServicio.InsertarAsync(sector,new Uri( WebApp.BaseAddress), "/api/Sectors/InsertarSector");
            if (!response.IsSuccess)
            {
                return Json(false);
            }
            return Json(true);
        }
    }
}