﻿namespace PedagogyPrime.Core.Entities
{
	public class UserSubject
	{
		public Guid UserId { get; set; }
		public User User { get; set; }
		public Guid SubjectId { get; set; }
		public Subject Subject { get; set; }
	}
}