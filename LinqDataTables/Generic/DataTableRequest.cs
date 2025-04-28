namespace LinqDataTables.Generic
{
    public class DataTableRequest<T> : DataTableRequest
    {
        public T Filters { get; set; }
    }
}