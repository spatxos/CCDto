using CCDto.application.Service.Crud.Dto;
using System;
using CCDto.entity.Table;

namespace CCDto.application.Service.DBTables.Dto
{
    [Serializable]
    public class DBTablesPagedResultRequestDto : PagedResultRequestDto, IPagedAndSortedResultRequest
    {
        public virtual string LoginName { get; set; }

        public virtual string Sorting { get; set; }

    }
}
