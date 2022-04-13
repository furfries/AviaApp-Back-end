using AviaApp.Models.Dto;

namespace AviaApp.Models;

public class UpdateRoleResponse
{
    public string Status { get; set; }
        
    public string Message { get; set; }

    public UserDto User { get; set; }
}