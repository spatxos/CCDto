using System;
using CCDto.Entity.Dto.Request;

namespace api.dbtable.entity.Dto
{
    [Serializable]
    public class DBTablesPagedResultRequestDto : PagedResultRequestDto, IPagedAndSortedResultRequest
    {
        public virtual string LoginName { get; set; }

        public virtual string Sorting { get; set; }

    }
}
