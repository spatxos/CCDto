using CCDto.entity.Dto.Request;
using System;

namespace api.dbconnecion.entity.Dto
{
    [Serializable]
    public class DBConnectionsPagedResultRequestDto : PagedResultRequestDto, IPagedAndSortedResultRequest
    {
        public virtual string LoginName { get; set; }

        public virtual string Sorting { get; set; }

    }
}
