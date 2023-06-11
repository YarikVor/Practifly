using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PractiFly.Api.MaterialBlocks
{
    public class ListMaterialsInfoDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Priority { get; set; }
        public bool IsPractical { get; set; }
    }
}
