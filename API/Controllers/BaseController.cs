

using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
namespace API.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        public BaseController()
        {
            if (ModelState.IsValid == false)
            {
                var modelErrors = ModelState
                    .Select(a => a.Value.Errors)
                    .Where(a => a.Count > 0)
                    .ToList();

                throw new ArgumentException(JsonConvert.SerializeObject(modelErrors));
            }
        }
    }
}