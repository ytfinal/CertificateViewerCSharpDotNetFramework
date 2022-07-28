using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;

namespace CertificateViewerCSharpDotNetFramework
{
    public partial class certificateViewerForm : Form
    {
        private X509Certificate2 x509Cert;

        public certificateViewerForm()
        {
            InitializeComponent();
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            if (CertFileOpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                certPathTextBox.Text = CertFileOpenFileDialog.FileName;
            }
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            ClearCertificateInfo();

            if (!System.IO.File.Exists(certPathTextBox.Text))
            {
                MessageBox.Show("File not found!");
                return;
            }



        }

        private void certPathTextBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void certPathTextBox_DragDrop(object sender, DragEventArgs e)
        {
            string[] fileDropContents = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string fileDropContent in fileDropContents)
            {
                certPathTextBox.Text = fileDropContent;
            }
        }

        private void CertFileOpenFileDialog_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void ClearCertificateInfo()
        {
            CertBase64TextBox.Text = string.Empty;
            SubjectTextBox.Text = string.Empty;
            IssuerTextBox.Text = string.Empty;
            VersionTextBox.Text = string.Empty;
            ValidDateTextBox.Text = string.Empty;
            ExpiryDateTextBox.Text = string.Empty;
            ThumbprintTextBox.Text = string.Empty;
            SerialNumberTextBox.Text = string.Empty;
            FriendlyNameTextBox.Text = string.Empty;
            PublicKeyFormatTextBox.Text = string.Empty;
            RawDataLengthTextBox.Text = string.Empty;
            CertificateToStringTextBox.Text = string.Empty;
            CertificateToXMLTextBox.Text = string.Empty;
        }
    }
}
