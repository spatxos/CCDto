using System;
using System.Collections.Generic;
using System.Text;

namespace CCDto.Service.Crud.Dto.Response
{
    /// <summary>
    /// This interface is defined to standardize to set "Total Count of Items" to a DTO.
    /// </summary>
    public interface IHasTotalCount
    {
        /// <summary>
        /// Total count of Items.
        /// </summary>
        long TotalCount { get; set; }
    }
}
