using System;
using System.Data;

namespace MiniDapper
{
    public static partial class SqlMapper
    {
        public interface ITypeHandler
        {
            void SetValue(IDbDataParameter parameter, object value);

            object Parse(Type destinationType, object value);
        }
    }
}
