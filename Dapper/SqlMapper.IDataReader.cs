using System;
using System.Collections.Generic;
using System.Data;

namespace Dapper
{
    public static partial class SqlMapper
    {
        public static IEnumerable<T> Parse<T>(this IDataReader reader)
        {
            if (reader.Read())
            {
                var effectiveType = typeof(T);
                var deser = GetDeserializer(effectiveType, reader, 0, -1, false);
                var convertToType = Nullable.GetUnderlyingType(effectiveType) ?? effectiveType;
                do
                {
                    object val = deser(reader);
                    if (val == null || val is T)
                    {
                        yield return (T)val;
                    }
                    else
                    {
                        yield return (T)Convert.ChangeType(val, convertToType, System.Globalization.CultureInfo.InvariantCulture);
                    }
                } while (reader.Read());
            }
        }

        public static IEnumerable<object> Parse(this IDataReader reader, Type type)
        {
            if (reader.Read())
            {
                var deser = GetDeserializer(type, reader, 0, -1, false);
                do
                {
                    yield return deser(reader);
                } while (reader.Read());
            }
        }

        public static IEnumerable<dynamic> Parse(this IDataReader reader)
        {
            if (reader.Read())
            {
                var deser = GetDapperRowDeserializer(reader, 0, -1, false);
                do
                {
                    yield return deser(reader);
                } while (reader.Read());
            }
        }

        public static Func<IDataReader, object> GetRowParser(this IDataReader reader, Type type,
            int startIndex = 0, int length = -1, bool returnNullIfFirstMissing = false)
        {
            return GetDeserializer(type, reader, startIndex, length, returnNullIfFirstMissing);
        }

        public static Func<IDataReader, T> GetRowParser<T>(this IDataReader reader, Type concreteType = null,
            int startIndex = 0, int length = -1, bool returnNullIfFirstMissing = false)
        {
            concreteType = concreteType ?? typeof(T);
            var func = GetDeserializer(concreteType, reader, startIndex, length, returnNullIfFirstMissing);
            if (concreteType.IsValueType)
            {
                return _ => (T)func(_);
            }
            else
            {
                return (Func<IDataReader, T>)(Delegate)func;
            }
        }
    }
}
