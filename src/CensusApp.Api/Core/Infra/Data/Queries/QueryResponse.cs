namespace CensusApp.Api.Core.Infra.Data.Queries
{
    public class QueryResponse<TRows>
    {
        public TRows Rows { get; }
        public int Count { get; }

        public QueryResponse(TRows rows, int count)
        {
            Rows = rows;
            Count = count;
        }
    }
}
