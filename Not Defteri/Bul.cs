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
    public partial class Bul : Form
    {
        public Bul()
        {
            InitializeComponent();
        }
        string last = "";
        int index = 0;
        private void bul_Load(object sender, EventArgs e)
        {
            radioButton2.Checked = true;
            textBox1.Text = ayarlar.Default.aranan;
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ayarlar.Default.aranan = textBox1.Text;
            Main ana = Application.OpenForms["Main"] as Main;
            RichTextBox metin = ana.Controls["richTextBox1"] as RichTextBox;
            last = textBox1.Text;
            metin.Find(textBox1.Text, index, metin.TextLength, RichTextBoxFinds.None);
            this.Text ="Bul (" +index.ToString()+")";
            if (radioButton1.Checked == true) index = metin.Text.LastIndexOf(textBox1.Text, index)+1;
            if (radioButton2.Checked == true) index = metin.Text.IndexOf(textBox1.Text, index) + 1;
            if (index == 0) MessageBox.Show("Aranan : " + textBox1.Text + " bulunamadı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ayarlar.Default.aranan_index = metin.Text.IndexOf(textBox1.Text, index) + 1;
            metin.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
        
        }
    }
}
