using api.dbfield.application;
using api.dbfield.application.Dto;
using CCDto.application.Base;
using CCDto.entity.Dto.Request;
using Microsoft.AspNetCore.Mvc;

namespace CCDto.Web.Areas.DB.Controllers
{
    [Area("DB")]
    [Route("DB/[controller]/[action]/{id?}")]
    //[Authorize]
    public class DBFieldController : WebBaseController
    {
        public IDBFieldService _dbfieldService;
        public DBFieldController(IDBFieldService dbfieldService)
        {
            _dbfieldService = dbfieldService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Edit(int id)
        {
            return View(id);
        }

        public JsonResult EditForm(DBFieldDto dbfieldDto)
        {
            returnMsg.IsSuccess = _dbfieldService.Save(dbfieldDto);
            returnMsg.Message = returnMsg.IsSuccess ? "提交成功！" : "提交失败！";
            return Json(returnMsg);
        }

        public JsonResult Delete(int id)
        {
            returnMsg.IsSuccess = _dbfieldService.Delete(id) > 0;
            returnMsg.Message = returnMsg.IsSuccess ? "删除成功！" : "删除失败！";
            return Json(returnMsg);
        }
        public JsonResult GetShowHeaders(DtoColumnRequest request)
        {
            return Json(_dbfieldService.GetDtoColumns(request));
        }

        public JsonResult GetDatas(DBFieldsPagedResultRequestDto requestDto)
        {
            return Json(_dbfieldService.GetPaging(null, requestDto));
        }

        public JsonResult getData(int id)
        {
            return Json(_dbfieldService.Get(id));
        }
    }
}
