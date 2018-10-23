using System.IO;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace PdfPasswordRemover
{
    public class RemovePassword : IRemovePassword
    {
        public void CopyPdf(byte[] fileData, string userPassword, string outputPath)
        {
            PdfReader.unethicalreading = true;
            var password = Encoding.ASCII.GetBytes(userPassword);
            var pdfReader = new PdfReader(fileData, password);
            var pages = pdfReader.NumberOfPages;
            var pdfDoc = new Document();
            var pdfCopy = new PdfCopy(pdfDoc, new FileStream(outputPath, FileMode.Create));

            pdfDoc.Open();
            var i = 0;
            while (i < pages)
            {
                pdfCopy.AddPage(pdfCopy.GetImportedPage(pdfReader, i + 1));
                i += 1;
            }

            pdfDoc.Close();
        }
    }
}