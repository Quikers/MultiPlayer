using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MultiPlayer {
    /// <summary>
    /// This class makes new labels everytime you use it, and stacking them downwards.
    /// </summary>
    public class Toast {
        public Form1 form1;

        private Point _location;
        private List<Label> _lList = new List<Label>();
        private List<Timer> _tList = new List<Timer>();

        /// <summary>
        /// This is the Toast class constructor.
        /// </summary>
        /// <param name="_form1">The form control where Toast will add itself to.</param>
        /// <param name="location">The exact location where the toasts will appear.</param>
        public Toast(Form1 form1, Point location) {
            this.form1 = form1;
            //this._location = new Point(location.X, location.Y + 24);
            this._location = new Point(0,50);
        }

        /// <summary>
        /// Update the Toast location.
        /// </summary>
        /// <param name="location">Location to change to.</param>
        public void UpdateLocation(Point location) {
            this._location = new Point(location.X, location.Y + 24);
        }

        /// <summary>
        /// This shows a new toast under the last toast. Much like a MessageBox without dialog.
        /// </summary>
        /// <param name="message">The message to show.</param>
        /// <param name="interval">The interval in miliseconds to delete the message.</param>
        public void Show(string message, int interval = 5000) {
            int locY = 0;
            for(int i = 0; i < _lList.Count; i++) {
                locY += _lList[i].Size.Height;
            }

            Point location = new Point(_location.X, _location.Y + locY);

            Label l = new Label {
                Tag = _lList.Count,
                Location = location,
                AutoSize = true,
                Font = new Font("Kootenay", 10),
                BackColor = Color.Black,
                ForeColor = Color.LimeGreen,
                Text = message
            };

            l.BringToFront();

            Timer t = new Timer {
                Tag = _tList.Count,
                Interval = interval,
            };

            t.Tick += (object s, EventArgs a) => t_tick(s, a, t, l);
            t.Start();
            
            _lList.Add(l);
            _tList.Add(t);
            form1.Controls.Add(l);
        }

        private void t_tick(object sender, EventArgs e, Timer t, Label l) {
            form1.Controls.Remove(l);
            _lList.Remove(l);
            _tList.Remove(t);

            for (int i = 0; i < _lList.Count; i++) {
                _lList[i].Location = new Point(_location.X, _location.Y + (i * _lList[i].Size.Height));
            }
        }
    }
}
