namespace PdfPasswordRemover
{
    public interface IRemovePassword
    {
        void CopyPdf(byte[] fileData, string userPassword, string outputPath);
    }
}