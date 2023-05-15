using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PractiFly.Api.Api.CourseThemes
{
    public class UpdateThemesDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Number { get; set; }
        public int LevelId { get; set; }
        public string Note { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
