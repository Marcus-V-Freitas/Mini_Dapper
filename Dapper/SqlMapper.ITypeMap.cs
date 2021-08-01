using System;
using System.Reflection;

namespace MiniDapper
{
    public static partial class SqlMapper
    {
        public interface ITypeMap
        {
            ConstructorInfo FindConstructor(string[] names, Type[] types);

            ConstructorInfo FindExplicitConstructor();

            IMemberMap GetConstructorParameter(ConstructorInfo constructor, string columnName);

            IMemberMap GetMember(string columnName);
        }
    }
}
