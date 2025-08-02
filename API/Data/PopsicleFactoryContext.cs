using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Data
{
    public class PopsicleFactoryContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public PopsicleFactoryContext(DbContextOptions<PopsicleFactoryContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }
        
        public DbSet<Popsicle> Popsicles { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
    }
}

