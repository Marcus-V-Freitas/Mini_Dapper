using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;

namespace Dapper
{
    public partial class DynamicParameters : SqlMapper.IDynamicParameters, SqlMapper.IParameterLookup, SqlMapper.IParameterCallbacks
    {
        internal const DbType EnumerableMultiParameter = (DbType)(-1);
        private static readonly Dictionary<SqlMapper.Identity, Action<IDbCommand, object>> paramReaderCache = new Dictionary<SqlMapper.Identity, Action<IDbCommand, object>>();
        private readonly Dictionary<string, ParamInfo> parameters = new Dictionary<string, ParamInfo>();
        private List<object> templates;

        object SqlMapper.IParameterLookup.this[string name] =>
            parameters.TryGetValue(name, out ParamInfo param) ? param.Value : null;

        public DynamicParameters()
        {
            RemoveUnused = true;
        }

        public DynamicParameters(object template)
        {
            RemoveUnused = true;
            AddDynamicParams(template);
        }

        public void AddDynamicParams(object param)
        {
            var obj = param;
            if (obj != null)
            {
                var subDynamic = obj as DynamicParameters;
                if (subDynamic == null)
                {
                    var dictionary = obj as IEnumerable<KeyValuePair<string, object>>;
                    if (dictionary == null)
                    {
                        templates = templates ?? new List<object>();
                        templates.Add(obj);
                    }
                    else
                    {
                        foreach (var kvp in dictionary)
                        {
                            Add(kvp.Key, kvp.Value, null, null, null);
                        }
                    }
                }
                else
                {
                    if (subDynamic.parameters != null)
                    {
                        foreach (var kvp in subDynamic.parameters)
                        {
                            parameters.Add(kvp.Key, kvp.Value);
                        }
                    }

                    if (subDynamic.templates != null)
                    {
                        templates = templates ?? new List<object>();
                        foreach (var t in subDynamic.templates)
                        {
                            templates.Add(t);
                        }
                    }
                }
            }
        }

        public void Add(string name, object value, DbType? dbType, ParameterDirection? direction, int? size)
        {
            parameters[Clean(name)] = new ParamInfo
            {
                Name = name,
                Value = value,
                ParameterDirection = direction ?? ParameterDirection.Input,
                DbType = dbType,
                Size = size
            };
        }

        public void Add(string name, object value = null, DbType? dbType = null, ParameterDirection? direction = null, int? size = null, byte? precision = null, byte? scale = null)
        {
            parameters[Clean(name)] = new ParamInfo
            {
                Name = name,
                Value = value,
                ParameterDirection = direction ?? ParameterDirection.Input,
                DbType = dbType,
                Size = size,
                Precision = precision,
                Scale = scale
            };
        }

        private static string Clean(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                switch (name[0])
                {
                    case '@':
                    case ':':
                    case '?':
                        return name.Substring(1);
                }
            }
            return name;
        }

        void SqlMapper.IDynamicParameters.AddParameters(IDbCommand command, SqlMapper.Identity identity)
        {
            AddParameters(command, identity);
        }

        public bool RemoveUnused { get; set; }

        protected void AddParameters(IDbCommand command, SqlMapper.Identity identity)
        {
            var literals = SqlMapper.GetLiteralTokens(identity.sql);

            if (templates != null)
            {
                foreach (var template in templates)
                {
                    var newIdent = identity.ForDynamicParameters(template.GetType());
                    Action<IDbCommand, object> appender;

                    lock (paramReaderCache)
                    {
                        if (!paramReaderCache.TryGetValue(newIdent, out appender))
                        {
                            appender = SqlMapper.CreateParamInfoGenerator(newIdent, true, RemoveUnused, literals);
                            paramReaderCache[newIdent] = appender;
                        }
                    }

                    appender(command, template);
                }

                foreach (IDbDataParameter param in command.Parameters)
                {
                    if (!parameters.ContainsKey(param.ParameterName))
                    {
                        parameters.Add(param.ParameterName, new ParamInfo
                        {
                            AttachedParam = param,
                            CameFromTemplate = true,
                            DbType = param.DbType,
                            Name = param.ParameterName,
                            ParameterDirection = param.Direction,
                            Size = param.Size,
                            Value = param.Value
                        });
                    }
                }

                var tmp = outputCallbacks;
                if (tmp != null)
                {
                    foreach (var generator in tmp)
                    {
                        generator();
                    }
                }
            }

            foreach (var param in parameters.Values)
            {
                if (param.CameFromTemplate) continue;

                var dbType = param.DbType;
                var val = param.Value;
                string name = Clean(param.Name);
                var isCustomQueryParameter = val is SqlMapper.ICustomQueryParameter;

                SqlMapper.ITypeHandler handler = null;
                if (dbType == null && val != null && !isCustomQueryParameter)
                {
#pragma warning disable 618
                    dbType = SqlMapper.LookupDbType(val.GetType(), name, true, out handler);
#pragma warning disable 618
                }
                if (isCustomQueryParameter)
                {
                    ((SqlMapper.ICustomQueryParameter)val).AddParameter(command, name);
                }
                else if (dbType == EnumerableMultiParameter)
                {
#pragma warning disable 612, 618
                    SqlMapper.PackListParameters(command, name, val);
#pragma warning restore 612, 618
                }
                else
                {
                    bool add = !command.Parameters.Contains(name);
                    IDbDataParameter p;
                    if (add)
                    {
                        p = command.CreateParameter();
                        p.ParameterName = name;
                    }
                    else
                    {
                        p = (IDbDataParameter)command.Parameters[name];
                    }

                    p.Direction = param.ParameterDirection;
                    if (handler == null)
                    {
#pragma warning disable 0618
                        p.Value = SqlMapper.SanitizeParameterValue(val);
#pragma warning restore 0618
                        if (dbType != null && p.DbType != dbType)
                        {
                            p.DbType = dbType.Value;
                        }
                        var s = val as string;
                        if (s?.Length <= DbString.DefaultLength)
                        {
                            p.Size = DbString.DefaultLength;
                        }
                        if (param.Size != null) p.Size = param.Size.Value;
                        if (param.Precision != null) p.Precision = param.Precision.Value;
                        if (param.Scale != null) p.Scale = param.Scale.Value;
                    }
                    else
                    {
                        if (dbType != null) p.DbType = dbType.Value;
                        if (param.Size != null) p.Size = param.Size.Value;
                        if (param.Precision != null) p.Precision = param.Precision.Value;
                        if (param.Scale != null) p.Scale = param.Scale.Value;
                        handler.SetValue(p, val ?? DBNull.Value);
                    }

                    if (add)
                    {
                        command.Parameters.Add(p);
                    }
                    param.AttachedParam = p;
                }
            }

            if (literals.Count != 0) SqlMapper.ReplaceLiterals(this, command, literals);
        }

        public IEnumerable<string> ParameterNames => parameters.Select(p => p.Key);

        public T Get<T>(string name)
        {
            var paramInfo = parameters[Clean(name)];
            var attachedParam = paramInfo.AttachedParam;
            object val = attachedParam == null ? paramInfo.Value : attachedParam.Value;
            if (val == DBNull.Value)
            {
                if (default(T) != null)
                {
                    throw new ApplicationException("Attempting to cast a DBNull to a non nullable type! Note that out/return parameters will not have updated values until the data stream completes (after the 'foreach' for Query(..., buffered: false), or after the GridReader has been disposed for QueryMultiple)");
                }
                return default(T);
            }
            return (T)val;
        }

        public DynamicParameters Output<T>(T target, Expression<Func<T, object>> expression, DbType? dbType = null, int? size = null)
        {
            var failMessage = "Expression must be a property/field chain off of a(n) {0} instance";
            failMessage = string.Format(failMessage, typeof(T).Name);
            Action @throw = () => throw new InvalidOperationException(failMessage);

            var lastMemberAccess = expression.Body as MemberExpression;

            if (lastMemberAccess == null
                || (!(lastMemberAccess.Member is PropertyInfo)
                    && !(lastMemberAccess.Member is FieldInfo)))
            {
                if (expression.Body.NodeType == ExpressionType.Convert
                    && expression.Body.Type == typeof(object)
                    && ((UnaryExpression)expression.Body).Operand is MemberExpression)
                {
                    lastMemberAccess = (MemberExpression)((UnaryExpression)expression.Body).Operand;
                }
                else
                {
                    @throw();
                }
            }

            MemberExpression diving = lastMemberAccess;
            List<string> names = new List<string>();
            List<MemberExpression> chain = new List<MemberExpression>();

            do
            {
                names.Insert(0, diving?.Member.Name);
                chain.Insert(0, diving);

                var constant = diving?.Expression as ParameterExpression;
                diving = diving?.Expression as MemberExpression;

                if (constant != null && constant.Type == typeof(T))
                {
                    break;
                }
                else if (diving == null
                    || (!(diving.Member is PropertyInfo)
                        && !(diving.Member is FieldInfo)))
                {
                    @throw();
                }
            }
            while (diving != null);

            var dynamicParamName = string.Concat(names.ToArray());

            var lookup = string.Join("|", names.ToArray());

            var cache = CachedOutputSetters<T>.Cache;
            var setter = (Action<object, DynamicParameters>)cache[lookup];
            if (setter != null) goto MAKECALLBACK;

            var dm = new DynamicMethod("ExpressionParam" + Guid.NewGuid().ToString(), null, new[] { typeof(object), GetType() }, true);
            var il = dm.GetILGenerator();

            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Castclass, typeof(T));

            var i = 0;
            for (; i < (chain.Count - 1); i++)
            {
                var member = chain[0].Member;

                if (member is PropertyInfo)
                {
                    var get = ((PropertyInfo)member).GetGetMethod(true);
                    il.Emit(OpCodes.Callvirt, get);
                }
                else
                {
                    il.Emit(OpCodes.Ldfld, (FieldInfo)member);
                }
            }

            var paramGetter = GetType().GetMethod("Get", new Type[] { typeof(string) }).MakeGenericMethod(lastMemberAccess.Type);

            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Ldstr, dynamicParamName);
            il.Emit(OpCodes.Callvirt, paramGetter);

            var lastMember = lastMemberAccess.Member;
            if (lastMember is PropertyInfo)
            {
                var set = ((PropertyInfo)lastMember).GetSetMethod(true);
                il.Emit(OpCodes.Callvirt, set);
            }
            else
            {
                il.Emit(OpCodes.Stfld, (FieldInfo)lastMember);
            }

            il.Emit(OpCodes.Ret);

            setter = (Action<object, DynamicParameters>)dm.CreateDelegate(typeof(Action<object, DynamicParameters>));
            lock (cache)
            {
                cache[lookup] = setter;
            }

        MAKECALLBACK:
            (outputCallbacks ?? (outputCallbacks = new List<Action>())).Add(() =>
            {
                var targetMemberType = lastMemberAccess?.Type;
                int sizeToSet = (!size.HasValue && targetMemberType == typeof(string)) ? DbString.DefaultLength : size ?? 0;

                if (parameters.TryGetValue(dynamicParamName, out ParamInfo parameter))
                {
                    parameter.ParameterDirection = parameter.AttachedParam.Direction = ParameterDirection.InputOutput;

                    if (parameter.AttachedParam.Size == 0)
                    {
                        parameter.Size = parameter.AttachedParam.Size = sizeToSet;
                    }
                }
                else
                {
                    dbType = (!dbType.HasValue)
#pragma warning disable 618
                    ? SqlMapper.LookupDbType(targetMemberType, targetMemberType?.Name, true, out SqlMapper.ITypeHandler handler)
#pragma warning restore 618
                    : dbType;

                    Add(dynamicParamName, expression.Compile().Invoke(target), null, ParameterDirection.InputOutput, sizeToSet);
                }

                parameter = parameters[dynamicParamName];
                parameter.OutputCallback = setter;
                parameter.OutputTarget = target;
            });

            return this;
        }

        private List<Action> outputCallbacks;

        void SqlMapper.IParameterCallbacks.OnCompleted()
        {
            foreach (var param in from p in parameters select p.Value)
            {
                param.OutputCallback?.Invoke(param.OutputTarget, this);
            }
        }
    }
}
