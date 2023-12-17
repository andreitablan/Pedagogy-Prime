namespace PedagogyPrime.Infrastructure.Commands.Users.Delete
{
    using Common;
    using Core.Common;
    using Core.IRepositories;
    using PedagogyPrime.Infrastructure.AOP.Handler;
    using PedagogyPrime.Infrastructure.IAuthorization;

    public class DeleteUserCommandHandler : BaseRequestHandler<DeleteUserCommand, BaseResponse<Guid>>
    {
        private readonly IUserRepository userRepository;

        public DeleteUserCommandHandler(
            IUserRepository userRepository,
            IUserAuthorization userAuthorization
        ) : base(userAuthorization)
        {
            this.userRepository = userRepository;
        }
        [HandlerAspect]
        public override async Task<BaseResponse<Guid>> Handle(
            DeleteUserCommand request,
            CancellationToken cancellationToken
        )
        {
            try
            {
                if (!(await IsAuthorized(request.UserId)))
                {
                    return BaseResponse<Guid>.Forbbiden();
                }

                var result = await userRepository.Delete(request.Id);

                if (result == 0)
                {
                    return BaseResponse<Guid>.NotFound("User");
                }

                return BaseResponse<Guid>.Ok(request.Id);
            }
            catch
            {
                return BaseResponse<Guid>.InternalServerError();
            }
        }
    }
}