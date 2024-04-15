using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreIdentitySample.Data
{
    public class ApplicationDbContext : 
        IdentityDbContext<UniqueVolvoUser, IdentityRole<Guid>, Guid>
    {
        //public  DbSet<Pets> Pets { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
