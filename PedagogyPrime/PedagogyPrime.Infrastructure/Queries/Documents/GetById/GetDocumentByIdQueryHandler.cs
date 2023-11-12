using PedagogyPrime.Core.Common;
using PedagogyPrime.Core.Entities;
using PedagogyPrime.Core.IRepositories;
using PedagogyPrime.Infrastructure.Common;
using PedagogyPrime.Infrastructure.Models.Document;

namespace PedagogyPrime.Infrastructure.Queries.Documents.GetById
{
	using IAuthorization;

	public class GetDocumentByIdQueryHandler : BaseRequestHandler<GetDocumentByIdQuery, BaseResponse<DocumentDetails>>
	{
		private readonly IDocumentRepository documentRepository;

		public GetDocumentByIdQueryHandler(IDocumentRepository documentRepository, IUserAuthorization userAuthorization) : base(userAuthorization)
		{
			this.documentRepository = documentRepository;
		}

		public override async Task<BaseResponse<DocumentDetails>> Handle(
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

				if(!(await IsAuthorized(request.UserId, document.UserId)))
				{
					return BaseResponse<DocumentDetails>.Forbbiden();
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