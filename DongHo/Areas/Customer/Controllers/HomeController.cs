using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DongHo.DataAcess.IRepository;
using DongHo.Model;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace DongHo.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(ILogger<HomeController> logger,IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var data = _unitOfWork.Product.GetAll(includeProperties:"Category,CoverType,Brand");
            return View(data);
        }
        public IActionResult Details(int productId)
        {
            ShoppingCart cartObj = new ShoppingCart()
            {
                Product = _unitOfWork.Product.GetFirstOrDefault(a => a.Id == productId, includeProperties: "Category,CoverType,Brand"),
                Count = 1,
                ProductId = productId,
            };

                return View(cartObj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            shoppingCart.ApplicationUserId = claim.Value;

            ShoppingCart cartObj = _unitOfWork.ShoppingCart.GetFirstOrDefault(a => a.ApplicationUserId == claim.Value
                                                && a.ProductId == shoppingCart.ProductId);
            if (cartObj == null)
            {
                _unitOfWork.ShoppingCart.Add(shoppingCart);
            }
            else
            {
                _unitOfWork.ShoppingCart.PlusCount(cartObj, shoppingCart.Count);
            }

            
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
