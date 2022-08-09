using Infrastructure.Model;
using Infrastructure.Service;
using IntefaceLogic.Model;
using InterfaceLogic.Inteface;
using InterfaceLogic.Model;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class MarketController : Controller
    {
        private readonly IUserContainer _userContainer;
        private readonly ICartContainer _cartContainer;

        public MarketController(IUserContainer userContainer, ICartContainer cartContainer)
        {
            _userContainer = userContainer;
            _cartContainer = cartContainer; 
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
                    MarketHashName = i.MarketHashName,
                    MarketRestriction = i.MarketRestriction,
                    Price = i.Price,
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

        [HttpPost]
        public IActionResult Cart(SkinInfoViewModel skin)
        {
            if (skin.Name == null)
            {
                TempData["Failure"] = "Something went wrong when adding item to cart, please try again later.";

                return RedirectToAction("Index", "Home");
            }

            // Add item to database cart table.
            CartItemModel item = new(skin.Name, skin.Price);

            if (_cartContainer.AddToCart(item))
            {
                TempData["Success"] = "Succesfully added item to cart.";

                // 
                // Check if session is empty with cart id
                // Get cart id based of uniqe skinid
                //

                return RedirectToAction("Cart", "Market");
            }

            TempData["Failure"] = "Something went wrong when adding item to cart, please try again later.";

            return View();
        }
    }
}
