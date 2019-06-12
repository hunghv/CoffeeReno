namespace Services.ViewModels.Request
{
    public class PagingRequest
    {
        public int? Skip { get; set; }
        public int? Take { get; set; }
        public string SortField { get; set; }
        public string SortDir { get; set; }
        public bool IsExport { get; set; }
    }
}
