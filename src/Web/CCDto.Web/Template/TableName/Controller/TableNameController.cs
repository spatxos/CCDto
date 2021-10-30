using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using com.msung.application.Base;
using com.msung.application.Service.Crud.Dto.Request;
using com.msung.application.Service.TableNames;
using com.msung.application.Service.TableNames.Dto;
using com.msung.common.AutoMapper;
using com.msung.entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ILDataWeb.Areas.TableSpace.Controllers
{
    [Area("TableSpace")]
    [Route("TableSpace/[controller]/[action]/{id?}")]
    //[Authorize]
    public class TableNameController : WebBaseController
    {
        public ITableNameService _tableNameService;
        public TableNameController(ITableNameService tableNameService)
        {
            _tableNameService = tableNameService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Edit(int id)
        {
            return View(id);
        }

        public JsonResult EditForm(TableNameDto tableNameDto)
        {
            returnMsg.IsSuccess = _tableNameService.Save(tableNameDto);
            returnMsg.Message = returnMsg.IsSuccess ? "提交成功！" : "提交失败！";
            return Json(returnMsg);
        }

        public JsonResult Delete(int id)
        {
            returnMsg.IsSuccess = _tableNameService.Delete(id) > 0;
            returnMsg.Message = returnMsg.IsSuccess ? "删除成功！" : "删除失败！";
            return Json(returnMsg);
        }
        public JsonResult GetShowHeaders(DtoColumnRequest request)
        {
            return Json(_tableNameService.GetDtoColumns(request));
        }

        public JsonResult GetDatas(TableNamesPagedResultRequestDto requestDto)
        {
            return Json(_tableNameService.GetPaging(null, requestDto));
        }

        public JsonResult getData(int id)
        {
            return Json(_tableNameService.Get(id));
        }
    }
}
