namespace PedagogyPrime.Infrastructure.Commands.Users.Create
{
    using Common;
    using Core.Entities;
    using Core.IRepositories;
    using PedagogyPrime.Core.Common;
    using PedagogyPrime.Infrastructure.AOP.Handler;
    using PedagogyPrime.Infrastructure.IAuthorization;

    public class CreateUserCommandHandler : BaseRequestHandler<CreateUserCommand, BaseResponse<Guid>>
    {
        private readonly IUserRepository userRepository;

        public CreateUserCommandHandler(
            IUserRepository userRepository,
            IUserAuthorization userAuthorization
        ) : base(userAuthorization)
        {
            this.userRepository = userRepository;
        }
        [HandlerAspect]
        public override async Task<BaseResponse<Guid>> Handle(
            CreateUserCommand request,
            CancellationToken cancellationToken
        )
        {
            try
            {
                var user = new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Password = request.Password,
                    Email = request.Email,
                    Username = request.Username,
                    Role = request.Role
                };

                await userRepository.Add(user);
                await userRepository.SaveChanges();

                return BaseResponse<Guid>.Created(user.Id);
            }
            catch
            {
                return BaseResponse<Guid>.InternalServerError();
            }
        }
    }
}