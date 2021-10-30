using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CCDto.application.Base;
using CCDto.application.Service.Crud.Dto.Request;
using CCDto.application.Service.DBTables;
using CCDto.application.Service.DBTables.Dto;
using CCDto.common.AutoMapper;
using CCDto.entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CCDto.Web.Areas.DB.Controllers
{
    [Area("DB")]
    [Route("DB/[controller]/[action]/{id?}")]
    //[Authorize]
    public class DBTableController : WebBaseController
    {
        public IDBTableService _dbtableService;
        public DBTableController(IDBTableService dbtableService)
        {
            _dbtableService = dbtableService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Edit(int id)
        {
            return View(id);
        }

        public JsonResult EditForm(DBTableDto dbtableDto)
        {
            returnMsg.IsSuccess = _dbtableService.Save(dbtableDto);
            returnMsg.Message = returnMsg.IsSuccess ? "提交成功！" : "提交失败！";
            return Json(returnMsg);
        }

        public JsonResult Delete(int id)
        {
            returnMsg.IsSuccess = _dbtableService.Delete(id) > 0;
            returnMsg.Message = returnMsg.IsSuccess ? "删除成功！" : "删除失败！";
            return Json(returnMsg);
        }
        public JsonResult GetShowHeaders(DtoColumnRequest request)
        {
            return Json(_dbtableService.GetDtoColumns(request));
        }

        public JsonResult GetDatas(DBTablesPagedResultRequestDto requestDto)
        {
            return Json(_dbtableService.GetPaging(null, requestDto));
        }

        public JsonResult getData(int id)
        {
            return Json(_dbtableService.Get(id));
        }
    }
}
