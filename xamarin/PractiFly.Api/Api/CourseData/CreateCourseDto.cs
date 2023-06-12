using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PractiFly.Api.Api.CourseData
{
    public class CreateCourseDto
    {
        public string Language { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int OwnerId { get; set; } 
        public string Note { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
