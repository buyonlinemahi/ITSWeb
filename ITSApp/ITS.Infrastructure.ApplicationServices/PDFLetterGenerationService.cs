using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITS.Infrastructure.ApplicationServices.Contracts;
using ITS.Domain.Models;
using ITS.Infrastructure.Global;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf.draw;
using ITS.Domain.Models.CaseAssessmentModel;
namespace ITS.Infrastructure.ApplicationServices
{
    public class PDFLetterGenerationService : ILetterGeneration
    {
        public byte[] GeneratePatientReferrerToSupplier(CasePatientReferrerSupplier casePatientReferrerSupplier, ITSUser currentUser)
        {
            using (MemoryStream output = new MemoryStream())
            {
                Document document = new Document();
                document.SetPageSize(PageSize.A4);

                PdfWriter.GetInstance(document, output);
                document.Open();
                string htmlText = LetterTemplate.AcceptAndRefer.PatientRefferedToSupplier;
                htmlText = htmlText.Replace("##patientfirstname##", casePatientReferrerSupplier.FirstName);
                htmlText = htmlText.Replace("##patientSurName##", casePatientReferrerSupplier.LastName);
                htmlText = htmlText.Replace("##patientaddress##", casePatientReferrerSupplier.Address);
                htmlText = htmlText.Replace("##patientcity##", casePatientReferrerSupplier.City);
                htmlText = htmlText.Replace("##patientregion##", casePatientReferrerSupplier.Region);
                htmlText = htmlText.Replace("##patientpostcode##", casePatientReferrerSupplier.PostCode);
                htmlText = htmlText.Replace("##casenumber##", casePatientReferrerSupplier.CaseNumber.ToString());
                htmlText = htmlText.Replace("##CurrentDate##", DateTime.Now.ToShortDateString());
                htmlText = htmlText.Replace("##CaseTreatmentType##", casePatientReferrerSupplier.TreatmentTypeName);
                htmlText = htmlText.Replace("##PatientFirstName##", casePatientReferrerSupplier.FirstName);
                htmlText = htmlText.Replace("##referrername##", casePatientReferrerSupplier.ReferrerName);
                htmlText = htmlText.Replace("##SupplierName##", casePatientReferrerSupplier.SupplierName);
                htmlText = htmlText.Replace("##SupplierAddress##", casePatientReferrerSupplier.Address);
                htmlText = htmlText.Replace("##SupplierCity##", casePatientReferrerSupplier.City);
                htmlText = htmlText.Replace("##SupplierRegion##", casePatientReferrerSupplier.Region);
                htmlText = htmlText.Replace("##SupplierPostCode##", casePatientReferrerSupplier.PostCode);
                htmlText = htmlText.Replace("##SupplierTelephone##", casePatientReferrerSupplier.Phone);
                htmlText = htmlText.Replace("##UserFirstName##", currentUser.FirstName);
                htmlText = htmlText.Replace("##UserSurName##", currentUser.LastName);
                List<IElement> htmlElements = HTMLWorker.ParseToList(new StringReader(htmlText), null);  // add the IElements to the document 
                for (int i = 0; i < htmlElements.Count; i++)
                {  // cast the element  
                    IElement htmlElement = ((IElement)htmlElements[i]);
                    if (htmlElement.Chunks.Any())
                    {
                        Chunk chnk = htmlElement.Chunks.FirstOrDefault();
                        chnk.Font.Size = 9;
                        Paragraph paragraph = new Paragraph(chnk);
                        document.Add(paragraph);
                    }
                }
                // close the document 
                document.Close();
                return output.ToArray();
            }
        }

        public byte[] GenerateInvoice(ITS.Domain.Models.CaseModel.CasePatientReferrer casePatientReferrer, IEnumerable<CaseTreatmentPricingType> treatmentPricing,
            IEnumerable<CaseBespokeServicePricingType> bespokePricing, decimal vat, string imageLogoPath,out decimal totalAmountOut, out string invoiceNumberOut)
        {
            using (MemoryStream output = new MemoryStream())
            {
                StyleSheet css = new StyleSheet();
                css.LoadStyle("center", "style", "text-align: center");
                FontFactory.RegisterDirectories();

                decimal totalAmount = 0;

                Document document = new Document();
                document.SetPageSize(PageSize.A4);

                PdfWriter.GetInstance(document, output);
                document.Open();

                var logo = iTextSharp.text.Image.GetInstance(imageLogoPath);
                logo.Alignment = iTextSharp.text.Image.ALIGN_CENTER;
                logo.ScaleAbsolute(360f, 50f);
                document.Add(logo);

                string htmlText = LetterTemplate.Invoice.InvoiceLetter;
                htmlText = htmlText.Replace("##InvoiceNumber##", string.Format("{0}-{1}", DateTime.Now.ToString("ddMMyyyy"), casePatientReferrer.CaseID));
                htmlText = htmlText.Replace("##InvoiceDate##", DateTime.Now.ToString("dd/MM/yyyy"));

                htmlText = htmlText.Replace("##PatientName##", (casePatientReferrer.FirstName + "  " + casePatientReferrer.LastName));
                htmlText = htmlText.Replace("##ReferrerReferenceNumber##", casePatientReferrer.CaseReferrerReferenceNumber);
                htmlText = htmlText.Replace("##CaseNumber##", Convert.ToString(casePatientReferrer.CaseNumber));

                htmlText = htmlText.Replace("##ReferrerName##", casePatientReferrer.ReferrerName);



                htmlText = htmlText.Replace("##ReferrerAddress##", casePatientReferrer.Address + "<br/>" + casePatientReferrer.Region + "<br/>" + casePatientReferrer.City + "<br/>" + casePatientReferrer.PostCode);
                StringBuilder str = new StringBuilder();
                decimal groupedTreatmentPricingSum = 0.0m;
                decimal groupedBespokePricingSum = 0.0m;
                decimal vatAmount = 0.0m;
                if (treatmentPricing != null)
                {
                    var groupedTreatmentPricing = treatmentPricing.GroupBy(x => x.PricingTypeName, (key, g) => new { Key = key, Amount = g.Sum(y => y.ReferrerPrice), Count = g.Count() });
                    groupedTreatmentPricingSum = groupedTreatmentPricing.Sum(x => x.Amount);
                    foreach (var price in groupedTreatmentPricing)
                    {
                        str.Append(string.Format("<tr><td style='font-weight: bold'>{0} x {1}</td><td style='font-weight: bold'>£{2}</td></tr>", price.Count, price.Key, price.Amount.ToString("N")));

                    }
                }
                if (bespokePricing != null)
                {
                    var groupedBespokePricing = bespokePricing.GroupBy(x => x.BespokeServiceName, (key, g) => new { Key = key, Amount = g.Sum(y => y.ReferrerPrice), Count = g.Count() });
                    groupedBespokePricingSum = groupedBespokePricing.Sum(x => x.Amount);
                    foreach (var price in groupedBespokePricing)
                    {
                        str.Append(string.Format("<tr><td style='font-weight: bold'>{0} x {1}</td><td style='font-weight: bold'>£{2}</td></tr>", price.Count, price.Key, price.Amount.ToString("N")));
                    }
                }

                vatAmount = (groupedBespokePricingSum + groupedTreatmentPricingSum) * decimal.Divide(vat, 100);
                totalAmount = groupedBespokePricingSum + groupedTreatmentPricingSum + vatAmount;
               
                
                
                if (vat != 0)
                {
                    str.Append(string.Format("<tr><td style='font-weight: bold'>{0} {1}%</td><td style='font-weight: bold'>£{2}</td></tr>", "VAT", vat, vatAmount.ToString("N")));
                }
                

                htmlText = htmlText.Replace("##REPLACEWITHPRICING##", str.ToString());
                htmlText = htmlText.Replace("##AMOUNT##", totalAmount.ToString("N"));


                List<IElement> htmlElements = HTMLWorker.ParseToList(new StringReader(htmlText), css);  // add the IElements to the document 
                for (int i = 0; i < htmlElements.Count; i++)
                {  // cast the element  
                    IElement htmlElement = ((IElement)htmlElements[i]);

                    if (htmlElement.GetType() == typeof(PdfPTable))
                    {
                        PdfPTable tb = (PdfPTable)htmlElement;

                        foreach (PdfPRow row in tb.Rows)
                        {
                            foreach (PdfPCell cell in row.GetCells())
                            {
                                foreach (IElement cellElement in cell.CompositeElements)
                                {
                                    foreach (Chunk cellElementChunk in cellElement.Chunks)
                                    {
                                        cellElementChunk.Font.Size = 9;
                                    }
                                }
                            }
                        }
                        document.Add(tb);
                    }

                    if (htmlElement.Chunks.Any())
                    {
                        Chunk chnk = htmlElement.Chunks.FirstOrDefault();
                        if (chnk.Content.Equals("##REPLACEWITHLINE##"))
                        {
                            Chunk linebreak = new Chunk(new LineSeparator(2f, 100f, new BaseColor(System.Drawing.Color.YellowGreen), Element.ALIGN_CENTER, -1));
                            document.Add(linebreak);
                        }
                        else
                        {
                            document.Add(htmlElement);
                        }
                    }
                }
                // close the document 
                document.Close();
                // for returning the values to calling methods
                totalAmountOut =  totalAmount;
                invoiceNumberOut =  string.Format("{0}-{1}", DateTime.Now.ToString("ddMMyyyy"), casePatientReferrer.CaseID);

                return output.ToArray();
            }
        }
    }
}
