using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITS.Infrastructure.Global
{
    public class LetterTemplate
    {
        public struct AcceptAndRefer
        {
            public const string PatientRefferedToSupplier =
 @"<p style='line-height:3px'> 
        ##patientfirstname## ##patientSurName##</p> 
   <p style='line-height:3px'> 
        ##patientaddress##</p> 
   <p style='line-height:3px'> 
        ##patientcity##</p> 
   <p style='line-height:3px'> 
        ##patientregion##</p> 
   <p style='line-height:3px'> 
        ##patientpostcode##</p>
   <br /> 
   <p style='line-height:5px'> 
        Our reference: ##casenumber##</p> 
   <p style='line-height:3px'> 
        ##CurrentDate##</p> 
   <br />
    <h2 style='font-weight: bold;text-align:center'> 
        ##CaseTreatmentType## Referral</h2>  
   <p style='line-height:3px'> 
        Dear ##PatientFirstName##,</p> 
   <br />
   <p style='line-height:13px'> 
        We have been asked by ##referrername## to arrange the above treatment for you. We 
        have arranged for this treatment to take place at one of our approved providers.Their 
        details are:</p> 
   <br />
   <p style='line-height:3px'> 
        ##SupplierName##</p> 
   <p style='line-height:3px'> 
        ##SupplierAddress##</p> 
   <p style='line-height:3px'> 
        ##SupplierCity##</p> 
   <p style='line-height:3px'> 
        ##SupplierRegion##</p> 
   <p style='line-height:3px'> 
        ##SupplierPostCode##</p> 
   <p style='line-height:3px'> 
        ##SupplierTelephone##</p> 
   <br />
   <p style='line-height:13px'> 
        We have asked the above clinic to contact you shortly to book your initial appointment. If 
        you wish to contact them yourself to book the appointment then please do so on the 
        above telephone number.</p> 
   <br />
   <p style='line-height:13px'> 
        You will not be charged for any treatment arranged by us. The provider will invoice 
        us directly and we will settle the costs with ##referrername## accordingly.</p> 
   <br />
   <p style='line-height:13px'> 
        Please be aware that the only costs agreed are for attended treatment sessions.
        It is standard practice for providers to charge for any missed appointments or late 
        cancellations with less than 24 hours notice. Treatment may therefore be discontinued 
        if appointments are not attended.</p> 
   <br />

   <br />
   <p style='line-height:13px'> 
        May we take this opportunity to wish the best with your treatment.</p> 
   <br />
   <p style='line-height:3px'> 
        Yours sincerely,</p> 
   <br />
   <p style='line-height:3px'> 
        ##UserFirstName## ##UserSurName##</p> 
   <p style='line-height:3px'> 
        Innovate Treatment Services</p>";
        }

        public struct Invoice
        {
            public const string InvoiceLetter = @"<html>
                             <head>
                             </head>
                             <body>
                               <p style='text-align:center; font-size: 6pt; color: gray'>T/A Innovate Treatment Services, City Point, Temple Gate, Bristol, BS1 6PL</p>
                               <p style='text-align:center; font-size: 6pt; color: gray'>Telephone: 0117 373 6171 Email: treatment@innovatehmg.co.uk Web: www.innovatehmg.co.uk</p>
                               <p style='font-size: 9pt'>##ReferrerName##</p>
                               <p style='font-size: 9pt'>##ReferrerAddress##</p>
                               <p style='text-align:center; font-size: 11pt; font-weight: bold'>INVOICE</p>
                               <h4>##REPLACEWITHLINE##</h4>
                               <table>
                                <tr>
                                 <td style='font-weight: bold'>Invoice No:</td>
                                 <td style='font-weight: bold'>##InvoiceNumber##</td>
                                </tr>
                                <tr>
                                 <td style='font-weight: bold'>Patient name: </td>
                                 <td style='font-weight: bold'>##PatientName##</td>
                                </tr>
                                <tr>
                                 <td style='font-weight: bold'>Referrer Ref:</td>
                                 <td style='font-weight: bold'>##ReferrerReferenceNumber##</td>
                                </tr>
                                <tr>
                                 <td style='font-weight: bold'>Innovate Treatment Services Ref:</td>
                                 <td style='font-weight: bold'>##CaseNumber##</td>
                                </tr>
                                <tr>
                                 <td style='font-weight: bold'>Date of Invoice:</td>
                                 <td style='font-weight: bold'>##InvoiceDate##</td>
                                </tr>
                               </table>
                               <h4>##REPLACEWITHLINE##</h4>
                                <table>
                                <tr>
                                 <td style='font-weight: bold'>Description</td>
                                 <td style='font-weight: bold'>Cost</td>
                                </tr>
                                ##REPLACEWITHPRICING##
                                 <tr>
                                 <td style='font-weight: bold'>INVOICE TOTAL</td>
                                 <td style='font-weight: bold'>£ ##AMOUNT##</td>
                                </tr>
                               </table>
                               <h4>##REPLACEWITHLINE##</h4>
                               <p style='text-align: center; font-size: 9pt; font-weight: bold'>Please pay this invoice within 30 days</p>
                               <p style='text-align: center; font-size: 9pt'>If there is any reason why you are unable to issue payment please contact Innovate Treatment Services on 0117 373 6171</p>
                               <br />
                               <table>
                                <tr>
                                 <td style='font-weight: bold'>For Payment by BACS please pay:</td>
                                 <td style='font-weight: bold'>For Postal cheques please remit to:</td>
                                </tr>
                                <tr>
                                 <td style='font-weight: bold'>Lloyds TSB</td>
                                 <td style='font-weight: bold'>Innovate Treatment Services</td>
                                </tr>
                                <tr>
                                 <td style='font-weight: bold'>Account Name: Innovate Treatment Services</td>
                                 <td style='font-weight: bold'>City Point</td>
                                </tr>
                                <tr>
                                 <td style='font-weight: bold'>Sort: 30-90-76</td>
                                 <td style='font-weight: bold'>Temple Gate</td>
                                </tr>
                                <tr>
                                 <td style='font-weight: bold'>Account No: 39996560</td>
                                 <td style='font-weight: bold'>Bristol</td>
                                </tr>
                                <tr>
                                 <td style='font-weight: bold'>Ref: ##CaseNumber##</td>
                                 <td style='font-weight: bold'>BS1 6PL</td>
                                </tr>
                               </table>
                               <p style='font-size: 9pt;font-weight: bold'>Amount Payable: £ ##AMOUNT##</p>
                               <br>
                               <p style='text-align: center; font-size: 9pt; font-weight: bold'>Please ensure our reference number of ##CaseNumber## is included on both BACS & Cheque payments.</p>
                               <br/>
                               <p style='font-weight: bold; font-size: 9pt'> Company Reg: 07758732 </p>                             
                               </body>
                             </html>";
        }
    }
}
