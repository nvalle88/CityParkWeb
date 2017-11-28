using CityParkWeb.Entities.Utils;
using System;
using System.Threading.Tasks;
namespace bd.log.servicios.Servicios
{
    public class InicializarWebApp
    {

        #region Methods



        public static async Task InicializarWeb(Uri baseAddreess)
        {
            try
            {
               
                // WebApp.BaseAddress = "http://cityparkws.azurewebsites.net";
                WebApp.BaseAddress = "http://localhost:59363";


            }
            catch (Exception ex)
            {

            }

        }

        #endregion
    }
}
