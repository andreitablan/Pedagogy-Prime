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
                var oldDocument = await documentRepository.GetById(request.Id);
                if (oldDocument == null)
                {
                    return BaseResponse<DocumentDetails>.NotFound();
                }

                var updatedDocument = new Document
                {
                    Id = oldDocument.Id,
                    UserId = request.UserId,
                    Content = request.Content,
                    State = request.State,
                    Type = request.Type,
                    FirebaseLink = request.FirebaseLink
                };

                documentRepository.Update(updatedDocument);
                await documentRepository.SaveChanges();

                var documentDetails = GenericMapper<Document, DocumentDetails>.Map(updatedDocument);

                return BaseResponse<DocumentDetails>.Ok(documentDetails);
            }
            catch (Exception e)
            {
                return BaseResponse<DocumentDetails>.BadRequest(new List<string>
                {
                    e.Message
                });
            }
        }
    }
}
