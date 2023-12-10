using PedagogyPrime.Core.Common;
using PedagogyPrime.Core.Entities;
using PedagogyPrime.Core.IRepositories;
using PedagogyPrime.Infrastructure.Common;
using PedagogyPrime.Infrastructure.Models.Document;

namespace PedagogyPrime.Infrastructure.Queries.Documents.GetAll
{
	using IAuthorization;
	using PedagogyPrime.Infrastructure.AOP.Handler;

	public class GetAllDocumentsQueryHandler : BaseRequestHandler<GetAllDocumentsQuery, BaseResponse<List<DocumentDetails>>>
	{
		private readonly IDocumentRepository documentRepository;

		public GetAllDocumentsQueryHandler(IDocumentRepository documentRepository, IUserAuthorization userAuthorization) : base(userAuthorization)
		{
			this.documentRepository = documentRepository;
		}
        [HandlerAspect]
        public override async Task<BaseResponse<List<DocumentDetails>>> Handle(
			GetAllDocumentsQuery request,
			CancellationToken cancellationToken
		)
		{
			try
			{
				if(!(await IsAuthorized(request.UserId)))
				{
					return BaseResponse<List<DocumentDetails>>.Forbbiden();
				}

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