﻿using Microsoft.EntityFrameworkCore;

namespace Student.Portal.Models.DataBase
{
    public partial class DatabaseConnection : DbContext
    {

        public DatabaseConnection()
        {
        }

        public DatabaseConnection(DbContextOptions<DatabaseConnection> options)
            : base(options)
        {
        }

        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser);

            });

        }
    }
}