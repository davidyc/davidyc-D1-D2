using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplicationForCodeTest.Controllers
{
    public class WithMethodAttibuteController : Controller
    {
        // GET: WithMethodAttibute
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        private ActionResult Index2()
        {
            return View();
        }
    }
}