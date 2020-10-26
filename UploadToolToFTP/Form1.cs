using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace UploadToolToFTP
{
    public partial class Form1 : Form
    {
        string websiteURL, username, password;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            using (WebClient client = new WebClient())
            {
                string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                string filename = Path.GetFullPath(FileList[0]);

                client.UploadProgressChanged += ProgressChanged;
                client.UploadFileCompleted += UploadCompleted;

                client.Credentials = new NetworkCredential(username, password);
                client.UploadFileAsync(new Uri(websiteURL), filename);
            }
        }
        private void ProgressChanged(object sender, UploadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }
        private void UploadCompleted(object sender, UploadFileCompletedEventArgs e)
        {
            label1.Text = "Upload Complete!";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.Text = websiteURL;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.Text = username;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox3.Text = password;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
            {
                String[] strGetFormats = e.Data.GetFormats();
                e.Effect = DragDropEffects.None;
            }
        }
    }
}
