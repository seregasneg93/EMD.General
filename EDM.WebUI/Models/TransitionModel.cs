using EDM.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDM.WebUI.Models
{
    public class TransitionModel
    {
        public WorkStatus From { get; set; }
        public WorkStatus To { get; set; }

    }
}
