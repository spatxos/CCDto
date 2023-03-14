using System.Threading.Tasks;
using api.dbfield.entity.Dto;
using CCDto.application;
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

        public async Task<JsonResult> EditFormAsync(DBFieldDto dbfieldDto)
        {
            returnMsg.IsSuccess = await _dbfieldService.SaveAsync(dbfieldDto);
            returnMsg.Message = returnMsg.IsSuccess ? "提交成功！" : "提交失败！";
            return Json(returnMsg);
        }

        public async Task<JsonResult> DeleteAsync(int id)
        {
            returnMsg.IsSuccess = await _dbfieldService.DeleteAsync(id) > 0;
            returnMsg.Message = returnMsg.IsSuccess ? "删除成功！" : "删除失败！";
            return Json(returnMsg);
        }
        public JsonResult GetShowHeaders(DtoColumnRequest request)
        {
            return Json(_dbfieldService.GetDtoColumns(request));
        }

        public async Task<JsonResult> GetDatasAsync(DBFieldsPagedResultRequestDto requestDto)
        {
            return Json(await _dbfieldService.GetPagingAsync(null, requestDto));
        }

        public async Task<JsonResult> getDataAsync(int id)
        {
            return Json(await _dbfieldService.GetAsync(id));
        }
    }
}
