using GlobalTradeSevkiyatTakip.Application.Interfaces.IServices;
using GlobalTradeSevkiyatTakip.Application.Interfaces.IUnitOfWork;
using GlobalTradeSevkiyatTakip.Persistance.EFContext;
using GlobalTradeSevkiyatTakip.Persistance.Services;
using GlobalTradeSevkiyatTakip.Persistance.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GlobalTradeSevkiyatTakip.Persistance.Registrations
{
    public static class PersistanceRegistration
    {
        public static void AddPersistanceServices(this IServiceCollection services,
           IConfiguration configuration)
        {
            services.AddDbContext<EFDatabaseContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("mySqlConnectionString"));
            });

            #region Services_Injection

            services.AddScoped<IUOW, Uow>();
            services.AddScoped<ICariServis, CariServis>();
            services.AddScoped<IDepoServis, DepoServis>();
            services.AddScoped<IDepoDetayServis, DepoDetayServis>();
            services.AddScoped<IIrsaliyeServis, IrsaliyeServis>();
            services.AddScoped<IIrsaliyeDetayServis, IrsaliyeDetayServis>();
            services.AddScoped<ISevkiyatDetayServis, SevkiyatDetayServis>();
            services.AddScoped<ISevkiyatServis, SevkiyatServis>();
            services.AddScoped<IStokServis, StokServis>();
            services.AddScoped<IStokDetayServis, StokDetayServis>();
            services.AddScoped<IKullaniciServis, KullaniciServis>();
            services.AddScoped<IEntegreServis, EntegreServis>();
            services.AddScoped<ISevkiyatNotServis, SevkiyatNotServis>();
            services.AddScoped<IDovizServis, DovizServis>();
            services.AddScoped<IParaBirimServis, ParaBirimServis>();
            services.AddScoped<IFaturaServis, FaturaServis>();
            services.AddScoped<IFaturaDetayServis, FaturaDetayServis>();
            services.AddScoped<IHizmetServis, HizmetServis>();
            services.AddScoped<ISifremiUnuttumServis, SifremiUnuttumServis>();

            #endregion
        }
    }
}
