using System;
using CCDto.entity.Dto.Request;

namespace api.dbtable.application.Dto
{
    [Serializable]
    public class DBTablesPagedResultRequestDto : PagedResultRequestDto, IPagedAndSortedResultRequest
    {
        public virtual string LoginName { get; set; }

        public virtual string Sorting { get; set; }

    }
}
