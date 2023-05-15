using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PractiFly.Api.Api.CourseMaterials
{
    public class MaterialIncludingCourseDto
    {
        int courseId { get; set; }
        int headingId { get; set; }

        string code { get; set; } = null!;
    }
}
