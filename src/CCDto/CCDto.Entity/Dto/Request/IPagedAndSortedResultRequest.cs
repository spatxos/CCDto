using System;
using System.Collections.Generic;
using System.Text;

namespace CCDto.Entity.Dto.Request
{
    /// <summary>
    /// This interface is defined to standardize to request a paged and sorted result.
    /// </summary>
    public interface IPagedAndSortedResultRequest : IPagedResultRequest, ISortedResultRequest
    {

    }
}
