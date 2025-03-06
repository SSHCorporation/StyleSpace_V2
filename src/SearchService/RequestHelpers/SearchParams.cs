namespace SearchService
{
    public class SearchParams
    {
        // Add properties and methods as needed
        public string SearchTerm { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string Name { get; set; }
        public string OrderBy { get; set; }
        public string Filter { get; set; }
    }
}