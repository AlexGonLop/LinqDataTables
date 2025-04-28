using System.Collections.Generic;

namespace LinqDataTables.Generic
{
    public class DataTableResponse<T> : DataTableResponse
    {
        public new IEnumerable<T> Data { get; set; }
    }
}