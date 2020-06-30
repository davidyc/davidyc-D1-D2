﻿using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;
using MusicStoreLogger;
using MvcMusicStore.Models;
using NLog.Fluent;

namespace MvcMusicStore.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private const string PromoCode = "FREE";
        private readonly ILogger _logger;

        private readonly MusicStoreEntities _storeContext = new MusicStoreEntities();

        public CheckoutController(ILogger logger)
        {          
            _logger = logger;
            _logger.Debug("Controller Checkout was created");
        }

        // GET: /Checkout/
        public ActionResult AddressAndPayment()
        {
            _logger.Info("CheckoutController AddressAndPayment method");
            return View();
        }

        // POST: /Checkout/AddressAndPayment
        [HttpPost]
        public async Task<ActionResult> AddressAndPayment(FormCollection values)
        {
            var order = new Order();
            TryUpdateModel(order);

            if (ModelState.IsValid 
                && string.Equals(values["PromoCode"], PromoCode, StringComparison.OrdinalIgnoreCase))
            {
                order.Username = User.Identity.Name;
                order.OrderDate = DateTime.Now;

                _storeContext.Orders.Add(order);

                await ShoppingCart.GetCart(_storeContext, this).CreateOrder(order);

                await _storeContext.SaveChangesAsync();
                _logger.Debug($"Order ID = {order.OrderId}");
                return RedirectToAction("Complete", new { id = order.OrderId });
            }

            return View(order);
        }

        // GET: /Checkout/Complete
        public async Task<ActionResult> Complete(int id)
        {
            return await _storeContext.Orders.AnyAsync(o => o.OrderId == id && o.Username == User.Identity.Name)
                ? View(id)
                : View("Error");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _storeContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}