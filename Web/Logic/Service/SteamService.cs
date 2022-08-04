using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Logic.Service
{
    public static class SteamService
    {
        private static string baseUrlSteamCommunity = "https://steamcommunity.com/"; // Base url for accessing the Steam endpoint.
        private static string baseUrlSteamApi = "https://api.steampowered.com/";
        private static string steamApiKey = "378B08DE26CC13172957E6F215670861"; // Api key for Steam to validate (used in numerous requests).

        private static HttpClient steamClient = new(); // Client for accessing Steam end points.

        /// <summary>
        /// Gets the correct url where the user will be send to, to login.
        /// </summary>
        /// <returns>Returns a string of the url where the user can login.</returns>
        public static string GetLoginUrl(string hostUrl)
        {
            string loginUrl = baseUrlSteamCommunity + "openid/login";

            // Create a query string.
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["openid.ns"] = "http://specs.openid.net/auth/2.0";
            query["openid.mode"] = "checkid_setup";
            query["openid.return_to"] = $"https://{hostUrl}/Session/AfterLogin";
            query["openid.realm"] = $"https://{hostUrl}";
            query["openid.identity"] = "http://specs.openid.net/auth/2.0/identifier_select";
            query["openid.claimed_id"] = "http://specs.openid.net/auth/2.0/identifier_select";

            string parameters = query.ToString();

            loginUrl += $"?{parameters}";

            return loginUrl;
        }

        // FOR AUTHENTICATING VALID DATA (AGAINST SPOOFING)
        //public static async Task<bool> ValidateLoginData(string queryString)
        //{

        //}


        /// <summary>
        /// Gets the summaries of a user account (Steam ID is also included).<br></br>
        /// Api documentation location: steanwebapi.azurewebsites.net
        /// </summary>
        /// <param name="steamId"></param>
        /// <returns>Returns a dictionary of the user account summaries</returns>
        /// <exception cref="ArgumentException"></exception>
        public static async Task<Dictionary<string, string>> GetUserSummaries(string steamId)
        {
            string userSummariesUrl = baseUrlSteamApi + $"ISteamUser/GetPlayerSummaries/v0002/?key={steamApiKey}&steamids={steamId}";

            var response = await steamClient.GetAsync(userSummariesUrl);

            // If the response is succesfull get the body and convert the json object to a dictionairy.
            if (response.IsSuccessStatusCode)
            {
                var jsonResult = await response.Content.ReadAsStringAsync();

                dynamic result = JsonConvert.DeserializeObject(jsonResult);
                result = result["response"]["players"][0];

                Dictionary<string, string> userSummaries = new()
                {
                    { "steamid", result.steamid.ToString() },
                    { "communityvisibilitystate", result.communityvisibilitystate.ToString() },
                    { "profilestate", result.profilestate.ToString() },
                    { "personaname", result.personaname.ToString() },
                    { "profileurl", result.profileurl.ToString() },
                    { "avatar", result.avatar.ToString() },
                    { "avatarmedium", result.avatarmedium.ToString() },
                    { "avatarfull", result.avatarfull.ToString() },
                    { "avatarhash", result.avatarhash.ToString() },
                    { "lastlogoff", result.lastlogoff.ToString() },
                    { "personastate", result.personastate.ToString() },
                    { "realname", result.realname.ToString() },
                    { "primaryclanid", result.primaryclanid.ToString() },
                    { "timecreated", result.timecreated.ToString() },
                    { "personastateflags", result.personastateflags.ToString() },
                    { "loccountrycode", result.loccountrycode.ToString() }
                };

                return userSummaries;
            }

            throw new ArgumentException("No valid response");
        }
    }
}
