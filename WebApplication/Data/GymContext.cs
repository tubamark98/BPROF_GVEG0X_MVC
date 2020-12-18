using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class GymContext : DbContext
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
                    UseSqlServer(@"data source=(LocalDB)\MSSQLLocalDB;attachdbfilename=|DataDirectory|\GymDB.mdf;integrated security=True;MultipleActiveResultSets=True");
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

            //Need to clean these up 1 day
            modelBuilder.Entity<WorkoutDetail_v2>(entity =>
            {
                entity.HasNoKey();
            });
            //modelBuilder.Entity<WorkoutDetail>(entity =>
            //{
            //    entity.HasOne(client => client.GymClient).WithOne( work => work.WorkoutDetail).HasForeignKey<GymClient>(x => x.GymID);
            //});
        }
       
        public DbSet<GymClient> GymClients { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<ExtraInfo> ExtraInfos { get; set; }
        //public DbSet<WorkoutDetail> WorkoutDetails { get; set; }

    }
}
