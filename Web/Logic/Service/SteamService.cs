using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Logic.Service
{
    public static class SteamService
    {
        private static string baseUrl = "https://steamcommunity.com/";
        private static HttpClient steamAuthenticateClient = new();
        /// <summary>
        /// Sends the user to steam. Steam will then authenticate the user throuhg their login system.
        /// </summary>
        /// <returns>Returns true if user was succesfully authenticated</returns>
        public static async Task<string> GetLoginUrl(string hostUrl)
        {
            string loginUrl = baseUrl + "openid/login";

            var query = HttpUtility.ParseQueryString(string.Empty);
            query["openid.ns"] = "http://specs.openid.net/auth/2.0";
            query["openid.mode"] = "checkid_setup";
            query["openid.return_to"] = $"https://{hostUrl}";
            query["openid.realm"] = $"https://{hostUrl}";
            query["openid.identity"] = "http://specs.openid.net/auth/2.0/identifier_select";
            query["openid.claimed_id"] = "http://specs.openid.net/auth/2.0/identifier_select";

            string parameters = query.ToString();

            loginUrl += $"?{parameters}";
            
            return loginUrl;
        }
    }
}
