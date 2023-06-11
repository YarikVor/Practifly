using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PractiFly.Api.CourseThemes
{
    public class ListMaterialsCourseInfoDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int PriorityLevel { get; set; }
        public bool IsIncluded { get; set; }
    }
}
