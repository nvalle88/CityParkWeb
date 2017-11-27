using System;
using System.Threading.Tasks;
using System.Security.Claims;
using Newtonsoft.Json;
using System.Web.Mvc;
using CityParkWeb.Entities.ModeloTranferencia;
using CityParkWeb.Services.Servicios;
using CityParkWeb.Entities.Utils;
using CityParkWeb.Entities.Negocio;
using Microsoft.Owin.Security.Cookies;
using System.Web.Security;
using CityParkWeb.Seguridad;

namespace CityParkWeb.Controllers
{
    public class LoginController : Controller
    {


        private void InicializarMensaje(string mensaje)
        {
            if (mensaje == null)
            {
                mensaje = "";
            }
            ViewData["Error"] = mensaje;
        }

        public ActionResult Index(string mensaje, string returnUrl=null)
        {
            InicializarMensaje(mensaje);
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginRequest login,string returnUrl=null)
        {

            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(LoginController.Index));
            }

            var result =await ProveedorAutenticacion.ValidateUser(login);
            if (!result)
            {
                return RedirectToAction("Index", new { mensaje = "Usuario o contraseña incorrecta" });
            }

            FormsAuthentication.RedirectFromLoginPage(login.UserName, false);
            return null;

        }

        public async Task<ActionResult> SignOut()
        {
            var a=User.Identity.Name;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");

        }
    }
}