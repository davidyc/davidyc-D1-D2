﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplicationForCodeTest.Controllers
{
    [Authorize]
    public class WithOut : Controller
    {
        // GET: WithOut
        public ActionResult Index()
        {
            return View();
        }
    }
}