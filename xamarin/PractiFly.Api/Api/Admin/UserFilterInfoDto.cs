using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PractiFly.Api.Api.Admin
{
    public class UserFilterInfoDto
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public DateOnly RegistrationDateFrom { get; set; }
        public DateOnly RegistrationDateTo { get; set; }
        public string Email { get; set; } = null!;
        public string Role { get; set; } = null!;


    }
}
