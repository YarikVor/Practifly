using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PractiFly.Api.Api.HeadingCourse
{
    public class HeadingsInCourseDto
    {
        public int ? courseId { get; set; } 
        public string beginCode { get; set; } = null!;
    }
}
