namespace PedagogyPrime.Infrastructure.Commands.Users.Delete
{
    using Common;
    using Core.Common;

    public class DeleteUserCommand : BaseRequest<BaseResponse<Guid>>
    {
        public Guid Id { get; set; }

        public DeleteUserCommand(Guid id)
        {
            Id = id;
        }
    }
}