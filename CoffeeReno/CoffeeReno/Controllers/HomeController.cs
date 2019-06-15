using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoffeeReno.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Services.Interfaces;

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

        [Route("")]
        public async Task<IActionResult> Index()
        {
            //    var a = await _adminServices.SaveBlog(new UserProfileModel
            //    {
            //        Name = "hunghv2"
            //    });
            var vm = new ProfileViewModel
            {
                Claims = User?.Claims,
                Name = User?.Identity?.Name
            };

            return View(vm);
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
