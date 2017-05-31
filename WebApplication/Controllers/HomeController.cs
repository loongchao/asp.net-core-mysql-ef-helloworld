﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            WebApplication2.Model.Employee e = new Model.Employee();
            e.Name = "AAAAAAAAAAAA";
            return View(e);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View("Index");
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
