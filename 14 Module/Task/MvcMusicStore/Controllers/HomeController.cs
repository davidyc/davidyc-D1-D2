using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using MvcMusicStore.Models;
using MusicStoreLogger;


namespace MvcMusicStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly MusicStoreEntities _storeContext = new MusicStoreEntities();
        private readonly ILogger _logger;
        public HomeController(ILogger logger)
        {
            _logger = logger;
            _logger.Debug("Controller home was created");
        }
        // GET: /Home/
        public async Task<ActionResult> Index()
        {
            _logger.Info("Homecontroller Index method");

            return View(await _storeContext.Albums
                .OrderByDescending(a => a.OrderDetails.Count())
                .Take(6)
                .ToListAsync());
            
        }

        protected override void Dispose(bool disposing)
        {
            _logger.Info("Homecontroller Index method dispose");
            if (disposing)
            {
                _storeContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}