using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSTAPP.REP
{

    public class HeadeFooter : PdfPageEventHelper
    {
        public HeadeFooter(Image img, string cName, string cAddr, string tName, string tAddr, string ttl, string sAddr, Dictionary<string, string> dlist)
        {
            image = img;
            comName = cName;
            comAddr = cAddr;
            toName = tName;
            toAddr = tAddr;
            title = ttl;
            shipAddr = sAddr;
            datalist = dlist;
            //thlist = thl;
        }
        //float[] wd = new float[] { 6f, 33f, 12f, 10f, 10f, 12f, 17f };
        //List<string> thlist = new List<string>();
        //PdfPTable bill_table = new PdfPTable(7);
        Dictionary<string, string> datalist;
        string comName, comAddr, toName, toAddr, title, shipAddr;
        Image image;
        Paragraph para, para1, para2, para3, para4, para5;
        PdfPTable headerTable = new PdfPTable(6);

        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            //image.SetAbsolutePosition(0, 0);
            var boldFont = FontFactory.GetFont(FontFactory.TIMES_BOLD, 12);
            var normalFont = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10);
            var normalboaldFont = FontFactory.GetFont(FontFactory.TIMES_BOLD, 10);

            para = new Paragraph();
            para1 = new Paragraph();
            para2 = new Paragraph();
            para3 = new Paragraph();
            para3.Add(" ");
            para4 = new Paragraph();

            para2.Add(new Chunk(title, boldFont));

            para.Add(new Chunk(comName, boldFont));
            para1.Add(new Chunk(comAddr, normalFont));
            para1.Alignment = Element.ALIGN_RIGHT;
            para.Alignment = Element.ALIGN_RIGHT;
            //image.ScaleAbsolute(125f, 100f);
            if (image != null)
            {
                image.WidthPercentage = 55;
            }
            headerTable.WidthPercentage = 100;

            PdfPCell ct = new PdfPCell();
            ct.Colspan = 6;
            ct.AddElement(para2);
            ct.Border = Rectangle.NO_BORDER;
            ct.Padding = 5;
            ct.BackgroundColor = new BaseColor(239, 240, 241);
            ct.BorderWidthBottom = 1;
            ct.BorderColorBottom = new BaseColor(0, 0, 0);

            PdfPCell c1 = new PdfPCell();
            c1.Colspan = 2;
            c1.AddElement(image);
            c1.Padding = 10;
            //c1. = 1;
            c1.Border = Rectangle.NO_BORDER;
            c1.BorderWidthBottom = 1;
            c1.BorderColorBottom = new BaseColor(0, 0, 0);

            PdfPCell c2 = new PdfPCell();
            c2.Colspan = 4;
            c2.AddElement(para);
            c2.AddElement(para1);
            c2.Padding = 20;
            c2.Border = Rectangle.NO_BORDER;
            c2.HorizontalAlignment = 990;
            c2.BorderWidthBottom = 1;
            c2.BorderColorBottom = new BaseColor(0, 0, 0);

            PdfPCell c3 = new PdfPCell();
            c3.Border = Rectangle.NO_BORDER;
            c3.Colspan = 2;
            para4.Add(new Chunk("To", normalboaldFont));
            c3.AddElement(para4);
            para4 = new Paragraph();
            para4.Add(new Chunk(toName, boldFont));
            para4.Alignment = Element.ALIGN_LEFT;
            para4.IndentationLeft = 10;
            c3.AddElement(para4);
            para4 = new Paragraph();
            para4.Add(new Chunk(toAddr, normalFont));
            para4.Alignment = Element.ALIGN_LEFT;
            para4.IndentationLeft = 10;
            c3.AddElement(para4);
            c3.AddElement(para3);
            c3.BorderWidthBottom = 1;
            c3.BorderColorBottom = new BaseColor(0, 0, 0);

            PdfPCell c4 = new PdfPCell();
            c4.Border = Rectangle.NO_BORDER;
            c4.Colspan = 2;
            para4 = new Paragraph();
            if (shipAddr != null && shipAddr.Length > 0)
                para4.Add(new Chunk("Shipping Address", normalboaldFont));
            else
                para4.Add(new Chunk("", normalFont));
            c4.AddElement(para4);
            para4 = new Paragraph();
            if (shipAddr != null && shipAddr.Length > 0)
                para4.Add(new Chunk(shipAddr, normalFont));
            else
                para4.Add(new Chunk("", normalFont));
            para4.IndentationLeft = 10;
            para4.Alignment = Element.ALIGN_LEFT;
            c4.AddElement(para4);
            c4.AddElement(para3);
            c4.BorderWidthBottom = 1;
            c4.BorderColorBottom = new BaseColor(0, 0, 0);

            PdfPCell c5 = new PdfPCell();
            c5.Border = Rectangle.NO_BORDER;
            c5.Colspan = 2;
            PdfPTable rtbl = new PdfPTable(2);
            foreach (var item in datalist)
            {
                para5 = new Paragraph(new Chunk(item.Key, normalboaldFont));
                PdfPCell dlc = new PdfPCell(para5);
                dlc.Border = Rectangle.NO_BORDER;
                rtbl.AddCell(dlc);
                para5.Alignment = Element.ALIGN_RIGHT;

                para5 = new Paragraph(new Chunk(item.Value, normalFont));
                para5.Alignment = Element.ALIGN_RIGHT;
                dlc = new PdfPCell(para5);
                dlc.Border = Rectangle.NO_BORDER;
                rtbl.AddCell(dlc);
            }
            rtbl.WidthPercentage = 100;
            c5.AddElement(rtbl);
            c5.AddElement(para3);
            c5.BorderWidthBottom = 1;
            c5.BorderColorBottom = new BaseColor(0, 0, 0);


            ct.FixedHeight = 30;
            c2.FixedHeight = 110;
            c1.FixedHeight = 110;
            c3.FixedHeight = 110;
            c4.FixedHeight = 110;
            c5.FixedHeight = 110;

            headerTable.AddCell(ct);
            headerTable.AddCell(c1);
            headerTable.AddCell(c2);
            headerTable.AddCell(c3);
            headerTable.AddCell(c4);
            headerTable.AddCell(c5);


            //thlist.ForEach(p =>
            //{
            //    Paragraph p1 = new Paragraph(p, normalboaldFont);
            //    PdfPCell pcl1 = new PdfPCell();
            //    p1.Alignment = Element.ALIGN_CENTER;
            //    pcl1.AddElement(p1);
            //    pcl1.Border = 0;
            //    pcl1.PaddingTop = 5;
            //    pcl1.BackgroundColor = new BaseColor(239, 240, 243);
            //    bill_table.AddCell(pcl1);
            //});

            //bill_table.HorizontalAlignment = 0;
            //bill_table.WidthPercentage = 100;
            //bill_table.SetWidths(wd);
        }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            Paragraph footer = new Paragraph(new Chunk(" ", FontFactory.GetFont(FontFactory.TIMES, 10, Font.BOLD)));
            footer.Add(new Chunk(" ", FontFactory.GetFont(FontFactory.TIMES, 10, Font.NORMAL)));
            footer.Alignment = Element.ALIGN_RIGHT;
            PdfPTable footerTbl = new PdfPTable(1);

            footerTbl.TotalWidth = 550;
            footerTbl.HorizontalAlignment = Element.ALIGN_RIGHT;
            PdfPCell cell = new PdfPCell(footer);
            cell.Border = 1;
            cell.PaddingLeft = 20;
            cell.PaddingTop = 5;
            footerTbl.AddCell(cell);
            footerTbl.WriteSelectedRows(0, -1, 20, 30, writer.DirectContent);
        }
        public override void OnStartPage(PdfWriter writer, Document document)
        {
            //document.Add(image);
            document.Add(headerTable);
            document.Add(para3);
            //document.Add(bill_table);
        }
    }

    public class RoundedBorder : IPdfPCellEvent
    {
        public void CellLayout(PdfPCell cell, Rectangle rect, PdfContentByte[] canvas)
        {
            PdfContentByte cb = canvas[PdfPTable.BACKGROUNDCANVAS];
            cb.RoundRectangle(
              rect.Left + 1.5f,
              rect.Bottom + 1.5f,
              rect.Width - 3,
              rect.Height - 3, 4
            );
            cb.Stroke();
        }
    }
}
