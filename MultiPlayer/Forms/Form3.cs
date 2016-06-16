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
            form1.toast.Show("Connecting...");

            string[] ipPort = textBox1.Text.Split(':');
            IPAddress ip;

            string s = null;

            if (IPAddress.TryParse(ipPort[0], out ip)) {
                try {
                    int port = Convert.ToInt16(ipPort[1]);

                    form1.connect(ip, port);
                    this.Close();
                } catch(Exception ex) {
                    if (ex.ToString().IndexOf("actively refused") > -1) form1.toast.Show("Could not connect to " + ip.ToString());
                    else form1.toast.Show(ex.Message);
                }
            } else form1.toast.Show("The IP address you entered is not a valid IP address.");
        }
    }
}
