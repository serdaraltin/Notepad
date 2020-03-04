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
    public partial class Degistir : Form
    {
        public Degistir()
        {
            InitializeComponent();
        }
        string last = "";
        int index = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            ayarlar.Default.aranan = textBox1.Text;
            Main ana = Application.OpenForms["Main"] as Main;
            RichTextBox metin = ana.Controls["richTextBox1"] as RichTextBox;
          
            metin.Find(textBox1.Text, index, metin.TextLength, RichTextBoxFinds.None);
            index = metin.Text.IndexOf(textBox1.Text, index) + 1;
            if (index == 0) MessageBox.Show("Aranan : " + textBox1.Text + " bulunamadı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ayarlar.Default.aranan_index = metin.Text.IndexOf(textBox1.Text, index) + 1;
            
            metin.Focus();
        }

        private void Degistir_Load(object sender, EventArgs e)
        {
            textBox1.Text = ayarlar.Default.aranan;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ayarlar.Default.aranan = textBox1.Text;
            Main ana = Application.OpenForms["Main"] as Main;
            RichTextBox metin = ana.Controls["richTextBox1"] as RichTextBox;
           
            metin.Find(textBox1.Text,metin.Text.LastIndexOf(textBox1.Text, index) + 1, metin.TextLength, RichTextBoxFinds.None);
            metin.SelectedText = textBox2.Text;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Main ana = Application.OpenForms["Main"] as Main;
            RichTextBox metin = ana.Controls["richTextBox1"] as RichTextBox;
            while (index < metin.Text.LastIndexOf(textBox1.Text))
            {
                metin.Find(textBox1.Text, index, metin.TextLength, RichTextBoxFinds.None);
                metin.SelectedText = textBox2.Text;
                index = metin.Text.IndexOf(textBox1.Text, index) + 1;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
