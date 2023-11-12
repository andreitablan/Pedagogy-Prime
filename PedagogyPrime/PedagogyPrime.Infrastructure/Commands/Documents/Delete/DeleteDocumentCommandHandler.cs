using MediatR;
using PedagogyPrime.Core.Common;
using PedagogyPrime.Core.IRepositories;

namespace PedagogyPrime.Infrastructure.Commands.Documents.Delete
{
	public class DeleteDocumentCommandHandler : IRequestHandler<DeleteDocumentCommand, BaseResponse<bool>>
	{
		private readonly IDocumentRepository documentRepository;

		public DeleteDocumentCommandHandler(IDocumentRepository documentRepository)
		{
			this.documentRepository = documentRepository;
		}

		public async Task<BaseResponse<bool>> Handle(
			DeleteDocumentCommand request,
			CancellationToken cancellationToken
		)
		{
			try
			{
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