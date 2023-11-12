using MediatR;
using PedagogyPrime.Core.Common;
using PedagogyPrime.Core.Entities;
using PedagogyPrime.Core.IRepositories;
using PedagogyPrime.Infrastructure.Common;
using PedagogyPrime.Infrastructure.Models.Document;

namespace PedagogyPrime.Infrastructure.Queries.Documents.GetById
{
	public class GetDocumentByIdQueryHandler : IRequestHandler<GetDocumentByIdQuery, BaseResponse<DocumentDetails>>
	{
		private readonly IDocumentRepository documentRepository;

		public GetDocumentByIdQueryHandler(IDocumentRepository documentRepository)
		{
			this.documentRepository = documentRepository;
		}

		public async Task<BaseResponse<DocumentDetails>> Handle(
			GetDocumentByIdQuery request,
			CancellationToken cancellationToken
		)
		{
			try
			{
				var document = await documentRepository.GetById(request.Id);

				if(document == null)
				{
					return BaseResponse<DocumentDetails>.NotFound("Document");
				}

				var documentDetails = GenericMapper<Document, DocumentDetails>.Map(document);

				return BaseResponse<DocumentDetails>.Ok(documentDetails);
			}
			catch
			{
				return BaseResponse<DocumentDetails>.InternalServerError();
			}
		}
	}
}