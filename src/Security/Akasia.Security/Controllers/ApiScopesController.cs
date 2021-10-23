using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Akasia.Security.Controllers
{
    [Authorize]
    public class ApiScopesController : Controller
    {
        private readonly ILogger<ApiScopesController> _logger;

        public ApiScopesController(ILogger<ApiScopesController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
