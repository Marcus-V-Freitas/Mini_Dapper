using System.Data;

namespace Dapper
{
    public interface IWrappedDataReader : IDataReader
    {
        IDataReader Reader { get; }
        IDbCommand Command { get; }
    }
}
