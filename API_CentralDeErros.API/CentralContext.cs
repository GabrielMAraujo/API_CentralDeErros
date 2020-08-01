﻿using System;
using API_CentralDeErros.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace API_CentralDeErros.API
{
    public class CentralContext : DbContext
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
                $"Password={Configuration["env:DB_PASSWORD"]};");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}