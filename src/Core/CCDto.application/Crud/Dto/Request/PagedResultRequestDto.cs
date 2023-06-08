using CCDto.BaseService.Crud.Dto.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Text;

namespace CCDto.BaseService.Crud.Dto.Request
{
    /// <summary>
    /// Simply implements <see cref="IPagedResultRequest"/>.
    /// </summary>
    [Serializable]
    public class PagedResultRequestDto : LimitedResultRequestDto, IPagedResultRequest
    {
        [Range(0, int.MaxValue)]
        public virtual int PageIndex { get; set; }

    }
}
