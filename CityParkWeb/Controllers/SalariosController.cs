using CityParkWeb.Entities.Helpers;
using CityParkWeb.Entities.ModeloTranferencia;
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
    public class SalariosController : Controller
    {
        public async Task<ActionResult> Edit(int id)
        {

            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var salario = new SalarioBasico { SalarioBasicoId = id };

            var salarioRequest = await ApiServicio.ObtenerElementoAsync1<Response>(salario,
                                                              new Uri(WebApp.BaseAddress),
                                                              "api/SalariosBasicos/GetSalarioBasico");
            if (!salarioRequest.IsSuccess)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var result = JsonConvert.DeserializeObject<SalarioBasico>(salarioRequest.Result.ToString());
            return View(result);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(SalarioBasico salarioBasico)
        {
            if (!ModelState.IsValid)
            {
                return View(salarioBasico);
            }

            var agenteRequest = await ApiServicio.EditarAsync<Response>(salarioBasico,
                                                              new Uri(WebApp.BaseAddress),
                                                              "api/SalariosBasicos/EditSalarioBasico");
            if (!agenteRequest.IsSuccess)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(int id)
        {

            var salario = new SalarioBasico { SalarioBasicoId = id };
            var response = await ApiServicio.EliminarAsync<Response>(salario,
                                                             new Uri(WebApp.BaseAddress),
                                                             "api/SalariosBasicos/DeleteSalarioBasico");
            if (!response.IsSuccess)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return RedirectToAction("Index");
        }

        private async Task<List<SalarioBasico>> ObtenerSalariosBasicosEmpresa()
        {
            IdentityPersonalizado ci = (IdentityPersonalizado)HttpContext.User.Identity;
            string nombreUsuario = ci.Identity.Name;
            var administrador = new Administrador { Nombre = nombreUsuario };
            administrador = await ProveedorAutenticacion.GetUser(administrador);

            var empresa = new Empresa { EmpresaId = administrador.EmpresaId };

            var listaSalariosBasicos = await ApiServicio.Listar<SalarioBasico>(empresa,
                                                             new Uri(WebApp.BaseAddress),
                                                             "api/SalariosBasicos/GetSalariosBasicosPorEmpresa");
            return listaSalariosBasicos;
        }

        public async Task<ActionResult> Index()
        {
            var listaSalariosBasicos = await ObtenerSalariosBasicosEmpresa();
            if (listaSalariosBasicos == null)
            {
                return View(new List<SalarioBasico>());
            }
            return View(listaSalariosBasicos);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(SalarioBasico salarioBasico)
        {
            if (!ModelState.IsValid)
            {
                return View(salarioBasico);
            }
            IdentityPersonalizado ci = (IdentityPersonalizado)HttpContext.User.Identity;
            string nombreUsuario = ci.Identity.Name;
            var administrador = new Administrador { Nombre = nombreUsuario };
            administrador = await ProveedorAutenticacion.GetUser(administrador);
            salarioBasico.EmpresaId = administrador.EmpresaId;

            var response = await ApiServicio.InsertarAsync(salarioBasico,
                                                            new Uri(WebApp.BaseAddress),
                                                            "api/SalariosBasicos/CreateSalarioBasico");


            if (!response.IsSuccess)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return RedirectToAction("Index");
        }
    }
}