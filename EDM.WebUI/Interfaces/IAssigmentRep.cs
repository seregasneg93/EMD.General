using EDM.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDM.WebUI.Interfaces
{
  public interface IAssigmentRep
    {
        IEnumerable<Assignment> GetAllAssigments();
    }
}
