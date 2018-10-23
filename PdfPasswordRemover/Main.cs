using System;
using System.IO;
using System.Windows.Forms;
using Languages.Implementation;
using Languages.Interfaces;

namespace PdfPasswordRemover
{
    public partial class Main : Form
    {
        private readonly IBruteforcing _bruteForce =
            new Bruteforcing(
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ!_-?:;,'<>|$%&/()[]{}ß0123456789", 1, 16);

        private readonly ILanguageManager _lm = new LanguageManager();
        private readonly IRemovePassword _removePassword = new RemovePassword();
        private ILanguage _lang;

        public Main()
        {
            InitializeComponent();
            InitializeLanguageManager();
            LoadTitleAndDescription();
            LoadLanguagesToCombo();
        }

        private void LoadTitleAndDescription()
        {
            Text = Application.ProductName + @" " + Application.ProductVersion;
        }

        private void InitializeLanguageManager()
        {
            _lm.SetCurrentLanguage("de-DE");
            _lm.OnLanguageChanged += OnLanguageChanged;
        }

        private void LoadLanguagesToCombo()
        {
            foreach (var lang in _lm.GetLanguages())
                comboBoxSelectLanguage.Items.Add(lang.Name);
            comboBoxSelectLanguage.SelectedIndex = 0;
        }

        private void OnLanguageChanged(object sender, EventArgs eventArgs)
        {
            _lang = _lm.GetCurrentLanguage();
            labelSelectedLanguage.Text = _lang.GetWord("SelectLanguage");
            buttonRemovePassword.Text = _lang.GetWord("RemovePassword");
            labelTypePassword.Text = _lang.GetWord("TypePassword");
            SetShowHidePasswordButtonText();
        }

        private void LoadPdfFile(string userPassword)
        {
            var openFileDialog = GetNewOpenFileDialog();

            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            if (string.IsNullOrWhiteSpace(userPassword))
                BruteForceUserPassword(openFileDialog.FileName);
            else
                RemovePdfPassword(openFileDialog.FileName, textBoxPassword.Text);
        }

        private void RemovePdfPassword(string fileName, string userPassword)
        {
            var readBytes = File.ReadAllBytes(fileName);
            var saveFileDialog = GetNewSaveFileDialog();

            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;

            _removePassword.CopyPdf(readBytes, userPassword, saveFileDialog.FileName);
        }

        private void BruteForceUserPassword(string fileName)
        {
            var readBytes = File.ReadAllBytes(fileName);
            var saveFileDialog = GetNewSaveFileDialog();

            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;

            foreach (string bruteForcePassword in _bruteForce)
                try
                {
                    _removePassword.CopyPdf(readBytes, bruteForcePassword, saveFileDialog.FileName);
                }
                catch (Exception)
                {
                    // ignored
                }
        }

        private void TryLoadPdfFile(object sender, EventArgs e)
        {
            try
            {
                LoadPdfFile(textBoxPassword.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, ex.Message, MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            }
        }

        private void ComboBoxSelectLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            _lm.SetCurrentLanguageFromName(comboBoxSelectLanguage.SelectedItem.ToString());
        }

        private OpenFileDialog GetNewOpenFileDialog()
        {
            return new OpenFileDialog
            {
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = "*.pdf",
                Filter = _lang.GetWord("PdfFileFilter"),
                Multiselect = false,
                Title = _lang.GetWord("OpenFileTitle")
            };
        }

        private SaveFileDialog GetNewSaveFileDialog()
        {
            return new SaveFileDialog
            {
                CheckPathExists = true,
                DefaultExt = "*.pdf",
                Filter = _lang.GetWord("PdfFileFilter"),
                Title = _lang.GetWord("SaveFileTitle")
            };
        }

        private void ButtonShowPassword_Click(object sender, EventArgs e)
        {
            switch (textBoxPassword.PasswordChar)
            {
                case '*':
                    textBoxPassword.PasswordChar = '\0';
                    buttonShowPassword.Text = _lang.GetWord("HidePassword");
                    break;
                case '\0':
                    textBoxPassword.PasswordChar = '*';
                    buttonShowPassword.Text = _lang.GetWord("ShowPassword");
                    break;
            }
        }

        private void SetShowHidePasswordButtonText()
        {
            switch (textBoxPassword.PasswordChar)
            {
                case '*':
                    buttonShowPassword.Text = _lang.GetWord("ShowPassword");
                    break;
                case '\0':
                    buttonShowPassword.Text = _lang.GetWord("HidePassword");
                    break;
                default:
                    buttonShowPassword.Text = _lang.GetWord("ShowPassword");
                    break;
            }
        }
    }
}