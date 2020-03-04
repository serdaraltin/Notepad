using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace Not_Defteri
{
    public partial class Veri_Dönüştürücü : Form
    {
        public Veri_Dönüştürücü()
        {
            InitializeComponent();
        }
        string base64 = "";
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.Image = Clipboard.GetImage();
             
            }
            catch {
                MessageBox.Show("Panoda herhangi bir görsel öğe bulunamadu!", "Veri Dönüştürücü", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Byte[] resim = null;
            ImageConverter imgConverter = new ImageConverter();
            resim = (System.Byte[])imgConverter.ConvertTo(pictureBox1.Image, Type.GetType("System.Byte[]"));
            base64 = Convert.ToBase64String(resim);
            richTextBox1.Text = base64;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text != "")
            {
                Clipboard.SetText(richTextBox1.Text);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text != "")
            {
                try
                {
                    byte[] imageBytes = Convert.FromBase64String(richTextBox1.Text);
                    MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                    ms.Write(imageBytes, 0, imageBytes.Length);
                    pictureBox1.Image = Image.FromStream(ms, true);
                }
                catch (Exception hata)
                {
                    MessageBox.Show(hata.Message.ToString(), "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                Clipboard.SetImage(pictureBox1.Image);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (Clipboard.GetText() != "")
            {
                richTextBox1.Text = Clipboard.GetText();
            }
            }
    }
}
