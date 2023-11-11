using MediatR;
using PedagogyPrime.Core.Common;
using PedagogyPrime.Core.Entities;
using PedagogyPrime.Core.IRepositories;
using PedagogyPrime.Infrastructure.Common;
using PedagogyPrime.Infrastructure.Models.Document;

namespace PedagogyPrime.Infrastructure.Commands.Documents.Update
{
	public class UpdateDocumentCommandHandler : IRequestHandler<UpdateDocumentCommand, BaseResponse<DocumentDetails>>
	{
		private readonly IDocumentRepository documentRepository;

		public UpdateDocumentCommandHandler(IDocumentRepository documentRepository)
		{
			this.documentRepository = documentRepository;
		}

		public async Task<BaseResponse<DocumentDetails>> Handle(
			UpdateDocumentCommand request,
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

				document.Id = document.Id;
				document.UserId = request.UserId;
				document.Content = request.Content;
				document.State = request.State;
				document.Type = request.Type;
				document.FirebaseLink = request.FirebaseLink;

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