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
    public partial class Git : Form
    {
        public Git()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Main ana = Application.OpenForms["Main"] as Main;
            RichTextBox rich = ana.Controls["richTextBox1"] as RichTextBox;
            try
            {
                int line = Convert.ToInt32(textBox1.Text)-1;
                int index = rich.GetFirstCharIndexFromLine(line);
                rich.Select(index, 0);
                ana.Focus();
            }
            catch
            {
                MessageBox.Show("Sadece sayı biçiminde giriş yapılabilr!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
