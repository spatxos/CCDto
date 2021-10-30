using System;
using System.Collections.Generic;
using System.Text;

namespace CCDto.application.Service.Crud.Dto
{
    /// <summary>
    /// This interface is defined to standardize to request a limited result.
    /// </summary>
    public interface ILimitedResultRequest
    {
        /// <summary>
        /// Max expected result count.
        /// </summary>
        int PageSize { get; set; }
    }
}
