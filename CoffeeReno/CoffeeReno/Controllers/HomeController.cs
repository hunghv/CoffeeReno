using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoffeeReno.Models;
using Microsoft.Extensions.Logging;
using Services.Interfaces;
using Services.ViewModels.Request;

namespace CoffeeReno.Controllers
{
    public class HomeController : Controller
    {
        #region Contructor, Fields

        private readonly IAdminServices _adminServices;
        private readonly ILogger _logger;
        public HomeController(IAdminServices adminServices, ILogger<HomeController> logger)
        {
            _adminServices = adminServices;
            _logger = logger;
        }

        #endregion

        public async Task<IActionResult> Index()
        {
            var a = await _adminServices.SaveBlog(new UserProfileModel
            {
                Name = "hunghv2"
            });

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
