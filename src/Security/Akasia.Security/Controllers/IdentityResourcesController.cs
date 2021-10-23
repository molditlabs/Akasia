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
    public class IdentityResourcesController : Controller
    {
        private readonly ILogger<IdentityResourcesController> _logger;

        public IdentityResourcesController(ILogger<IdentityResourcesController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
