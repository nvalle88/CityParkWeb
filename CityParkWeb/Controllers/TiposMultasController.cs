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
    public class TiposMultasController : Controller
    {
        // GET: Agentes
        public ActionResult VerAgentesTiempoReal()
        {
            return View();
        }

        private async Task<List<TipoMultas>> ObtenerTiposMultasPorEmpresa()
        {
            IdentityPersonalizado ci = (IdentityPersonalizado)HttpContext.User.Identity;
            string nombreUsuario = ci.Identity.Name;
            var administrador = new Administrador { Nombre = nombreUsuario };
            administrador = await ProveedorAutenticacion.GetUser(administrador);

            var empresa = new Empresa { EmpresaId = administrador.EmpresaId };

            var lista = await ApiServicio.Listar<TipoMultas>(empresa,
                                                             new Uri(WebApp.BaseAddress),
                                                             "api/TiposMultas/GetTiposMultasPorEmpresa");
            return lista;
        }


        public async Task<ActionResult> Edit(int id)
        {

            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tipoMulta = new TipoMultas { TipoMultaId = id};

            var agenteRequest = await ApiServicio.ObtenerElementoAsync1<Response>(tipoMulta,
                                                              new Uri(WebApp.BaseAddress),
                                                              "api/TiposMultas/GetTipoMulta");
            if (!agenteRequest.IsSuccess)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var result = JsonConvert.DeserializeObject<TipoMultas>(agenteRequest.Result.ToString());
            return View(result);
        }


        [HttpPost]
        public async Task<ActionResult> Edit(TipoMultas tipoMultas)
        {
            if (!ModelState.IsValid)
            {
                return View(tipoMultas);
            }
            var tipoMultaRequest = await ApiServicio.EditarAsync<Response>(tipoMultas,
                                                              new Uri(WebApp.BaseAddress),
                                                              "api/TiposMultas/EditTipoMulta");
            if (!tipoMultaRequest.IsSuccess)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(int id)
        {

            var tipoMulta = new TipoMultas { TipoMultaId = id };
            var response = await ApiServicio.EliminarAsync<Response>(tipoMulta,
                                                             new Uri(WebApp.BaseAddress),
                                                             "api/TiposMultas/DeleteTipoMulta");

            if (!response.IsSuccess)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return RedirectToAction("Index");
        }
      
        private async Task<List<SalarioBasico>> ObtenerSalarioBasicoPorEmpresa()
        {
            IdentityPersonalizado ci = (IdentityPersonalizado)HttpContext.User.Identity;
            string nombreUsuario = ci.Identity.Name;
            var administrador = new Administrador { Nombre = nombreUsuario };
            administrador = await ProveedorAutenticacion.GetUser(administrador);

            var empresa = new Empresa { EmpresaId = administrador.EmpresaId };

            var lista = await ApiServicio.Listar<SalarioBasico>(empresa,
                                                             new Uri(WebApp.BaseAddress),
                                                             "api/SalariosBasicos/GetSalariosBasicosPorEmpresa");
            return lista;
        }

        public async Task<ActionResult> Index()
        {
            var lista = await ObtenerTiposMultasPorEmpresa();
            if (lista == null)
            {
                return View(new List<TipoMultas>());
            }
            return View(lista);
        }



        public async Task<ActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(TipoMultas tipoMultas)
        {
            if (!ModelState.IsValid)
            {
                return View(tipoMultas);
            }
            IdentityPersonalizado ci = (IdentityPersonalizado)HttpContext.User.Identity;
            string nombreUsuario = ci.Identity.Name;
            var administrador = new Administrador { Nombre = nombreUsuario };
            administrador = await ProveedorAutenticacion.GetUser(administrador);
            tipoMultas.EmpresaId = administrador.EmpresaId;

            var response = await ApiServicio.InsertarAsync(tipoMultas,
                                                            new Uri(WebApp.BaseAddress),
                                                            "api/TiposMultas/CreateTipoMulta");


            if (!response.IsSuccess)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return RedirectToAction("Index");
        }
    }
}