using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MultiPlayer {
    public partial class Form2 : Form {
        public Form1 form1;

        public Form2(Form1 _form1) {
            InitializeComponent();
            this.Visible = true;

            form1 = _form1;
            label2.Text = "Current username: " + form1.username;
        }

        private void form2_closing(object sender, FormClosingEventArgs e) {
            form1.form2 = null;
        }

        private void button1_Click(object sender, EventArgs e) {
            if (textBox1.Text.Length > 2) {
                try {
                    string[] s = { textBox1.Text };
                    File.WriteAllLines("Settings.ini", s);
                } catch (Exception ex) {
                    Console.WriteLine("Exception: " + ex.Message);
                }

                form1.username = textBox1.Text;
                label2.Text = "Current username: " + form1.username;
            } else MessageBox.Show("Your username has to at least be 3 characters long.", "Invalid username");
        }

        private void button2_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
