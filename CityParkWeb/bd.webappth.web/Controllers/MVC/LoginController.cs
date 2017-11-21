using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication.Cookies;
using CityParkWeb.servicios.Interfaces;
using CityParkWeb.entidades.Utils;
using CityParkWeb.entidades.entidades.Negocio;
using CityParkWeb.entidades.ViewModels;

namespace CityParkWeb.web.Controllers.MVC
{
    public class LoginController : Controller
    {

        private readonly IApiServicio apiServicio;


        public LoginController(IApiServicio apiServicio)
        {
            this.apiServicio = apiServicio;

        }

        private void InicializarMensaje(string mensaje)
        {
            if (mensaje == null)
            {
                mensaje = "";
            }
            ViewData["Error"] = mensaje;
        }

        public IActionResult Index(string mensaje, string returnUrl=null)
        {
            InicializarMensaje(mensaje);
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }




        public async Task<IActionResult> Login(Login login,string returnUrl=null)
        {

            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(LoginController.Index));
            }        

           var response = await apiServicio.ObtenerElementoAsync1<Response>(login,
                                                             new Uri(WebApp.BaseAddress),
                                                             "api/Administradores/Login");

           

            if (!response.IsSuccess)
            {
                return RedirectToAction("Index", new { mensaje = response.Message });
            }

            var usuario = JsonConvert.DeserializeObject<Administrador>(response.Result.ToString());

            var claims = new[]
            {
                new Claim(ClaimTypes.Name,usuario.Nombre),
                new Claim(ClaimTypes.UserData,Convert.ToString(usuario.EmpresaId))
               
            };



            var principal = new ClaimsPrincipal(new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme));

           await HttpContext.Authentication.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            if (string.IsNullOrEmpty(returnUrl))
            {
                return RedirectToAction(nameof(HomesController.Index), "Homes");
            }

            return LocalRedirect(returnUrl);

        }

        public async Task<IActionResult> Salir()
        {
           
            await HttpContext.Authentication.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(LoginController.Index), "Login");


        }
    }
}