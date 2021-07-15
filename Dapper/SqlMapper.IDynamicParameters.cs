using System.Data;

namespace Dapper
{
    public static partial class SqlMapper
    {
        public interface IDynamicParameters
        {
            void AddParameters(IDbCommand command, Identity identity);
        }
    }
}
