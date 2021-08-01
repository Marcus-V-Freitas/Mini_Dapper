using System.Data;

namespace MiniDapper
{
    public static partial class SqlMapper
    {
        public interface IDynamicParameters
        {
            void AddParameters(IDbCommand command, Identity identity);
        }
    }
}
