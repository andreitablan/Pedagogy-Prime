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
                var course = await documentRepository.GetById(request.UserId);
                if (course == null)
                {
                    return BaseResponse<bool>.NotFound();
                }

                await documentRepository.Delete(request.UserId);
                await documentRepository.SaveChanges();
                return BaseResponse<bool>.Ok();
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
