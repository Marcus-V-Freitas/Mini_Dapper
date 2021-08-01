using System;
using System.Data;

namespace MiniDapper
{
    public static partial class SqlMapper
    {
        public class UdtTypeHandler : ITypeHandler
        {
            private readonly string udtTypeName;
            public UdtTypeHandler(string udtTypeName)
            {
                if (string.IsNullOrEmpty(udtTypeName)) throw new ArgumentException("Cannot be null or empty", udtTypeName);
                this.udtTypeName = udtTypeName;
            }

            object ITypeHandler.Parse(Type destinationType, object value)
            {
                return value is DBNull ? null : value;
            }

            void ITypeHandler.SetValue(IDbDataParameter parameter, object value)
            {
#pragma warning disable 0618
                parameter.Value = SanitizeParameterValue(value);
#pragma warning restore 0618
                if (!(value is DBNull)) StructuredHelper.ConfigureUDT(parameter, udtTypeName);
            }
        }
    }
}
