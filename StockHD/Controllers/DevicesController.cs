﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StockHD.Data;
using StockHD.Models;
using System.Diagnostics;


namespace StockHD.Controllers
{
    public class DevicesController : Controller
    {

       

        public IActionResult Index()
        {
            return View();
        }
    }
}
