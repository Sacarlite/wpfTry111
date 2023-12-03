using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfTry.Model.Entities;
using Font = iTextSharp.text.Font;
using Image = iTextSharp.text.Image;
using wpfTry.Viev;

namespace wpfTry.Services.PdfConverter
{
    public class PdfConverter
    {
        public void ExportToPdf(Order order, string strFilePath)
        {
            Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(strFilePath, FileMode.Create));
            document.Open();
            BaseFont customFont = BaseFont.CreateFont("ARIALUNI.TTF", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            
            iTextSharp.text.Font font5 = new Font(customFont, 12, Font.NORMAL);

            PdfPTable table = new PdfPTable(4);
            PdfPRow row = null;
            List<OrderTable> orderTable = DatabaseLocator.Context.OrderTable.Where(u=>u.Order.Id==order.Id).ToList();
            float[] widths = new float[4];
            for (int i = 0; i < 4; i++)
                widths[i] = 4f;
            table.SetWidths(widths);

            table.WidthPercentage = 100;
            int iCol = 0;   
            string colname = "";
            PdfPCell cell = new PdfPCell(new Phrase("Заказ"));
          

            cell.Colspan = 4;
            table.AddCell(new Phrase("Артикул", font5));
            table.AddCell(new Phrase("Изображение", font5));
            table.AddCell(new Phrase("Название", font5));
            table.AddCell(new Phrase("Количество", font5));
            foreach (var elem in orderTable)
            {
                table.AddCell(new Phrase(elem.Product.Id.ToString(), font5));
                table.AddCell(Image.GetInstance(elem.Product.Image));
                table.AddCell(new Phrase(elem.Product.Name, font5));
                table.AddCell(new Phrase(elem.Quantity.ToString(), font5));

            }
            document.Add(table);
            document.Close();
        }
    }
}
