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


        public async Task<ActionResult> Edit(int id)
        {
            var agente = new Agente { AgenteId = id };

           var agenteRequest = await ApiServicio.ObtenerElementoAsync1<Response>(agente,
                                                             new Uri(WebApp.BaseAddress),
                                                             "api/Agentes/GetAgente");

            if (!agenteRequest.IsSuccess)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var result = JsonConvert.DeserializeObject<Agente>(agenteRequest.Result.ToString());
            return View(result);
        }


        [HttpPost]
        public async Task<ActionResult> Edit(Agente agente)
        {
            if (!ModelState.IsValid)
            {
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

        public async Task<ActionResult> Index()
        {

            IdentityPersonalizado ci = (IdentityPersonalizado)HttpContext.User.Identity;
            string nombreUsuario = ci.Identity.Name;
            var administrador = new Administrador { Nombre = nombreUsuario };
            administrador = await ProveedorAutenticacion.GetUser(administrador);

            var empresa = new Empresa { EmpresaId = administrador.EmpresaId };

            var response = await ApiServicio.Listar<Agente>(empresa,
                                                             new Uri(WebApp.BaseAddress),
                                                             "api/Agentes/GetAgentesPorEmpresa");

            if (response==null)
            {
                return View(new List<Agente>());
            }
            return View(response);
        }


      
        public async Task<ActionResult> Create()
        {
            IdentityPersonalizado ci = (IdentityPersonalizado)HttpContext.User.Identity;
            string nombreUsuario = ci.Identity.Name;
            var administrador = new Administrador { Nombre = nombreUsuario };
            administrador = await ProveedorAutenticacion.GetUser(administrador);

            var agente = new Agente { EmpresaId = administrador.EmpresaId };

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Agente agente)
        {
            if (string.IsNullOrEmpty(agente.Nombre) || string.IsNullOrEmpty(agente.Apellido))
            {
                return View();
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