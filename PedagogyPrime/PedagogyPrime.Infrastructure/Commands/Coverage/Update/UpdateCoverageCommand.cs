﻿namespace PedagogyPrime.Infrastructure.Commands.Coverage.Update
{
	using Common;
	using Core.Common;

	public class UpdateCoverageCommand : BaseRequest<BaseResponse<bool>>
	{
		public Guid Id { get; set; }
		public List<string> GoodWords { get; set; }
		public List<string> BadWords { get; set; }
		public Double Precentage { get; set; }
		public Guid CourseId { get; set; }
	}
}