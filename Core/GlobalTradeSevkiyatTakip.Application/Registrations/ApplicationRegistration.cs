using GlobalTradeSevkiyatTakip.Application.Mapping;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GlobalTradeSevkiyatTakip.Application.Registrations
{
    public static class ApplicationRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services,
           IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(CariMapper));
            services.AddAutoMapper(typeof(DepoMapper));
            services.AddAutoMapper(typeof(IrsaliyeMapper));
            services.AddAutoMapper(typeof(SevkiyatMapper));
            services.AddAutoMapper(typeof(StokMapper));
            services.AddAutoMapper(typeof(MarkaMapper));
            services.AddAutoMapper(typeof(RenkMapper));
            services.AddAutoMapper(typeof(StokDetayMapper));
            services.AddAutoMapper(typeof(KullaniciMapper));
            services.AddAutoMapper(typeof(EntegreMapper));
            services.AddAutoMapper(typeof(SevkiyatNotMapper));
            services.AddAutoMapper(typeof(DovizMapper));
            services.AddAutoMapper(typeof(ParaBirimMapper));
            services.AddAutoMapper(typeof(FaturaMapper));

        }
    }
}
