using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplicationForCodeTest.Controllers
{
    public class WithOutAttributeController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }
    }
}