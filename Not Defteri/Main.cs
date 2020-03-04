using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;
using System.Drawing.Printing;
namespace Not_Defteri
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\data.db");
         string acikdosya="";
         bool veritabanıkaydı;
         bool tükçekarekter;
         bool geçmişkaydı;
         string acılan_dosya;
        
        /* private bool resim(this RichTextBox txt, string file)
         {
             Bitmap bmp = new Bitmap(file);
             Clipboard.SetDataObject(bmp);
             DataFormats.Format format = DataFormats.GetFormat(DataFormats.Bitmap);
             if (richTextBox1.CanPaste(format))
             {
                 richTextBox1.Paste(format);
                 return true;
             }
             else
             {
                 return false;
             }

         }*/
         private void gecmis_listele()
         {
             öncekiDosyalarToolStripMenuItem.DropDownItems.Clear();
             baglan.Open();
             OleDbCommand komut = new OleDbCommand("Select *From file", baglan);
             OleDbDataReader oku = komut.ExecuteReader();
             int a=0;
             while (oku.Read())
             {
                 a++;
                 if (a < 11)
                 {
                     öncekiDosyalarToolStripMenuItem.DropDownItems.Add(oku["dosya"].ToString());
                     
                 }
                 }
             baglan.Close();

         }
      
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            toolStripStatusLabel6.ForeColor = Color.Red;
            toolStripProgressBar1.Value = 0;
            toolStripStatusLabel7.Text = "0/0";

            toolStripStatusLabel1.Text = "Boyut : " + richTextBox1.TextLength.ToString();
            toolStripStatusLabel3.Text = "Satır Sayısı : " + richTextBox1.Lines.Length.ToString();
            toolStripStatusLabel2.Text = "Karekter : " + richTextBox1.SelectionStart.ToString();         

        }

        private void durumÇubuğuToolStripMenuItem_Click(object sender, EventArgs e)
        {
     
        }

        private void bulToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bul nul = new Bul();
            nul.Show();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ac = new OpenFileDialog();
            ac.Title = "Aç";
            ac.Filter = "Metin Belgeleri|*.txt|Tüm Dosyalar|*.*";
            if (ac.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Clear();
                acılan_dosya = ac.FileName;
                acikdosya = ac.FileName;
                StreamReader oku = new StreamReader(ac.FileName,Encoding.Default);
                string metin = oku.ReadToEnd();
               /* while (metin != null)
                {
                    richTextBox1.Text += metin+"\n";
                    metin = oku.ReadLine();
                }*/
                richTextBox1.Text = metin;
                oku.Close();

                this.Text = ac.FileName + " - Not Defteri";
            }
            if (ayarlar.Default.veri_tabanı_kaydı == true)
            {

                baglan.Open();
                OleDbCommand komut = new OleDbCommand("insert into saves (isim,veri,tarih) values(@isim,@veri,@tarih)", baglan);
                komut.Parameters.AddWithValue("@isim", ac.SafeFileName);
                komut.Parameters.AddWithValue("@veri", richTextBox1.Text);
                komut.Parameters.AddWithValue("@tarih", DateTime.Now.ToString());
                komut.ExecuteNonQuery();
                baglan.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripProgressBar1.Maximum = 100;
         //   this.Size=new Size(ayarlar.Default.genislik, ayarlar.Default.yukseklik);
            if (File.Exists(Application.StartupPath + "\\data.db")==false)
            {
                MessageBox.Show("Veri Tabanı bulunamadı!\nBirçok hata oluşabilir!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            solToolStripMenuItem.Checked = true;
            string g = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string[] kelimeler = Environment.GetCommandLineArgs();
            foreach (string dosya_yolu in kelimeler)
            {
                if (!dosya_yolu.StartsWith(Application.StartupPath))
                {
                    this.Text = dosya_yolu+" - Not Defteri";
                    acılan_dosya = dosya_yolu;
                    acikdosya = dosya_yolu;
                }
                try
                {
                    StreamReader oku = new StreamReader(acılan_dosya, Encoding.UTF8);
                    string veri = oku.ReadToEnd();
                    richTextBox1.Text = veri;
                    oku.Close();
                }
                catch (Exception hata)
                {
                    //MessageBox.Show(hata.Message.ToString(), "Dosya Açılamadı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            if (ayarlar.Default.son_dosya != "" && ayarlar.Default.son_belge == true && acikdosya=="")
            {
                DialogResult soru = MessageBox.Show("Son kullandığınız belge açılsın mı?", "Son Belge", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (soru == DialogResult.Yes)
                {
                    try
                    {
                        StreamReader oku = new StreamReader(ayarlar.Default.son_dosya, Encoding.Default);
                        string metin = oku.ReadToEnd();
                      /*  while (metin != null)
                        {
                            richTextBox1.Text += metin + "\n";
                            metin = oku.ReadLine();
                        }*/
                        richTextBox1.Text = metin;
                        oku.Close();
                        this.Text = ayarlar.Default.son_dosya + " - Not Defteri";
                        acikdosya = ayarlar.Default.son_dosya;
                    }
                    catch
                    {
                        MessageBox.Show("Dosya silinmiş veya taşınmış.", "Dosya Açılamadı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
         
            //gecmis_listele();
            richTextBox1.BackColor = ayarlar.Default.metin_kutusu;
            richTextBox1.ForeColor = ayarlar.Default.metin_rengi;
            menuStrip1.BackColor = ayarlar.Default.menü_rengi;
            richTextBox1.Font = ayarlar.Default.metin_font;
            richTextBox1.MaxLength = ayarlar.Default.maximum_uzunluk;
            veritabanıkaydı = ayarlar.Default.veri_tabanı_kaydı;
            tükçekarekter = ayarlar.Default.türkçe_karekter;
            geçmişkaydı = ayarlar.Default.geçmiş_kaydı;
            //menuStrip1.ForeColor = ayarlar.Default.metin_rengi;
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
             richTextBox1.Paste();
            
        }
        
        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void tarihSaatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Focus();
            SendKeys.Send(DateTime.Now.ToString());
        }

        private void değiştirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Degistir degis = new Degistir();
            degis.Show();
        }

        private void yazıTipiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog richfont = new FontDialog();
            richfont.MaxSize = 18;
            richfont.MinSize = 5;
            richfont.Font = richTextBox1.Font;
            if (richfont.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Font = richfont.Font;
                ayarlar.Default.metin_font = richfont.Font;
            }
        }

        private void sözcükKaydırToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sözcükKaydırToolStripMenuItem.Checked == false)
            {
                sözcükKaydırToolStripMenuItem.Checked = true;
                richTextBox1.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
            }
            else
            {
                sözcükKaydırToolStripMenuItem.Checked = false;
                richTextBox1.ScrollBars = RichTextBoxScrollBars.Both;
            }
        }

        private void richTextBox1_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void richTextBox1_SelectionChanged(object sender, EventArgs e)
        {
            toolStripStatusLabel5.Text = "Seçili : " + richTextBox1.SelectedText.Length.ToString();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog kaydet = new SaveFileDialog();
            kaydet.Title = "Kaydet";
            kaydet.Filter = "Metin Dosyaları|*.txt|Notas|*.ntx|Tüm Dosyalar|*.*";
            if (acikdosya == "")
            {
            
            if (kaydet.ShowDialog() == DialogResult.OK)
            {
                
                    File.CreateText(kaydet.FileName).Close();
                    StreamWriter yaz = new StreamWriter(kaydet.FileName);
                    toolStripProgressBar1.Value = 0;
                    //toolStripProgressBar1.Maximum = Convert.ToInt32(richTextBox1.Lines.LongLength);
                 /*   for (int line = 0; line < richTextBox1.Lines.LongLength; line++)
                    {
                        toolStripStatusLabel7.Text = toolStripProgressBar1.Value.ToString() + "/" + toolStripProgressBar1.Maximum.ToString();
                        toolStripProgressBar1.Value++;
                        string metin = richTextBox1.Lines[line].ToString();
                        yaz.WriteLine(metin);                      
                    }*/
                  
                    yaz.Write(richTextBox1.Text);
                  // toolStripProgressBar1.Value = Convert.ToInt32(richTextBox1.Lines.LongLength);
                    toolStripStatusLabel7.Text = Convert.ToInt32(richTextBox1.Lines.LongLength).ToString() + "/" + Convert.ToInt32(richTextBox1.Lines.LongLength).ToString();
                    yaz.Close();
                    if (geçmişkaydı == true)
                    {
                        baglan.Open();
                        OleDbCommand komut = new OleDbCommand("insert into file (dosya,tarih) values(@dosya,@tarih)", baglan);
                        komut.Parameters.AddWithValue("@dosya", kaydet.FileName);
                        komut.Parameters.AddWithValue("@tarih", DateTime.Now.ToString());
                        komut.ExecuteNonQuery();
                        baglan.Close();
                    }
                    if (ayarlar.Default.veri_tabanı_kaydı == true)
                    {

                        baglan.Open();
                        OleDbCommand komut = new OleDbCommand("insert into saves (isim,veri,tarih) values(@isim,@veri,@tarih)", baglan);
                        komut.Parameters.AddWithValue("@isim", kaydet.FileName);
                        komut.Parameters.AddWithValue("@veri", richTextBox1.Text);
                        komut.Parameters.AddWithValue("@tarih", DateTime.Now.ToString());
                        komut.ExecuteNonQuery();
                        baglan.Close();
                    }
                    acikdosya = kaydet.FileName;
                    this.Text = kaydet.FileName + " - Not Defteri";
                    toolStripStatusLabel6.ForeColor = Color.Green;
                    MessageBox.Show(kaydet.FileName + "\n dosyası kaydedildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
                else
                {
                    if (geçmişkaydı == true)
                    {
                        baglan.Open();
                        OleDbCommand komut = new OleDbCommand("insert into file (dosya,tarih) values(@dosya,@tarih)", baglan);
                        komut.Parameters.AddWithValue("@dosya", acikdosya);
                        komut.Parameters.AddWithValue("@tarih", DateTime.Now.ToString());
                        komut.ExecuteNonQuery();
                        baglan.Close();
                    }
                    if (ayarlar.Default.veri_tabanı_kaydı == true)
                    {

                        baglan.Open();
                        OleDbCommand komut = new OleDbCommand("insert into saves (isim,veri,tarih) values(@isim,@veri,@tarih)", baglan);
                        komut.Parameters.AddWithValue("@isim", acılan_dosya);
                        komut.Parameters.AddWithValue("@veri", richTextBox1.Text);
                        komut.Parameters.AddWithValue("@tarih", DateTime.Now.ToString());
                        komut.ExecuteNonQuery();
                        baglan.Close();
                    }
                    try
                    {
                        StreamWriter yaz = new StreamWriter(acılan_dosya);
                        toolStripProgressBar1.Value = 0;
                        /* for (int line = 0; line < richTextBox1.Lines.LongLength; line++)
                         {
                             toolStripStatusLabel7.Text = toolStripProgressBar1.Value.ToString() + "/" + toolStripProgressBar1.Maximum.ToString();
                             toolStripProgressBar1.Value++;
                             string metin = richTextBox1.Lines[line].ToString();
                             yaz.WriteLine(metin);
                         }*/

                        yaz.Write(richTextBox1.Text);
                        yaz.Close();
                        toolStripStatusLabel6.ForeColor = Color.Green;
                        //toolStripProgressBar1.Maximum = Convert.ToInt32(richTextBox1.Lines.LongLength);
                        toolStripStatusLabel7.Text = Convert.ToInt32(richTextBox1.Lines.LongLength).ToString() + "/" + Convert.ToInt32(richTextBox1.Lines.LongLength).ToString();
                        toolStripProgressBar1.Value = 100;
                      
                    }
                    catch {
                        toolStripStatusLabel4.Text = "Dosya başka bir program tarafından kullanımda !";
                    }
                    
                   
                   
                  
                }
            
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog kaydet = new SaveFileDialog();
            kaydet.Title = "Kaydet";
            kaydet.Filter = "Tüm Dosyalar|*.*"; if (kaydet.ShowDialog() == DialogResult.OK)
            {
                File.CreateText(kaydet.FileName).Close();
                StreamWriter yaz = new StreamWriter(kaydet.FileName);
                toolStripProgressBar1.Value = 0;
                //toolStripProgressBar1.Maximum = Convert.ToInt32(richTextBox1.Lines.LongLength);
             /*   for (int line = 0; line < richTextBox1.Lines.LongLength; line++)
                {
                    string metin = richTextBox1.Lines[line].ToString();
                    yaz.WriteLine(metin);
                }*/
               
                yaz.Write(richTextBox1.Text);
                toolStripStatusLabel7.Text = Convert.ToInt32(richTextBox1.Lines.LongLength).ToString() + "/" + Convert.ToInt32(richTextBox1.Lines.LongLength).ToString();
                toolStripProgressBar1.Value = 100;
                yaz.Close();
                if (geçmişkaydı == true)
                {
                    baglan.Open();
                    OleDbCommand komut = new OleDbCommand("insert into file (dosya,tarih) values(@dosya,@tarih)", baglan);
                    komut.Parameters.AddWithValue("ædosya", kaydet.FileName);
                    komut.Parameters.AddWithValue("@tarih", DateTime.Now.ToString());
                    komut.ExecuteNonQuery();
                    baglan.Close();
                   // gecmis_listele();
                }
                MessageBox.Show(kaydet.FileName + "\n dosyası kaydedildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text != "" && acikdosya == "")
            {
                DialogResult sor = MessageBox.Show("Yapılan değişiklikleri kaydetmet istyor musunuz?", "Not Defteri", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (sor == DialogResult.Yes)
                {
                    saveToolStripMenuItem.PerformClick();
                }
                if (sor == DialogResult.No)
                {
                    ayarlar.Default.yukseklik = this.Height;
                    ayarlar.Default.genislik = this.Width;
                    ayarlar.Default.Save();
                    Environment.Exit(0);
                }
                if (sor == DialogResult.Cancel)
                {
                  
                }

            }
            else
            {
                ayarlar.Default.yukseklik = this.Height;
                ayarlar.Default.genislik = this.Width;
                ayarlar.Default.Save();
            }

        }

        private void gitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Git git = new Git();
            git.Show();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Secenekler secenek = new Secenekler();
            secenek.ShowDialog();
        }

        private void öncekiDosyalarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Gecmis gecmis = new Gecmis();
            gecmis.ShowDialog();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void sonrakiniBulToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string aranan = ayarlar.Default.aranan;
            int index=ayarlar.Default.aranan_index;
            richTextBox1.Find(aranan, index, richTextBox1.TextLength, RichTextBoxFinds.None);
            index = richTextBox1.Text.IndexOf(aranan, index) + 1;
            richTextBox1.Text.IndexOf(aranan,index);
            ayarlar.Default.aranan_index = index;
        }

        private void soloDayaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
            richTextBox1.DeselectAll();
            soloDayaToolStripMenuItem.Checked = true;
            sAğaHizalaToolStripMenuItem.Checked = false;
            solToolStripMenuItem.Checked = false;
        }

        private void solToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
            richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
            richTextBox1.DeselectAll();
            soloDayaToolStripMenuItem.Checked = false;
            sAğaHizalaToolStripMenuItem.Checked = false;
            solToolStripMenuItem.Checked = true;
        }

        private void sAğaHizalaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
            richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
            richTextBox1.DeselectAll();
            soloDayaToolStripMenuItem.Checked = false;
            sAğaHizalaToolStripMenuItem.Checked = true;
            solToolStripMenuItem.Checked = false;

        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (richTextBox1.Text != ""&&acikdosya=="")
            {
                DialogResult sor = MessageBox.Show("Yapılan değişiklikleri kaydetmet istyor musunuz?","Not Defteri", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (sor == DialogResult.Yes)
                {
                    saveToolStripMenuItem.PerformClick();
                }
                if (sor == DialogResult.No)
                {
                    ayarlar.Default.yukseklik = this.Height;
                    ayarlar.Default.genislik = this.Width;
                    ayarlar.Default.Save();
                    Environment.Exit(0);
                }
                if (sor == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
                
            }
            else
            {
                ayarlar.Default.yukseklik = this.Height;
                ayarlar.Default.genislik = this.Width;
                ayarlar.Default.Save();
            }
        }

        private void öncekiniBulToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string aranan = ayarlar.Default.aranan;
            int index = ayarlar.Default.aranan_index;
            index = richTextBox1.Text.LastIndexOf(aranan, index) + 1;
            richTextBox1.Find(aranan, index, richTextBox1.TextLength, RichTextBoxFinds.None);
            richTextBox1.Text.LastIndexOf(aranan, index);
            ayarlar.Default.aranan_index = index;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Application.ExecutablePath.ToString());
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hakkında info = new Hakkında();

            info.ShowDialog();
        }

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintDialog yazdır = new PrintDialog();
            
            yazdır.ShowDialog();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (richTextBox1.Text != "") tümünüSeçToolStripMenuItem.Enabled = true;
            else tümünüSeçToolStripMenuItem.Enabled = false;
            if (richTextBox1.SelectedText != "")
            {
                kesToolStripMenuItem.Enabled = true;
                kopyalaToolStripMenuItem.Enabled = true;
                silToolStripMenuItem1.Enabled= true;
            }
            else
            {
                kesToolStripMenuItem.Enabled = false;
                kopyalaToolStripMenuItem.Enabled = false;
                  silToolStripMenuItem1.Enabled = false;
            }
            if (richTextBox1.CanUndo == true) geriALToolStripMenuItem.Enabled = true;
            else geriALToolStripMenuItem.Enabled = false;
            if (Clipboard.GetText() != ""|| Clipboard.GetImage()!=null)
            {
                yapıştırToolStripMenuItem.Enabled = true;
            }
            else
            {
                yapıştırToolStripMenuItem.Enabled = false;
              
            }
        }

        private void tümünüSeçToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void kesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void kopyalaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void yapıştırToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
         
        }
       
        private void silToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectedText = "";
        }

        private void geriALToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void karekterDüzeltmesiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text != "") selectAllToolStripMenuItem.Enabled = true;
            else selectAllToolStripMenuItem.Enabled = false;
            if (richTextBox1.SelectedText != "")
            {

                cutToolStripMenuItem.Enabled = true;
                silToolStripMenuItem.Enabled = true;
                copyToolStripMenuItem.Enabled = true;
            }
            else
            {

                cutToolStripMenuItem.Enabled = false;
                silToolStripMenuItem.Enabled = false;
                copyToolStripMenuItem.Enabled = false;
            }
            if (richTextBox1.CanUndo == true) undoToolStripMenuItem.Enabled = true;
            else undoToolStripMenuItem.Enabled = false;
            if (richTextBox1.CanRedo == true)redoToolStripMenuItem.Enabled = true;
            else redoToolStripMenuItem.Enabled = false;
            if (Clipboard.GetText() != "" || Clipboard.GetImage() != null)
            {
                pasteToolStripMenuItem.Enabled = true;
            }
            else
            {
                pasteToolStripMenuItem.Enabled = false;

            }
        }

        private void editToolStripMenuItem_MouseMove(object sender, MouseEventArgs e)
        {
            editToolStripMenuItem.PerformClick();
        }

        private void veriDönüştürücüToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Veri_Dönüştürücü veri = new Veri_Dönüştürücü();
            veri.ShowDialog();
        }

        private void defaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                richTextBox1.Clear();
                StreamReader oku = new StreamReader(acılan_dosya, Encoding.Default);
                string veri = oku.ReadToEnd();
                /*   while (veri != null)
                   {
                       richTextBox1.Text += veri + "\n";
                       veri = oku.ReadLine();
                   }
                   oku.Close();*/
                richTextBox1.Text = veri;
                oku.Close();
            }
            catch { }
        }

        private void utf8ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                richTextBox1.Clear();
                StreamReader oku = new StreamReader(acılan_dosya, Encoding.UTF7);
                string veri = oku.ReadToEnd();
                /*   while (veri != null)
                   {
                       richTextBox1.Text += veri + "\n";
                       veri = oku.ReadLine();
                   }
                   oku.Close();*/
                richTextBox1.Text = veri;
                oku.Close();
            }
            catch { }
        }

        private void uTF8ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                richTextBox1.Clear();
                StreamReader oku = new StreamReader(acılan_dosya, Encoding.UTF8);
                string veri = oku.ReadToEnd();
                /*   while (veri != null)
                   {
                       richTextBox1.Text += veri + "\n";
                       veri = oku.ReadLine();
                   }
                   oku.Close();*/
                richTextBox1.Text = veri;
                oku.Close();
            }
            catch { }
        }

        private void uTF32ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                richTextBox1.Clear();
                StreamReader oku = new StreamReader(acılan_dosya, Encoding.UTF32);
                string veri = oku.ReadToEnd();
                /*   while (veri != null)
                   {
                       richTextBox1.Text += veri + "\n";
                       veri = oku.ReadLine();
                   }
                   oku.Close();*/
                richTextBox1.Text = veri;
                oku.Close();
            }
            catch { }
        }

        private void unicodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                richTextBox1.Clear();
                StreamReader oku = new StreamReader(acılan_dosya, Encoding.Unicode);
                string veri = oku.ReadToEnd();
                /*   while (veri != null)
                   {
                       richTextBox1.Text += veri + "\n";
                       veri = oku.ReadLine();
                   }
                   oku.Close();*/
                richTextBox1.Text = veri;
                oku.Close();
            }
            catch { }
        }

        private void şifreleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text != "")
            {
                kripto sifrele = new kripto();
                sifrele.ShowDialog();
            }
            else
                MessageBox.Show("Metin içeriği boş.","Not Defteri",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
        }

        private void çözümleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text != "")
            {
                un_kripto cozumle = new un_kripto();
                cozumle.ShowDialog();
            }
            else
                MessageBox.Show("Metin içeriği boş.", "Not Defteri", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void richTextBox1_MouseClick(object sender, MouseEventArgs e)
        {
            toolStripStatusLabel2.Text = "Karekter : " + richTextBox1.SelectionStart.ToString();
           // toolStripStatusLabel8.Text = "Satır : " + richTextBox1.Lines.
        }
    }
}
