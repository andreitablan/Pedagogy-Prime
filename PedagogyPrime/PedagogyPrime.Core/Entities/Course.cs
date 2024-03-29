﻿namespace PedagogyPrime.Core.Entities
{
	public class Course : BaseEntity
	{
		public String Name { get; set; }
		public String Description { get; set; }
		public String ContentUrl { get; set; }
		public bool IsVisibleForStudents { get; set; }
		public Coverage Coverage { get; set; }
		public Guid SubjectId { get; set; }
		public Subject Subject { get; set; }
		public CourseForum CourseForum { get; set; }
		public int Index { get; set; }
	}
}