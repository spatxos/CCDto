using System.Threading.Tasks;
using api.dbtable.entity.Dto;
using CCDto.application;
using CCDto.application.Base;
using CCDto.entity.Dto.Request;
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

        public async Task<JsonResult> EditFormAsync(DBTableDto dbtableDto)
        {
            returnMsg.IsSuccess = await _dbtableService.SaveAsync(dbtableDto);
            returnMsg.Message = returnMsg.IsSuccess ? "提交成功！" : "提交失败！";
            return Json(returnMsg);
        }

        public async Task<JsonResult> DeleteAsync(int id)
        {
            returnMsg.IsSuccess = await  _dbtableService.DeleteAsync(id) > 0;
            returnMsg.Message = returnMsg.IsSuccess ? "删除成功！" : "删除失败！";
            return Json(returnMsg);
        }
        public JsonResult GetShowHeaders(DtoColumnRequest request)
        {
            return Json(_dbtableService.GetDtoColumns(request));
        }

        public async Task<JsonResult> GetDatasAsync(DBTablesPagedResultRequestDto requestDto)
        {
            return Json(await _dbtableService.GetPagingAsync(null, requestDto));
        }

        public async Task<JsonResult> getData(int id)
        {
            return Json(await _dbtableService.GetAsync(id));
        }
    }
}
