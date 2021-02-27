using EDM.DataModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDM.WebUI.Controllers
{
    //[Route("api/")]
    public class APIController : Controller
    {
        AppDbContext context;

        public APIController(AppDbContext _context)
        {
            context = _context;
        }

        [Route("api/assignments")]
        public JsonResult Assignments()
        {
            var data = context.Assignments.Include(t => t.RefChildren).Where(t=> t.Parent == null);
            return Json(data);
        }

        [Route("api/assignments/{id}")]
        public JsonResult Assignments(int id)
        {
            return Json(context.Assignments.FirstOrDefault(t => t.Id == id)?.RefChildren);
        }

    }
}
