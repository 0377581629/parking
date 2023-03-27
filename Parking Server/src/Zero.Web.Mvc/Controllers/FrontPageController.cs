using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Zero.Identity;

namespace Zero.Web.Controllers
{
    public class FrontPageController : ZeroControllerBase
    {
        private readonly SignInManager _signInManager;

        public FrontPageController(SignInManager signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            return View("Index");
        }
    }
}