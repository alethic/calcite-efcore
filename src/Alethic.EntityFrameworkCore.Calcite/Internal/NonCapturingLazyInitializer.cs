using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace Alethic.EntityFrameworkCore.Calcite.Internal
{

    static class NonCapturingLazyInitializer
    {

        public static TValue EnsureInitialized<TParam, TValue>(
            [NotNull] ref TValue? target,
            TParam param,
            Func<TParam, TValue> valueFactory)
            where TValue : class
        {
            var tmp = Volatile.Read(ref target);
            if (tmp != null)
                return tmp;

            Interlocked.CompareExchange(ref target, valueFactory(param), null);

            return target;
        }

        public static TValue EnsureInitialized<TParam1, TParam2, TValue>(
            [NotNull] ref TValue? target,
            TParam1 param1,
            TParam2 param2,
            Func<TParam1, TParam2, TValue> valueFactory)
            where TValue : class
        {
            var tmp = Volatile.Read(ref target);
            if (tmp != null)
                return tmp;

            Interlocked.CompareExchange(ref target, valueFactory(param1, param2), null);

            return target;
        }

        public static TValue EnsureInitialized<TParam1, TParam2, TParam3, TValue>(
            [NotNull] ref TValue? target,
            TParam1 param1,
            TParam2 param2,
            TParam3 param3,
            Func<TParam1, TParam2, TParam3, TValue> valueFactory)
            where TValue : class
        {
            var tmp = Volatile.Read(ref target);
            if (tmp != null)
                return tmp;

            Interlocked.CompareExchange(ref target, valueFactory(param1, param2, param3), null);

            return target;
        }

        public static TValue EnsureInitialized<TParam, TValue>(
            ref TValue target,
            ref bool initialized,
            TParam param,
            Func<TParam, TValue> valueFactory)
            where TValue : class?
        {
            var alreadyInitialized = Volatile.Read(ref initialized);
            if (alreadyInitialized)
                return Volatile.Read(ref target);

            Volatile.Write(ref target, valueFactory(param));
            Volatile.Write(ref initialized, true);

            return target;
        }

        public static TValue EnsureInitialized<TValue>(
            [NotNull] ref TValue? target,
            TValue value)
            where TValue : class
        {
            var tmp = Volatile.Read(ref target);
            if (tmp != null)
                return tmp;

            Interlocked.CompareExchange(ref target, value, null);

            return target;
        }

        public static TValue EnsureInitialized<TParam, TValue>(
            [NotNull] ref TValue? target,
            TParam param,
            Action<TParam> valueFactory)
            where TValue : class
        {
            var tmp = Volatile.Read(ref target);
            if (tmp != null)
                return tmp;

            valueFactory(param);

            var tmp2 = Volatile.Read(ref target);
            return tmp2;
        }

    }

}
