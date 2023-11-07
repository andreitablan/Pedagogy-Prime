using System.Collections.Generic;
using System;

namespace PedagogyPrime.Core.Entities
{    
    public class Assignment: BaseEntity
    {
        public Guid CourseId { get; set; }
        public DateTime Deadline { get; set; }
        public string Description { get; set; }
        public List<Homework> Homeworks { get; set; }
        public Homework Solution { get; set; }
    }
}