using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

namespace LinqDataTables.Linq
{
    internal static class LinqExtensions
    {
        public static IOrderedQueryable<TSource> OrderBy<TSource>
        (
            this IQueryable<TSource> source,
            string field,
            bool ascending = false
        )
        {
            if (source is null)
                throw new ArgumentNullException("source is null");
            if (string.IsNullOrEmpty(field?.Trim()))
                throw new ArgumentException("Field is null or empty");

            var type = typeof(TSource);
            var property = type.GetProperty(field) ?? throw new InvalidDataException(field);
            var param = Expression.Parameter(type, "O");
            var propAccess = Expression.MakeMemberAccess(param, property);
            var lamdaExp = Expression.Lambda(propAccess, param);

            var argsTypes = new Type[] { type, property.PropertyType };
            var exp = Expression.Call(
                type: typeof(Queryable),
                methodName: ascending ? "OrderBy" : "OrderByDescending",
                argsTypes,
                source.Expression,
                Expression.Quote(lamdaExp)
            );

            return (IOrderedQueryable<TSource>)source.Provider.CreateQuery<TSource>(exp);
        }

        public static IOrderedQueryable<TSource> ThenBy<TSource>
        (
            this IOrderedQueryable<TSource> source,
            string field,
            bool ascending = false
        )
        {
            if (source is null)
                throw new ArgumentNullException("source is null");
            if (string.IsNullOrEmpty(field?.Trim()))
                throw new ArgumentException("Field is null or empty");

            var type = typeof(TSource);
            var property = type.GetProperty(field) ?? throw new InvalidDataException(field);
            var param = Expression.Parameter(type, "O");
            var propAccess = Expression.MakeMemberAccess(param, property);
            var lamdaExp = Expression.Lambda(propAccess, param);

            var argsTypes = new Type[] { type, property.PropertyType };
            var exp = Expression.Call(
                type: typeof(Queryable),
                methodName: ascending ? "ThenBy" : "ThenByDescending",
                argsTypes,
                source.Expression,
                Expression.Quote(lamdaExp)
            );

            return (IOrderedQueryable<TSource>)source.Provider.CreateQuery<TSource>(exp);
        }
    }
}