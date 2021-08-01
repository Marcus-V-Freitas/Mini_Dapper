using System.Data;

namespace MiniDapper
{
    public static partial class SqlMapper
    {
        public interface ICustomQueryParameter
        {
            void AddParameter(IDbCommand command, string name);
        }
    }
}
