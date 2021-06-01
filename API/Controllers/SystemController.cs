
using System;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/system")]
    public class SystemController : BaseController
    {
        [Route("datetime")]
        [HttpGet]
        public ActionResult<DateTime> GetSystemDateTime()
        {
            return DateTime.UtcNow;
        }
    }
}