using com.msung.application.Service.Crud.Dto;
using System;
using com.msung.entity.Table;

namespace com.msung.application.Service.MaterialBatchs.Dto
{
    [Serializable]
    public class MaterialBatchsPagedResultRequestDto : PagedResultRequestDto, IPagedAndSortedResultRequest
    {
        public virtual string LoginName { get; set; }

        public virtual string Sorting { get; set; }

    }
}
