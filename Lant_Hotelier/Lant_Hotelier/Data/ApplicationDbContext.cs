using Lant_Hotelier.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lant_Hotelier.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<LantHotelier> LanturiHoteliere { get; set; }
        public DbSet<Hotel> Hoteluri { get; set; }
        public DbSet<Camera> Camere { get; set; }
        public DbSet<Galerie> Galerii { get; set; }
        public DbSet<Contact> Contacte { get; set; }
        public DbSet<Servicii> Servicii { get; set; }
        public DbSet<Oferte> Oferte { get; set; }
        public DbSet<Evenimente> Evenimente { get; set; }
        public DbSet<Utilizator> Utilizatori { get; set; }
        public DbSet<Recenzie> Recenzii { get; set; }
        public DbSet<Rezervare> Rezervari { get; set; }
        public DbSet<HotelServiciu> HotelServicii { get; set; }
        public DbSet<HotelOferta> Hoteluri_Oferte { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<HotelServiciu>()
                .HasKey(hs => new { hs.ID_Hotel, hs.ID_Servicii });

            modelBuilder.Entity<HotelOferta>()
                .HasKey(ho => new { ho.ID_Hotel, ho.ID_Oferta });

            modelBuilder.Entity<Camera>()
                .Property(c => c.Pret)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Servicii>()
                .Property(s => s.Pret)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Servicii>()
                .HasOne(s => s.LantHotelier)
                .WithMany()
                .HasForeignKey(s => s.ID_Lant)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Oferte>()
                .Property(o => o.PretVechi)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Oferte>()
                .Property(o => o.PretNou)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Rezervare>()
                .Property(r => r.PretTotal)
                .HasColumnType("decimal(18,2)");
        }
    }
}
