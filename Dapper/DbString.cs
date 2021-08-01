using System;
using System.Data;

namespace MiniDapper
{
    public sealed class DbString : SqlMapper.ICustomQueryParameter
    {
        public static bool IsAnsiDefault { get; set; }

        public const int DefaultLength = 4000;

        public DbString()
        {
            Length = -1;
            IsAnsi = IsAnsiDefault;
        }
        public bool IsAnsi { get; set; }
        public bool IsFixedLength { get; set; }
        public int Length { get; set; }
        public string Value { get; set; }
        public void AddParameter(IDbCommand command, string name)
        {
            if (IsFixedLength && Length == -1)
            {
                throw new InvalidOperationException("If specifying IsFixedLength,  a Length must also be specified");
            }
            bool add = !command.Parameters.Contains(name);
            IDbDataParameter param;
            if (add)
            {
                param = command.CreateParameter();
                param.ParameterName = name;
            }
            else
            {
                param = (IDbDataParameter)command.Parameters[name];
            }
#pragma warning disable 0618
            param.Value = SqlMapper.SanitizeParameterValue(Value);
#pragma warning restore 0618
            if (Length == -1 && Value != null && Value.Length <= DefaultLength)
            {
                param.Size = DefaultLength;
            }
            else
            {
                param.Size = Length;
            }
            param.DbType = IsAnsi ? (IsFixedLength ? DbType.AnsiStringFixedLength : DbType.AnsiString) : (IsFixedLength ? DbType.StringFixedLength : DbType.String);
            if (add)
            {
                command.Parameters.Add(param);
            }
        }
    }
}
