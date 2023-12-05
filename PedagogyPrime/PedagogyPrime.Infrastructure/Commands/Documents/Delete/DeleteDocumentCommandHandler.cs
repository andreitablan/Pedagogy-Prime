using PedagogyPrime.Core.Common;
using PedagogyPrime.Core.IRepositories;

namespace PedagogyPrime.Infrastructure.Commands.Documents.Delete
{
	using Common;
	using PedagogyPrime.Infrastructure.AOP.Handler;
	using PedagogyPrime.Infrastructure.IAuthorization;

	public class DeleteDocumentCommandHandler : BaseRequestHandler<DeleteDocumentCommand, BaseResponse<bool>>
	{
		private readonly IDocumentRepository documentRepository;

		public DeleteDocumentCommandHandler(IDocumentRepository documentRepository, IUserAuthorization userAuthorization) : base(userAuthorization)
		{
			this.documentRepository = documentRepository;
		}
        [HandlerAspect]
        public override async Task<BaseResponse<bool>> Handle(
			DeleteDocumentCommand request,
			CancellationToken cancellationToken
		)
		{
			try
			{
				if(!(await IsAuthorized(request.UserId)))
				{
					return BaseResponse<bool>.Forbbiden();
				}

				var result = await documentRepository.Delete(request.Id);

				if(result == 0)
				{
					return BaseResponse<bool>.NotFound("Document");
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