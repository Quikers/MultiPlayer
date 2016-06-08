using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiPlayer {
    /// <summary>
    /// Handles option saving, loading and converting.
    /// </summary>
    class Option {

        /// <summary>
        /// Handles option saving, loading and converting.
        /// </summary>
        /// <param name="fileName">The name (or full path + name) of the settings file.</param>
        public Option(string fileName, ref Hashtable options) {
            Hashtable settings = new Hashtable();

            try { // Read options from file
                string[] opts;
                opts = File.ReadAllLines("Settings.ini");

                for (int i = 0; i < opts.Length; i++) {
                    string[] keyvalue = opts[i].Split('=');
                    settings.Add(keyvalue[0], keyvalue[1]);
                }
            } catch (Exception ex) {
                if (ex.ToString().IndexOf("Could not find") > -1) {
                    File.Create("Settings.ini");
                } else if (ex.ToString().IndexOf("bounds") > -1) { } else
                    MessageBox.Show(ex.ToString());
            }

            options = settings;
        }

        public override string ToString() {
            string s = "";
            foreach (KeyValuePair<string, string> option in options) {
                s += option.Key + "=" + option.Value + "\n";
            }

            return s;
        }
    }
}
