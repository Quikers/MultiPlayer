using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MultiPlayer {
    public class Toast {
        public Form1 Form1;

        private Point _location;
        private List<Label> _lList = new List<Label>();
        private List<Timer> _tList = new List<Timer>();

        public Toast(Form1 _form1, Point location) {
            Form1 = _form1;
            this._location = new Point(location.X, location.Y + 24);
        }

        public void UpdateLocation(Point location) {
            this._location = new Point(location.X, location.Y + 24);
        }

        public void Show(string message, int interval = 5000) {
            int locY = 0;
            for(int i = 0; i < _lList.Count; i++) {
                locY += _lList[i].Size.Height;
            }

            Point location = new Point(_location.X, _location.Y + locY);

            Label l = new Label();
            l.Tag = _lList.Count;
            l.Location = location;
            l.AutoSize = true;
            l.Font = new Font("Kootenay", 10);
            l.BackColor = Color.Black;
            l.ForeColor = Color.LimeGreen;
            l.BringToFront();
            l.Text = message;

            Timer t = new Timer();
            t.Tag = _tList.Count;
            t.Interval = interval;
            t.Tick += (object s, EventArgs a) => t_tick(s, a, t, l);
            t.Start();
            
            _lList.Add(l);
            _tList.Add(t);
            Form1.Controls.Add(l);
        }

        private void t_tick(object sender, EventArgs e, Timer t, Label l) {
            Form1.Controls.Remove(l);
            _lList.Remove(l);
            _tList.Remove(t);

            for (int i = 0; i < _lList.Count; i++) {
                _lList[i].Location = new Point(_location.X, _location.Y + (i * _lList[i].Size.Height));
            }
        }
    }
}
