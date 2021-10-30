using System;
using System.Collections.Generic;
using System.Text;

namespace CCDto.application.Service.Crud.Dto.Response
{
    /// <summary>
    /// This interface is defined to standardize to return a page of items to clients.
    /// </summary>
    /// <typeparam name="T">Type of the items in the <see cref="IListResult{T}.Items"/> list</typeparam>
    public interface IPagedResult<T> : IListResult<T>, IHasTotalCount
    {

    }
}
