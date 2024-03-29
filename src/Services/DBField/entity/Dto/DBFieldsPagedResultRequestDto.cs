using System;
using CCDto.Entity.Dto.Request;

namespace api.dbfield.entity.Dto
{
    [Serializable]
    public class DBFieldsPagedResultRequestDto : PagedResultRequestDto, IPagedAndSortedResultRequest
    {
        public virtual string LoginName { get; set; }

        public virtual string Sorting { get; set; }

    }
}
