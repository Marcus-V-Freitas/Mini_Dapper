using System.Data;

namespace MiniDapper
{
    internal sealed class TableValuedParameter : SqlMapper.ICustomQueryParameter
    {
        private readonly DataTable table;
        private readonly string typeName;

        public TableValuedParameter(DataTable table) : this(table, null) { }

        public TableValuedParameter(DataTable table, string typeName)
        {
            this.table = table;
            this.typeName = typeName;
        }

        void SqlMapper.ICustomQueryParameter.AddParameter(IDbCommand command, string name)
        {
            var param = command.CreateParameter();
            param.ParameterName = name;
            Set(param, table, typeName);
            command.Parameters.Add(param);
        }

        internal static void Set(IDbDataParameter parameter, DataTable table, string typeName)
        {
#pragma warning disable 0618
            parameter.Value = SqlMapper.SanitizeParameterValue(table);
#pragma warning restore 0618
            if (string.IsNullOrEmpty(typeName) && table != null)
            {
                typeName = table.GetTypeName();
            }
            if (!string.IsNullOrEmpty(typeName)) StructuredHelper.ConfigureTVP(parameter, typeName);
        }
    }
}
