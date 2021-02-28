using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EDM.WebUI.Models
{
    public class AssignmentModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Не указано имя")]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Implement { get; set; }
        public DateTime DateRegistered { get; set; }
        public string Status { get; set; }
        public double Complexity { get; set; }
        public double Achievement { get; set; }
        public DateTime DateEnd { get; set; }
    }
}
