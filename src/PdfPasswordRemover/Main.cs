// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Main.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   The main form.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace PdfPasswordRemover;

/// <summary>
/// The main form.
/// </summary>
public partial class Main : Form
{
    /// <summary>
    /// The brute forcing class.
    /// </summary>
    private readonly IBruteforcing bruteForcing = new Bruteforcing(
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ!_-?:;,'<>|$%&/()[]{}ß0123456789",
        1,
        16);

    /// <summary>
    /// The language manager.
    /// </summary>
    private readonly ILanguageManager languageManager = new LanguageManager();

    /// <summary>
    /// The password remover.
    /// </summary>
    private readonly IRemovePassword removePassword = new RemovePassword();

    /// <summary>
    /// The language.
    /// </summary>
    private ILanguage? language;

    /// <summary>
    /// Initializes a new instance of the <see cref="Main"/> class.
    /// </summary>
    public Main()
    {
        this.InitializeComponent();
        this.InitializeLanguageManager();
        this.LoadTitleAndDescription();
        this.LoadLanguagesToCombo();
    }

    /// <summary>
    /// Loads the title and description.
    /// </summary>
    private void LoadTitleAndDescription()
    {
        this.Text = Application.ProductName + @" " + Application.ProductVersion;
    }

    /// <summary>
    /// Initializes the language manager.
    /// </summary>
    private void InitializeLanguageManager()
    {
        this.languageManager.SetCurrentLanguage("de-DE");
        this.languageManager.OnLanguageChanged += this.OnLanguageChanged;
    }

    /// <summary>
    /// Loads the languages to the combo box.
    /// </summary>
    private void LoadLanguagesToCombo()
    {
        foreach (var lang in this.languageManager.GetLanguages())
        {
            this.comboBoxSelectLanguage.Items.Add(lang.Name);
        }

        this.comboBoxSelectLanguage.SelectedIndex = 0;
    }

    /// <summary>
    /// Handles the language changed event.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The event args.</param>
    private void OnLanguageChanged(object sender, EventArgs e)
    {
        this.language = this.languageManager.GetCurrentLanguage();
        this.labelSelectedLanguage.Text = this.language.GetWord("SelectLanguage");
        this.buttonRemovePassword.Text = this.language.GetWord("RemovePassword");
        this.labelTypePassword.Text = this.language.GetWord("TypePassword");
        this.SetShowHidePasswordButtonText();
    }

    /// <summary>
    /// Loads the PDF file.
    /// </summary>
    /// <param name="userPassword">The user password.</param>
    private void LoadPdfFile(string userPassword)
    {
        var openFileDialog = this.GetNewOpenFileDialog();

        if (openFileDialog.ShowDialog() != DialogResult.OK)
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(userPassword))
        {
            this.BruteForceUserPassword(openFileDialog.FileName);
        }
        else
        {
            this.RemovePdfPassword(openFileDialog.FileName, this.textBoxPassword.Text);
        }
    }

    /// <summary>
    /// Removes the PDF file's password.
    /// </summary>
    /// <param name="fileName">The file name.</param>
    /// <param name="userPassword">The password.</param>
    private void RemovePdfPassword(string fileName, string userPassword)
    {
        var readBytes = File.ReadAllBytes(fileName);
        var saveFileDialog = this.GetNewSaveFileDialog();

        if (saveFileDialog.ShowDialog() != DialogResult.OK)
        {
            return;
        }

        this.removePassword.CopyPdf(readBytes, userPassword, saveFileDialog.FileName);
    }

    /// <summary>
    /// Runs a brute-force attack on the PDF file's password.
    /// </summary>
    /// <param name="fileName">The file name.</param>
    private void BruteForceUserPassword(string fileName)
    {
        var readBytes = File.ReadAllBytes(fileName);
        var saveFileDialog = this.GetNewSaveFileDialog();

        if (saveFileDialog.ShowDialog() != DialogResult.OK)
        {
            return;
        }

        foreach (string bruteForcePassword in this.bruteForcing)
        {
            try
            {
                this.removePassword.CopyPdf(readBytes, bruteForcePassword, saveFileDialog.FileName);
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }

    /// <summary>
    /// Tries to load the PDF file.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The event args.</param>
    private void TryLoadPdfFile(object sender, EventArgs e)
    {
        try
        {
            this.LoadPdfFile(this.textBoxPassword.Text);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.StackTrace, ex.Message, MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
        }
    }

    /// <summary>
    /// Handles the selected language changed event.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The event args.</param>
    private void ComboBoxSelectLanguageSelectedIndexChanged(object sender, EventArgs e)
    {
        this.languageManager.SetCurrentLanguageFromName(this.comboBoxSelectLanguage.SelectedItem.ToString());
    }

    /// <summary>
    /// Gets a new <see cref="OpenFileDialog"/>.
    /// </summary>
    /// <returns>A new <see cref="OpenFileDialog"/>.</returns>
    private OpenFileDialog GetNewOpenFileDialog()
    {
        if (this.language is null)
        {
            return new OpenFileDialog
            {
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = "*.pdf",
                Filter = string.Empty,
                Multiselect = false,
                Title = string.Empty
            };
        }

        return new OpenFileDialog
        {
            CheckFileExists = true,
            CheckPathExists = true,
            DefaultExt = "*.pdf",
            Filter = this.language.GetWord("PdfFileFilter"),
            Multiselect = false,
            Title = this.language.GetWord("OpenFileTitle")
        };
    }

    /// <summary>
    /// Gets a new <see cref="SaveFileDialog"/>.
    /// </summary>
    /// <returns>A new <see cref="SaveFileDialog"/></returns>.
    private SaveFileDialog GetNewSaveFileDialog()
    {
        if (this.language is null)
        {
            return new SaveFileDialog
            {
                CheckPathExists = true,
                DefaultExt = "*.pdf",
                Filter = string.Empty,
                Title = string.Empty
            };
        }

        return new SaveFileDialog
        {
            CheckPathExists = true,
            DefaultExt = "*.pdf",
            Filter = this.language.GetWord("PdfFileFilter"),
            Title = this.language.GetWord("SaveFileTitle")
        };
    }

    /// <summary>
    /// Handles the button click to show the password.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The event args.</param>
    private void ButtonShowPasswordClick(object sender, EventArgs e)
    {
        if (this.language is null)
        {
            return;
        }

        switch (this.textBoxPassword.PasswordChar)
        {
            case '*':
                this.textBoxPassword.PasswordChar = '\0';
                this.buttonShowPassword.Text = this.language.GetWord("HidePassword");
                break;
            case '\0':
                this.textBoxPassword.PasswordChar = '*';
                this.buttonShowPassword.Text = this.language.GetWord("ShowPassword");
                break;
        }
    }

    /// <summary>
    /// Sets the show or hide password text.
    /// </summary>
    private void SetShowHidePasswordButtonText()
    {
        if (this.language is null)
        {
            return;
        }

        this.buttonShowPassword.Text = this.textBoxPassword.PasswordChar switch
        {
            '*' => this.language.GetWord("ShowPassword"),
            '\0' => this.language.GetWord("HidePassword"),
            _ => this.language.GetWord("ShowPassword"),
        };
    }
}
