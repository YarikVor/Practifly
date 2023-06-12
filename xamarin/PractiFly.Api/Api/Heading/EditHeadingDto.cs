﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PractiFly.Api.Api.Heading
{
    public class EditHeadingDto
    {
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string Udc { get; set; } = null!;
        public string Note { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Id { get; set; }
    }
}
