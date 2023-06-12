using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PractiFly.Api.Api.HeadingCourse
{
    public class ChangeHeadingInCourseDto
    {
        public int CourseId { get; set; }
        public int HeadingId { get; set; }
        public bool IsIncluded { get; set; }
    }
}
