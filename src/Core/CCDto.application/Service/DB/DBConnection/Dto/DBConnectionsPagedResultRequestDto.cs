using CCDto.application.Service.Crud.Dto;
using System;
using CCDto.entity.Table;

namespace CCDto.application.Service.DBConnections.Dto
{
    [Serializable]
    public class DBConnectionsPagedResultRequestDto : PagedResultRequestDto, IPagedAndSortedResultRequest
    {
        public virtual string LoginName { get; set; }

        public virtual string Sorting { get; set; }

    }
}
