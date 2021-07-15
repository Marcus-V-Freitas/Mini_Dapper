using System.Collections.Generic;

namespace Dapper
{
    public static partial class SqlMapper
    {
        internal struct LiteralToken
        {
            public string Token { get; }

            public string Member { get; }

            internal LiteralToken(string token, string member)
            {
                Token = token;
                Member = member;
            }

            internal static readonly IList<LiteralToken> None = new LiteralToken[0];
        }
    }
}
