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
                    //UseSqlServer(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=GymDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                    UseSqlServer(@"Server=tcp:tubamark.database.windows.net,1433;Initial Catalog=GymAzureDB;Persist Security Info=False;User ID=tylondys;Password=AtCHias52v5VpkY;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
            //database: GymAzureDB
            //kiszolgalo: tubamark.database.windows.net
            //admin: tylondys
            //pw: AtCHias52v5VpkY
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole>().HasData(
                new { Id = "341743f0-asd2–42de-afbf-59kmkkmk72cf6", Name = "Admin", NormalizedName = "ADMIN" },
                new { Id = "341743f0-dee2–42de-bbbb-59kmkkmk72cf6", Name = "Client", NormalizedName = "CLIENT" });

            var adminUser = new IdentityUser
            {
                Id = "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                Email = "val.afo00@gmail.com",
                NormalizedEmail = "val.afo00@gmail.com",
                EmailConfirmed = true,
                UserName = "val.afo00@gmail.com",
                NormalizedUserName = "val.afo00@gmail.com",
                SecurityStamp = string.Empty
            };

            var clientUser = new IdentityUser
            {
                Id = "e2174cf0–9412–4cfe-afbf-59f706d72cf6",
                Email = "johncena@gmail.com",
                NormalizedEmail = "johncena@gmail.com",
                EmailConfirmed = true,
                UserName = "johncena@gmail.com",
                NormalizedUserName = "johncena@gmail.com",
                SecurityStamp = string.Empty
            };

            adminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "RoppantMonguzCsont23");
            clientUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "OtvarosLengyelBodybuilder12");


            modelBuilder.Entity<IdentityUser>().HasData(adminUser);
            modelBuilder.Entity<IdentityUser>().HasData(clientUser);

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "341743f0-asd2–42de-afbf-59kmkkmk72cf6",
                UserId = "02174cf0–9412–4cfe-afbf-59f706d72cf6"
            });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "341743f0-dee2–42de-bbbb-59kmkkmk72cf6",
                UserId = "e2174cf0–9412–4cfe-afbf-59f706d72cf6"
            });


            modelBuilder.Entity<GymClient>(entity =>
            {
                entity.HasOne(client => client.Trainer).WithMany(trainer => trainer.GymClients).HasForeignKey(client => client.TrainerID).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ExtraInfo>(entity =>
            {
                entity.HasOne(infos => infos.GymClient).WithMany(client => client.ExtraInfos).HasForeignKey(infos => infos.GymID).OnDelete(DeleteBehavior.Cascade);
            });
        }
       
        public DbSet<GymClient> GymClients { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<ExtraInfo> ExtraInfos { get; set; }
    }
}
