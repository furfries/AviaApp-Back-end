using System.Collections.Generic;

namespace AviaApp.Models;

public class AuthResponse
{
    public string Status { get; set; }

    public IList<string> Reasons { get; set; }
}