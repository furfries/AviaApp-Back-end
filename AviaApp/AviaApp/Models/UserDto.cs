using System.Collections.Generic;

namespace AviaApp.Models;

public class UserDto
{
    public string Email { get; set; }

    public IList<string> Roles { get; set; }
}