using System.Collections.Generic;

namespace AviaApp.Models.Dto;

public class UserDto
{
    public string Email { get; set; }

    public IList<string> Roles { get; set; }
}