using CityParkWeb.Entities.Helpers;
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
    [Authorize]
    public class VendedoresController : Controller
    {
        public async Task<ActionResult> Edit(int id)
        {
            var vendedor = new Vendedor { VendedorId = id };

            var vendedorRequest = await ApiServicio.ObtenerElementoAsync1<Response>(vendedor,
                                                              new Uri(WebApp.BaseAddress),
                                                              "api/Vendedors/GetVendedor");

            if (!vendedorRequest.IsSuccess)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var result = JsonConvert.DeserializeObject<Vendedor>(vendedorRequest.Result.ToString());
            return View(result);
        }


        [HttpPost]
        public async Task<ActionResult> Edit(Vendedor vendedor)
        {
            if (!ModelState.IsValid)
            {
                return View(vendedor);
            }
            var vendedorRequest = await ApiServicio.EditarAsync<Response>(vendedor,
                                                              new Uri(WebApp.BaseAddress),
                                                              "api/Vendedors/EditVendedor");
            if (!vendedorRequest.IsSuccess)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(int id)
        {

            var vendedor = new Vendedor { VendedorId = id };
            var response = await ApiServicio.EliminarAsync<Response>(vendedor,
                                                             new Uri(WebApp.BaseAddress),
                                                             "api/Vendedors/DeleteVendedor");

            if (!response.IsSuccess)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> ResetPassword(int id)
        {

            var vendedor = new Vendedor { VendedorId = id };
            var response = await ApiServicio.EditarAsync<Response>(vendedor,
                                                             new Uri(WebApp.BaseAddress),
                                                             "api/Vendedors/ResetPassword");

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

            var response = await ApiServicio.Listar<Vendedor>(empresa,
                                                             new Uri(WebApp.BaseAddress),
                                                             "api/Vendedors/GetVendedoresPorEmpresa");

            if (response == null)
            {
                return View(new List<Vendedor>());
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
        public async Task<ActionResult> Create(Vendedor vendedor)
        {
            if (!ModelState.IsValid)
            {
                return View(vendedor);
            }
            IdentityPersonalizado ci = (IdentityPersonalizado)HttpContext.User.Identity;
            string nombreUsuario = ci.Identity.Name;
            var administrador = new Administrador { Nombre = nombreUsuario };
            administrador = await ProveedorAutenticacion.GetUser(administrador);
            var codificar = CodificarHelper.SHA512(new Codificar { Entrada = vendedor.Nombre });
            vendedor.Contrasena = codificar.Salida;
            vendedor.EmpresaId = administrador.EmpresaId;

            var response = await ApiServicio.InsertarAsync(vendedor,
                                                            new Uri(WebApp.BaseAddress),
                                                            "api/Vendedors/CreateVendedor");
            if (!response.IsSuccess)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return RedirectToAction("Index", "Vendedores");
        }
    }
}