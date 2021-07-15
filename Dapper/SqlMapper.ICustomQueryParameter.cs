using System.Data;

namespace Dapper
{
    public static partial class SqlMapper
    {
        public interface ICustomQueryParameter
        {
            void AddParameter(IDbCommand command, string name);
        }
    }
}
