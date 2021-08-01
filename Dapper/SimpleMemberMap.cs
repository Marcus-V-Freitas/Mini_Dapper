using System;
using System.Reflection;

namespace MiniDapper
{
    internal sealed class SimpleMemberMap : SqlMapper.IMemberMap
    {
        public SimpleMemberMap(string columnName, PropertyInfo property)
        {
            ColumnName = columnName ?? throw new ArgumentNullException(nameof(columnName));
            Property = property ?? throw new ArgumentNullException(nameof(property));
        }

        public SimpleMemberMap(string columnName, FieldInfo field)
        {
            ColumnName = columnName ?? throw new ArgumentNullException(nameof(columnName));
            Field = field ?? throw new ArgumentNullException(nameof(field));
        }

        public SimpleMemberMap(string columnName, ParameterInfo parameter)
        {
            ColumnName = columnName ?? throw new ArgumentNullException(nameof(columnName));
            Parameter = parameter ?? throw new ArgumentNullException(nameof(parameter));
        }

        public string ColumnName { get; }

        public Type MemberType => Field?.FieldType ?? Property?.PropertyType ?? Parameter?.ParameterType;

        public PropertyInfo Property { get; }

        public FieldInfo Field { get; }

        public ParameterInfo Parameter { get; }
    }
}
