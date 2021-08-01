namespace MiniDapper
{
    public static partial class SqlMapper
    {

        public interface IParameterCallbacks : IDynamicParameters
        {
            void OnCompleted();
        }
    }
}
