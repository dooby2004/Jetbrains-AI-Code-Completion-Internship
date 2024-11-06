using System.Diagnostics;
using System.Security.Cryptography;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;

namespace Modux_MD5
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
        }

        private void decryptBtn_Click(object sender, EventArgs e)
        {
            decryptOutput.Text = String.Empty;
            decryptOutput.Update();
            try
            {
                (Int32 code, string result) = MD5Methods.DecryptFromFile(decryptInput.Text, File.OpenRead(keywordsPath.Text), MD5Methods.EncryptMD5);
                switch (code)
                {
                    case 0:
                        decryptOutput.Text = result;
                        break;
                    case 1:
                        decryptOutput.Text = "No Solution Found";
                        break;
                    case 2:
                        decryptOutput.Text = "Invalid Hash";
                        break;
                }
            }
            catch (ArgumentException ex)
            {
                decryptOutput.Text = "No File Path Provided";
            }
            catch (FileNotFoundException ex)
            {
                decryptOutput.Text = "Invalid File Path";
            }
        }

        private void encryptBtn_Click(object sender, EventArgs e)
        {
            encryptOutput.Text = MD5Methods.Encrypt(encryptInput.Text, MD5Methods.EncryptMD5);
        }

        private void keywordsBtn_Click(object sender, EventArgs e)
        {
            // Source: https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.openfiledialog?view=windowsdesktop-8.0
            using (OpenFileDialog keywordsOpen = new OpenFileDialog())
            {
                keywordsOpen.Filter = "Text/CSV files (*.txt, *.csv)|*.txt;*.csv|All files (*.*)|*.*";
                keywordsOpen.FilterIndex = 1;
                if (keywordsOpen.ShowDialog() == DialogResult.OK)
                {
                    keywordsPath.Text = keywordsOpen.FileName;
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void decryptHeading_Click(object sender, EventArgs e)
        {

        }

        private void altKeywordsBtn_Click(object sender, EventArgs e)
        {
            // Source: https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.openfiledialog?view=windowsdesktop-8.0
            using (OpenFileDialog keywordsOpen = new OpenFileDialog())
            {
                keywordsOpen.Filter = "Text/CSV files (*.txt, *.csv)|*.txt;*.csv|All files (*.*)|*.*";
                keywordsOpen.FilterIndex = 1;
                if (keywordsOpen.ShowDialog() == DialogResult.OK)
                {
                    altKeywordsPath.Text = keywordsOpen.FileName;
                }
            }
        }

        private void altDecryptBtn_Click(object sender, EventArgs e)
        {
            altDecryptOutput.Text = String.Empty;
            altDecryptOutput.Update();
            try
            {
                (Int32 code, string result) = MD5Methods.ForceDecrypt(altDecryptInput.Text, File.ReadAllLines(altKeywordsPath.Text), MD5Methods.EncryptMD5);
                switch (code)
                {
                    case 0:
                        altDecryptOutput.Text = result;
                        break;
                    case 1:
                        altDecryptOutput.Text = "No Solution Found";
                        break;
                    case 2:
                        altDecryptOutput.Text = "Invalid Hash";
                        break;
                }
            }
            catch (ArgumentException ex)
            {
                altDecryptOutput.Text = "No File Path Provided";
            }
            catch (FileNotFoundException ex)
            {
                altDecryptOutput.Text = "Invalid File Path";
            }
        }
    }


}
