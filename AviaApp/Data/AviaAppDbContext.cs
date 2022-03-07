using Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class AviaAppDbContext : IdentityDbContext<AviaAppUser>
{
    public AviaAppDbContext(DbContextOptions<AviaAppDbContext> options) : base(options)
    {
    }
}