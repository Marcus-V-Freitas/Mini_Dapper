using System;
using System.Data;

namespace Dapper
{
    internal class FeatureSupport
    {
        private static readonly FeatureSupport
            Default = new FeatureSupport(false),
            Postgres = new FeatureSupport(true);

        public static FeatureSupport Get(IDbConnection connection)
        {
            string name = connection?.GetType().Name;
            if (string.Equals(name, "npgsqlconnection", StringComparison.OrdinalIgnoreCase)) return Postgres;
            return Default;
        }

        private FeatureSupport(bool arrays)
        {
            Arrays = arrays;
        }

        public bool Arrays { get; }
    }
}
