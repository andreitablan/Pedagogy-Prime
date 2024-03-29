﻿using PedagogyPrime.Core.Common;
using PedagogyPrime.Core.IRepositories;

namespace PedagogyPrime.Infrastructure.Commands.SubjectForums.Delete
{
	using Common;
	using PedagogyPrime.Infrastructure.AOP.Handler;
	using PedagogyPrime.Infrastructure.IAuthorization;

	public class DeleteSubjectForumCommandHandler : BaseRequestHandler<DeleteSubjectForumCommand, BaseResponse<bool>>
	{
		private readonly ISubjectForumRepository subjectForumRepository;

		public DeleteSubjectForumCommandHandler(ISubjectForumRepository subjectForumRepository, IUserAuthorization userAuthorization) : base(userAuthorization)
		{
			this.subjectForumRepository = subjectForumRepository;
		}
        [HandlerAspect]
        public override async Task<BaseResponse<bool>> Handle(
            DeleteSubjectForumCommand request,
			CancellationToken cancellationToken
		)
		{
			try
			{
				if(!(await IsAuthorized(request.UserId)))
				{
					return BaseResponse<bool>.Forbbiden();
				}

				var result = await subjectForumRepository.Delete(request.Id);

				if(result == 0)
				{
					return BaseResponse<bool>.NotFound("SubjectForum");
				}

				return BaseResponse<bool>.Ok(true);
			}
			catch
			{
				return BaseResponse<bool>.InternalServerError();
			}
		}
	}
}