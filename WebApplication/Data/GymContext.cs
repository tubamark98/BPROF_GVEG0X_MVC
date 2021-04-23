using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class GymContext : IdentityDbContext<IdentityUser>
    {
        public GymContext(DbContextOptions<GymContext> opt) : base(opt)
        {

        }

        public GymContext()
        {
            this.Database.EnsureCreated();

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.
                    UseLazyLoadingProxies().
                    //UseSqlServer(@"data source=(LocalDB)\MSSQLLocalDB;attachdbfilename=|DataDirectory|\GymDB.mdf;integrated security=True;MultipleActiveResultSets=True");
                    UseSqlServer(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=GymDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GymClient>(entity =>
            {
                entity.HasOne(client => client.Trainer).WithMany(trainer => trainer.GymClients).HasForeignKey(client => client.TrainerID);
            });

            modelBuilder.Entity<ExtraInfo>(entity =>
            {
                entity.HasOne(infos => infos.GymClient).WithMany(client => client.ExtraInfos).HasForeignKey(infos => infos.GymID);
            });
        }
       
        public DbSet<GymClient> GymClients { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<ExtraInfo> ExtraInfos { get; set; }
    }
}
