using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeReno.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeReno.Controllers
{
    [Route("profile")]
    public class ProfileController : Controller 
    {
        [Route("")]
        [Authorize]
        public IActionResult Index()
        {
            var vm = new ProfileViewModel
            {
                Claims = User.Claims,
                Name = User.Identity.Name
            };
            return View(vm);
        }
    }
}