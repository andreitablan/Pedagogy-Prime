using MediatR;
using PedagogyPrime.Core.Common;
using PedagogyPrime.Core.Entities;
using PedagogyPrime.Core.IRepositories;

namespace PedagogyPrime.Infrastructure.Commands.Documents.Create
{
    public class CreateDocumentCommandHandler : IRequestHandler<CreateDocumentCommand, BaseResponse<bool>>
    {
        private readonly IDocumentRepository documentRepository;

        public CreateDocumentCommandHandler(IDocumentRepository documentRepository)
        {
            this.documentRepository = documentRepository;
        }
        public async Task<BaseResponse<bool>> Handle(
            CreateDocumentCommand request,
            CancellationToken cancellationToken
        )
        {
            try
            {
                var document = new Document
                {
                    Id = Guid.NewGuid(),
                    Content = request.Content,
                    State = request.State,
                    Type = request.Type,
                    FirebaseLink = request.FirebaseLink,
                };

                await documentRepository.Add(document);
                await documentRepository.SaveChanges();

                return BaseResponse<bool>.Created();
            }
            catch (Exception e)
            {
                return BaseResponse<bool>.BadRequest(new List<string>
                {
                    e.Message
                });
            }
        }
    }
}
