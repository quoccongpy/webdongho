using DongHo.DataAcess.IRepository;
using DongHo.Model;
using DongHo.Model.ViewModels;
using DongHo.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DongHo.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    [BindProperties]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
       
        public ShoppingCartVM ShoppingCartVM { get; set; }
        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            ShoppingCartVM = new ShoppingCartVM()
            {
                listCart = _unitOfWork.ShoppingCart.GetAll(a => a.ApplicationUserId == claim.Value, includeProperties: "Product"),
                OrderHeader = new Model.OrderHeader(),
            };

            foreach(var cart in ShoppingCartVM.listCart)
            {
                cart.Price = GetPriceBaseOnQuantity(cart.Count,cart.Product.Price,cart.Product.Price5,cart.Product.Price10);
                //ShoppingCartVM.cartTotal += (cart.Price * cart.Count);
                ShoppingCartVM.OrderHeader.OrderTotal+= (cart.Price * cart.Count);
            }
            return View(ShoppingCartVM);
        }


        private double GetPriceBaseOnQuantity(double quantity, double price, double price5, double price10)
        {
            if (quantity <= 5)
            {
                return price;
            }
            else
            {
                if (quantity <= 10)
                {
                    return price5;
                }
                return price10;
            }
        }



        public IActionResult Plus(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(a => a.Id == cartId);
            _unitOfWork.ShoppingCart.PlusCount(cart, 1);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Minus(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(a => a.Id == cartId);
            if (cart.Count <= 1)
            {
                _unitOfWork.ShoppingCart.Remove(cart);
            }
            else
            {
                _unitOfWork.ShoppingCart.MinusCount(cart, 1);
            }
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Remove(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(a => a.Id == cartId);
            _unitOfWork.ShoppingCart.Remove(cart);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Summary()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            ShoppingCartVM = new ShoppingCartVM()
            {
                listCart = _unitOfWork.ShoppingCart.GetAll(a => a.ApplicationUserId == claim.Value, includeProperties: "Product"),
                OrderHeader = new Model.OrderHeader(),
            };
            ShoppingCartVM.OrderHeader.ApplicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(a => a.Id == claim.Value);
            ShoppingCartVM.OrderHeader.PhoneNumber = ShoppingCartVM.OrderHeader.ApplicationUser.PhoneNumber;
            ShoppingCartVM.OrderHeader.StreetAddress = ShoppingCartVM.OrderHeader.ApplicationUser.StreetAddress;
            ShoppingCartVM.OrderHeader.Ward = ShoppingCartVM.OrderHeader.ApplicationUser.Ward;
            ShoppingCartVM.OrderHeader.Province = ShoppingCartVM.OrderHeader.ApplicationUser.Province;
            ShoppingCartVM.OrderHeader.District = ShoppingCartVM.OrderHeader.ApplicationUser.District;
            ShoppingCartVM.OrderHeader.Name = ShoppingCartVM.OrderHeader.ApplicationUser.Name;

            foreach (var cart in ShoppingCartVM.listCart)
            {
                cart.Price = GetPriceBaseOnQuantity(cart.Count, cart.Product.Price, cart.Product.Price5, cart.Product.Price10);
                ShoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
            }
            return View(ShoppingCartVM);
        }

        [ActionName("Summary")]
        [HttpPost]
        public IActionResult SummaryPost()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            ShoppingCartVM.listCart = _unitOfWork.ShoppingCart.GetAll(a=>a.ApplicationUserId==claim.Value,includeProperties:"Product");

            //ShoppingCartVM = new ShoppingCartVM()
            //{
            //    OrderHeader = new Model.OrderHeader(),
            //};

            ShoppingCartVM.OrderHeader.PaymentStatus = SD.PaymentStatusPending;
            ShoppingCartVM.OrderHeader.OrderStatus = SD.StatusPending;
            ShoppingCartVM.OrderHeader.OrderDate = DateTime.Now;
            ShoppingCartVM.OrderHeader.ApplicationUserId = claim.Value;

            foreach (var cart in ShoppingCartVM.listCart)
            {
                cart.Price = GetPriceBaseOnQuantity(cart.Count, cart.Product.Price, cart.Product.Price5, cart.Product.Price10);
                ShoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
            }

            _unitOfWork.OrderHeader.Add(ShoppingCartVM.OrderHeader);
            _unitOfWork.Save();

            foreach(var cartDetail in ShoppingCartVM.listCart)
            {
                OrderDetail orderDetail = new OrderDetail()
                {
                    ProductId = cartDetail.ProductId,
                    OrderId = ShoppingCartVM.OrderHeader.Id,
                    Price = cartDetail.Price,
                    Count = cartDetail.Count,
                };
                _unitOfWork.OrderDetail.Add(orderDetail);
                _unitOfWork.Save();
            }

            _unitOfWork.ShoppingCart.RemoveRange(ShoppingCartVM.listCart);
            _unitOfWork.Save();
            return RedirectToAction("Index", "Home");
        }

    }
}
