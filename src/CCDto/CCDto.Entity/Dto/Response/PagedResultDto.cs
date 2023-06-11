using CCDto.Entity.Base;
using CCDto.Entity.Dto.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace CCDto.Entity.Dto.Response
{
    /// <summary>
    /// Implements <see cref="IPagedResult{T}"/>.
    /// </summary>
    /// <typeparam name="T">Type of the items in the <see cref="ListResultDto{T}.Items"/> list</typeparam>
    [Serializable]
    public class PagedResultDto<T> : ListResultDto<T>, IPagedResult<T>
    {
        /// <summary>
        /// Total count of Items.
        /// </summary>
        public long TotalCount { get; set; }

        public IPagedResultRequest PagedInput { get; set; }

        public long CurrentPageNo { get; set; }

        public long TotalPageNo { get; set; }

        public int PageTableFoot;

        /// <summary>
        /// Creates a new <see cref="PagedResultDto{T}"/> object.
        /// </summary>
        public PagedResultDto()
        {

        }

        /// <summary>
        /// Creates a new <see cref="PagedResultDto{T}"/> object.
        /// </summary>
        /// <param name="totalCount">Total count of Items</param>
        /// <param name="items">List of items in current page</param>
        public PagedResultDto(long totalCount, PagedResultRequestDto pagedInput, IReadOnlyList<T> items)
            : base(items)
        {
            TotalCount = totalCount;
            PagedInput = pagedInput;

            if (PagedInput.PageSize > 0 && totalCount > 0)
            {
                CurrentPageNo = (PagedInput.PageIndex * +PagedInput.PageSize) / PagedInput.PageSize;
                TotalPageNo = (long)Math.Ceiling((decimal)TotalCount / PagedInput.PageSize);

                //PageTableFoot = _pageTableFoot.ToString();
            }
        }
    }
}
