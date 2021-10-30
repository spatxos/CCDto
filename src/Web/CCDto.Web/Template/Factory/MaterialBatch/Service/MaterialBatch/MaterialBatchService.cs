using com.msung.application.Service.Crud;
using com.msung.application.Service.MaterialBatchs.Dto;
using com.msung.common.Encrypt;
using com.msung.entity;
using com.msung.entity.Base;
using com.msung.entity.Request;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using com.msung.entity.Table;
using com.msung.entity.Table.Factory;

namespace com.msung.application.Service.MaterialBatchs
{
    public class MaterialBatchService : AsyncCrudAppService<MaterialBatch, MaterialBatchDto, int,MaterialBatchsPagedResultRequestDto,MaterialBatchDto>, IMaterialBatchService
    {
        public ReturnMsg returnMsg = new ReturnMsg();
        public MaterialBatchService(IFreeSql fsql) : base(fsql)
        {

        }
    }
}
