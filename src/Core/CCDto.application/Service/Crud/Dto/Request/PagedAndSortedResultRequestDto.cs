using System;
using System.Collections.Generic;
using System.Text;

namespace CCDto.application.Service.Crud.Dto
{
    /// <summary>
    /// Simply implements <see cref="IPagedAndSortedResultRequest"/>.
    /// </summary>
    [Serializable]
    public class PagedAndSortedResultRequestDto : PagedResultRequestDto, IPagedAndSortedResultRequest
    {
        public virtual string Sorting { get; set; }
    }
}
