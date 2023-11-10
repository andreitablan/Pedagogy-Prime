﻿using PedagogyPrime.Core.Common;
using PedagogyPrime.Infrastructure.Common;
using PedagogyPrime.Infrastructure.Models.Course;

namespace PedagogyPrime.Infrastructure.Commands.Courses.Update
{
    public class UpdateCourseCommand : BaseRequest<BaseResponse<CourseDetails>>
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public String Content { get; set; }
        public Double Coverage { get; set; }
        public String Subject { get; set; }
    }
}
