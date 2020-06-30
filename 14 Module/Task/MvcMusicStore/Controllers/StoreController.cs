using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using MusicStoreLogger;
using MvcMusicStore.Models;

namespace MvcMusicStore.Controllers
{
    public class StoreController : Controller
    {
        private readonly MusicStoreEntities _storeContext = new MusicStoreEntities();
        private readonly ILogger _logger;

        public StoreController(ILogger logger)
        {
            _logger = logger;
            _logger.Debug("Controller Store was created");
        }

        // GET: /Store/
        public async Task<ActionResult> Index()
        {
            _logger.Info("StoreController Index method");
            return View(await _storeContext.Genres.ToListAsync());
        }

        // GET: /Store/Browse?genre=Disco
        public async Task<ActionResult> Browse(string genre)
        {
            _logger.Info("StoreController Browse method");
            return View(await _storeContext.Genres.Include("Albums").SingleAsync(g => g.Name == genre));
        }

        public async Task<ActionResult> Details(int id)
        {
            _logger.Info("StoreController Details method");
            var album = await _storeContext.Albums.FindAsync(id);

            return album != null ? View(album) : (ActionResult)HttpNotFound();
        }

        [ChildActionOnly]
        public ActionResult GenreMenu()
        {
            _logger.Info("StoreController GenreMenu method");
            return PartialView(
                _storeContext.Genres.OrderByDescending(
                    g => g.Albums.Sum(a => a.OrderDetails.Sum(od => od.Quantity))).Take(9).ToList());
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