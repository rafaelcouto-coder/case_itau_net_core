using System.Data;

namespace CaseItau.Application.Abstractions.Data;

public interface IConnectionFactory
{
    IDbConnection CreateConnection();
}
