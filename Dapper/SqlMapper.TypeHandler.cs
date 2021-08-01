using System;
using System.Data;

namespace MiniDapper
{
    public static partial class SqlMapper
    {
        public abstract class TypeHandler<T> : ITypeHandler
        {
            public abstract void SetValue(IDbDataParameter parameter, T value);

            public abstract T Parse(object value);

            void ITypeHandler.SetValue(IDbDataParameter parameter, object value)
            {
                if (value is DBNull)
                {
                    parameter.Value = value;
                }
                else
                {
                    SetValue(parameter, (T)value);
                }
            }

            object ITypeHandler.Parse(Type destinationType, object value)
            {
                return Parse(value);
            }
        }

        public abstract class StringTypeHandler<T> : TypeHandler<T>
        {
            protected abstract T Parse(string xml);

            protected abstract string Format(T xml);

            public override void SetValue(IDbDataParameter parameter, T value)
            {
                parameter.Value = value == null ? (object)DBNull.Value : Format(value);
            }

            public override T Parse(object value)
            {
                if (value == null || value is DBNull) return default(T);
                return Parse((string)value);
            }
        }
    }
}
