using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CMCSPOE.Models;
using System.Security.Claims;

namespace CMCSPOE.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<CMCSPOE.Models.Claims> Claims { get; set; } = default!;
        
    }
}
