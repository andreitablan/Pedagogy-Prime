using PedagogyPrime.Core.Common;
using PedagogyPrime.Core.Entities;
using PedagogyPrime.Core.IRepositories;
using PedagogyPrime.Infrastructure.Common;
using PedagogyPrime.Infrastructure.IAuthorization;

namespace PedagogyPrime.Infrastructure.Commands.UsersSubjects.AddParticipant
{
    public class AddParticipantCommandHandler : BaseRequestHandler<AddParticipantCommand, BaseResponse<bool>>
    {
        private readonly ISubjectRepository subjectRepository;
        private readonly IUserSubjectRepository userSubjectRepository;

        public AddParticipantCommandHandler(IUserAuthorization userAuthorization, ISubjectRepository subjectRepository, IUserSubjectRepository userSubjectRepository) : base(userAuthorization)
        {
            this.subjectRepository = subjectRepository;
            this.userSubjectRepository = userSubjectRepository;
        }

        public override async Task<BaseResponse<bool>> Handle(AddParticipantCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var subject = await subjectRepository.GetById(request.SubjectId);

                if (subject == null)
                {
                    return BaseResponse<bool>.NotFound("Subject");
                }

                var usersSubjects = request.UserIds.Select(x => new UserSubject
                {
                    UserId = x,
                    SubjectId = subject.Id,
                }).ToList();


                await userSubjectRepository.AddRange(usersSubjects);


                await userSubjectRepository.SaveChanges();


                return BaseResponse<bool>.Ok(true);
            }
            catch (Exception ex)
            {
                return BaseResponse<bool>.InternalServerError();
            }
        }
    }
}
