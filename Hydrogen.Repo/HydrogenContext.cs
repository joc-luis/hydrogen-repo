using System;
using System.Data;
using Hydrogen.Repo.Enums;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace Hydrogen.Repo
{
    public class HydrogenContext
    {
        public QueryFactory QueryFactory { get; }
        public IDbTransaction? DbTransaction { get; set; }

        public HydrogenContext(IDbConnection connection, DbConnectionType connectionType)
        {
            switch (connectionType)
            {
                case DbConnectionType.Npgsql:
                    QueryFactory = new QueryFactory(connection, new PostgresCompiler());
                    break;
                case DbConnectionType.MySql:
                    QueryFactory = new QueryFactory(connection, new MySqlCompiler());
                    break;
                case DbConnectionType.SqlServer:
                    QueryFactory = new QueryFactory(connection, new SqlServerCompiler());
                    break;
                case DbConnectionType.SQLite:
                    QueryFactory = new QueryFactory(connection, new SqliteCompiler());
                    break;
                case DbConnectionType.Oracle:
                    QueryFactory = new QueryFactory(connection, new OracleCompiler());
                    break;
                case DbConnectionType.Firebird:
                    QueryFactory = new QueryFactory(connection, new FirebirdCompiler());
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(connectionType), connectionType, null);
            }
        }
    }
}