using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Not_Defteri
{
    public partial class kripto : Form
    {
        public kripto()
        {
            InitializeComponent();
        }
        string metin = "";
        private void kripto_Load(object sender, EventArgs e)
        {
            Main main = Application.OpenForms["Main"] as Main;
            metin = main.richTextBox1.Text;
            button1.PerformClick();
            label3.Text="0/"+metin.Length.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            string key = "";
            char[] ch = "0123456789abcdefghijklmnopqrstuvwxyz".ToCharArray();
            for (int i = 0; i <= 10; i++)
            {
                key += ch[random.Next(0, ch.Length - 1)].ToString();
            }
            textBox1.Text = key;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                label3.Text = "0/" + (metin.Length - 1).ToString();
                progressBar1.Value = 0;
                progressBar1.Maximum = metin.Length - 1;
                string sifrelimetin = "";
                int anahtar = 0;
                for (int a = 0; a <= textBox1.Text.Length - 1; a++)
                {
                    char karekter = Convert.ToChar(textBox1.Text[a].ToString());
                    anahtar += (int)karekter;

                }
                anahtar = anahtar / textBox1.Text.Length;
                char carp = Convert.ToChar(textBox1.Text[0].ToString());
                anahtar = anahtar * (int)carp;
                for (int i = 0; i <= metin.Length - 1; i++)
                {
                    char hane = Convert.ToChar(metin[i].ToString());
                    int sifrele = (int)hane * anahtar;
                    sifrelimetin += sifrele.ToString() + " ";
                    progressBar1.Value = i;
                    label3.Text = i.ToString() + "/" + (metin.Length - 1).ToString();
                }
                Main main = Application.OpenForms["Main"] as Main;
                RichTextBox rich = main.Controls["richTextBox1"] as RichTextBox;
                rich.Text = sifrelimetin;
                MessageBox.Show("Metin şifrelendi.", "Not Defteri", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception hata)
            {
                MessageBox.Show("Metin şifrelemiyor." + Environment.NewLine + "(" + hata.Message.ToString() + ")", "Not Defteri", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox1.Text);
        }
    }
}
