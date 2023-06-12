using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PractiFly.Api.CourseMaterials
{
    public class MaterialIncludingCourseInfoDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsPractical { get; set; }
        public bool IsIncluded { get; set; }
        public int PriorityLevel { get; set; }
    }
}
