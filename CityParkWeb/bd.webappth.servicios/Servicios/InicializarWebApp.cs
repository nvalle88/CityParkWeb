using CityParkWeb.entidades.Utils;
using System;
using System.Threading.Tasks;

namespace CityParkWeb.servicios.Servicios
{
    public class InicializarWebApp
    {
        #region Methods


        public static async Task InicializarWeb(string baseAddreess)
        {
            try
            {
                WebApp.BaseAddress = baseAddreess;
            }
            catch (Exception ex)
            {

            }

        }

        #endregion
    }
}