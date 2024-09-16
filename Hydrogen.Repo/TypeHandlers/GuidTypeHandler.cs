using System;
using System.Data;
using Dapper;

namespace Hydrogen.Repo.TypeHandlers;

public class GuidTypeHandler : SqlMapper.TypeHandler<Guid>
{
    public override void SetValue(IDbDataParameter parameter, Guid value)
    {
        parameter.DbType = DbType.Guid;

        parameter.Value = value;
    }

    public override Guid Parse(object value)
    {
        return Guid.Parse(value.ToString());
    }
}