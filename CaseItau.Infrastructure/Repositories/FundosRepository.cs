using CaseItau.Domain.Fundos;
using CaseItau.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CaseItau.Infrastructure.Repositories;

internal sealed class FundosRepository(ApplicationDbContext dbContext) : IFundosRepository
{
    private readonly ApplicationDbContext _db = dbContext;

    public async Task<IEnumerable<Fundo>> GetAllAsync(CancellationToken ct = default)
    {
        return await _db.Set<Fundo>()
            .Include(f => f.TipoFundo)
            .ToListAsync(ct);
    }

    public async Task<Fundo?> GetByCodeAsync(string code, CancellationToken ct = default)
    {
        return await _db.Set<Fundo>()
            .Include(f => f.TipoFundo)
            .FirstOrDefaultAsync(x => x.Codigo == code, ct);
    }

    public async Task<Fundo?> GetByCnpjAsync(string cnpj, CancellationToken ct = default)
    {
        return await _db.Set<Fundo>()
            .Include(f => f.TipoFundo)
            .FirstOrDefaultAsync(x => x.Cnpj == cnpj, ct);
    }

    public void Add(Fundo entity)
    {
        _db.Add(entity);
    }

    public void Delete(Fundo entity)
    {
        _db.Set<Fundo>().Remove(entity);
    }

    public void Update(Fundo entity)
    {
        _db.Set<Fundo>().Update(entity);
    }
}
