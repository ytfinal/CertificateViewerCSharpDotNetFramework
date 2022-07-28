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

            if (certPathTextBox.Text.EndsWith(".cer"))
            {
                try
                {
                    byte[] certFileBytes = System.IO.File.ReadAllBytes(certPathTextBox.Text);
                    CertBase64TextBox.Text = System.Text.Encoding.UTF8.GetString(certFileBytes, 0, certFileBytes.Length);
                    x509Cert = new X509Certificate2(certFileBytes);
                    LoadCertificateInfo();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading Public Key Certificeate (.cer) {Environment.NewLine}{ex.Message}");
                }
            }
            else if (certPathTextBox.Text.EndsWith(".pfx"))
            {
                try
                {
                    byte[] pfxFileBytes = System.IO.File.ReadAllBytes(certPathTextBox.Text);
                    string pfxBase64 = Convert.ToBase64String(pfxFileBytes);
                    CertBase64TextBox.Text = pfxBase64;
                    x509Cert = new X509Certificate2(pfxFileBytes, PFXPasswordTextBox.Text);
                    LoadCertificateInfo();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error Loading Private Key Certficate (.pfx) {Environment.NewLine}{ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Unsupported file format. Load .cer or .pfx");
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

        private void LoadCertificateInfo()
        {
            SubjectTextBox.Text = x509Cert.Subject;
            IssuerTextBox.Text = x509Cert.Issuer;
            VersionTextBox.Text = x509Cert.Version.ToString();
            ValidDateTextBox.Text = x509Cert.NotBefore.ToString();
            ExpiryDateTextBox.Text = x509Cert.NotAfter.ToString();
            ThumbprintTextBox.Text = x509Cert.Thumbprint.ToString();
            SerialNumberTextBox.Text = x509Cert.SerialNumber;
            FriendlyNameTextBox.Text = x509Cert.PublicKey.Oid.FriendlyName;
            PublicKeyFormatTextBox.Text = x509Cert.PublicKey.EncodedKeyValue.Format(true);
            RawDataLengthTextBox.Text = x509Cert.RawData.Length.ToString();
            CertificateToStringTextBox.Text = x509Cert.ToString(true);
            CertificateToXMLTextBox.Text = x509Cert.PublicKey.Key.ToXmlString(false);
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
