﻿using Infrastructure.Service;
using IntefaceLogic.Model;
using InterfaceDal.Interface;
using InterfaceLogic.Inteface;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;
using System.Web;

namespace Web.Controllers
{
    public class SessionController : Controller
    {
        private readonly IUserDal _userDal;
        private readonly IUserContainer _userContainer;

        public SessionController(IUserDal userDal, IUserContainer userContainer)
        {
            _userDal = userDal; 
            _userContainer = userContainer;
        }

        // Get login url for Steam and redirect to that link.
        public IActionResult Login()
        {
            string hostUrl = Request.Host.ToString();

            string loginUrl = SteamService.GetLoginUrl(hostUrl);

            return Redirect(loginUrl);
        }

        // When coming back from Steam the data we got will be validated and some additional data will be received for logging the user in the website.
        public IActionResult AfterLogin()
        {
            // Output data in a readable way we got from Steam (not important for final build). 
            var parameters = HttpUtility.ParseQueryString(Request.QueryString.ToString()); // Gets the current url of the page and parses the querystring into the variable.
            var items = parameters.AllKeys.SelectMany(parameters.GetValues, (k, v) => new { key = k, value = v });

            foreach (var item in items)
            {
                Console.WriteLine("{0} {1}", item.key, item.value);
            }

            string steamId = parameters["openid.identity"].Split('/').Last();

            // Validate the SteamID we got from Steam (data might be spoofed by the user).
            NameValueCollection queryStringData = HttpUtility.ParseQueryString(Request.QueryString.ToString());
            if (SteamService.ValidateSteamId(queryStringData).Result)
            {
                Dictionary<string, string> userSummaries = SteamService.GetUserSummaries(steamId).Result; // Get additional data for user login such as avatar and username.

                // Create user if it doesn't exist yet.
                UserModel user = new(Convert.ToInt64(steamId), userSummaries["personaname"], userSummaries["avatarmedium"], _userDal);

                bool isCreated = _userContainer.CreateUser(user); // Try to create new user if it doesnt exist yet.

                // Set all session data for the user.
                HttpContext.Session.SetString("SteamID", steamId);
                HttpContext.Session.SetString("UserName", userSummaries["personaname"]);
                HttpContext.Session.SetString("ProfilePic", userSummaries["avatarmedium"]);

                TempData["Success"] = "Logged in succesfully!";

                return RedirectToAction("Index", "Home");
            }

            TempData["Failure"] = "Something went wrong, if this error still occures after a few tries contact an administrator!";

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {


            return RedirectToAction("Index", "Home");
        }
    }
}
