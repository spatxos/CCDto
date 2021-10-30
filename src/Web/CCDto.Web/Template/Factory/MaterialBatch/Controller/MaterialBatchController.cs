using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using com.msung.application.Base;
using com.msung.application.Service.Crud.Dto.Request;
using com.msung.application.Service.MaterialBatchs;
using com.msung.application.Service.MaterialBatchs.Dto;
using com.msung.common.AutoMapper;
using com.msung.entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ILDataWeb.Areas.Factory.Controllers
{
    [Area("Factory")]
    [Route("Factory/[controller]/[action]/{id?}")]
    //[Authorize]
    public class MaterialBatchController : WebBaseController
    {
        public IMaterialBatchService _materialbatchService;
        public MaterialBatchController(IMaterialBatchService materialbatchService)
        {
            _materialbatchService = materialbatchService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Edit(int id)
        {
            return View(id);
        }

        public JsonResult EditForm(MaterialBatchDto materialbatchDto)
        {
            returnMsg.IsSuccess = _materialbatchService.Save(materialbatchDto);
            returnMsg.Message = returnMsg.IsSuccess ? "提交成功！" : "提交失败！";
            return Json(returnMsg);
        }

        public JsonResult Delete(int id)
        {
            returnMsg.IsSuccess = _materialbatchService.Delete(id) > 0;
            returnMsg.Message = returnMsg.IsSuccess ? "删除成功！" : "删除失败！";
            return Json(returnMsg);
        }
        public JsonResult GetShowHeaders(DtoColumnRequest request)
        {
            return Json(_materialbatchService.GetDtoColumns(request));
        }

        public JsonResult GetDatas(MaterialBatchsPagedResultRequestDto requestDto)
        {
            return Json(_materialbatchService.GetPaging(null, requestDto));
        }

        public JsonResult getData(int id)
        {
            return Json(_materialbatchService.Get(id));
        }
    }
}
