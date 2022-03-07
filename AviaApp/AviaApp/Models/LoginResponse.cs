using System;
using System.Collections.Generic;

namespace AviaApp.Models;

public class LoginResponse
{
    public string Token { get; set; }

    public DateTime ValidTo { get; set; }

    public IList<string> Roles { get; set; }
}