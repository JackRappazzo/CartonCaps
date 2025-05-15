namespace CartonCaps.Api.Controllers.Messages
{
    public class PagedResponse<T>
    {
        public int Total { get; set; }
        public int PageStart { get; set; }
        public int NumberPerPage { get; set; }

        public virtual IEnumerable<T>  Items { get; set; }
    }
}
