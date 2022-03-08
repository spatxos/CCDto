using CCDto.application.Service.Tool.NPOI.Dto;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCDto.application.Service.Tool.NPOI
{
    public class NPOIService : INPOIService
    {
        #region 导入
        /// <summary>
        /// 导入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stream"></param>
        /// <param name="fileName">.xlsx</param>
        /// <returns></returns>
        public List<T> ExcelToList<T>(Stream stream, string fileName) where T : class, new()
        {
            IWorkbook workbook = null;
            string _ext = fileName.Substring(fileName.LastIndexOf("."), fileName.Length - fileName.LastIndexOf("."));
            if (_ext == ".xlsx")
            {
                workbook = new XSSFWorkbook(stream);
            }
            else
            {
                workbook = new HSSFWorkbook(stream);
            }

            ISheet sheet = workbook.GetSheetAt(0);
            IRow ITitleRow = sheet.GetRow(0);
            int totalColumn = ITitleRow.LastCellNum;
            int totalRow = sheet.LastRowNum;

            Dictionary<string, int> dic = new Dictionary<string, int>();
            var properties = typeof(T).GetProperties();
            for (int i = 0, len = properties.Length; i < len; i++)
            {
                object[] _attributes = properties[i].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (_attributes.Length == 0)
                {
                    continue;
                }
                string _description = ((DescriptionAttribute)_attributes[0]).Description;
                if (!string.IsNullOrWhiteSpace(_description))
                {
                    dic.Add(_description, i);
                }
            }

            string _value = string.Empty;
            string _type = string.Empty;
            int index = 0;
            List<T> list = new List<T>();
            for (int i = 1; i <= totalRow; i++)
            {
                IRow row = sheet.GetRow(i);
                if (row == null)
                {
                    continue;
                }
                var obj = new T();
                for (int j = 0; j < totalColumn; j++)
                {
                    if (dic.TryGetValue(ITitleRow.GetCell(j).ToString(), out index) && row.GetCell(j) != null)
                    {
                        _type = (properties[index].PropertyType).FullName;
                        _value = row.GetCell(j).ToString();
                        if (_type == "System.String")
                        {
                            properties[index].SetValue(obj, _value, null);
                        }
                        else if (_type == "System.DateTime")
                        {
                            DateTime pdt = Convert.ToDateTime(_value, CultureInfo.InvariantCulture);
                            properties[index].SetValue(obj, pdt, null);
                        }
                        else if (_type == "System.Boolean")
                        {
                            bool pb = Convert.ToBoolean(_value);
                            properties[index].SetValue(obj, pb, null);
                        }
                        else if (_type == "System.Int16")
                        {
                            short pi16 = Convert.ToInt16(_value);
                            properties[index].SetValue(obj, pi16, null);
                        }
                        else if (_type == "System.Int32")
                        {
                            int pi32 = Convert.ToInt32(_value);
                            properties[index].SetValue(obj, pi32, null);
                        }
                        else if (_type == "System.Int64")
                        {
                            long pi64 = Convert.ToInt64(_value);
                            properties[index].SetValue(obj, pi64, null);
                        }
                        else if (_type == "System.Byte")
                        {
                            byte pb = Convert.ToByte(_value);
                            properties[index].SetValue(obj, pb, null);
                        }
                        else
                        {
                            properties[index].SetValue(obj, null, null);
                        }
                    }
                }
                list.Add(obj);
            }
            return list;
        }

        #endregion

        #region 导出
        /// <summary>
        /// 导出
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public byte[] ExportToExcel<T>(List<T> entities, ExportModel model)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            HSSFPalette palette = workbook.GetCustomPalette();
            HSSFColor hssFColor;
            byte red, green, bule;

            if (model.DataFields == null || model.DataFields.Length == 0)
            {
                model.DataFields = model.ColumnNames;
            }

            #region 标题

            // 标题字体
            IFont titleFont = workbook.CreateFont();
            var titleColor = model.TitleRow.CellStyle.Font.Color;
            red = titleColor[0];
            green = titleColor[1];
            bule = titleColor[2];
            palette.SetColorAtIndex(8, red, green, bule);
            hssFColor = palette.FindColor(red, green, bule);
            titleFont.Color = hssFColor.Indexed;
            titleFont.FontHeightInPoints = model.TitleRow.CellStyle.Font.FontHeightInPoints;

            // 标题前景色
            var titleForegroundColor = model.TitleRow.CellStyle.FillForegroundColor;
            red = titleForegroundColor[0];
            green = titleForegroundColor[1];
            bule = titleForegroundColor[2];
            palette.SetColorAtIndex(9, red, green, bule);
            hssFColor = palette.FindColor(red, green, bule);

            // 标题
            ICellStyle titleStyle = workbook.CreateCellStyle();
            titleStyle.SetFont(titleFont);
            titleStyle.FillPattern = FillPattern.SolidForeground;
            titleStyle.FillForegroundColor = hssFColor.Indexed;
            titleStyle.Alignment = HorizontalAlignment.Center;
            titleStyle.VerticalAlignment = VerticalAlignment.Center;

            ISheet sheet = workbook.CreateSheet("Sheet1");
            IRow row = sheet.CreateRow(0);
            row.HeightInPoints = model.DataRow.HeightInPoints;
            ICell cell = null;
            for (int i = 0; i < model.ColumnNames.Length; i++)
            {
                cell = row.CreateCell(i);
                cell.CellStyle = titleStyle;
                cell.SetCellValue(model.ColumnNames[i]);
            }

            #endregion

            if (entities.Count > 0)
            {
                // 数据行
                object cellValue = string.Empty;
                ICellStyle cellStyle = workbook.CreateCellStyle();
                IFont cellFont = workbook.CreateFont();
                cellFont.FontHeightInPoints = model.DataRow.CellStyle.Font.FontHeightInPoints;
                cellStyle.SetFont(cellFont);
                cellStyle.VerticalAlignment = VerticalAlignment.Center;
                for (int i = 0; i < entities.Count; i++)
                {
                    row = sheet.CreateRow(i + 1);
                    row.HeightInPoints = model.DataRow.HeightInPoints;
                    object entity = entities[i];
                    for (int j = 0; j < model.DataFields.Length; j++)
                    {
                        cellValue = entity.GetType().GetProperty(model.DataFields[j]).GetValue(entity);
                        cell = row.CreateCell(j);
                        cell.CellStyle = cellStyle;
                        cell.SetCellValue(Convert.ToString(cellValue));
                    }
                }

                // 调整列宽
                for (int i = 0; i <= entities.Count; i++)
                {
                    sheet.AutoSizeColumn(i);
                }
                for (int columnNum = 0; columnNum <= model.ColumnNames.Length; columnNum++)
                {
                    int columnWidth = sheet.GetColumnWidth(columnNum) / 256;
                    for (int rowNum = 1; rowNum <= sheet.LastRowNum; rowNum++)
                    {
                        IRow currentRow;
                        if (sheet.GetRow(rowNum) == null)
                        {
                            currentRow = sheet.CreateRow(rowNum);
                        }
                        else
                        {
                            currentRow = sheet.GetRow(rowNum);
                        }

                        if (currentRow.GetCell(columnNum) != null)
                        {
                            ICell currentCell = currentRow.GetCell(columnNum);
                            int length = Encoding.Default.GetBytes(currentCell.ToString()).Length;
                            if (columnWidth < length)
                            {
                                columnWidth = length;
                            }
                        }
                    }
                    columnWidth = Math.Min(columnWidth, 255);
                    sheet.SetColumnWidth(columnNum, columnWidth * 256);
                }
            }

            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                return ms.GetBuffer();
            }
        }

        #endregion
    }
}
