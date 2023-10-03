using GlobalTradeSevkiyatTakip.Domain.BaseEntities;
using GlobalTradeSevkiyatTakip.Domain.Entities;
using GlobalTradeSevkiyatTakip.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Persistance.EFContext
{
    public class EFDatabaseContext : DbContext
    {
        public EFDatabaseContext(DbContextOptions<EFDatabaseContext> options) : base(options)
        {

        }
        public EFDatabaseContext()
        {

        }
        public virtual DbSet<Cari> Cari { get; set; }
        public virtual DbSet<Depo> Depo { get; set; }
        public virtual DbSet<DepoDetay> DepoDetay { get; set; }
        public virtual DbSet<Irsaliye> Irsaliye { get; set; }
        public virtual DbSet<IrsaliyeDetay> IrsaliyeDetay { get; set; }
        public virtual DbSet<Sevkiyat> Sevkiyat { get; set; }
        public virtual DbSet<SevkiyatDetay> SevkiyatDetay { get; set; }
        public virtual DbSet<Stok> Stok { get; set; }
        public virtual DbSet<Hizmet> Hizmet { get; set; }
        public virtual DbSet<StokDetay> StokDetay { get; set; }
        public virtual DbSet<Renk> Renk { get; set; }
        public virtual DbSet<Marka> Marka { get; set; }
        public virtual DbSet<Kullanici> Kullanici { get; set; }
        public virtual DbSet<Entegre> EntegreKullanici { get; set; }
        public virtual DbSet<SevkiyatNot> SevkiyatNot { get; set; }
        public virtual DbSet<Doviz> Doviz { get; set; }
        public virtual DbSet<ParaBirim> ParaBirim { get; set; }
        public virtual DbSet<Fatura> Fatura { get; set; }
        public virtual DbSet<FaturaDetay> FaturaDetay { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entityList = ChangeTracker.Entries<BaseModel>()
                                          .Where(x => x.State == EntityState.Modified || x.State == EntityState.Added || x.State == EntityState.Deleted);
            foreach (var entity in entityList)
            {
                if (entity.State == EntityState.Added)
                {
                    ((BaseModel)entity.Entity).OlusturmaTarih = DateTime.Now;
                    ((BaseModel)entity.Entity).IsDeleted = false;
                }

                else if (entity.State == EntityState.Modified)
                    ((BaseModel)entity.Entity).OlusturmaTarih = DateTime.Now;

                else if (entity.State == EntityState.Deleted)
                {
                    ((BaseModel)entity.Entity).IsDeleted = true;
                    entity.State = EntityState.Modified;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Kullanici>()
             .HasData(
               new Kullanici
               {
                   ID = 1,
                   Ad = "default",
                   Soyad = "default",
                   Email = "default@gmail.com",
                   Parola = "jyBAdWogYwoYrNvTdwxwLKavHQiYqszX7dUAggFIfZhdDQFBvLfU1q3UMnCe1yVo",
                   Rol = KullaniciRolEnum.Admin,
               }
              );
            modelBuilder.Entity<Entegre>()
           .HasData(
             new Entegre
             {
                 ID = 1,
                 Email = "entegre@gmail.com",
                 Parola = "jyBAdWogYwoYrNvTdwxwLKavHQiYqszX7dUAggFIfZhdDQFBvLfU1q3UMnCe1yVo",
                 Rol = KullaniciRolEnum.Entegre,
             }
            );
        }

    }
}
