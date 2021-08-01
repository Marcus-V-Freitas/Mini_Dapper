using System;
using System.Reflection;

namespace MiniDapper
{
    public sealed class CustomPropertyTypeMap : SqlMapper.ITypeMap
    {
        private readonly Type _type;
        private readonly Func<Type, string, PropertyInfo> _propertySelector;

        public CustomPropertyTypeMap(Type type, Func<Type, string, PropertyInfo> propertySelector)
        {
            _type = type ?? throw new ArgumentNullException(nameof(type));
            _propertySelector = propertySelector ?? throw new ArgumentNullException(nameof(propertySelector));
        }

        public ConstructorInfo FindConstructor(string[] names, Type[] types) =>
            _type.GetConstructor(new Type[0]);

        public ConstructorInfo FindExplicitConstructor() => null;

        public SqlMapper.IMemberMap GetConstructorParameter(ConstructorInfo constructor, string columnName)
        {
            throw new NotSupportedException();
        }

        public SqlMapper.IMemberMap GetMember(string columnName)
        {
            var prop = _propertySelector(_type, columnName);
            return prop != null ? new SimpleMemberMap(columnName, prop) : null;
        }
    }
}
