using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using MusicStoreLogger;
using MvcMusicStore.Models;

namespace MvcMusicStore.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class StoreManagerController : Controller
    {
        private readonly MusicStoreEntities _storeContext = new MusicStoreEntities();
        private readonly ILogger _logger;

        public StoreManagerController(ILogger logger)
        {
            _logger = logger;
        }

        // GET: /StoreManager/
        public async Task<ActionResult> Index()
        {
            _logger.Info("StoreManagerController Index method");
            return View(await _storeContext.Albums
                .Include(a => a.Genre)
                .Include(a => a.Artist)
                .OrderBy(a => a.Price).ToListAsync());
        }

        // GET: /StoreManager/Details/5
        public async Task<ActionResult> Details(int id = 0)
        {
            _logger.Info("StoreManagerController Details method");
            var album = await _storeContext.Albums.FindAsync(id);
            
            if (album == null)
            {
                return HttpNotFound();
            }

            return View(album);
        }

        // GET: /StoreManager/Create
        public async Task<ActionResult> Create()
        {
            _logger.Info("StoreManagerController Create method");
            return await BuildView(null);
        }

        // POST: /StoreManager/Create
        [HttpPost]
        public async Task<ActionResult> Create(Album album)
        {
            _logger.Info("StoreManagerController Create method");
            if (ModelState.IsValid)
            {
                _storeContext.Albums.Add(album);
                
                await _storeContext.SaveChangesAsync();
                
                return RedirectToAction("Index");
            }

            return await BuildView(album);
        }

        // GET: /StoreManager/Edit/5
        public async Task<ActionResult> Edit(int id = 0)
        {
            _logger.Info("StoreManagerController Edit method");
            var album = await _storeContext.Albums.FindAsync(id);
            if (album == null)
            {
                return HttpNotFound();
            }

            return await BuildView(album);
        }

        // POST: /StoreManager/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(Album album)
        {
            _logger.Info("StoreManagerController Edit method");
            if (ModelState.IsValid)
            {
                _storeContext.Entry(album).State = EntityState.Modified;

                await _storeContext.SaveChangesAsync();
                
                return RedirectToAction("Index");
            }

            return await BuildView(album);
        }

        // GET: /StoreManager/Delete/5
        public async Task<ActionResult> Delete(int id = 0)
        {
            _logger.Info("StoreManagerController Delete method");
            var album = await _storeContext.Albums.FindAsync(id);
            if (album == null)
            {
                return HttpNotFound();
            }

            return View(album);
        }

        // POST: /StoreManager/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            _logger.Info("StoreManagerController DeleteConfirmed method");
            var album = await _storeContext.Albums.FindAsync(id);
            if (album == null)
            {
                return HttpNotFound();
            }

            _storeContext.Albums.Remove(album);

            await _storeContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        private async Task<ActionResult> BuildView(Album album)
        {
            _logger.Info("StoreManagerController BuildView method");
            ViewBag.GenreId = new SelectList(
                await _storeContext.Genres.ToListAsync(),
                "GenreId",
                "Name",
                album == null ? null : (object)album.GenreId);

            ViewBag.ArtistId = new SelectList(
                await _storeContext.Artists.ToListAsync(),
                "ArtistId",
                "Name",
                album == null ? null : (object)album.ArtistId);

            return View(album);
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