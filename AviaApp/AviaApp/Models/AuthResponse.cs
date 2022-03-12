using System.Collections.Generic;
using AviaApp.Enums;

namespace AviaApp.Models;

public class AuthResponse
{
    public string Status { get; set; }

    public IList<string> Reasons { get; set; }
}