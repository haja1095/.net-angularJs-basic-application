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
    public class PIREP
    {
        public CompanyService com = new CompanyService();
        public SupplierService sup = new SupplierService();
        public PurchaseInvoiceService pi = new PurchaseInvoiceService();
        public ReturnModel<PurchaseInvoiceViewModel> GetPIPDF(PurchaseInvoiceViewModel model, string url)
        {
            var myCom = com.GetSingle(new CompanyMaster() { Code = model.CompanyCode }).datam;
            if (myCom == null)
            {
                return new ReturnModel<PurchaseInvoiceViewModel>()
                {
                    code = "1",
                    error = "Company Not Found!"
                };
            }
            var mySup = sup.GetSingle(new SupplierMaster() { Code = model.SupplierCode }).datam;
            if (mySup == null)
            {
                return new ReturnModel<PurchaseInvoiceViewModel>()
                {
                    code = "1",
                    error = "Supplier Not Found!"
                };
            }
            var myPI = pi.GetSingle(model).datam;
            if (myPI == null)
            {
                return new ReturnModel<PurchaseInvoiceViewModel>()
                {
                    code = "1",
                    error = "PO Not Found!"
                };
            }
            //try
            //{
            using (System.IO.FileStream fs = new FileStream(url + "Reports/" + model.InvoiceNo + ".pdf", FileMode.Create))
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
                datalist.Add("Invoice No", myPI.InvoiceNo);
                datalist.Add("Invoice Date", myPI.InvoiceDate.Value.ToString("dd/MM/yyyy"));
                datalist.Add("Due Date", myPI.DueDate.Value.ToString("dd/MM/yyyy"));
               // if (myPO.ShippingDate.HasValue)
                   // datalist.Add("Shipping Date", myPO.ShippingDate.Value.ToString("dd/MM/yyyy"));
                var tms = new TermsService().GetSingle(new TermsMaster() { Code = myPI.TermsId }).datam;

                Dictionary<string, string> bdatalist = new Dictionary<string, string>();
                bdatalist.Add("Total Amount", myPI.TotalAmount);
                if (myPI.Discount != null && myPI.Discount.Length > 0)
                    bdatalist.Add("Discount", myPI.Discount);
                if (myPI.Message != null && myPI.Message.Length > 0)
                    bdatalist.Add("Message", myPI.Message);


                Image logo = null;
                if (myCom.Image != null && myCom.Image.Length > 1)
                    logo = Image.GetInstance(Convert.FromBase64String(myCom.Image.Split(',')[1]));
                writer.PageEvent = new HeadeFooter(logo, myCom.CompanyName, myCom.Address + " " + myCom.State + " " + myCom.PostalCode + "\n" + myCom.GSTIN, mySup.CompanyName, mySup.Address + " " + mySup.Place + " " + mySup.State + " " + mySup.PostalCode + "\n" + mySup.GSTIN, "Purchase Invoice", myPI.ShippingAddress, datalist);

                document.Open();


                PdfPTable bill_table = new PdfPTable(7);
                //thlist.ForEach(p =>
                //{
                //    Paragraph p1 = new Paragraph(p, normalboaldFont);
                //    PdfPCell pcl1 = new PdfPCell();
                //    p1.Alignment = Element.ALIGN_LEFT;
                //    pcl1.AddElement(p1);
                //    pcl1.Border = 0;
                //    pcl1.Padding = 5;
                //    pcl1.BackgroundColor = new BaseColor(239, 240, 243);
                //    bill_table.AddCell(pcl1);
                //});
                int ttn = 1;
                myPI.PurchaseInvoiceItems.ForEach(o =>
                {
                    Paragraph p1 = new Paragraph(new Chunk(myPI.PurchaseInvoiceItems.IndexOf(o) + 1 + "", normalFont));
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
                    if (o.Product.Taxable.HasValue == true && o.Product.Taxable.Value == true && myCom.State.ToUpper() == mySup.State.ToUpper())
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
                        if (myPI.PurchaseInvoiceItems.IndexOf(o) + 1 != myPI.PurchaseInvoiceItems.Count)
                        {
                            document.NewPage();
                        }
                    }
                    if (myPI.PurchaseInvoiceItems.IndexOf(o) + 1 == myPI.PurchaseInvoiceItems.Count)
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
                    byte[] bytes = File.ReadAllBytes(url + "Reports/" + model.InvoiceNo + ".pdf");
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
                    File.WriteAllBytes(url + "Reports/" + model.InvoiceNo + ".pdf", bytes);
                }

            }

            return new ReturnModel<PurchaseInvoiceViewModel>()
            {
                datam = new PurchaseInvoiceViewModel()
                {
                    URLData = "Reports/" + model.InvoiceNo + ".pdf"
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
