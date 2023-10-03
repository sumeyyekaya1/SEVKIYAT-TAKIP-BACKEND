using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace GlobalTradeSevkiyatTakip.Persistance.EFContext
{
    public class EFDatabaseContextFactory : IDesignTimeDbContextFactory<EFDatabaseContext>
    {
        EFDatabaseContext IDesignTimeDbContextFactory<EFDatabaseContext>.CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EFDatabaseContext>();
            optionsBuilder.UseSqlServer("Data Source=GLOBALTRADE\\AKINSOFT;Initial Catalog=GlobalTradeSevikatTakipDb; User Id=sa;Password=Abc123+-*q1;Trust Server Certificate=True;"); //Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False

            return new EFDatabaseContext(optionsBuilder.Options);
        }
    }
}
