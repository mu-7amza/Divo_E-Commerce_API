using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Divo.Errors;
using Microsoft.AspNetCore.Mvc;
using PL.Divo.Controllers;

namespace Divo.Controllers
{
    [ApiController]
    [Route("/error/{code}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : BaseApiController
    {
        [HttpGet]
        public IActionResult Error(int code)
        {
            return new ObjectResult(new ApiErrorResponse(code));
        }
    }
}