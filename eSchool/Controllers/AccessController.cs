using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eSchool.Models;


namespace eSchool.Controllers
{
    public class AccessController : Controller
    {
        public IActionResult Denied()
        {
            return View();
        }

    }
}
