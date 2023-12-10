using PedagogyPrime.Core.Common;
using PedagogyPrime.Core.Entities;
using PedagogyPrime.Core.IRepositories;
using PedagogyPrime.Infrastructure.AOP.Handler;
using PedagogyPrime.Infrastructure.Common;
using PedagogyPrime.Infrastructure.IAuthorization;
using PedagogyPrime.Infrastructure.Models.Assignment;

namespace PedagogyPrime.Infrastructure.Queries.Assignments.GetAll
{
    public class GetAllAssignmentsQueryHandler : BaseRequestHandler<GetAllAssignmentsQuerry, BaseResponse<List<AssignmentDetails>>>
    {
        private readonly IAssignmentRepository assignmentRepository;
        public GetAllAssignmentsQueryHandler(IAssignmentRepository assignmentRepository, IUserAuthorization userAuthorization) : base(userAuthorization)
        {
            this.assignmentRepository = assignmentRepository;
        }
        [HandlerAspect]
        public override async Task<BaseResponse<List<AssignmentDetails>>> Handle(
        GetAllAssignmentsQuerry request,
            CancellationToken cancellationToken
        )
        {
            try
            {
                var assignment = await assignmentRepository.GetAll();

                var assignmentDetails = assignment.Select(GenericMapper<Assignment, AssignmentDetails>.Map).ToList();

                return BaseResponse<List<AssignmentDetails>>.Ok(assignmentDetails);
            }
            catch
            {
                return BaseResponse<List<AssignmentDetails>>.InternalServerError();
            }
        }
    }
}
