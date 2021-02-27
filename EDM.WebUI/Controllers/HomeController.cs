using EDM.DataModel;
using EDM.WebUI.Interfaces;
using EDM.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EDM.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAssigmentRep _assigmentrep;
        public IEnumerable<Assignment> assignment { get; set; }

        public HomeController(ILogger<HomeController> logger , IAssigmentRep assigmentRep )
        {
            _logger = logger;
            _assigmentrep = assigmentRep;
        }

        public IActionResult Index()
        {
            assignment = _assigmentrep.GetAllAssigments();

            return View(assignment);
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
