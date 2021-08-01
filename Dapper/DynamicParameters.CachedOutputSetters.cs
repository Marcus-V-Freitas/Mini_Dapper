using System.Collections;

namespace MiniDapper
{
    public partial class DynamicParameters
    {
        internal static class CachedOutputSetters<T>
        {
            public static readonly Hashtable Cache = new Hashtable();
        }
    }
}
