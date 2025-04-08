using DatingApp.API.Models.Domain;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class ApplicationDbContext(DbContextOptions options):DbContext(options)
    {
        public DbSet<AppUser> Users { get; set; }
    }
}
