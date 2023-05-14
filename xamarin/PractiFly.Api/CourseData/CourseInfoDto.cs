using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PractiFly.Api.CourseData
{
    public class CourseInfoDto
    {
        public int Id { get; set; }
        public string Language { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Note { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
