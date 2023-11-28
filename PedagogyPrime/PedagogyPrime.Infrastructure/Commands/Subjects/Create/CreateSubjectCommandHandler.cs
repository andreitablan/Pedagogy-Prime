using PedagogyPrime.Core.Common;
using PedagogyPrime.Core.Entities;
using PedagogyPrime.Core.IRepositories;
using PedagogyPrime.Infrastructure.IAuthorization;

namespace PedagogyPrime.Infrastructure.Commands.Subjects.Create
{
    using Common;

    public class CreateSubjectCommandHandler : BaseRequestHandler<CreateSubjectCommand, BaseResponse<Guid>>
    {
        private readonly ISubjectRepository subjectRepository;
        private readonly IUserSubjectRepository userSubjectRepository;

        public CreateSubjectCommandHandler(
            ISubjectRepository subjectRepository,
            IUserAuthorization userAuthorization,
            IUserSubjectRepository userSubjectRepository) : base(userAuthorization)
        {
            this.subjectRepository = subjectRepository;
            this.userSubjectRepository = userSubjectRepository;
        }

        public override async Task<BaseResponse<Guid>> Handle(
            CreateSubjectCommand request,
            CancellationToken cancellationToken
        )
        {
            try
            {
                if (!(await IsAuthorized(request.UserId)))
                {
                    return BaseResponse<Guid>.Forbbiden();
                }

                var subject = new Subject
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    Period = request.Period,
                    NoOfCourses = request.NoOfCourses,
                };

                var userSubject = new UserSubject
                {
                    UserId = request.UserId,
                    SubjectId = subject.Id,
                };


                await subjectRepository.Add(subject);

                await userSubjectRepository.Add(userSubject);
                await subjectRepository.SaveChanges();

                return BaseResponse<Guid>.Created(subject.Id);
            }
            catch
            {
                return BaseResponse<Guid>.InternalServerError();
            }
        }
    }
}