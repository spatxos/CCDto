using CCDto.application.Service.Tool.NPOI.Dto;
using System.Collections.Generic;
using System.IO;

namespace CCDto.application.Service.Tool.NPOI
{
    public interface INPOIService 
    {
        byte[] ExportToExcel<T>(List<T> entities, ExportModel model);

        List<T> ExcelToList<T>(Stream stream, string fileName) where T : class, new();
    }
}