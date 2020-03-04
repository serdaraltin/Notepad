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
    public partial class un_kripto : Form
    {
        public un_kripto()
        {
            InitializeComponent();
        }
        string metin = "";
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                progressBar1.Value = 0;
                string cozumlenmismetin = "";
                int anahtar = 0;
                int level = 0;
                for (int a = 0; a <= textBox1.Text.Length - 1; a++)
                {
                    char karekter = Convert.ToChar(textBox1.Text[a].ToString());
                    anahtar += (int)karekter;
                }
                anahtar = anahtar / textBox1.Text.Length;
                char carp = Convert.ToChar(textBox1.Text[0].ToString());
                anahtar = anahtar * (int)carp;
                if (textBox1.Text != "")
                {
                    char[] aralık = { ' ' };
                    string[] haneler = metin.Split(aralık);
                    foreach (string hane in haneler)
                    {
                        level+=1;
                    }
                    progressBar1.Maximum=level;
                    label3.Text = "0/" + level.ToString();
                    foreach (string hane in haneler)
                    {
                        if (hane != "")
                        {
                            for (int ascii = 0; ascii <= 255; ascii++)
                            {
                                int hn = Convert.ToInt32(hane);
                                char sıra = Convert.ToChar(ascii);
                                int denk = anahtar * (int)sıra;
                             
                                if (denk == hn)
                                {
                                    cozumlenmismetin += sıra;
                                }
                            }
                        }
                        progressBar1.Value+=1;
                        label3.Text = progressBar1.Value.ToString()+"/" + level.ToString();
                    }
                }
                Main main = Application.OpenForms["Main"] as Main;
                RichTextBox rich = main.Controls["richTextBox1"] as RichTextBox;
                rich.Text = cozumlenmismetin;
                MessageBox.Show("Metin çözümlendi.", "Not Defteri", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception hata)
            {
                MessageBox.Show("İşlem yapılamıyor şifreleme algoritmasında veya metinde sorun var."+Environment.NewLine+"("+hata.Message.ToString()+")", "Not Defteri", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void un_kripto_Load(object sender, EventArgs e)
        {
            Main main = Application.OpenForms["Main"] as Main;
            metin = main.richTextBox1.Text;
            label3.Text = "0/0";
        }
    }
}
