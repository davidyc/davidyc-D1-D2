using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using MusicStoreLogger;
using MvcMusicStore.Models;
using MvcMusicStore.ViewModels;

namespace MvcMusicStore.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly MusicStoreEntities _storeContext = new MusicStoreEntities();
        private readonly ILogger _logger;

        public ShoppingCartController(ILogger logger)
        {
            _logger = logger;
            _logger.Debug("Controller  Shopping was created");
        }

        // GET: /ShoppingCart/
        public async Task<ActionResult> Index()
        {
            _logger.Info("ShoppingCartController Index method");
            var cart = ShoppingCart.GetCart(_storeContext, this);

            var viewModel = new ShoppingCartViewModel
            {
                CartItems = await cart.GetCartItems().ToListAsync(),
                CartTotal = await cart.GetTotal()
            };

            return View(viewModel);
        }

        // GET: /ShoppingCart/AddToCart/5
        public async Task<ActionResult> AddToCart(int id)
        {
            _logger.Info("ShoppingCartController AddToCart method");
            var cart = ShoppingCart.GetCart(_storeContext, this);

            await cart.AddToCart(await _storeContext.Albums.SingleAsync(a => a.AlbumId == id));

            await _storeContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // AJAX: /ShoppingCart/RemoveFromCart/5
        [HttpPost]
        public async Task<ActionResult> RemoveFromCart(int id)
        {
            _logger.Info("ShoppingCartController RemoveFromCart method");
            var cart = ShoppingCart.GetCart(_storeContext, this);

            var albumName = await _storeContext.Carts
                .Where(i => i.RecordId == id)
                .Select(i => i.Album.Title)
                .SingleOrDefaultAsync();

            var itemCount = await cart.RemoveFromCart(id);

            await _storeContext.SaveChangesAsync();

            var removed = (itemCount > 0) ? " 1 copy of " : string.Empty;

            var results = new ShoppingCartRemoveViewModel
            {
                Message = removed + albumName + " has been removed from your shopping cart.",
                CartTotal = await cart.GetTotal(),
                CartCount = await cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };

            return Json(results);
        }

        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            _logger.Info("ShoppingCartController CartSummary method");
            var cart = ShoppingCart.GetCart(_storeContext, this);

            var cartItems = cart.GetCartItems()
                .Select(a => a.Album.Title)
                .OrderBy(x => x)
                .ToList();

            ViewBag.CartCount = cartItems.Count();
            ViewBag.CartSummary = string.Join("\n", cartItems.Distinct());

            return PartialView("CartSummary");
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
