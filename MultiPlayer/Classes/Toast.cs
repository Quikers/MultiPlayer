using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MultiPlayer {
    public class Toast {
        public Form1 form1;

        private Point Location;
        private List<Label> lList = new List<Label>();
        private List<Timer> tList = new List<Timer>();

        public Toast(Form1 _form1, Point _location) {
            form1 = _form1;
            Location = new Point(_location.X, _location.Y + 24);
        }

        public void updateLocation(Point _location) {
            Location = new Point(_location.X, _location.Y + 24);
        }

        public void Show(string message, int interval = 5000) {
            int locY = 0;
            for(int i = 0; i < lList.Count; i++) {
                locY += lList[i].Size.Height;
            }

            Point location = new Point(Location.X, Location.Y + locY);

            Label l = new Label();
            l.Tag = lList.Count;
            l.Location = location;
            l.AutoSize = true;
            l.Font = new Font("Kootenay", 10);
            l.BackColor = Color.Black;
            l.ForeColor = Color.LimeGreen;
            l.BringToFront();
            l.Text = message;

            Timer t = new Timer();
            t.Tag = tList.Count;
            t.Interval = interval;
            t.Tick += (object s, EventArgs a) => t_tick(s, a, t, l);
            t.Start();
            
            lList.Add(l);
            tList.Add(t);
            form1.Controls.Add(l);
        }

        private void t_tick(object sender, EventArgs e, Timer t, Label l) {
            form1.Controls.Remove(l);
            lList.Remove(l);
            tList.Remove(t);

            for (int i = 0; i < lList.Count; i++) {
                lList[i].Location = new Point(Location.X, Location.Y + (i * lList[i].Size.Height));
            }
        }
    }
}
