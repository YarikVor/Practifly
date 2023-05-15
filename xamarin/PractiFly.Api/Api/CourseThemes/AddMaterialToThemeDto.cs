using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PractiFly.Api.Api.CourseThemes
{
    public class AddMaterialToThemeDto
    {
        public int ThemeId { get; set; }
        public int MaterialId { get; set; }
        public int Number { get; set; }
        public bool IsBasic { get; set; }
        public int LevelId { get; set; }
        public string Note { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
