using Entities.Concrete;
using iTextSharp.text.pdf;
using Microsoft.Extensions.Hosting;

namespace Business.Helpers.DebitHelpers
{
    public class GetDebitPDFPath
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public GetDebitPDFPath(IHostingEnvironment environment)
        {
            _hostingEnvironment = environment;
        }
        public string GetPdfPath(Debit debit, User user)
        {

            string templatePdfFileName = "Zimmet_Tutanagi.pdf";
            string templatePdfFilePath = Path.Combine(_hostingEnvironment.ContentRootPath, "../Business", "Helpers", "DebitHelpers", "DebitForms", templatePdfFileName);

            string dynamicPath = Path.Combine(_hostingEnvironment.ContentRootPath, "file-storage", "debits");

            FolderIsExists(dynamicPath);

            string pdfFileName = $"{debit.SerialNumber}_Zimmet.pdf";

            string pdfFilePath = Path.Combine(dynamicPath, pdfFileName);

            PdfReader pdfReader = new PdfReader(templatePdfFilePath);

            using (FileStream filledPdfStream = new FileStream(pdfFilePath, FileMode.Create))
            {
                PdfStamper pdfStamper = new PdfStamper(pdfReader, filledPdfStream);
                AcroFields pdfFormFields = pdfStamper.AcroFields;

                pdfFormFields.SetField("user_name", user.Name);
                pdfFormFields.SetField("user_title", "Personel");
                pdfFormFields.SetField("laptop", debit.Laptop);
                pdfFormFields.SetField("serial_number", debit.SerialNumber);
                pdfFormFields.SetField("barcode_number", debit.BarcodeNumber);
                pdfFormFields.SetField("model", debit.Model);
                pdfFormFields.SetField("cpu", debit.CPU);
                pdfFormFields.SetField("ram", debit.RAM);
                pdfFormFields.SetField("storage", debit.Storage);
                pdfFormFields.SetField("mouse", debit.Mouse);
                pdfFormFields.SetField("computer_bag", debit.ComputerBag);
                pdfFormFields.SetField("delivery_date", debit.DeliveryDate.Date.ToString());


                pdfStamper.FormFlattening = true;
                pdfStamper.Close();
            }

            return pdfFilePath;
        }

        private void FolderIsExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
