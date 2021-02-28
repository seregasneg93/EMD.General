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
        private readonly AppDbContext context;

        public HomeController(ILogger<HomeController> logger , AppDbContext _context )
        {
            _logger = logger;
            context = _context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("home/create/{parentId?}")]
        public IActionResult Create([FromRoute] int? parentId) // parentId
        {
            var data = context.Assignments.FirstOrDefault(t => parentId.HasValue && t.Id == parentId);

            if(data != null)
            {
                ViewBag.IsParent = true;
                ViewBag.ParentName = data.Name;
            }

            return View("CreateOrEdit");
        }

        [HttpPost]
        [Route("home/create/{parentId?}")]
        public IActionResult Create([FromRoute]int? parentId, AssignmentModel assignment) // parentId
        {
            Assignment parent = null;
            if (parentId.HasValue)
                parent = context.Assignments.FirstOrDefault(t => parentId.HasValue && t.Id == parentId);

            if (ModelState.IsValid)
            {
                var entity = new Assignment
                {
                    Achievement = assignment.Achievement,
                    Complexity = assignment.Complexity,
                    DateEnd = assignment.DateEnd,
                    Description = assignment.Description,
                    Name = assignment.Name,
                    Implement = assignment.Implement,
                    Status = WorkStatus.Assigned,
                    DateRegistered = DateTime.Now,
                    Parent = parent
                };
                context.Add(entity);
                context.SaveChanges();

                return Redirect("/home/index");
            }

            if (parent != null)
            {
                ViewBag.IsParent = true;
                ViewBag.ParentName = parent.Name;
            }

            return View("CreateOrEdit");
        }

        [Route("home/edit/{id}")]
        [HttpGet]
        public IActionResult Edit([FromRoute] int id)
        {
            var assignment = context.Assignments.FirstOrDefault(t => t.Id == id);

            if(assignment == null)
                return Redirect("/home/index");

            return View("CreateOrEdit", assignment);
        }

        [Route("home/edit/{id}")]
        [HttpPost]
        public IActionResult Edit([FromRoute] int id, AssignmentModel assignment)
        {
            var oldEntity = context.Assignments.FirstOrDefault(t => t.Id == id);

            if (oldEntity != null && ModelState.IsValid)
            {

                oldEntity.Achievement = assignment.Achievement;
                oldEntity.Complexity = assignment.Complexity;
                oldEntity.DateEnd = assignment.DateEnd;
                oldEntity.Description = assignment.Description;
                oldEntity.Name = assignment.Name;
                oldEntity.Implement = assignment.Implement;

                context.SaveChanges();

                return Redirect("/home/index");
            }
                

            return View("CreateOrEdit");
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
