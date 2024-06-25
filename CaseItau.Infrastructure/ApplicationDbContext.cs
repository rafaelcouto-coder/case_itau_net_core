using CaseItau.Application.Exceptions;
using CaseItau.Domain.Abstractions;
using CaseItau.Domain.Fundos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CaseItau.Infrastructure;

public sealed class ApplicationDbContext(
    DbContextOptions opt,
    IPublisher publisher
    ) : DbContext(opt), IUnitOfWork
{

    private static readonly JsonSerializerSettings JsonSerializerSettings = new()
    {
        TypeNameHandling = TypeNameHandling.All
    };

    private readonly IPublisher _publisher = publisher;

    public DbSet<Fundo> User { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        try
        {
            var result = await base.SaveChangesAsync(ct);

            return result;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new ConcurrencyException("Concurrency Exception occurred.", ex);
        }
    }
}