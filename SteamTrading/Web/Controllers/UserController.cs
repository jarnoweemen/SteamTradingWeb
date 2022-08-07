using Infrastructure.Model;
using Infrastructure.Service;
using IntefaceLogic.Model;
using InterfaceLogic.Inteface;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserContainer _userContainer;

        public UserController(IUserContainer userContainer)
        {
            _userContainer = userContainer;
        }

        public IActionResult Inventory()
        {
            if (HttpContext.Session.GetString("SteamID") != null)
            {
                // Get profile url
                UserModel? user = _userContainer.GetUser(Convert.ToInt64(HttpContext.Session.GetString("SteamID")));

                if (user == null)
                {
                    TempData["Failure"] = "Couldn't find your account, if this error persists please contact an administrator. You could also try logging out and in again.";

                    return RedirectToAction("Index", "Home");
                }

                IEnumerable<WeaponInfoModel>? inventoryItems = SteamService.GetUserInventoryByName(user.ProfileUrl).Result;

                if (inventoryItems == null)
                {
                    TempData["Failure"] = "Couldn't get user inventory.";

                    return RedirectToAction("Index", "Home");   
                }

                IEnumerable<SkinInfoViewModel> inventoryItemsModel = inventoryItems.Select(i => new SkinInfoViewModel
                {
                    Name = i.Name,
                    IconUrl = i.IconUrl,
                    Type = i.Type,
                    MarketRestriction = i.MarketRestriction,
                    Tradable = i.Tradable,
                    Marketable = i.Marketable,
                    Buyable = i.Buyable
                });

                return View(inventoryItemsModel);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
