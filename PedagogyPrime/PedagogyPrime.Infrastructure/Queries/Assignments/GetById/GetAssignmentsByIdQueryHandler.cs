using PedagogyPrime.Core.Common;
using PedagogyPrime.Core.Entities;
using PedagogyPrime.Core.IRepositories;
using PedagogyPrime.Infrastructure.Common;
using PedagogyPrime.Infrastructure.IAuthorization;
using PedagogyPrime.Infrastructure.Models.Assignment;
using PedagogyPrime.Infrastructure.Models.User;
using PedagogyPrime.Infrastructure.Queries.Users.GetById;
using PedagogyPrime.Persistence.Repositories;

namespace PedagogyPrime.Infrastructure.Queries.Assignments.GetById
{
    public class GetAssignmentByIdQueryHandler : BaseRequestHandler<GetAssignmentByIdQuery, BaseResponse<AssignmentDetails>>
    {
        private readonly IAssignmentRepository assignmentRepository;

        public GetAssignmentByIdQueryHandler(IAssignmentRepository assignmentRepository, IUserAuthorization userAuthorization) : base(userAuthorization)
        {
            this.assignmentRepository = assignmentRepository;
        }
        public override async Task<BaseResponse<AssignmentDetails>> Handle(
            GetAssignmentByIdQuery request,
            CancellationToken cancellationToken
        )
        {
            try
            {
                var assignment = await assignmentRepository.GetById(request.Id);

                if (assignment == null)
                {
                    return BaseResponse<AssignmentDetails>.NotFound("Assignment");
                }

                if (!(await IsAuthorized(request.UserId, assignment.Id)))
                {
                    return BaseResponse<AssignmentDetails>.Forbbiden();
                }

                var assignmentDetails = GenericMapper<Assignment, AssignmentDetails>.Map(assignment);

                return BaseResponse<AssignmentDetails>.Ok(assignmentDetails);
            }
            catch
            {
                return BaseResponse<AssignmentDetails>.InternalServerError();
            }
        }
    }
}
