﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Akasia.Security.Controllers
{
    public class ClientsController : Controller
    {
        private readonly ILogger<IdentityResourcesController> _logger;

        public ClientsController(ILogger<IdentityResourcesController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
