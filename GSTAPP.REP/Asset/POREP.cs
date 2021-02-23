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
    public class POREP
    {
        public CompanyService com = new CompanyService();
        public SupplierService sup = new SupplierService();
        public PurchaseOrderService po = new PurchaseOrderService();
        public ReturnModel<PurchaseOrderViewModel> GetPOPDF(PurchaseOrderViewModel model, string url)
        {
            var myCom = com.GetSingle(new CompanyMaster() { Code = model.CompanyCode }).datam;
            if (myCom == null)
            {
                return new ReturnModel<PurchaseOrderViewModel>()
                {
                    code = "1",
                    error = "Company Not Found!"
                };
            }
            var mySup = sup.GetSingle(new SupplierMaster() { Code = model.SupplierCode }).datam;
            if (mySup == null)
            {
                return new ReturnModel<PurchaseOrderViewModel>()
                {
                    code = "1",
                    error = "Supplier Not Found!"
                };
            }
            var myPO = po.GetSingle(model).datam;
            if (myPO == null)
            {
                return new ReturnModel<PurchaseOrderViewModel>()
                {
                    code = "1",
                    error = "PO Not Found!"
                };
            }
            //try
            //{
            using (System.IO.FileStream fs = new FileStream(url + "Reports/" + model.PO + ".pdf", FileMode.Create))
            {
                var boldFont = FontFactory.GetFont(FontFactory.TIMES_BOLD, 12);
                var unboldFont = FontFactory.GetFont(FontFactory.TIMES, 12);
                var boldLargFont = FontFactory.GetFont(FontFactory.TIMES_BOLD, 14);
                var largFont = FontFactory.GetFont(FontFactory.TIMES, 14);
                var normalFont = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10);
                var smallFont = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8);
                var normalboaldFont = FontFactory.GetFont(FontFactory.TIMES_BOLD, 10);

                Document document = new Document(PageSize.A4, 20f, 20f, 20f, 40f);
                PdfWriter writer = PdfWriter.GetInstance(document, fs);


                List<string> thlist = new List<string>();
                thlist.Add("S.No");
                thlist.Add("Description");
                thlist.Add("Qty");
                thlist.Add("Rate");
                thlist.Add("Discount");
                thlist.Add("Tax");
                thlist.Add("Amount");

                float[] wd = new float[] { 6f, 33f, 12f, 10f, 10f, 12f, 17f };

                Dictionary<string, string> datalist = new Dictionary<string, string>();
                datalist.Add("PO", myPO.PO);
                datalist.Add("PO Date", myPO.PODate.Value.ToString("dd/MM/yyyy"));
                datalist.Add("Due Date", myPO.DueDate.Value.ToString("dd/MM/yyyy"));
                if (myPO.ShippingDate.HasValue)
                    datalist.Add("Shipping Date", myPO.ShippingDate.Value.ToString("dd/MM/yyyy"));
                var tms = new TermsService().GetSingle(new TermsMaster() { Code = myPO.TermsId }).datam;

                Dictionary<string, string> bdatalist = new Dictionary<string, string>();
                bdatalist.Add("Total Amount", myPO.TotalAmount);
                if (myPO.Discount != null && myPO.Discount.Length > 0)
                    bdatalist.Add("Discount", myPO.Discount);
                if (myPO.Message != null && myPO.Message.Length > 0)
                    bdatalist.Add("Message", myPO.Message);


                Image logo = null;
                if (myCom.Image != null && myCom.Image.Length > 1)
                    logo = Image.GetInstance(Convert.FromBase64String(myCom.Image.Split(',')[1]));
                writer.PageEvent = new HeadeFooter(logo, myCom.CompanyName, myCom.Address + " " + myCom.State + " " + myCom.PostalCode + "\n" + myCom.GSTIN, mySup.CompanyName, mySup.Address + " " + mySup.Place + " " + mySup.State + " " + mySup.PostalCode + "\n" + mySup.GSTIN, "Purchase Order", myPO.ShippingAddress, datalist);

                document.Open();


                PdfPTable bill_table = new PdfPTable(7);
                thlist.ForEach(p =>
                {
                    Paragraph p1 = new Paragraph(p, normalboaldFont);
                    PdfPCell pcl1 = new PdfPCell();
                    p1.Alignment = Element.ALIGN_CENTER;
                    if (thlist.IndexOf(p) > 2)
                        p1.Alignment = Element.ALIGN_RIGHT;
                    pcl1.AddElement(p1);
                    pcl1.Border = 0;
                    pcl1.Padding = 5;
                    pcl1.BackgroundColor = new BaseColor(239, 240, 243);
                    bill_table.AddCell(pcl1);
                });
                int ttn = 1;
                bool fsttm = false;
                decimal bsrate = 0, bstaxamt = 0,bsdisamt = 0;
                myPO.PurchaseOrderItems.ForEach(o =>
                {
                    Paragraph p1 = new Paragraph(new Chunk(myPO.PurchaseOrderItems.IndexOf(o) + 1 + "", normalFont));
                    PdfPCell pcl1 = new PdfPCell();
                    p1.Alignment = Element.ALIGN_CENTER;
                    pcl1.AddElement(p1);
                    pcl1.Border = 0;
                    bill_table.AddCell(pcl1);
                    var dis = o.Discount != null && o.Discount.Length > 0 ? (o.IsPercentage == true ? "Discount : " + o.Discount + "%" : "") : "";
                    p1 = new Paragraph(new Chunk(o.Product.ProductName, normalboaldFont));
                    if (o.WeightorLength.HasValue)
                        p1.Add(new Chunk(" ( " + o.WeightorLength.Value + " kg/m/l )", smallFont));
                    pcl1 = new PdfPCell();
                    p1.Alignment = Element.ALIGN_LEFT;
                    Paragraph p2 = new Paragraph();
                    p2.Add(new Chunk(" "));
                    if (o.Product.Taxable.HasValue == true && o.Product.Taxable.Value == true && myCom.State.ToUpper() == mySup.State.ToUpper())
                    {
                        p2.Add(new Chunk("SGST : " + o.Product.SGST + "% " + ", CGST : " + o.Product.CGST + "% ", smallFont));
                        p2.IndentationLeft = 10;
                    }
                    else if (o.Product.Taxable.HasValue == true && o.Product.Taxable.Value == true)
                    {
                        p2.Add(new Chunk("IGST : " + o.Product.IGST + "% ", smallFont));
                        p2.IndentationLeft = 10;
                    }
                    p2.Add(new Chunk(dis, smallFont));
                    p1.Add(p2);
                    pcl1.AddElement(p1);
                    pcl1.Border = 0;
                    bill_table.AddCell(pcl1);

                    p1 = new Paragraph(new Chunk(o.UnitsOrPieces, normalFont));
                    pcl1 = new PdfPCell();
                    p1.Alignment = Element.ALIGN_CENTER;
                    pcl1.AddElement(p1);
                    pcl1.Border = 0;
                    bill_table.AddCell(pcl1);

                    p1 = new Paragraph(new Chunk((o.Rate).ToString(), normalFont));
                    pcl1 = new PdfPCell();
                    p1.Alignment = Element.ALIGN_RIGHT;
                    pcl1.AddElement(p1);
                    pcl1.Border = 0;
                    bill_table.AddCell(pcl1);

                    if(o.Rate.HasValue && o.UnitsOrPieces != null && o.UnitsOrPieces.Length > 0)
                    {
                        bsrate += Convert.ToDecimal(o.Rate) * Convert.ToDecimal(o.UnitsOrPieces);
                    }

                    decimal disamtfnl = 0;
                    if (o.Discount != null && o.Discount.Length > 0)
                    {
                        decimal amt1 = Convert.ToDecimal(o.Rate.HasValue ? o.Rate : 0) * Convert.ToDecimal(o.UnitsOrPieces.Length > 0 ? o.UnitsOrPieces : "0");
                        decimal disAmt2 = 0;
                        if (o.IsPercentage == true)
                        {
                            disAmt2 = amt1 * Convert.ToDecimal(o.Discount != null && o.Discount.Length > 0 ? o.Discount : "0") / 100;
                        }
                        else
                        {
                            disAmt2 = Convert.ToDecimal(o.Discount != null && o.Discount.Length > 0 ? o.Discount : "0");
                        }
                        disamtfnl = disAmt2;
                    }
                    disamtfnl = decimal.Round(Convert.ToDecimal(disamtfnl.ToString("F")), 2);
                    bsdisamt += disamtfnl;
                    p1 = new Paragraph(new Chunk(disamtfnl > 0 ? disamtfnl.ToString() : "", normalFont));
                    pcl1 = new PdfPCell();
                    p1.Alignment = Element.ALIGN_RIGHT;
                    pcl1.AddElement(p1);
                    pcl1.Border = 0;
                    bill_table.AddCell(pcl1);

                    decimal amt = Convert.ToDecimal(o.Rate.HasValue ? o.Rate : 0) * Convert.ToDecimal(o.UnitsOrPieces.Length > 0 ? o.UnitsOrPieces : "0");
                    decimal disAmt = 0;
                    if (o.IsPercentage == true)
                    {
                        disAmt = amt * Convert.ToDecimal(o.Discount != null && o.Discount.Length > 0 ? o.Discount : "0") / 100;
                    }
                    else
                    {
                        disAmt = Convert.ToDecimal(o.Discount != null && o.Discount.Length > 0 ? o.Discount : "0");
                    }
                    decimal afdis = amt - disAmt;
                    decimal txamt = afdis * Convert.ToDecimal(o.Tax.HasValue ? o.Tax : 0) / 100;
                    txamt = decimal.Round(txamt, 2);
                    p1 = new Paragraph(new Chunk((txamt == 0 ? "" : txamt + "") + "", normalFont));
                    pcl1 = new PdfPCell();
                    p1.Alignment = Element.ALIGN_RIGHT;
                    pcl1.AddElement(p1);
                    pcl1.Border = 0;
                    bill_table.AddCell(pcl1);
                    bstaxamt += txamt;

                    p1 = new Paragraph(new Chunk(o.TaxAmount != null ? (o.TaxAmount).ToString() : "", normalFont));
                    p1.Alignment = Element.ALIGN_RIGHT;
                    pcl1 = new PdfPCell();
                    pcl1.AddElement(p1);
                    pcl1.Border = 0;
                    bill_table.AddCell(pcl1);
                    ttn += 1;

                    bill_table.HorizontalAlignment = 0;
                    bill_table.WidthPercentage = 100;
                    bill_table.SetWidths(wd);
                    var ssdd = 15;
                    if (ttn > 9 && myPO.PurchaseOrderItems.IndexOf(o) + 1 >= myPO.PurchaseOrderItems.Count - 4 && fsttm == false)
                    {
                        ttn = 1;
                        document.Add(bill_table);
                        fsttm = true;
                        bill_table = new PdfPTable(7);
                        if (myPO.PurchaseOrderItems.IndexOf(o) + 1 != myPO.PurchaseOrderItems.Count)
                        {
                            document.NewPage();
                            thlist.ForEach(p =>
                            {
                                Paragraph p12 = new Paragraph(p, normalboaldFont);
                                PdfPCell pcl12 = new PdfPCell();
                                p12.Alignment = Element.ALIGN_CENTER;
                                if (thlist.IndexOf(p) > 2)
                                    p12.Alignment = Element.ALIGN_RIGHT;
                                pcl12.AddElement(p12);
                                pcl12.Border = 0;
                                pcl12.Padding = 5;
                                pcl12.BackgroundColor = new BaseColor(239, 240, 243);
                                bill_table.AddCell(pcl12);
                            });
                        }
                    }
                    else if (ttn == ssdd)
                    {
                        ttn = 1;
                        document.Add(bill_table);

                        bill_table = new PdfPTable(7);
                        if (myPO.PurchaseOrderItems.IndexOf(o) + 1 != myPO.PurchaseOrderItems.Count)
                        {
                            document.NewPage();
                            thlist.ForEach(p =>
                            {
                                Paragraph p12 = new Paragraph(p, normalboaldFont);
                                PdfPCell pcl12 = new PdfPCell();
                                p12.Alignment = Element.ALIGN_CENTER;
                                if (thlist.IndexOf(p) > 2)
                                    p12.Alignment = Element.ALIGN_RIGHT;
                                pcl12.AddElement(p12);
                                pcl12.Border = 0;
                                pcl12.Padding = 5;
                                pcl12.BackgroundColor = new BaseColor(239, 240, 243);
                                bill_table.AddCell(pcl12);
                            });
                        }
                    }
                    if (myPO.PurchaseOrderItems.IndexOf(o) + 1 == myPO.PurchaseOrderItems.Count)
                    {
                        document.Add(bill_table);
                    }

                });

                Paragraph bp1 = new Paragraph(new Chunk(" ", normalFont));
                document.Add(bp1);

                PdfPTable btmTable = new PdfPTable(6);
                btmTable.WidthPercentage = 100;

                bp1 = new Paragraph(new Chunk("Message : ", normalboaldFont));
                Chunk bcc = new Chunk(myPO.Message, normalFont);
                bp1.Add(bcc);
                var dsds = new PdfPCell(bp1);
                dsds.CellEvent = new RoundedBorder();
                dsds.Border = 0;
                dsds.Padding = 10;
                dsds.BackgroundColor = new BaseColor(239, 240, 241);
                PdfPTable bvbv = new PdfPTable(1);
                bvbv.AddCell(dsds);
                bvbv.WidthPercentage = 100;
                bvbv.HorizontalAlignment = Rectangle.ALIGN_LEFT;

                PdfPCell bt_11 = new PdfPCell();
                bt_11.Border = 0;
                bt_11.Colspan = 3;

                //dd1 += bsrate+"-"+bsdisamt+"+"+bstaxamt;
                PdfPCell bt_12 = new PdfPCell();
                bt_12.Border = 0;
                string d12 = "";
                if (bsrate > 0)
                {
                    d12 += "Products amount :\n";
                }
                if(bsdisamt > 0)
                {
                    d12 += "Products discount amount :\n";
                }
                if (bstaxamt > 0)
                {
                    d12 += "Products tax amount :\n";
                }
                Paragraph m12 = new Paragraph(new Chunk(d12, normalFont));
                m12.Alignment = Rectangle.ALIGN_RIGHT;
                bt_12.AddElement(m12);
                bt_12.Colspan = 2;

                PdfPCell bt_13 = new PdfPCell();
                bt_13.Border = 0;
                d12 = "";
                if (bsrate > 0)
                {
                    d12 +=  bsrate+ "\n";
                }
                if (bsdisamt > 0)
                {
                    d12 += bsdisamt+"\n";
                }
                if (bstaxamt > 0)
                {
                    d12 += bstaxamt+"\n";
                }
                m12 = new Paragraph(new Chunk(d12, normalFont));
                m12.Alignment = Rectangle.ALIGN_RIGHT;
                bt_13.AddElement(m12);


                PdfPCell bt3 = new PdfPCell(bvbv);
                bt3.Border = 0;
                bt3.Colspan = 4;

                string dd1 = "", dd2 = "";
                if (myPO.IsPercentage == true)
                {
                    if (myPO.Discount.Length > 0)
                    {
                        dd1 = "Discount ("+myPO.Discount+" %) :";
                        dd2 = decimal.Round(Convert.ToDecimal(myPO.DiscountAmount), 2)+"";
                    }
                }
                else
                {
                    if(myPO.Discount.Length > 0)
                    {
                        dd1 = "Discount :";
                        dd2 = decimal.Round(Convert.ToDecimal((Convert.ToDecimal(myPO.Discount)).ToString("F")), 2) + "";
                    }
                }
                


                PdfPCell bt4 = new PdfPCell();
                bp1 = new Paragraph(new Chunk(dd1, normalboaldFont));
                bp1.Alignment = Rectangle.ALIGN_RIGHT;
                bt4.AddElement(bp1);
                bt4.Border = 0;

                PdfPCell bt4_1 = new PdfPCell();
                bp1 = new Paragraph(new Chunk(dd2, normalFont));
                bp1.Alignment = Rectangle.ALIGN_RIGHT;
                bt4_1.AddElement(bp1);
                bt4_1.Border = 0;
                //bt4.Colspan = 2;


                Paragraph bp2 = new Paragraph(new Chunk("Terms : ", normalboaldFont));
                bp2.Add(new Chunk(myPO.Terms.TermsName, normalFont));
                Paragraph aa = new Paragraph(new Chunk("\n\tRemarks : " + myPO.Terms.Remarks, normalFont));
                Paragraph ab = new Paragraph(new Chunk("\n\tNumber Of Days : " + myPO.Terms.NumberOfDays, normalFont));
                aa.IndentationLeft = 5;
                ab.IndentationLeft = 5;
                bp2.Add(aa);
                bp2.Add(ab);
                dsds = new PdfPCell(bp2);
                dsds.CellEvent = new RoundedBorder();
                dsds.Border = 0;
                dsds.Padding = 10;
                dsds.BackgroundColor = new BaseColor(239, 240, 241);
                bvbv = new PdfPTable(1);
                bvbv.AddCell(dsds);
                bvbv.WidthPercentage = 100;
                bvbv.HorizontalAlignment = Rectangle.ALIGN_LEFT;

                PdfPCell bt44 = new PdfPCell(bvbv);
                bt44.Border = 0;
                bt44.Colspan = 4;

                PdfPCell bt45 = new PdfPCell();
                bp1 = new Paragraph(new Chunk("Total :", boldFont));
                bp1.Alignment = Rectangle.ALIGN_RIGHT;
                bt45.AddElement(bp1);
                bt45.Border = 0;

                PdfPCell bt45_1 = new PdfPCell();
                bp1 = new Paragraph(new Chunk(myPO.TotalAmount, boldFont));
                bp1.Alignment = Rectangle.ALIGN_RIGHT;
                bt45_1.AddElement(bp1);
                bt45_1.Border = 0;
                //bt45.Colspan = 2;

                btmTable.AddCell(bt_11);
                btmTable.AddCell(bt_12);
                btmTable.AddCell(bt_13);
                btmTable.AddCell(bt3);
                btmTable.AddCell(bt4);
                btmTable.AddCell(bt4_1);
                btmTable.AddCell(bt44);
                btmTable.AddCell(bt45);
                btmTable.AddCell(bt45_1);
                document.Add(btmTable);
                //PdfContentByte over = writer.DirectContent;

                //over.SaveState();

                ////over.Rectangle(10, 10, 50, 50);
                //over.Rectangle(25, 0, 250, 50);
                //over.SetColorFill(new BaseColor(239, 240, 241));
                //over.Fill();
                ////PdfGState gs1 = new PdfGState();
                ////gs1.FillOpacity = 0.5f;
                ////over.SetGState(gs1);

                ////over.Rectangle(0, 0, 60, 60);
                ////over.SetColorFill(new BaseColor(255, 0, 0, 150));
                ////over.Fill();

                //over.RestoreState();


                //PdfContentByte over1 = writer.DirectContent;

                //over1.SaveState();
                //over1.Rectangle(300, 0, 250, 50);
                //over1.SetColorFill(new BaseColor(239, 240, 241));
                //over1.Fill();
                //over1.RestoreState();



                document.Close();
                writer.Close();
                fs.Close();

                using (System.IO.MemoryStream ms = new MemoryStream())
                {
                    byte[] bytes = File.ReadAllBytes(url + "Reports/" + model.PO + ".pdf");
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
                    File.WriteAllBytes(url + "Reports/" + model.PO + ".pdf", bytes);
                }

            }

            return new ReturnModel<PurchaseOrderViewModel>()
            {
                datam = new PurchaseOrderViewModel()
                {
                    URLData = "Reports/" + model.PO + ".pdf"
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
