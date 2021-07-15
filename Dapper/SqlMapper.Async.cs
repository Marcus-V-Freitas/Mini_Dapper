using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Dapper
{
    public static partial class SqlMapper
    {
        public static Task<IEnumerable<dynamic>> QueryAsync(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null) =>
            QueryAsync<dynamic>(cnn, typeof(DapperRow), new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.Buffered, default(CancellationToken)));

        public static Task<IEnumerable<dynamic>> QueryAsync(this IDbConnection cnn, CommandDefinition command) =>
            QueryAsync<dynamic>(cnn, typeof(DapperRow), command);

        public static Task<dynamic> QueryFirstAsync(this IDbConnection cnn, CommandDefinition command) =>
            QueryRowAsync<dynamic>(cnn, Row.First, typeof(DapperRow), command);

        public static Task<dynamic> QueryFirstOrDefaultAsync(this IDbConnection cnn, CommandDefinition command) =>
            QueryRowAsync<dynamic>(cnn, Row.FirstOrDefault, typeof(DapperRow), command);

        public static Task<dynamic> QuerySingleAsync(this IDbConnection cnn, CommandDefinition command) =>
            QueryRowAsync<dynamic>(cnn, Row.Single, typeof(DapperRow), command);

        public static Task<dynamic> QuerySingleOrDefaultAsync(this IDbConnection cnn, CommandDefinition command) =>
            QueryRowAsync<dynamic>(cnn, Row.SingleOrDefault, typeof(DapperRow), command);

        public static Task<IEnumerable<T>> QueryAsync<T>(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null) =>
            QueryAsync<T>(cnn, typeof(T), new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.Buffered, default(CancellationToken)));

        public static Task<T> QueryFirstAsync<T>(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null) =>
            QueryRowAsync<T>(cnn, Row.First, typeof(T), new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.None, default(CancellationToken)));

        public static Task<T> QueryFirstOrDefaultAsync<T>(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null) =>
            QueryRowAsync<T>(cnn, Row.FirstOrDefault, typeof(T), new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.None, default(CancellationToken)));

        public static Task<T> QuerySingleAsync<T>(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null) =>
            QueryRowAsync<T>(cnn, Row.Single, typeof(T), new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.None, default(CancellationToken)));

        public static Task<T> QuerySingleOrDefaultAsync<T>(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null) =>
            QueryRowAsync<T>(cnn, Row.SingleOrDefault, typeof(T), new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.None, default(CancellationToken)));

        public static Task<dynamic> QueryFirstAsync(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null) =>
            QueryRowAsync<dynamic>(cnn, Row.First, typeof(DapperRow), new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.None, default(CancellationToken)));

        public static Task<dynamic> QueryFirstOrDefaultAsync(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null) =>
            QueryRowAsync<dynamic>(cnn, Row.FirstOrDefault, typeof(DapperRow), new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.None, default(CancellationToken)));

        public static Task<dynamic> QuerySingleAsync(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null) =>
            QueryRowAsync<dynamic>(cnn, Row.Single, typeof(DapperRow), new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.None, default(CancellationToken)));

        public static Task<dynamic> QuerySingleOrDefaultAsync(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null) =>
            QueryRowAsync<dynamic>(cnn, Row.SingleOrDefault, typeof(DapperRow), new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.None, default(CancellationToken)));

        public static Task<IEnumerable<object>> QueryAsync(this IDbConnection cnn, Type type, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            return QueryAsync<object>(cnn, type, new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.Buffered, default(CancellationToken)));
        }

        public static Task<object> QueryFirstAsync(this IDbConnection cnn, Type type, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            return QueryRowAsync<object>(cnn, Row.First, type, new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.None, default(CancellationToken)));
        }
        public static Task<object> QueryFirstOrDefaultAsync(this IDbConnection cnn, Type type, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            return QueryRowAsync<object>(cnn, Row.FirstOrDefault, type, new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.None, default(CancellationToken)));
        }
        public static Task<object> QuerySingleAsync(this IDbConnection cnn, Type type, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            return QueryRowAsync<object>(cnn, Row.Single, type, new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.None, default(CancellationToken)));
        }
        public static Task<object> QuerySingleOrDefaultAsync(this IDbConnection cnn, Type type, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            return QueryRowAsync<object>(cnn, Row.SingleOrDefault, type, new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.None, default(CancellationToken)));
        }

        public static Task<IEnumerable<T>> QueryAsync<T>(this IDbConnection cnn, CommandDefinition command) =>
            QueryAsync<T>(cnn, typeof(T), command);

        public static Task<IEnumerable<object>> QueryAsync(this IDbConnection cnn, Type type, CommandDefinition command) =>
            QueryAsync<object>(cnn, type, command);

        public static Task<object> QueryFirstAsync(this IDbConnection cnn, Type type, CommandDefinition command) =>
            QueryRowAsync<object>(cnn, Row.First, type, command);

        public static Task<T> QueryFirstAsync<T>(this IDbConnection cnn, CommandDefinition command) =>
            QueryRowAsync<T>(cnn, Row.First, typeof(T), command);

        public static Task<object> QueryFirstOrDefaultAsync(this IDbConnection cnn, Type type, CommandDefinition command) =>
            QueryRowAsync<object>(cnn, Row.FirstOrDefault, type, command);

        public static Task<T> QueryFirstOrDefaultAsync<T>(this IDbConnection cnn, CommandDefinition command) =>
            QueryRowAsync<T>(cnn, Row.FirstOrDefault, typeof(T), command);

        public static Task<object> QuerySingleAsync(this IDbConnection cnn, Type type, CommandDefinition command) =>
            QueryRowAsync<object>(cnn, Row.Single, type, command);

        public static Task<T> QuerySingleAsync<T>(this IDbConnection cnn, CommandDefinition command) =>
            QueryRowAsync<T>(cnn, Row.Single, typeof(T), command);

        public static Task<object> QuerySingleOrDefaultAsync(this IDbConnection cnn, Type type, CommandDefinition command) =>
            QueryRowAsync<object>(cnn, Row.SingleOrDefault, type, command);

        public static Task<T> QuerySingleOrDefaultAsync<T>(this IDbConnection cnn, CommandDefinition command) =>
            QueryRowAsync<T>(cnn, Row.SingleOrDefault, typeof(T), command);

        private static Task<DbDataReader> ExecuteReaderWithFlagsFallbackAsync(DbCommand cmd, bool wasClosed, CommandBehavior behavior, CancellationToken cancellationToken)
        {
            var task = cmd.ExecuteReaderAsync(GetBehavior(wasClosed, behavior), cancellationToken);
            if (task.Status == TaskStatus.Faulted && Settings.DisableCommandBehaviorOptimizations(behavior, task.Exception.InnerException))
            {
                return cmd.ExecuteReaderAsync(GetBehavior(wasClosed, behavior), cancellationToken);
            }
            return task;
        }

        private static Task TryOpenAsync(this IDbConnection cnn, CancellationToken cancel)
        {
            if (cnn is DbConnection dbConn)
            {
                return dbConn.OpenAsync(cancel);
            }
            else
            {
                throw new InvalidOperationException("Async operations require use of a DbConnection or an already-open IDbConnection");
            }
        }

        private static DbCommand TrySetupAsyncCommand(this CommandDefinition command, IDbConnection cnn, Action<IDbCommand, object> paramReader)
        {
            if (command.SetupCommand(cnn, paramReader) is DbCommand dbCommand)
            {
                return dbCommand;
            }
            else
            {
                throw new InvalidOperationException("Async operations require use of a DbConnection or an IDbConnection where .CreateCommand() returns a DbCommand");
            }
        }

        private static async Task<IEnumerable<T>> QueryAsync<T>(this IDbConnection cnn, Type effectiveType, CommandDefinition command)
        {
            object param = command.Parameters;
            var identity = new Identity(command.CommandText, command.CommandType, cnn, effectiveType, param?.GetType());
            var info = GetCacheInfo(identity, param, command.AddToCache);
            bool wasClosed = cnn.State == ConnectionState.Closed;
            var cancel = command.CancellationToken;
            using (var cmd = command.TrySetupAsyncCommand(cnn, info.ParamReader))
            {
                DbDataReader reader = null;
                try
                {
                    if (wasClosed) await cnn.TryOpenAsync(cancel).ConfigureAwait(false);
                    reader = await ExecuteReaderWithFlagsFallbackAsync(cmd, wasClosed, CommandBehavior.SequentialAccess | CommandBehavior.SingleResult, cancel).ConfigureAwait(false);

                    var tuple = info.Deserializer;
                    int hash = GetColumnHash(reader);
                    if (tuple.Func == null || tuple.Hash != hash)
                    {
                        if (reader.FieldCount == 0)
                            return Enumerable.Empty<T>();
                        tuple = info.Deserializer = new DeserializerState(hash, GetDeserializer(effectiveType, reader, 0, -1, false));
                        if (command.AddToCache) SetQueryCache(identity, info);
                    }

                    var func = tuple.Func;

                    if (command.Buffered)
                    {
                        var buffer = new List<T>();
                        var convertToType = Nullable.GetUnderlyingType(effectiveType) ?? effectiveType;
                        while (await reader.ReadAsync(cancel).ConfigureAwait(false))
                        {
                            object val = func(reader);
                            if (val == null || val is T)
                            {
                                buffer.Add((T)val);
                            }
                            else
                            {
                                buffer.Add((T)Convert.ChangeType(val, convertToType, CultureInfo.InvariantCulture));
                            }
                        }
                        while (await reader.NextResultAsync(cancel).ConfigureAwait(false)) { }
                        command.OnCompleted();
                        return buffer;
                    }
                    else
                    {
                        wasClosed = false;
                        var deferred = ExecuteReaderSync<T>(reader, func, command.Parameters);
                        reader = null;
                        return deferred;
                    }
                }
                finally
                {
                    using (reader) { }
                    if (wasClosed) cnn.Close();
                }
            }
        }

        private static async Task<T> QueryRowAsync<T>(this IDbConnection cnn, Row row, Type effectiveType, CommandDefinition command)
        {
            object param = command.Parameters;
            var identity = new Identity(command.CommandText, command.CommandType, cnn, effectiveType, param?.GetType());
            var info = GetCacheInfo(identity, param, command.AddToCache);
            bool wasClosed = cnn.State == ConnectionState.Closed;
            var cancel = command.CancellationToken;
            using (var cmd = command.TrySetupAsyncCommand(cnn, info.ParamReader))
            {
                DbDataReader reader = null;
                try
                {
                    if (wasClosed) await cnn.TryOpenAsync(cancel).ConfigureAwait(false);
                    reader = await ExecuteReaderWithFlagsFallbackAsync(cmd, wasClosed, (row & Row.Single) != 0
                    ? CommandBehavior.SequentialAccess | CommandBehavior.SingleResult
                    : CommandBehavior.SequentialAccess | CommandBehavior.SingleResult | CommandBehavior.SingleRow, cancel).ConfigureAwait(false);

                    T result = default(T);
                    if (await reader.ReadAsync(cancel).ConfigureAwait(false) && reader.FieldCount != 0)
                    {
                        var tuple = info.Deserializer;
                        int hash = GetColumnHash(reader);
                        if (tuple.Func == null || tuple.Hash != hash)
                        {
                            tuple = info.Deserializer = new DeserializerState(hash, GetDeserializer(effectiveType, reader, 0, -1, false));
                            if (command.AddToCache) SetQueryCache(identity, info);
                        }

                        var func = tuple.Func;

                        object val = func(reader);
                        if (val == null || val is T)
                        {
                            result = (T)val;
                        }
                        else
                        {
                            var convertToType = Nullable.GetUnderlyingType(effectiveType) ?? effectiveType;
                            result = (T)Convert.ChangeType(val, convertToType, CultureInfo.InvariantCulture);
                        }
                        if ((row & Row.Single) != 0 && await reader.ReadAsync(cancel).ConfigureAwait(false)) ThrowMultipleRows(row);
                        while (await reader.ReadAsync(cancel).ConfigureAwait(false)) { }
                    }
                    else if ((row & Row.FirstOrDefault) == 0)
                    {
                        ThrowZeroRows(row);
                    }
                    while (await reader.NextResultAsync(cancel).ConfigureAwait(false)) { }
                    return result;
                }
                finally
                {
                    using (reader) { }
                    if (wasClosed) cnn.Close();
                }
            }
        }

        public static Task<int> ExecuteAsync(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null) =>
            ExecuteAsync(cnn, new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.Buffered, default(CancellationToken)));

        public static Task<int> ExecuteAsync(this IDbConnection cnn, CommandDefinition command)
        {
            object param = command.Parameters;
            IEnumerable multiExec = GetMultiExec(param);
            if (multiExec != null)
            {
                return ExecuteMultiImplAsync(cnn, command, multiExec);
            }
            else
            {
                return ExecuteImplAsync(cnn, command, param);
            }
        }

        private struct AsyncExecState
        {
            public readonly DbCommand Command;
            public readonly Task<int> Task;
            public AsyncExecState(DbCommand command, Task<int> task)
            {
                Command = command;
                Task = task;
            }
        }

        private static async Task<int> ExecuteMultiImplAsync(IDbConnection cnn, CommandDefinition command, IEnumerable multiExec)
        {
            bool isFirst = true;
            int total = 0;
            bool wasClosed = cnn.State == ConnectionState.Closed;
            try
            {
                if (wasClosed) await cnn.TryOpenAsync(command.CancellationToken).ConfigureAwait(false);

                CacheInfo info = null;
                string masterSql = null;
                if ((command.Flags & CommandFlags.Pipelined) != 0)
                {
                    const int MAX_PENDING = 100;
                    var pending = new Queue<AsyncExecState>(MAX_PENDING);
                    DbCommand cmd = null;
                    try
                    {
                        foreach (var obj in multiExec)
                        {
                            if (isFirst)
                            {
                                isFirst = false;
                                cmd = command.TrySetupAsyncCommand(cnn, null);
                                masterSql = cmd.CommandText;
                                var identity = new Identity(command.CommandText, cmd.CommandType, cnn, null, obj.GetType());
                                info = GetCacheInfo(identity, obj, command.AddToCache);
                            }
                            else if (pending.Count >= MAX_PENDING)
                            {
                                var recycled = pending.Dequeue();
                                total += await recycled.Task.ConfigureAwait(false);
                                cmd = recycled.Command;
                                cmd.CommandText = masterSql;
                                cmd.Parameters.Clear();
                            }
                            else
                            {
                                cmd = command.TrySetupAsyncCommand(cnn, null);
                            }
                            info.ParamReader(cmd, obj);

                            var task = cmd.ExecuteNonQueryAsync(command.CancellationToken);
                            pending.Enqueue(new AsyncExecState(cmd, task));
                            cmd = null;
                        }
                        while (pending.Count != 0)
                        {
                            var pair = pending.Dequeue();
                            using (pair.Command) { }
                            total += await pair.Task.ConfigureAwait(false);
                        }
                    }
                    finally
                    {
                        using (cmd) { }
                        while (pending.Count != 0)
                        {
                            using (pending.Dequeue().Command) { }
                        }
                    }
                }
                else
                {
                    using (var cmd = command.TrySetupAsyncCommand(cnn, null))
                    {
                        foreach (var obj in multiExec)
                        {
                            if (isFirst)
                            {
                                masterSql = cmd.CommandText;
                                isFirst = false;
                                var identity = new Identity(command.CommandText, cmd.CommandType, cnn, null, obj.GetType());
                                info = GetCacheInfo(identity, obj, command.AddToCache);
                            }
                            else
                            {
                                cmd.CommandText = masterSql;
                                cmd.Parameters.Clear();
                            }
                            info.ParamReader(cmd, obj);
                            total += await cmd.ExecuteNonQueryAsync(command.CancellationToken).ConfigureAwait(false);
                        }
                    }
                }

                command.OnCompleted();
            }
            finally
            {
                if (wasClosed) cnn.Close();
            }
            return total;
        }

        private static async Task<int> ExecuteImplAsync(IDbConnection cnn, CommandDefinition command, object param)
        {
            var identity = new Identity(command.CommandText, command.CommandType, cnn, null, param?.GetType());
            var info = GetCacheInfo(identity, param, command.AddToCache);
            bool wasClosed = cnn.State == ConnectionState.Closed;
            using (var cmd = command.TrySetupAsyncCommand(cnn, info.ParamReader))
            {
                try
                {
                    if (wasClosed) await cnn.TryOpenAsync(command.CancellationToken).ConfigureAwait(false);
                    var result = await cmd.ExecuteNonQueryAsync(command.CancellationToken).ConfigureAwait(false);
                    command.OnCompleted();
                    return result;
                }
                finally
                {
                    if (wasClosed) cnn.Close();
                }
            }
        }

        public static Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(this IDbConnection cnn, string sql, Func<TFirst, TSecond, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null) =>
            MultiMapAsync<TFirst, TSecond, DontMap, DontMap, DontMap, DontMap, DontMap, TReturn>(cnn,
                new CommandDefinition(sql, param, transaction, commandTimeout, commandType, buffered ? CommandFlags.Buffered : CommandFlags.None, default(CancellationToken)), map, splitOn);

        public static Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(this IDbConnection cnn, CommandDefinition command, Func<TFirst, TSecond, TReturn> map, string splitOn = "Id") =>
            MultiMapAsync<TFirst, TSecond, DontMap, DontMap, DontMap, DontMap, DontMap, TReturn>(cnn, command, map, splitOn);

        public static Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TReturn>(this IDbConnection cnn, string sql, Func<TFirst, TSecond, TThird, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null) =>
            MultiMapAsync<TFirst, TSecond, TThird, DontMap, DontMap, DontMap, DontMap, TReturn>(cnn,
                new CommandDefinition(sql, param, transaction, commandTimeout, commandType, buffered ? CommandFlags.Buffered : CommandFlags.None, default(CancellationToken)), map, splitOn);

        public static Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TReturn>(this IDbConnection cnn, CommandDefinition command, Func<TFirst, TSecond, TThird, TReturn> map, string splitOn = "Id") =>
            MultiMapAsync<TFirst, TSecond, TThird, DontMap, DontMap, DontMap, DontMap, TReturn>(cnn, command, map, splitOn);

        public static Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TReturn>(this IDbConnection cnn, string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null) =>
            MultiMapAsync<TFirst, TSecond, TThird, TFourth, DontMap, DontMap, DontMap, TReturn>(cnn,
                new CommandDefinition(sql, param, transaction, commandTimeout, commandType, buffered ? CommandFlags.Buffered : CommandFlags.None, default(CancellationToken)), map, splitOn);

        public static Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TReturn>(this IDbConnection cnn, CommandDefinition command, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, string splitOn = "Id") =>
            MultiMapAsync<TFirst, TSecond, TThird, TFourth, DontMap, DontMap, DontMap, TReturn>(cnn, command, map, splitOn);

        public static Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(this IDbConnection cnn, string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null) =>
            MultiMapAsync<TFirst, TSecond, TThird, TFourth, TFifth, DontMap, DontMap, TReturn>(cnn,
                new CommandDefinition(sql, param, transaction, commandTimeout, commandType, buffered ? CommandFlags.Buffered : CommandFlags.None, default(CancellationToken)), map, splitOn);

        public static Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(this IDbConnection cnn, CommandDefinition command, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, string splitOn = "Id") =>
            MultiMapAsync<TFirst, TSecond, TThird, TFourth, TFifth, DontMap, DontMap, TReturn>(cnn, command, map, splitOn);

        public static Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(this IDbConnection cnn, string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null) =>
            MultiMapAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, DontMap, TReturn>(cnn,
                new CommandDefinition(sql, param, transaction, commandTimeout, commandType, buffered ? CommandFlags.Buffered : CommandFlags.None, default(CancellationToken)), map, splitOn);

        public static Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(this IDbConnection cnn, CommandDefinition command, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map, string splitOn = "Id") =>
             MultiMapAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, DontMap, TReturn>(cnn, command, map, splitOn);

        public static Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(this IDbConnection cnn, string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null) =>
            MultiMapAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(cnn,
                new CommandDefinition(sql, param, transaction, commandTimeout, commandType, buffered ? CommandFlags.Buffered : CommandFlags.None, default(CancellationToken)), map, splitOn);

        public static Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(this IDbConnection cnn, CommandDefinition command, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map, string splitOn = "Id") =>
            MultiMapAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(cnn, command, map, splitOn);

        private static async Task<IEnumerable<TReturn>> MultiMapAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(this IDbConnection cnn, CommandDefinition command, Delegate map, string splitOn)
        {
            object param = command.Parameters;
            var identity = new Identity<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh>(command.CommandText, command.CommandType, cnn, typeof(TFirst), param?.GetType());
            var info = GetCacheInfo(identity, param, command.AddToCache);
            bool wasClosed = cnn.State == ConnectionState.Closed;
            try
            {
                if (wasClosed) await cnn.TryOpenAsync(command.CancellationToken).ConfigureAwait(false);
                using (var cmd = command.TrySetupAsyncCommand(cnn, info.ParamReader))
                using (var reader = await ExecuteReaderWithFlagsFallbackAsync(cmd, wasClosed, CommandBehavior.SequentialAccess | CommandBehavior.SingleResult, command.CancellationToken).ConfigureAwait(false))
                {
                    if (!command.Buffered) wasClosed = false;
                    var results = MultiMapImpl<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(null, CommandDefinition.ForCallback(command.Parameters), map, splitOn, reader, identity, true);
                    return command.Buffered ? results.ToList() : results;
                }
            }
            finally
            {
                if (wasClosed) cnn.Close();
            }
        }

        public static Task<IEnumerable<TReturn>> QueryAsync<TReturn>(this IDbConnection cnn, string sql, Type[] types, Func<object[], TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            var command = new CommandDefinition(sql, param, transaction, commandTimeout, commandType, buffered ? CommandFlags.Buffered : CommandFlags.None, default(CancellationToken));
            return MultiMapAsync(cnn, command, types, map, splitOn);
        }

        private static async Task<IEnumerable<TReturn>> MultiMapAsync<TReturn>(this IDbConnection cnn, CommandDefinition command, Type[] types, Func<object[], TReturn> map, string splitOn)
        {
            if (types.Length < 1)
            {
                throw new ArgumentException("you must provide at least one type to deserialize");
            }

            object param = command.Parameters;
            var identity = new IdentityWithTypes(command.CommandText, command.CommandType, cnn, types[0], param?.GetType(), types);
            var info = GetCacheInfo(identity, param, command.AddToCache);
            bool wasClosed = cnn.State == ConnectionState.Closed;
            try
            {
                if (wasClosed) await cnn.TryOpenAsync(command.CancellationToken).ConfigureAwait(false);
                using (var cmd = command.TrySetupAsyncCommand(cnn, info.ParamReader))
                using (var reader = await ExecuteReaderWithFlagsFallbackAsync(cmd, wasClosed, CommandBehavior.SequentialAccess | CommandBehavior.SingleResult, command.CancellationToken).ConfigureAwait(false))
                {
                    var results = MultiMapImpl(null, default(CommandDefinition), types, map, splitOn, reader, identity, true);
                    return command.Buffered ? results.ToList() : results;
                }
            }
            finally
            {
                if (wasClosed) cnn.Close();
            }
        }

        private static IEnumerable<T> ExecuteReaderSync<T>(IDataReader reader, Func<IDataReader, object> func, object parameters)
        {
            using (reader)
            {
                while (reader.Read())
                {
                    yield return (T)func(reader);
                }
                while (reader.NextResult()) { }
                (parameters as IParameterCallbacks)?.OnCompleted();
            }
        }

        public static Task<GridReader> QueryMultipleAsync(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null) =>
            QueryMultipleAsync(cnn, new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.Buffered));

        public static async Task<GridReader> QueryMultipleAsync(this IDbConnection cnn, CommandDefinition command)
        {
            object param = command.Parameters;
            var identity = new Identity(command.CommandText, command.CommandType, cnn, typeof(GridReader), param?.GetType());
            CacheInfo info = GetCacheInfo(identity, param, command.AddToCache);

            DbCommand cmd = null;
            IDataReader reader = null;
            bool wasClosed = cnn.State == ConnectionState.Closed;
            try
            {
                if (wasClosed) await cnn.TryOpenAsync(command.CancellationToken).ConfigureAwait(false);
                cmd = command.TrySetupAsyncCommand(cnn, info.ParamReader);
                reader = await ExecuteReaderWithFlagsFallbackAsync(cmd, wasClosed, CommandBehavior.SequentialAccess, command.CancellationToken).ConfigureAwait(false);

                var result = new GridReader(cmd, reader, identity, command.Parameters as DynamicParameters, command.AddToCache, command.CancellationToken);
                wasClosed = false;
                return result;
            }
            catch
            {
                if (reader != null)
                {
                    if (!reader.IsClosed)
                    {
                        try { cmd.Cancel(); }
                        catch
                        {
                        }
                    }
                    reader.Dispose();
                }
                cmd?.Dispose();
                if (wasClosed) cnn.Close();
                throw;
            }
        }

        public static Task<IDataReader> ExecuteReaderAsync(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null) =>
            ExecuteWrappedReaderImplAsync(cnn, new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.Buffered), CommandBehavior.Default).CastResult<DbDataReader, IDataReader>();

        public static Task<DbDataReader> ExecuteReaderAsync(this DbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null) =>
            ExecuteWrappedReaderImplAsync(cnn, new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.Buffered), CommandBehavior.Default);

        public static Task<IDataReader> ExecuteReaderAsync(this IDbConnection cnn, CommandDefinition command) =>
            ExecuteWrappedReaderImplAsync(cnn, command, CommandBehavior.Default).CastResult<DbDataReader, IDataReader>();

        public static Task<DbDataReader> ExecuteReaderAsync(this DbConnection cnn, CommandDefinition command) =>
            ExecuteWrappedReaderImplAsync(cnn, command, CommandBehavior.Default);

        public static Task<IDataReader> ExecuteReaderAsync(this IDbConnection cnn, CommandDefinition command, CommandBehavior commandBehavior) =>
            ExecuteWrappedReaderImplAsync(cnn, command, commandBehavior).CastResult<DbDataReader, IDataReader>();

        public static Task<DbDataReader> ExecuteReaderAsync(this DbConnection cnn, CommandDefinition command, CommandBehavior commandBehavior) =>
            ExecuteWrappedReaderImplAsync(cnn, command, commandBehavior);

        private static async Task<DbDataReader> ExecuteWrappedReaderImplAsync(IDbConnection cnn, CommandDefinition command, CommandBehavior commandBehavior)
        {
            Action<IDbCommand, object> paramReader = GetParameterReader(cnn, ref command);

            DbCommand cmd = null;
            bool wasClosed = cnn.State == ConnectionState.Closed, disposeCommand = true;
            try
            {
                cmd = command.TrySetupAsyncCommand(cnn, paramReader);
                if (wasClosed) await cnn.TryOpenAsync(command.CancellationToken).ConfigureAwait(false);
                var reader = await ExecuteReaderWithFlagsFallbackAsync(cmd, wasClosed, commandBehavior, command.CancellationToken).ConfigureAwait(false);
                wasClosed = false;
                disposeCommand = false;
                return WrappedReader.Create(cmd, reader);
            }
            finally
            {
                if (wasClosed) cnn.Close();
                if (cmd != null && disposeCommand) cmd.Dispose();
            }
        }

        public static Task<object> ExecuteScalarAsync(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null) =>
            ExecuteScalarImplAsync<object>(cnn, new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.Buffered));

        public static Task<T> ExecuteScalarAsync<T>(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null) =>
            ExecuteScalarImplAsync<T>(cnn, new CommandDefinition(sql, param, transaction, commandTimeout, commandType, CommandFlags.Buffered));

        public static Task<object> ExecuteScalarAsync(this IDbConnection cnn, CommandDefinition command) =>
            ExecuteScalarImplAsync<object>(cnn, command);

        public static Task<T> ExecuteScalarAsync<T>(this IDbConnection cnn, CommandDefinition command) =>
            ExecuteScalarImplAsync<T>(cnn, command);

        private static async Task<T> ExecuteScalarImplAsync<T>(IDbConnection cnn, CommandDefinition command)
        {
            Action<IDbCommand, object> paramReader = null;
            object param = command.Parameters;
            if (param != null)
            {
                var identity = new Identity(command.CommandText, command.CommandType, cnn, null, param.GetType());
                paramReader = GetCacheInfo(identity, command.Parameters, command.AddToCache).ParamReader;
            }

            DbCommand cmd = null;
            bool wasClosed = cnn.State == ConnectionState.Closed;
            object result;
            try
            {
                cmd = command.TrySetupAsyncCommand(cnn, paramReader);
                if (wasClosed) await cnn.TryOpenAsync(command.CancellationToken).ConfigureAwait(false);
                result = await cmd.ExecuteScalarAsync(command.CancellationToken).ConfigureAwait(false);
                command.OnCompleted();
            }
            finally
            {
                if (wasClosed) cnn.Close();
                cmd?.Dispose();
            }
            return Parse<T>(result);
        }
    }
}
