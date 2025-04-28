using System;
using System.Collections;

namespace LinqDataTables
{
    public class DataTableResponse
    {
        public int Draw { get; set; }
        public int RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }
        public IEnumerable Data { get; set; } = Array.Empty<string>();
        public string Error { get; set; }
    }
}