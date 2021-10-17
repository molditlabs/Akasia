using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Akasia.Security.Controllers
{
    public class ApiResourcesController : Controller
    {
        private readonly ILogger<ApiResourcesController> _logger;

        public ApiResourcesController(ILogger<ApiResourcesController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
