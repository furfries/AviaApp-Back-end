using System.Collections.Generic;

namespace AviaApp.Models;

public class LoginResponse
{
    public string Token { get; set; }

    public IList<string> Roles { get; set; }
}