using PedagogyPrime.Core.Common;
using PedagogyPrime.Core.IRepositories;

namespace PedagogyPrime.Infrastructure.Commands.Subjects.Delete
{
	using Common;
	using PedagogyPrime.Infrastructure.AOP.Handler;
	using PedagogyPrime.Infrastructure.IAuthorization;

	public class DeleteSubjectCommandHandler : BaseRequestHandler<DeleteSubjectCommand, BaseResponse<bool>>
	{
		private readonly ISubjectRepository subjectRepository;

		public DeleteSubjectCommandHandler(ISubjectRepository subjectRepository, IUserAuthorization userAuthorization) : base(userAuthorization)
		{
			this.subjectRepository = subjectRepository;
		}
        [HandlerAspect]
        public override async Task<BaseResponse<bool>> Handle(
			DeleteSubjectCommand request,
			CancellationToken cancellationToken
		)
		{
			try
			{
				if(!(await IsAuthorized(request.UserId)))
				{
					return BaseResponse<bool>.Forbbiden();
				}

				var result = await subjectRepository.Delete(request.Id);

				if(result == 0)
				{
					return BaseResponse<bool>.NotFound("Subject");
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