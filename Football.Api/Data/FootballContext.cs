using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Football.Api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Football.Api.Data
{
    public class FootballContext : IdentityDbContext<User,Role,int>
    {
        public FootballContext(DbContextOptions<FootballContext> options) :base(options)
        {
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Nationality> Nationalities { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .HasMany(u => u.UserRoles)
                .WithOne(ur => ur.User)
                .HasForeignKey(ur => ur.UserId);


            builder.Entity<Role>()
                .HasMany(u => u.UserRoles)
                .WithOne(ur => ur.Role)
                .HasForeignKey(ur => ur.RoleId);
        }
    }
}
