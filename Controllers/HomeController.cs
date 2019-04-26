using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MagicBall.Models;

namespace MagicBall.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
          return View();
        }

        [HttpPost("question")]
        public IActionResult Question()
        {
          return RedirectToAction("Index");
        }


    }
}
