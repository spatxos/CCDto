using CCDto.application.Base;
using CCDto.entity.Dto.Request;
using Microsoft.AspNetCore.Mvc;
using CCDto.application;
using api.dbconnecion.entity.Dto;
using System.Threading.Tasks;

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

        public async Task<JsonResult> EditFormAsync(DBConnectionDto dbconnectionDto)
        {
            returnMsg.IsSuccess = await _dbconnectionService.SaveAsync(dbconnectionDto);
            returnMsg.Message = returnMsg.IsSuccess ? "提交成功！" : "提交失败！";
            return Json(returnMsg);
        }

        public async Task<JsonResult>  DeleteAsync(int id)
        {
            returnMsg.IsSuccess = await _dbconnectionService.DeleteAsync(id) > 0;
            returnMsg.Message = returnMsg.IsSuccess ? "删除成功！" : "删除失败！";
            return Json(returnMsg);
        }
        public JsonResult GetShowHeaders(DtoColumnRequest request)
        {
            return Json(_dbconnectionService.GetDtoColumns(request));
        }

        public async Task<JsonResult>  GetDatasAsync(DBConnectionsPagedResultRequestDto requestDto)
        {
            return Json(await _dbconnectionService.GetPagingAsync(null, requestDto));
        }

        public async Task<JsonResult> getDataAsync(int id)
        {
            return Json(await _dbconnectionService.GetAsync(id));
        }
    }
}
