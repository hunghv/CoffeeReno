using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Services.ViewModels;

namespace CoffeeReno.Common
{
    public class CoffeeRenoControllerBase: Controller
    {
        public CoffeeRenoControllerBase()
        {

        }
        protected async Task<SignedModel> SignInedModel()
        {
            var authResult = await HttpContext.AuthenticateAsync();
            return null;
        }

    }

    
}
