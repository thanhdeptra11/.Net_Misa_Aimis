using System.Collections.Generic;

namespace Common.Model
{
    public class PagingResponse<T>
    {
        /// <summary>
        /// Tổng số bản ghi thỏa mãn điều kiện
        /// </summary>
        public long TotalRecords { get; set; }

        /// <summary>
        /// Tổng số trang
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Dữ liệu của trang hiện tại
        /// </summary>
        public IEnumerable<T> Data { get; set; }

        public PagingResponse(long totalRecords, int totalPages, IEnumerable<T> data)
        {
            TotalRecords = totalRecords;
            TotalPages = totalPages;
            Data = data;
        }
    }
}
