using PedagogyPrime.Core.Common;
using PedagogyPrime.Core.Entities;
using PedagogyPrime.Core.IRepositories;
using PedagogyPrime.Infrastructure.IAuthorization;

namespace PedagogyPrime.Infrastructure.Commands.Documents.Create
{
	using Common;
	using PedagogyPrime.Infrastructure.AOP.Handler;

	public class CreateDocumentCommandHandler : BaseRequestHandler<CreateDocumentCommand, BaseResponse<bool>>
	{
		private readonly IDocumentRepository documentRepository;

		public CreateDocumentCommandHandler(IDocumentRepository documentRepository, IUserAuthorization userAuthorization) : base(userAuthorization)
		{
			this.documentRepository = documentRepository;
		}
        [HandlerAspect]
        public override async Task<BaseResponse<bool>> Handle(
			CreateDocumentCommand request,
			CancellationToken cancellationToken
		)
		{
			try
			{
				if(!(await IsAuthorized(request.UserId)))
				{
					return BaseResponse<bool>.Forbbiden();
				}

				var document = new Document
				{
					Id = Guid.NewGuid(),
					ContentUrl = request.ContentUrl,
					State = request.State,
					Type = request.Type,
				};

				await documentRepository.Add(document);
				await documentRepository.SaveChanges();

				return BaseResponse<bool>.Created();
			}
			catch
			{
				return BaseResponse<bool>.InternalServerError();
			}
		}
	}
}