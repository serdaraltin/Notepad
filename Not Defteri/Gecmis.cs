using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
namespace Not_Defteri
{
    public partial class Gecmis : Form
    {
        public Gecmis()
        {
            InitializeComponent();
        }
        OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\data.db");
        private void gecmis_listele()
        {
            listView1.Items.Clear();
            baglan.Open();
            OleDbCommand komut = new OleDbCommand("Select *From file", baglan);
            OleDbDataReader oku = komut.ExecuteReader();
            int a = 0;
            while (oku.Read())
            {
                a++;
                ListViewItem itemler = new ListViewItem(a.ToString());
                itemler.SubItems.Add(oku["dosya"].ToString());
                itemler.SubItems.Add(oku["tarih"].ToString());
                listView1.Items.Add(itemler);
            }
            baglan.Close();
        }
        private void Gecmis_Load(object sender, EventArgs e)
        {
            gecmis_listele();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.Text = listView1.SelectedItems[0].SubItems[1].Text;
            }
            catch {}
        }

        private void Gecmis_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
        }

        private void açToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ayarlar.Default.son_dosya = listView1.SelectedItems[0].SubItems[1].Text;
                ayarlar.Default.Save();
                Main ana = Application.OpenForms["Main"] as Main;
                RichTextBox rich = ana.Controls["richTextBox1"] as RichTextBox;
                rich.Clear();
                StreamReader oku = new StreamReader(ayarlar.Default.son_dosya);
                string metin = oku.ReadLine();
                string tüm = "";
                while (metin != null)
                {
                    rich.Text += metin + "\n";
                    metin = oku.ReadLine();
                }
                oku.Close();
                ana.Text = ayarlar.Default.son_dosya;
            }
            catch { }
           // this.Hide();
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            açToolStripMenuItem.PerformClick();
        }
    }
}
