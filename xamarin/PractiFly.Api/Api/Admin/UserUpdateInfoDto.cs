using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PractiFly.Api.Api.Admin;

public class UserUpdateInfoDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public DateTime Birthday { get; set; }
    public string FilePhoto { get; set; } = null!;
    public string Role { get; set; } = null!;
}


