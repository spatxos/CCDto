using com.msung.application.Service.Crud;
using com.msung.application.Service.MaterialBatchs.Dto;
using com.msung.entity;
using com.msung.entity.Base;
using com.msung.entity.Request;
using com.msung.entity.Table;
using com.msung.entity.Table.Factory;

namespace com.msung.application.Service.MaterialBatchs
{
    public interface IMaterialBatchService : IAsyncCrudAppService<MaterialBatch, MaterialBatchDto, int, MaterialBatchsPagedResultRequestDto, MaterialBatchDto>
    {
    }
}
