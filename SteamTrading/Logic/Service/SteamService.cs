using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["openid.ns"] = "http://specs.openid.net/auth/2.0";
            queryString["openid.mode"] = "checkid_setup";
            queryString["openid.return_to"] = $"https://{hostUrl}/Session/AfterLogin";
            queryString["openid.realm"] = $"https://{hostUrl}";
            queryString["openid.identity"] = "http://specs.openid.net/auth/2.0/identifier_select";
            queryString["openid.claimed_id"] = "http://specs.openid.net/auth/2.0/identifier_select";

            string parameters = queryString.ToString();

            loginUrl += $"?{parameters}";

            return loginUrl;
        }

        /// <summary>
        /// Validate the data we got after the user logged into Steam (against spoofing).
        /// </summary>
        /// <param name="queryStringData"></param>
        /// <returns>Returns true if the data has been correctly validated.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static async Task<bool> ValidateSteamId(NameValueCollection queryStringData)
        {
            string authenticateUrl = baseUrlSteamCommunity + "openid/login";

            // Creating a new querystring with the data we got from Steam.
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["openid.assoc_handle"] = queryStringData["openid.assoc_handle"];
            queryString["openid.signed"] = queryStringData["openid.signed"];
            queryString["openid.sig"] = queryStringData["openid.sig"];
            queryString["openid.ns"] = queryStringData["openid.ns"];

            var signed = queryString["openid.signed"].Split(','); // Split each item in signed and turn it into an array of items.

            // Foreach item in signed get the value and use it with the right item after the value is trimmed for " symbols.
            foreach (var item in signed)
            {
                var value = queryStringData[$"openid.{item}"];
                queryString[$"openid.{item}"] = value.Trim('"');
            }

            queryString["openid.mode"] = "check_authentication"; // This is so Steam knows the data needs to be authenticated.

            StringContent content = new StringContent(queryString.ToString());


            // Set the headers of the request and the content.
            steamClient.DefaultRequestHeaders.Add("method", "POST");
            steamClient.DefaultRequestHeaders.Add("header", "Accept-language: en");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            content.Headers.ContentLength = queryString.ToString().Length;

            var response = await steamClient.PostAsync(authenticateUrl, content);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();

                if (result.Contains("true"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            throw new ArgumentException("No valid response");
        }


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

                dynamic result = JsonConvert.DeserializeObject<dynamic>(jsonResult);
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
