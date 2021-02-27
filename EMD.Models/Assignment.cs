using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EMD.Models
{
    public class Assignment
    {
        [Key]
        public int AssignmentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Implement { get; set; }
        public DateTime DateRegistered { get; set; }
        public string Status { get; set; }
        public double Complexity { get; set; }
        public double Achievement { get; set; }
        public double DateEnd { get; set; }
        public int ParentId { get; set; }

        public virtual Assignment Parent { get; set; }
      
        public virtual ICollection<Assignment> Children { get; set; }
    }
}
