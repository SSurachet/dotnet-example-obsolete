
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service.Models;
using Service.Services;

namespace API.Controllers
{
    [Route("api/auth")]
    public class AuthenticationController : BaseController
    {
        private AuthenticationService authenticationService;

        public AuthenticationController(AuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        [Route("login")]
        [HttpPost]
        public async Task<ActionResult<LoginTokenModel>> Login(LoginModel model)
        {
            var result = await authenticationService.Login(model);
            return Ok(result);
        }

        [Route("register")]
        [HttpPost]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            await authenticationService.Register(model);
            return Ok();
        }
    }
}