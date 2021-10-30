using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCDto.application.Service.Tool.NPOI.Dto
{
    public class ExportModel
    {
        public string[] DataFields { get; set; }

        public string[] ColumnNames { get; set; }

        public RowModel TitleRow { get; set; } = new RowModel()
        {
            HeightInPoints = 22,
            CellStyle = new CellStyleModel
            {
                FillForegroundColor = new byte[] { 0, 74, 134 },
                Font = new FontModel
                {
                    FontHeightInPoints = 12,
                    Color = new byte[] { 255, 255, 255 }
                }
            }
        };

        public RowModel DataRow { get; set; } = new RowModel()
        {
            HeightInPoints = 22,
            CellStyle = new CellStyleModel
            {
                FillForegroundColor = new byte[] { 255, 255, 255 },
                Font = new FontModel
                {
                    FontHeightInPoints = 10,
                    Color = new byte[] { 0, 0, 0 }
                }
            }
        };
    }

    public class RowModel
    {
        public CellStyleModel CellStyle { get; set; }

        public float HeightInPoints { get; set; }
    }

    public class CellStyleModel
    {
        public byte[] FillForegroundColor { get; set; }

        public FontModel Font { get; set; }
    }

    public class FontModel
    {
        public short FontHeightInPoints { get; set; }

        /// <summary>
        /// R,G,B
        /// </summary>
        public byte[] Color { get; set; }
    }
}
