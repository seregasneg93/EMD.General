using EDM.DataModel;
using EDM.WebUI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDM.WebUI.Models
{
    public class AssigmentRep : IAssigmentRep
    {
        private readonly AppDbContext _context;

        public AssigmentRep(AppDbContext context)
        {
            _context = context;
        }

        public void Test()
        {
            
        }

        IEnumerable<Assignment> IAssigmentRep.GetAllAssigments()
        {
            return _context.Assignments.ToList();
        }
    }
}
