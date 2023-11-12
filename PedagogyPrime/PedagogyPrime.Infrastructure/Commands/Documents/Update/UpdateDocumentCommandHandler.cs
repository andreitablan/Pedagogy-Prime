using PedagogyPrime.Core.Common;
using PedagogyPrime.Core.Entities;
using PedagogyPrime.Core.IRepositories;
using PedagogyPrime.Infrastructure.Common;
using PedagogyPrime.Infrastructure.Models.Document;

namespace PedagogyPrime.Infrastructure.Commands.Documents.Update
{
	using IAuthorization;

	public class UpdateDocumentCommandHandler : BaseRequestHandler<UpdateDocumentCommand, BaseResponse<DocumentDetails>>
	{
		private readonly IDocumentRepository documentRepository;

		public UpdateDocumentCommandHandler(IDocumentRepository documentRepository, IUserAuthorization userAuthorization) : base(userAuthorization)
		{
			this.documentRepository = documentRepository;
		}

		public override async Task<BaseResponse<DocumentDetails>> Handle(
			UpdateDocumentCommand request,
			CancellationToken cancellationToken
	   )
		{
			try
			{
				if(!(await IsAuthorized(request.UserId)))
				{
					return BaseResponse<DocumentDetails>.Forbbiden();
				}

				var document = await documentRepository.GetById(request.Id);

				if(document == null)
				{
					return BaseResponse<DocumentDetails>.NotFound("Document");
				}

				document.Id = document.Id;
				document.UserId = request.UserId;
				document.ContentUrl = request.ContentUrl;
				document.State = request.State;
				document.Type = request.Type;

				await documentRepository.SaveChanges();

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