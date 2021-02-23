using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text;
using GSTAPP.BAL;

namespace GSTAPP.REP
{
    public class SOREP
    {
        public CompanyService com = new CompanyService();
        public CustomerService cus = new CustomerService();
        public SalesOrderService so = new SalesOrderService();
        public ReturnModel<SalesOrderViewModel> GetSOPDF(SalesOrderViewModel model, string url)
        {
            var myCom = com.GetSingle(new CompanyMaster() { Code = model.CompanyCode }).datam;
            if (myCom == null)
            {
                return new ReturnModel<SalesOrderViewModel>()
                {
                    code = "1",
                    error = "Company Not Found!"
                };
            }
            var myCus = cus.GetSingle(new CustomerMaster() { Code = model.CustomerCode }).datam;
            if (myCus == null)
            {
                return new ReturnModel<SalesOrderViewModel>()
                {
                    code = "1",
                    error = "Customer Not Found!"
                };
            }
            var mySO = so.GetSingle(model).datam;
            if (mySO == null)
            {
                return new ReturnModel<SalesOrderViewModel>()
                {
                    code = "1",
                    error = "SO Not Found!"
                };
            }
            //try
            //{
            using (System.IO.FileStream fs = new FileStream(url + "Reports/" + model.SO + ".pdf", FileMode.Create))
            {
                var boldFont = FontFactory.GetFont(FontFactory.TIMES_BOLD, 12);
                var normalFont = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10);
                var smallFont = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8);
                var normalboaldFont = FontFactory.GetFont(FontFactory.TIMES_BOLD, 10);
                Document document = new Document(PageSize.A4, 20f, 20f, 20f, 40f);
                PdfWriter writer = PdfWriter.GetInstance(document, fs);
                List<string> thlist = new List<string>();
                thlist.Add("S.No");
                thlist.Add("Description");
                thlist.Add("Qty");
                thlist.Add("Discount");
                thlist.Add("Tax");
                thlist.Add("Rate");
                thlist.Add("Amount");
                float[] wd = new float[] { 6f, 33f, 12f, 10f, 10f, 12f, 17f };
                Dictionary<string, string> datalist = new Dictionary<string, string>();
                datalist.Add("SO", mySO.SO);
                datalist.Add("SO Date", mySO.SODate.Value.ToString("dd/MM/yyyy"));
                datalist.Add("Due Date", mySO.DueDate.Value.ToString("dd/MM/yyyy"));
                if (mySO.ShippingDate.HasValue)
                    datalist.Add("Shipping Date", mySO.ShippingDate.Value.ToString("dd/MM/yyyy"));
                var tms = new TermsService().GetSingle(new TermsMaster() { Code = mySO.TermsId }).datam;
                Dictionary<string, string> bdatalist = new Dictionary<string, string>();
                bdatalist.Add("Total Amount", mySO.TotalAmount);
                if (mySO.Discount != null && mySO.Discount.Length > 0)
                    bdatalist.Add("Discount", mySO.Discount);
                if (mySO.Message != null && mySO.Message.Length > 0)
                    bdatalist.Add("Message", mySO.Message);
                Image logo = null;
                if (myCom.Image != null && myCom.Image.Length > 1)
                    logo = Image.GetInstance(Convert.FromBase64String(myCom.Image.Split(',')[1]));
                writer.PageEvent = new HeadeFooter(logo, myCom.CompanyName, myCom.Address + " " + myCom.State + " " + myCom.PostalCode + "\n" + myCom.GSTIN, myCus.CompanyName, myCus.Address + " " + myCus.Place + " " + myCus.State + " " + myCus.PostalCode + "\n" + myCus.GSTIN, "Sales Order", mySO.ShippingAddress, datalist);
                document.Open();
                PdfPTable bill_table = new PdfPTable(7);
                int ttn = 1;
                mySO.SalesOrderItems.ForEach(o =>
                {
                    Paragraph p1 = new Paragraph(new Chunk(mySO.SalesOrderItems.IndexOf(o) + 1 + "", normalFont));
                    PdfPCell pcl1 = new PdfPCell();
                    p1.Alignment = Element.ALIGN_LEFT;
                    pcl1.AddElement(p1);
                    pcl1.Border = 0;
                    bill_table.AddCell(pcl1);
                    p1 = new Paragraph(new Chunk(o.Product.ProductName, normalboaldFont));
                    if (o.WeightorLength.HasValue)
                        p1.Add(new Chunk(" ( " + o.WeightorLength.Value + " kg/m/l )", smallFont));
                    pcl1 = new PdfPCell();
                    p1.Alignment = Element.ALIGN_LEFT;
                    if (o.Product.Taxable.HasValue == true && o.Product.Taxable.Value == true && myCom.State.ToUpper() == myCus.State.ToUpper())
                    {
                        Paragraph p2 = new Paragraph(new Chunk("SGST : " + o.Product.SGST + "% " + ", CGST : " + o.Product.CGST + "% ", smallFont));
                        p2.IndentationLeft = 10;
                        p1.Add(p2);
                    }
                    else if (o.Product.Taxable.HasValue == true && o.Product.Taxable.Value == true)
                    {
                        Paragraph p2 = new Paragraph(new Chunk("IGST : " + o.Product.IGST + "% ", smallFont));
                        p2.IndentationLeft = 10;
                        p1.Add(p2);
                    }
                    pcl1.AddElement(p1);
                    pcl1.Border = 0;
                    bill_table.AddCell(pcl1);
                    p1 = new Paragraph(new Chunk(o.UnitsOrPieces, normalboaldFont));
                    pcl1 = new PdfPCell();
                    p1.Alignment = Element.ALIGN_LEFT;
                    pcl1.AddElement(p1);
                    pcl1.Border = 0;
                    bill_table.AddCell(pcl1);
                    p1 = new Paragraph(new Chunk(o.Discount + "%", normalboaldFont));
                    pcl1 = new PdfPCell();
                    p1.Alignment = Element.ALIGN_LEFT;
                    pcl1.AddElement(p1);
                    pcl1.Border = 0;
                    bill_table.AddCell(pcl1);
                    var q = o.UnitsOrPieces != null && o.UnitsOrPieces.Length > 0 ? Convert.ToDouble(o.UnitsOrPieces) : 0.0;
                    var dis = o.Discount != null && o.Discount.Length > 0 ? Convert.ToDouble(o.Discount) : 0.0;
                    var rtr = o.Rate != null ? Convert.ToDouble(o.Rate) : 0.0;
                    double ttl = 0d;
                    if (dis > 0)
                    {
                        ttl = ((rtr * q) * dis / 100);
                        var t = o.Tax;
                        if (t.HasValue)
                        {
                            ttl = /*Math.Round(*/((rtr * q) - ttl) * (Convert.ToDouble(t) / 100)/*,2)*/;
                        }
                        else
                        {
                            ttl = 0;
                        }
                    }
                    else
                    {
                        ttl = (rtr * q);
                        var t = o.Tax;
                        if (t.HasValue)
                        {
                            ttl = /*Math.Round(*/(ttl) * (Convert.ToDouble(t) / 100)/*, 2)*/;
                        }
                        else
                        {
                            ttl = 0;
                        }
                    }
                    p1 = new Paragraph(new Chunk((ttl == 0 ? "" : ttl + "") + "", normalboaldFont));
                    pcl1 = new PdfPCell();
                    p1.Alignment = Element.ALIGN_LEFT;
                    pcl1.AddElement(p1);
                    pcl1.Border = 0;
                    bill_table.AddCell(pcl1);
                    p1 = new Paragraph(new Chunk((o.Rate).ToString(), normalboaldFont));
                    pcl1 = new PdfPCell();
                    p1.Alignment = Element.ALIGN_RIGHT;
                    pcl1.AddElement(p1);
                    pcl1.Border = 0;
                    bill_table.AddCell(pcl1);
                    p1 = new Paragraph(new Chunk(o.TaxAmount != null ? (o.TaxAmount).ToString() : "", normalboaldFont));
                    p1.Alignment = Element.ALIGN_RIGHT;
                    pcl1 = new PdfPCell();
                    pcl1.AddElement(p1);
                    pcl1.Border = 0;
                    bill_table.AddCell(pcl1);
                    ttn += 1;
                    if (ttn == 16)
                    {
                        ttn = 1;
                        bill_table.HorizontalAlignment = 0;
                        bill_table.WidthPercentage = 100;
                        bill_table.SetWidths(wd);
                        document.Add(bill_table);

                        bill_table = new PdfPTable(7);
                        if (mySO.SalesOrderItems.IndexOf(o) + 1 != mySO.SalesOrderItems.Count)
                        {
                            document.NewPage();
                        }
                    }
                    if (mySO.SalesOrderItems.IndexOf(o) + 1 == mySO.SalesOrderItems.Count)
                    {

                        bill_table.HorizontalAlignment = 0;
                        bill_table.WidthPercentage = 100;
                        bill_table.SetWidths(wd);
                        document.Add(bill_table);
                    }

                });
                document.Close();
                writer.Close();
                fs.Close();

                using (System.IO.MemoryStream ms = new MemoryStream())
                {
                    byte[] bytes = File.ReadAllBytes(url + "Reports/" + model.SO + ".pdf");
                    using (MemoryStream stream = new MemoryStream())
                    {
                        PdfReader reader = new PdfReader(bytes);
                        using (PdfStamper stamper = new PdfStamper(reader, stream))
                        {
                            int pages = reader.NumberOfPages;
                            for (int i = 1; i <= pages; i++)
                            {
                                ColumnText.ShowTextAligned(stamper.GetUnderContent(i), Element.ALIGN_RIGHT, new Phrase(i.ToString() + " of " + reader.NumberOfPages, normalFont), 568f, 15f, 0);
                            }
                        }
                        bytes = stream.ToArray();
                    }
                    File.WriteAllBytes(url + "Reports/" + model.SO + ".pdf", bytes);
                }

            }

            return new ReturnModel<SalesOrderViewModel>()
            {
                datam = new SalesOrderViewModel()
                {
                    URLData = "Reports/" + model.SO + ".pdf"
                }
            };
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }
    }
}
