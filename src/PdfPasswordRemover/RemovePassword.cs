// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RemovePassword.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   A class to remove passwords.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace PdfPasswordRemover
{
    using System.IO;
    using System.Text;

    using iTextSharp.text;
    using iTextSharp.text.pdf;

    /// <inheritdoc cref="IRemovePassword"/>
    /// <summary>
    /// A class to remove passwords.
    /// </summary>
    /// <seealso cref="IRemovePassword"/>
    public class RemovePassword : IRemovePassword
    {
        /// <inheritdoc cref="IRemovePassword"/>
        /// <summary>
        /// Copies the PDF and removes the password.
        /// </summary>
        /// <param name="fileData">The file data.</param>
        /// <param name="userPassword">The user password.</param>
        /// <param name="outputPath">The output path.</param>
        /// <seealso cref="IRemovePassword"/>
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