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
    public partial class Secenekler : Form
    {
        public Secenekler()
        {
            InitializeComponent();
        }
        Color metink;
        Color metinr;
        Color menü;
        Font metinf;
        private void button1_Click(object sender, EventArgs e)
        {
            ColorDialog renk = new ColorDialog();
            renk.Color = pictureBox1.BackColor;
            if (renk.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.BackColor = renk.Color;
                metink = renk.Color;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ColorDialog renk = new ColorDialog();
            renk.Color = pictureBox2.BackColor;
            if (renk.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.BackColor = renk.Color;
                metinr = renk.Color;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ColorDialog renk = new ColorDialog();
            renk.Color = pictureBox3.BackColor;
            if (renk.ShowDialog() == DialogResult.OK)
            {
                pictureBox3.BackColor = renk.Color;
                menü = renk.Color;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FontDialog font = new FontDialog();
            font.MaxSize = 18;
            font.MinSize = 5;
            font.Font = label7.Font;
            if (font.ShowDialog() == DialogResult.OK)
            {
                label7.Font = font.Font;
                metinf = font.Font;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ayarlar.Default.metin_kutusu = metink;
            ayarlar.Default.metin_rengi = metinr;
            ayarlar.Default.menü_rengi = menü;
            ayarlar.Default.metin_font = metinf;
            ayarlar.Default.veri_tabanı_kaydı = checkBox1.Checked;
            ayarlar.Default.türkçe_karekter = checkBox2.Checked;
            ayarlar.Default.geçmiş_kaydı = checkBox3.Checked;
            ayarlar.Default.son_belge = checkBox5.Checked;
            ayarlar.Default.maximum_uzunluk = Convert.ToInt32(numericUpDown1.Value);
            ayarlar.Default.Save();
            
            Main ana = Application.OpenForms["Main"] as Main;
            RichTextBox rich = ana.Controls["richTextBox1"] as RichTextBox;
            MenuStrip menu = ana.Controls["MenuStrip1"] as MenuStrip;
            rich.BackColor = metink;
            rich.ForeColor = metinr;
            menu.BackColor = menü;
            rich.Font = metinf;
            rich.MaxLength = Convert.ToInt32(numericUpDown1.Value);
           // menu.ForeColor = metinr;
            MessageBox.Show("Ayarlar kaydedildi.", "Kaydetme", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Seçenekler_Load(object sender, EventArgs e)
        {
            label7.Font = ayarlar.Default.metin_font;
            numericUpDown1.Value = ayarlar.Default.maximum_uzunluk;
            checkBox1.Checked = ayarlar.Default.veri_tabanı_kaydı;
            checkBox2.Checked = ayarlar.Default.türkçe_karekter;
            checkBox3.Checked = ayarlar.Default.geçmiş_kaydı;
            pictureBox1.BackColor = ayarlar.Default.metin_kutusu;
            pictureBox2.BackColor = ayarlar.Default.metin_rengi;
            pictureBox3.BackColor = ayarlar.Default.menü_rengi;
            checkBox5.Checked = ayarlar.Default.son_belge;
            metink = ayarlar.Default.metin_kutusu;
            metinr = ayarlar.Default.metin_rengi;
            menü = ayarlar.Default.menü_rengi;
            metinf = ayarlar.Default.metin_font;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            label7.Font = new Font("Segoe UI", 8, FontStyle.Regular);
            numericUpDown1.Value = 2147483647;
            checkBox1.Checked = false;
            checkBox2.Checked = true;
            checkBox3.Checked = true;
            pictureBox1.BackColor = Color.White;
            pictureBox2.BackColor = Color.Black;
            pictureBox3.BackColor = Color.White;
            checkBox5.Checked = false;

            ayarlar.Default.metin_kutusu = Color.White;
            ayarlar.Default.metin_rengi = Color.Black;
            ayarlar.Default.menü_rengi = Color.White;
            ayarlar.Default.metin_font = new Font("Segoe UI", 8,FontStyle.Regular);
            ayarlar.Default.veri_tabanı_kaydı = false;
            ayarlar.Default.türkçe_karekter = true;
            ayarlar.Default.geçmiş_kaydı = true;
            ayarlar.Default.son_belge = false;
            ayarlar.Default.maximum_uzunluk = 2147483647;
            ayarlar.Default.Save();
           
            Main ana = Application.OpenForms["Main"] as Main;
            RichTextBox rich = ana.Controls["richTextBox1"] as RichTextBox;
            MenuStrip menü = ana.Controls["MenuStrip1"] as MenuStrip;
            rich.BackColor = Color.White;
            rich.ForeColor = Color.Black;
            menü.BackColor = Color.White;
            rich.Font = new Font("Segoe UI", 8, FontStyle.Regular);
            rich.MaxLength = 2147483647;
            menü.ForeColor = Color.Black;
            MessageBox.Show("Ayarlar sıfırlandı.", "Sıfırlama", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}
