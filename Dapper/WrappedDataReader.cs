using System.Data;

namespace MiniDapper
{
    public interface IWrappedDataReader : IDataReader
    {
        IDataReader Reader { get; }
        IDbCommand Command { get; }
    }
}
