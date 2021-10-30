using CCDto.application.Service.Crud.Dto;
using System;
using CCDto.entity.Table;

namespace CCDto.application.Service.DBFields.Dto
{
    [Serializable]
    public class DBFieldsPagedResultRequestDto : PagedResultRequestDto, IPagedAndSortedResultRequest
    {
        public virtual string LoginName { get; set; }

        public virtual string Sorting { get; set; }

    }
}
