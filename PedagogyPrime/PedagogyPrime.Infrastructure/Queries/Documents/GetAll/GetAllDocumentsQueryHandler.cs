using MediatR;
using PedagogyPrime.Core.Common;
using PedagogyPrime.Core.Entities;
using PedagogyPrime.Core.IRepositories;
using PedagogyPrime.Infrastructure.Common;
using PedagogyPrime.Infrastructure.Models.Document;

namespace PedagogyPrime.Infrastructure.Queries.Documents.GetAll
{
	public class GetAllDocumentsQueryHandler : IRequestHandler<GetAllDocumentsQuery, BaseResponse<List<DocumentDetails>>>
	{
		private readonly IDocumentRepository documentRepository;

		public GetAllDocumentsQueryHandler(IDocumentRepository documentRepository)
		{
			this.documentRepository = documentRepository;
		}

		public async Task<BaseResponse<List<DocumentDetails>>> Handle(
			GetAllDocumentsQuery request,
			CancellationToken cancellationToken
		)
		{
			try
			{
				var documents = await documentRepository.GetAll();

				var documentsDetails = documents.Select(GenericMapper<Document, DocumentDetails>.Map).ToList();

				return BaseResponse<List<DocumentDetails>>.Ok(documentsDetails);
			}
			catch
			{
				return BaseResponse<List<DocumentDetails>>.InternalServerError();
			}
		}
	}
}