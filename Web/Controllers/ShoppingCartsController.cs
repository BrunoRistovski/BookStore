using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Domain_Models;
using Repository;
using Service.Interface;
using System.Security.Claims;
using Microsoft.CodeAnalysis;

namespace Web.Controllers
{
    public class ShoppingCartsController : Controller
    {
        private readonly IShoppingCartsService _shoppingCartService;

        public ShoppingCartsController(IShoppingCartsService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        // GET: ShoppingCarts
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? null;

            return View(_shoppingCartService.getShoppingCartDetails(userId ?? ""));
        }

        // GET: ShoppingCarts/Delete/5
        public async Task<IActionResult> DeleteBookFromShoppingCart(Guid? bookId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? null;

            var result = _shoppingCartService.deleteFromShoppingCart(userId, bookId);

            return RedirectToAction("Index", "ShoppingCarts");
        }

        public Boolean Order()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? null;

            var result = _shoppingCartService.orderBooks(userId ?? "");

            return result;
        }

        public IActionResult PayOrder(string stripeEmail, string stripeToken)
        {

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";

            var cart = _shoppingCartService.getShoppingCartDetails(userId);

                this.Order();
                return RedirectToAction("SuccessPayment");
        }

        public IActionResult SuccessPayment()
        {
            return View();
        }

    }
}
