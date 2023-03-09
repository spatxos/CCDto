using dbconnecion.application;
using dbconnecion.application.Dto;
using CCDto.application.Base;
using CCDto.entity.Dto.Request;
using Microsoft.AspNetCore.Mvc;

namespace CCDto.Web.Areas.DB.Controllers
{
    [Area("DB")]
    [Route("DB/[controller]/[action]/{id?}")]
    //[Authorize]
    public class DBConnectionController : WebBaseController
    {
        public IDBConnectionService _dbconnectionService;
        public DBConnectionController(IDBConnectionService dbconnectionService)
        {
            _dbconnectionService = dbconnectionService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Edit(int id)
        {
            return View(id);
        }

        public JsonResult EditForm(DBConnectionDto dbconnectionDto)
        {
            returnMsg.IsSuccess = _dbconnectionService.Save(dbconnectionDto);
            returnMsg.Message = returnMsg.IsSuccess ? "提交成功！" : "提交失败！";
            return Json(returnMsg);
        }

        public JsonResult Delete(int id)
        {
            returnMsg.IsSuccess = _dbconnectionService.Delete(id) > 0;
            returnMsg.Message = returnMsg.IsSuccess ? "删除成功！" : "删除失败！";
            return Json(returnMsg);
        }
        public JsonResult GetShowHeaders(DtoColumnRequest request)
        {
            return Json(_dbconnectionService.GetDtoColumns(request));
        }

        public JsonResult GetDatas(DBConnectionsPagedResultRequestDto requestDto)
        {
            return Json(_dbconnectionService.GetPaging(null, requestDto));
        }

        public JsonResult getData(int id)
        {
            return Json(_dbconnectionService.Get(id));
        }
    }
}
