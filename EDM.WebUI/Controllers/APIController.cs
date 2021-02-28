using EDM.DataModel;
using EDM.WebUI.Models;
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

        TransitionModel[] Transitions = new TransitionModel[]
        {
            new TransitionModel{ From = WorkStatus.Assigned, To = WorkStatus.InProgress },
            new TransitionModel{ From = WorkStatus.InProgress, To = WorkStatus.Completed },
            new TransitionModel{ From = WorkStatus.InProgress, To = WorkStatus.Stopped },
            new TransitionModel{ From = WorkStatus.Stopped, To = WorkStatus.InProgress }
        };

        public APIController(AppDbContext _context)
        {
            context = _context;
        }

        [Route("api/assignments")]
        public JsonResult Assignments()
        {
            context.Assignments.Load();
            var data = context.Assignments.Where(t=> t.Parent == null);
            return Json(data);
        }

        [Route("api/status_res")]
        public JsonResult GetStatusResources()
        {
            return Json(new[] {
                new {
                    Name = "assigned",
                    Value = Resource.WorkStatus_Assigned },
                new {
                    Name = "inProgress",
                    Value = Resource.WorkStatus_InProgress },
                new {
                    Name = "stopped",
                    Value = Resource.WorkStatus_Stopped },
                new {
                    Name = "completed",
                    Value = Resource.WorkStatus_Completed },
            });
        }

        bool CheckStatusForComplete(Assignment assignment)
        {
            if ((assignment.RefChildren?.Count ?? 0) > 0)
                return assignment.Status == WorkStatus.Completed ||
                    (Transitions.Any(t => t.From == assignment.Status && t.To == WorkStatus.Completed)
                    && assignment.RefChildren.All(child =>
                        CheckStatusForComplete(child)));

            return assignment.Status == WorkStatus.Completed 
                || Transitions.Any(t => t.From == assignment.Status && t.To == WorkStatus.Completed);
        }

        void ChangeStatusToComplete(Assignment assignment)
        {
            assignment.Status = WorkStatus.Completed;
            if(assignment.RefChildren != null)
                foreach (var child in assignment.RefChildren)
                    ChangeStatusToComplete(child);
        }

        [Route("api/change_status")]
        public JsonResult ChangeStatus([FromQuery]int id, [FromQuery]WorkStatus newStatus)
        {
            context.Assignments.Load();

            var entity = context.Assignments.FirstOrDefault(t => t.Id == id);

            if (entity == null)
                return Json(new { 
                    Status = "Error",
                    Message = "Assignment does not exist"
                });

            if(Transitions.Any(t => t.From == entity.Status && t.To == newStatus))
            {
                if (newStatus != WorkStatus.Completed)
                    entity.Status = newStatus;
                else
                {
                    if (CheckStatusForComplete(entity))
                    {
                        ChangeStatusToComplete(entity);
                    }
                    else
                        return Json(new
                        {
                            Status = "Error",
                            Message = "Sub Assignment can not be completed"
                        });
                }

                context.SaveChanges();

                return Json(new
                {
                    Status = "Ok",
                    Message = ""
                });
            }
            else
                return Json(new
                {
                    Status = "Error",
                    Message = "Transition not allowed"
                });
        }

        void RemoveChilds(Assignment assignment)
        {
            context.Remove(assignment);
            if (assignment.RefChildren != null)
                foreach (var child in assignment.RefChildren)
                    RemoveChilds(child);
        }

        [Route("api/remove")]
        public IActionResult RemoveTask([FromQuery]int id)
        {
            context.Assignments.Load();

            var entity = context.Assignments.FirstOrDefault(t => t.Id == id);

            if (entity == null)
                return Json(new
                {
                    Status = "Error",
                    Message = "Assignment does not exist"
                });

            RemoveChilds(entity);
            context.SaveChanges();

            return Json(new
            {
                Status = "Ok",
                Message = ""
            });
        }
    }
}
