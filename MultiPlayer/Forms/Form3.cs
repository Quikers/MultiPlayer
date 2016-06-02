using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net;
using System.Net.Sockets;
using System.IO;

namespace MultiPlayer {
    public partial class Form3 : Form {
        
        Form1 form1;

        public Form3 (Form1 _form1) {
            InitializeComponent();
            Visible = true;

            form1 = _form1;
        }

        private void form3_closing (object sender, EventArgs e) { // Start server
            form1.form3 = null;
        }

        private void button2_Click (object sender, EventArgs e) {
            this.Close();
        }

        private void button1_Click (object sender, EventArgs e) {
            IPAddress ip;
            if (IPAddress.TryParse(textBox1.Text, out ip)) {
                try {
                    form1.connect(ip);
                    this.Close();
                } catch(Exception ex) {
                    form1.toast.Show(ex.Message);
                    form1.toast.Show("Could not connect to " + ip.ToString());
                }
            } else form1.toast.Show("The IP address you entered is not a valid IP address.");
        }
    }
}
