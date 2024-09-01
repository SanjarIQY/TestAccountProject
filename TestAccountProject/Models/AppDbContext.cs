﻿using Microsoft.EntityFrameworkCore;
using TestAccountProject.Models;

namespace TestAccountProject.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Transaction> Transaction { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();   
        }

    }
}
