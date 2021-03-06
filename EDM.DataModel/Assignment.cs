﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EDM.DataModel
{
    public class Assignment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Implement { get; set; }
        public DateTime DateRegistered { get; set; }
        public WorkStatus Status { get; set; }
        public double Complexity { get; set; }
        public double Achievement { get; set; }
        public DateTime DateEnd { get; set; }

        [JsonIgnore]
        public virtual Assignment Parent { get; set; }
        [InverseProperty("Parent")]
        public virtual ICollection<Assignment> RefChildren { get; set; }
    }

    public enum WorkStatus
    {
        Assigned,
        InProgress,
        Stopped,
        Completed
    }
}
