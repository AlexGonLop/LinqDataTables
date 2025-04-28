using System;
using LinqDataTables.Models;

namespace LinqDataTables
{
    public class DataTableRequest
    {
        public int Draw { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public DataTableSearch Search { get; set; }
        public DataTableOrder[] Order { get; set; } = Array.Empty<DataTableOrder>();
        public DataTableColumn[] Columns { get; set; } = Array.Empty<DataTableColumn>();
    }
}