using CityParkWeb.Entities.Helpers;
using CityParkWeb.Entities.Negocio;
using CityParkWeb.Entities.Utils;
using CityParkWeb.Seguridad;
using CityParkWeb.Services.Servicios;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CityParkWeb.Controllers
{
    [Authorize]
    public class AgentesController : Controller
    {
        // GET: Agentes
        public ActionResult VerAgentesTiempoReal()
        {
            return View();
        }

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


        public async Task<ActionResult> Edit(int id)
        {

            if (id<=0)
            {
              return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           var agente = new Agente { AgenteId = id };

           var agenteRequest = await ApiServicio.ObtenerElementoAsync1<Response>(agente,
                                                             new Uri(WebApp.BaseAddress),
                                                             "api/Agentes/GetAgente");
            if (!agenteRequest.IsSuccess)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var result = JsonConvert.DeserializeObject<Agente>(agenteRequest.Result.ToString());
            ViewBag.SectorId = new SelectList(await obtenerSectoresPorEmpresa(), "SectorId", "NombreSector", result.SectorId);

            return View(result);
        }


        [HttpPost]
        public async Task<ActionResult> Edit(Agente agente)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.SectorId = new SelectList(await obtenerSectoresPorEmpresa(), "SectorId", "NombreSector", agente.SectorId);
                return View(agente);
            }
            
            var agenteRequest = await ApiServicio.EditarAsync<Response>(agente,
                                                              new Uri(WebApp.BaseAddress),
                                                              "api/Agentes/EditAgente");
            if (!agenteRequest.IsSuccess)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return RedirectToAction("Index");
        }



        public async Task<ActionResult> Delete(int id)
        {

            var agente = new Agente { AgenteId = id };
            var response = await ApiServicio.EliminarAsync<Response>(agente,
                                                             new Uri(WebApp.BaseAddress),
                                                             "api/Agentes/DeleteAgente");

            if (!response.IsSuccess)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> ResetPassword(int id)
        {

            var agente = new Agente { AgenteId = id };
            var response = await ApiServicio.EditarAsync<Response>(agente,
                                                             new Uri(WebApp.BaseAddress),
                                                             "api/Agentes/ResetPassword");

            if (!response.IsSuccess)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return RedirectToAction("Index");
        }

        private async Task<List<Agente>> ObtenerAgentesPorEmpresa()
        {
            IdentityPersonalizado ci = (IdentityPersonalizado)HttpContext.User.Identity;
            string nombreUsuario = ci.Identity.Name;
            var administrador = new Administrador { Nombre = nombreUsuario };
            administrador = await ProveedorAutenticacion.GetUser(administrador);

            var empresa = new Empresa { EmpresaId = administrador.EmpresaId };

            var listaAgentes = await ApiServicio.Listar<Agente>(empresa,
                                                             new Uri(WebApp.BaseAddress),
                                                             "api/Agentes/GetAgentesPorEmpresa");
            return listaAgentes;
        }

        public async Task<ActionResult> Index()
        {
            var listaAgentes = await ObtenerAgentesPorEmpresa();
            if (listaAgentes == null)
            {
                return View(new List<Agente>());
            }
            return View(listaAgentes);
        }


      
        public async Task<ActionResult> Create()
        {
            ViewBag.SectorId = new SelectList(await obtenerSectoresPorEmpresa(), "SectorId", "NombreSector");
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Agente agente)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.SectorId = new SelectList(await obtenerSectoresPorEmpresa(), "SectorId", "NombreSector", agente.SectorId);
                return View(agente);
            }
            IdentityPersonalizado ci = (IdentityPersonalizado)HttpContext.User.Identity;
            string nombreUsuario = ci.Identity.Name;
            var administrador = new Administrador { Nombre = nombreUsuario };
            administrador = await ProveedorAutenticacion.GetUser(administrador);
            var codificar= CodificarHelper.SHA512(new Codificar { Entrada =agente.Nombre });
            agente.Contrasena = codificar.Salida;
            agente.EmpresaId = administrador.EmpresaId;

            var response = await ApiServicio.InsertarAsync(agente,
                                                            new Uri(WebApp.BaseAddress),
                                                            "api/Agentes/CreateAgente");

            
            if (!response.IsSuccess)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); 
            }

            return RedirectToAction("Index","Agentes");
        }
    }
}