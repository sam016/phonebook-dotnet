namespace Sam016.Phonebook.API.Models.Requests
{
    public abstract class PaginationRequest
    {
        /// <summary>
        /// Page number (TODO)
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Count of items per page (TODO)
        /// </summary>
        public int PageSize { get; set; }
    }
}
