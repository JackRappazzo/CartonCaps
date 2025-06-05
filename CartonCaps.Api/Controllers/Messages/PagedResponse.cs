namespace CartonCaps.Api.Controllers.Messages
{

    /// <summary>
    /// Generic response model for returning paged data
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedResponse<T>
    {
        /// <summary>
        /// Total number of records available
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// The index the page starts on
        /// </summary>
        public int PageStart { get; set; }

        /// <summary>
        /// The number of items per page
        /// </summary>
        public int NumberPerPage { get; set; }


        /// <summary>
        /// The collection of items
        /// </summary>
        public virtual required IEnumerable<T>  Items { get; set; }
    }
}
