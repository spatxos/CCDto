using CCDto.entity.Base;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace CCDto.application.Service.Crud.Dto.Response
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

        public PageTableFoot _pageTableFoot = new PageTableFoot();

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

            pagedInput.PageTableFoot = _pageTableFoot;

            if (PagedInput.PageSize > 0 && totalCount > 0)
            {
                CurrentPageNo = (PagedInput.PageIndex * +PagedInput.PageSize) / PagedInput.PageSize;
                TotalPageNo = (long)Math.Ceiling((decimal)TotalCount / PagedInput.PageSize);

                _pageTableFoot.Info = GetInfo().ToString();
                _pageTableFoot.Paginate = GetPagePaginate().ToString();
                _pageTableFoot.Foot = string.Format(GetFoot().ToString(), _pageTableFoot.Info, _pageTableFoot.Paginate);
                pagedInput.PageTableFoot = _pageTableFoot;
                //PageTableFoot = _pageTableFoot.ToString();
            }
        }

        internal StringBuilder GetFoot()
        {
            var sb = new StringBuilder();
            sb.Append(@"<div class='row'>
                           <div class='col-md-5 col-sm-12'>
                             {0}
                           </div>
                           <div class='col-md-7 col-sm-12'>
                               {1}
                           </div>
                       </div>");
            return sb;
        }
        internal StringBuilder GetInfo()
        {
            var sb = new StringBuilder();
            sb.Append(@$"<div class='dataTables_info' id='sample_1_info' role='status' aria-live='polite' style='margin-left: 3%;'>Showing {CurrentPageNo} to {PagedInput.PageSize} of {TotalCount} entries,total {TotalPageNo} pages.</div>");
            return sb;
        }
        internal StringBuilder GetPagePaginate()
        {
            var sb = new StringBuilder();

            sb.Append(@"<div class='dataTables_paginate paging_bootstrap_number' id='sample_1_paginate'>
                                <ul class='pagination' style='visibility: visible;'>");

            if (TotalPageNo > 10)
            {
                sb.Append(@$"<li class='prev {(CurrentPageNo == 1 ? "disabled" : "")}'>
                        <a href='javascript:;' title='Prev'  pageindex='{1}'>
                            首页
                        </a>
                    </li>");
            }

            sb.Append(@$"<li class='prev {(CurrentPageNo == 1? "disabled" : "")}'>
                        <a href='javascript:;' title='Prev' {(CurrentPageNo == 1 ? "": $"pageindex = '{(CurrentPageNo -1)}'")}>
                            <i class='fa fa-angle-left'></i>
                        </a>
                    </li>");

            if (TotalPageNo <= 10)
            {
                for (var i = 1; i <= TotalPageNo; i++)
                {
                    sb.Append(@$"<li class='{(CurrentPageNo == i ? "active" : "")}'><a href='javascript:;'  pageindex='{i}'>{i}</a></li>");
                }
            }
            else
            {
                var startIndex = 1;
                if (CurrentPageNo + 10 > TotalPageNo)
                {
                    startIndex = (int)(TotalPageNo - 10);
                }
                else
                {
                    startIndex = (int)CurrentPageNo;
                }
                for (var i = startIndex; i <= (startIndex + 10); i++)
                {
                    sb.Append(@$"<li class='{(CurrentPageNo == i ? "active" : "")}'><a href='javascript:;'  pageindex='{i}'>{i}</a></li>");
                }
            }

            sb.Append(@$"<li class='next {(CurrentPageNo == TotalPageNo ? "disabled" : "")}'>
                            <a href='javascript:;' {(CurrentPageNo == TotalPageNo ? "" : $"pageindex ='{(CurrentPageNo + 1)}'")} title='Next' >
                                <i class='fa fa-angle-right'></i>
                            </a>
                        </li>");

            if (TotalPageNo > 10)
            {
                sb.Append(@$"<li class='next {(CurrentPageNo == TotalPageNo ? "disabled" : "")}'>
                            <a href='javascript:;' pageindex='{TotalPageNo}' title='Next' >
                                末页
                            </a>
                        </li>");
            }

            sb.Append(@$"</ul></div>");

            return sb;
        }
    }
    public class PageTableFoot:BaseEntity
    {
        public string Foot { get; set; }
        public string Info { get; set; }
        public string Paginate { get; set; }
    }
}
