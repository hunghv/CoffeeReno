using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CoffeeReno.Common;
using Microsoft.AspNetCore.Mvc;
using CoffeeReno.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Services.Interfaces;

namespace CoffeeReno.Controllers
{
    [AllowAnonymous]
    public class HomeController : CoffeeRenoControllerBase
    {
        #region Contructor, Fields
        private readonly IAdminServices _adminServices;
        private readonly ILogger _logger;
        private readonly ClaimsPrincipal _user;
        public IList<AuthenticationScheme> ExternalLogins { get; set; }
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HomeController(IAdminServices adminServices, ILogger<HomeController> logger, IHttpContextAccessor contextAccessor)
        {
            _adminServices = adminServices;
            _logger = logger;
            _httpContextAccessor = contextAccessor;
        }

        #endregion
        public IDictionary<string, string> AuthProperties { get; set; }
        [Route("")]
        public async Task<IActionResult> Index()
        {
            var authResult = await HttpContext.AuthenticateAsync();

            var vm = new ProfileViewModel
            {
                Claims = authResult.Principal.Claims,
                Name = authResult.Principal.Identity?.Name
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
