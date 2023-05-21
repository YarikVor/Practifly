using PractiFly.Api.Api.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PractiFly.Api.Admin
{
    public class AdminUserInfoDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Birthday { get; set; } = null!;
        public string RegistrationDate { get; set; } = null!;
        public string FilePhoto { get; set; } = null!;
        public string Role { get; set; } = null!;
    }
}
