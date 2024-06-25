using CaseItau.Domain.Fundos;

namespace CaseItau.Domain.Repositories;

public interface IFundosRepository
{
    Task<IEnumerable<Fundo>> GetAllAsync(CancellationToken ct = default);
    Task<Fundo?> GetByCodeAsync(string code, CancellationToken ct = default);
    Task<Fundo?> GetByCnpjAsync(string cnpj, CancellationToken ct = default);
    void Add(Fundo entity);
    void Delete(Fundo entity);
    void Update(Fundo entity);
}
