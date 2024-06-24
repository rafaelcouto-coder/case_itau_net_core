using CaseItau.Application.Abstractions.Data;
using CaseItau.Infrastructure.Helpers;
using Microsoft.Data.Sqlite;
using System.Data;

namespace CaseItau.Infrastructure.Data
{
    internal class ConnectionFactory : IConnectionFactory
    {
        public IDbConnection CreateConnection() => new SqliteConnection(Constants.DbConnectionString);
    }
}
