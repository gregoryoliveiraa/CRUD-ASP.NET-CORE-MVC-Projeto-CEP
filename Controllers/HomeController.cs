using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using e.mix.Models;
using e.mix.repository;

namespace e.mix.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CEPContext _cEPContext;

        public HomeController(ILogger<HomeController> logger, CEPContext cEPContext)
        {
            _logger = logger;
            _cEPContext = cEPContext;
        }

        public IActionResult Index()
        {
            var teste = _cEPContext.Cep.Find(1);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
