// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRemovePassword.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   An interface to remove passwords.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace PdfPasswordRemover
{
    /// <summary>
    /// An interface to remove passwords.
    /// </summary>
    public interface IRemovePassword
    {
        /// <summary>
        /// Copies the PDF and removes the password.
        /// </summary>
        /// <param name="fileData">The file data.</param>
        /// <param name="userPassword">The user password.</param>
        /// <param name="outputPath">The output path.</param>
        void CopyPdf(byte[] fileData, string userPassword, string outputPath);
    }
}