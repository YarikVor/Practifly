using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PractiFly.Api.CourseData
{

    public class OwnerInfoDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string FilePhoto { get; set; } = null!;
    }
}
