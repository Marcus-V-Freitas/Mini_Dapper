using System;
using System.ComponentModel;
using System.Data;

namespace MiniDapper
{
    public static partial class SqlMapper
    {
        [Obsolete(ObsoleteInternalUsageOnly, false)]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static class TypeHandlerCache<T>
        {
            [Obsolete(ObsoleteInternalUsageOnly, true)]
            public static T Parse(object value) => (T)handler.Parse(typeof(T), value);

            [Obsolete(ObsoleteInternalUsageOnly, true)]
            public static void SetValue(IDbDataParameter parameter, object value) => handler.SetValue(parameter, value);

            internal static void SetHandler(ITypeHandler handler)
            {
#pragma warning disable 618
                TypeHandlerCache<T>.handler = handler;
#pragma warning restore 618
            }

            private static ITypeHandler handler;
        }
    }
}
