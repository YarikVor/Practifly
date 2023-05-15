using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PractiFly.Api.Api.MaterialBlocks
{
    public class CreateMaterialBlockDto
    {
        public string Name { get; set; } 
        public int Priority { get; set; }
        public string Note { get; set; }
        public string Url { get; set; }
        public bool IsPractical { get; set; }
    }
}
