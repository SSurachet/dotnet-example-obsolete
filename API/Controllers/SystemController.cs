
using System;
using Core.Exceptions;
using Core.Extensions.IHostEnvironmentExtension;
using Core.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace API.Controllers
{
    [Route("api/system")]
    public class SystemController : BaseController
    {
        private IHostEnvironment HostEnvironment { get; }
        public SystemController(IHostEnvironment hostEnvironment)
        {
            HostEnvironment = hostEnvironment;
        }

        [Route("datetime")]
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<DateTime> GetSystemDateTime()
        {
            return Ok(DateTime.UtcNow);
        }

        [Route("error")]
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<ErrorResponseModel> Error()
        {
            throw new CustomException("MESSAGE", "TITLE", "CODE");
        }

        [Route("env")]
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<string> Env()
        {
            var configuration = HostEnvironment.BuildConfiguration();
            configuration.GetSection("JWT:ValidIssuer").Get<string>();
            configuration.GetSection("JWT:Secret").Get<string>();
            return Ok(configuration.GetSection("JWT:ValidAudience").Get<string>());
        }
    }
}