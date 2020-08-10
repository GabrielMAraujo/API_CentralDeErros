using System;
using API_CentralDeErros.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace API_CentralDeErros.Infra
{
    public class CentralContext : IdentityDbContext
    {
        public DbSet<Alert> Alerts { get; set; }
        public DbSet<User> Users { get; set; }

        private readonly IConfiguration Configuration;

        public CentralContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Configurando o acesso ao servidor
            optionsBuilder.UseSqlServer($"Server={Configuration["env:DB_SERVER_ADDRESS"]};" +
                $"Database={Configuration["env:DB_SERVER_DATABASE_NAME"]};" +
                $"User Id={Configuration["env:DB_USER_ID"]};" +
                $"Password={Configuration["env:DB_PASSWORD"]};",
                b => b.MigrationsAssembly("API_CentralDeErros"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
