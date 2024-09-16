using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;

namespace Hydrogen.Repo.TypeHandlers;

public class ByteArrayTypeHandler : SqlMapper.TypeHandler<IEnumerable<byte>>
{
    public override void SetValue(IDbDataParameter parameter, IEnumerable<byte> value)
    {
        parameter.DbType = DbType.String;

        parameter.Value = Encoding.ASCII.GetString(value.ToArray());
    }

    public override IEnumerable<byte> Parse(object? value)
    {
        if (value == null)
        {
            return null;
        }

        return (byte[])value;
    }
}