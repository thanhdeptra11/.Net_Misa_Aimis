namespace Common.Model
{
    public class PagingRequest
    {
        /// <summary>
        /// Số thứ tự trang
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// Số bản ghi trên 1 trang
        /// </summary>
        public int PageSize { get; set; } = 25;

        /// <summary>
        /// Từ khóa tìm kiếm
        /// </summary>
        public string? SearchTerm { get; set; }
        /// <summary>
        /// Filter condition
        /// </summary>
        public List<FilterCondition>? Filters { get; set; }
    }
}
