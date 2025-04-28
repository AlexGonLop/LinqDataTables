using System;
using System.Linq;
using LinqDataTables.Generic;
using LinqDataTables.Models;

namespace LinqDataTables.Linq
{
    public static class LinqDataTable
    {
        public static DataTableResponse<TSource> ToDataTable<TSource>
        (
            this IQueryable<TSource> source,
            DataTableRequest request
        )
        {
            var total = source.Count();
            var mainSource = source;

            if (request.Order.Length > 0)
                mainSource = ApplyOrder(mainSource, request.Order);

            var totalFiltered = total;//mainSource.Count();
            if (request.Start > 0)
                mainSource = mainSource.Skip(request.Start);
            if (request.Length > 0)
                mainSource = mainSource.Take(request.Length);

            var result = mainSource.ToList();

            return new DataTableResponse<TSource>
            {
                Draw = request.Draw,
                RecordsTotal = total,
                RecordsFiltered = totalFiltered,
                Data = result,
            };
        }

        private static IQueryable<TSource> ApplyOrder<TSource>
        (
            IQueryable<TSource> source,
            DataTableOrder[] order
        )
        {
            IOrderedQueryable<TSource> ordered = null;
            for (var i = 0; i < order.Length; i++)
            {
                var item = order[i];
                var isAscending = item.Dir.Equals("ASC", StringComparison.InvariantCultureIgnoreCase);

                ordered = i == 0 ? source.OrderBy(item.Name, isAscending) : ordered.ThenBy(item.Name, isAscending);
            }

            return ordered;
        }
    }
}