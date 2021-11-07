
using System;
using Core.Exceptions;
using Core.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/system")]
    public class SystemController : BaseController
    {
        [Route("datetime")]
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<DateTime> GetSystemDateTime()
        {
            return DateTime.UtcNow;
        }

        [Route("error")]
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<ErrorResponseModel> Error()
        {
            throw new CustomException("MESSAGE", "TITLE", "CODE");
        }
    }
}