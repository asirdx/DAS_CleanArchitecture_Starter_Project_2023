using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace CleanArchitecture.WebApi.Configurations;

public sealed class PersistanceServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostBuilder host)
    {
        services.AddAutoMapper(typeof(CleanArchitecture.Persistance.AssemblyRefence).Assembly);

        string connectionString = configuration.GetConnectionString("SqlServer"); //builder.Configuration idi configuration oldu

        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
        services.AddIdentity<User, Role>(options =>
        {
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 1;
            options.Password.RequireUppercase = false;
        }).AddEntityFrameworkStores<AppDbContext>();

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day) // RollingInterval ile hergün logun yeni bir dosyaya kaydını sağladık
            .WriteTo.MSSqlServer(
             connectionString: connectionString,
             tableName: "Logs",
             autoCreateSqlTable: true) //MS-SQL table'ını otomatik oluşturur
            .CreateLogger();

        host.UseSerilog();
    }
}
