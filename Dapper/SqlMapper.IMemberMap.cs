using System;
using System.Reflection;

namespace Dapper
{
    public static partial class SqlMapper
    {
        public interface IMemberMap
        {
            string ColumnName { get; }

            Type MemberType { get; }

            PropertyInfo Property { get; }

            FieldInfo Field { get; }

            ParameterInfo Parameter { get; }
        }
    }
}
