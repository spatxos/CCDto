using CCDto.application.Service.Crud.Dto.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace CCDto.application.Service.Crud.Dto
{
    /// <summary>
    /// This interface is defined to standardize to request a paged result.
    /// </summary>
    public interface IPagedResultRequest : ILimitedResultRequest
    {
        /// <summary>
        /// Skip count (beginning of the page).
        /// </summary>
        int PageIndex { get; set; }

        PageTableFoot PageTableFoot { get; set; }
    }
}
