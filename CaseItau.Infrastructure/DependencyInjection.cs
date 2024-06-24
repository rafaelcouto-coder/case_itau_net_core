using CaseItau.Domain.Abstractions;
using CaseItau.Domain.Repositories;
using CaseItau.Infrastructure.Helpers;
using CaseItau.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CaseItau.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
    {
        AddPersistence(services);

        return services;
    }

    private static void AddPersistence(IServiceCollection services)
    {
        var connectionsSTring = Constants.DbConnectionString;

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlite(connectionsSTring).UseSnakeCaseNamingConvention();
        });

        services.AddScoped<IFundosRepository, FundosRepository>();
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());
    }
}
