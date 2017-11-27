using CityParkWeb.Entities.ModeloTranferencia;
using CityParkWeb.Entities.Negocio;
using CityParkWeb.Entities.Utils;
using CityParkWeb.Services.Servicios;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Security;

namespace CityParkWeb.Seguridad
{
    public static class ProveedorAutenticacion 
    {

        public static bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion,
            string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public static string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public static bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public static string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public static void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public async static Task<bool> ValidateUser(LoginRequest login)
        {

            var response = await ApiServicio.ObtenerElementoAsync1<Response>(login,
                                                             new Uri(WebApp.BaseAddress),
                                                             "api/Administradores/Login");
            if (!response.IsSuccess)
            {
                return false;
            }
            var usuario = JsonConvert.DeserializeObject<Administrador>(response.Result.ToString());
            new UsuarioMembership(usuario);
            return true;
        }

        public static bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public static MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public async static Task<Administrador> GetUser(Administrador administrador)
        {

            var user = new UsuarioMembership();

            var response = await ApiServicio.ObtenerElementoAsync1<Response>(administrador,
                                                             new Uri(WebApp.BaseAddress),
                                                             "api/Administradores/GetUserAdmin");

            if (!response.IsSuccess)
            {
                return null;
            }
            var usuario = JsonConvert.DeserializeObject<Administrador>(response.Result.ToString());
            return usuario;
        }

        public static string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public static bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public static MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public static int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public static MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public static MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public static bool EnablePasswordRetrieval { get; }
        public static bool EnablePasswordReset { get; }
        public static bool RequiresQuestionAndAnswer { get; }
        public static string ApplicationName { get; set; }
        public static int MaxInvalidPasswordAttempts { get; }
        public static int PasswordAttemptWindow { get; }
        public static bool RequiresUniqueEmail { get; }
        public static MembershipPasswordFormat PasswordFormat { get; }
        public static int MinRequiredPasswordLength { get; }
        public static int MinRequiredNonAlphanumericCharacters { get; }
        public static string PasswordStrengthRegularExpression { get; }
    }
}
