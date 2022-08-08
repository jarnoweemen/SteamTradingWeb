using Infrastructure.Model;
using Infrastructure.Service;
using IntefaceLogic.Model;
using InterfaceLogic.Inteface;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class MarketController : Controller
    {
        private readonly IUserContainer _userContainer;

        public MarketController(IUserContainer userContainer)
        {
            _userContainer = userContainer;
        }

        public IActionResult Buy()
        {
            // Get all bot accounts
            IEnumerable<UserModel>? userModels = _userContainer.GetAllBots();

            if (userModels == null)
            {
                TempData["Failure"] = "There is nothing for sale at the moment";

                return RedirectToAction("Index", "Home");
            }

            // Get all inventories of those bots
            List<IEnumerable<SkinInfoViewModel>> skinsViewModel = new();

            foreach (UserModel userModel in userModels)
            {
                IEnumerable<SkinInfoModel>? skins = SteamService.GetUserInventoryByName(userModel.ProfileUrl).Result;

                if (skins == null) break;

                IEnumerable<SkinInfoViewModel> skinViewModel = skins.Select(i => new SkinInfoViewModel()
                {
                    Name = i.Name,
                    IconUrl = i.IconUrl,
                    Type = i.Type,
                    ItemCategory = i.ItemCategory,
                    MarketRestriction = i.MarketRestriction,
                    Tradable = i.Tradable,
                    Marketable = i.Marketable
                });

                skinsViewModel.Add(skinViewModel);
            }

            return View(skinsViewModel);
        }

        public IActionResult Cart()
        {
            return View();
        }
    }
}
