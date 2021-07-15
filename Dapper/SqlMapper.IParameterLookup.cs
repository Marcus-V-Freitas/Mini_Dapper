namespace Dapper
{
    public static partial class SqlMapper
    {
        public interface IParameterLookup : IDynamicParameters
        {
            object this[string name] { get; }
        }
    }
}
