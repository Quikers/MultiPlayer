using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;

using System.Net;
using System.Net.Sockets;
using System.IO;

using MultiPlayerLib;

namespace MultiPlayer {
    public partial class Form1 : Form {
        /// <summary>
        /// MultiPlayer form window state.
        /// </summary>
        enum mpWindowState {
            Windowed,
            FullscreenWindowed
        }

        //Option settings = new Option("Settings.ini");
        Thread ctThread;

        TcpClient clientSocket;
        NetworkStream serverStream = default(NetworkStream);
        string readData = null;

        public Form2 form2;
        public Form3 form3;
        public Toast toast;
        public OpenFileDialog fileDiag;
        GlobalMouseHandler gmh;
        mpWindowState windowState = new mpWindowState();

        private Point savedLoc = new Point(0, 0);
        private Size savedSize = new Size(0, 0);
        private FormBorderStyle savedBorder = new FormBorderStyle();

        public string username = "";

        public Form1() {
            InitializeComponent();

            checkForUpdatesToolStripMenuItem.Visible = false;

            toast = new Toast(this, MediaController.Location);

            fileDiag = new OpenFileDialog();
            fileDiag.Title = "Open media file";
            fileDiag.FileName = "";
            fileDiag.Filter = "All Files (*.*)|*.*|.mp4 Files|*.mp4|.mkv Files|*.mkv";
            fileDiag.Multiselect = true;

            gmh = new GlobalMouseHandler(); // Instantiate new global MouseEventHandler
            gmh.MouseMoved += new MouseMovedEvent(Mouse_Moved); // Add Mouse_Move event
            gmh.LmbDoubleClick += new MouseMovedEvent(LMB_DoubleClick); // Add Mouse_Move event
            Application.AddMessageFilter(gmh);
        }

        /// <summary>
        /// Saves the given option to the Settings.ini file.
        /// </summary>
        /// <param name="name">The name of the option</param>
        /// <param name="value">The value of the option</param>
        private void saveOptions(Option options) {

        }

        /// <summary>
        /// Saves the given option to the Settings.ini file.
        /// </summary>
        /// <param name="name">The name of the option</param>
        /// <param name="value">The value of the option</param>
        private void loadOptions(string name, string value) {

        }

        /// <summary>
        /// EventHandler for mouse movement inside the form.
        /// </summary>
        private void Mouse_Moved() {
            Size mpSize = MediaController.Size;
            Point mPos = Cursor.Position;
            int mpY = 52;

            if (mPos.X > this.Location.X &&
                mPos.X < this.Location.X + mpSize.Width + 10 &&
                mPos.Y > this.Location.Y + mpSize.Height - 32 &&
                mPos.Y < this.Location.Y + mpY + mpSize.Height) {

                MediaController.ShowToolbar();

            } else
                MediaController.HideToolbar();

            if ( MediaController.Fullscreen == true) {
                if (menuStrip1.Visible == false) {
                    if (mPos.Y < menuStrip1.Size.Height) {
                        menuStrip1.Visible = true;
                        menuStrip1.Focus();
                    }
                } else if (mPos.Y > menuStrip1.Size.Height && menuStrip1.Focused == true)
                    MediaController.Focus();
            }
        }

        /// <summary>
        /// EventHandler for double left-mousebutton clicks.
        /// </summary>
        private void LMB_DoubleClick() {
            Point mPos = Cursor.Position;
            Size mpSize = MediaController.Size;
            int mpY = 52;

            if (mPos.X > this.Location.X &&
                mPos.X < this.Location.X + mpSize.Width &&
                mPos.Y > this.Location.Y + mpY &&
                mPos.Y < this.Location.Y + mpY + mpSize.Height) {

                if (windowState == mpWindowState.Windowed)
                    toggleFullscreen(mpWindowState.FullscreenWindowed);
                if (windowState == mpWindowState.FullscreenWindowed)
                    toggleFullscreen(mpWindowState.Windowed);
            }
                    
        }

        /// <summary>
        /// Toggles the fullscreen on or off depending on the defined fullscreen state.
        /// </summary>
        /// <param name="newState">Sets to specific WindowState</param>
        private void toggleFullscreen(mpWindowState newState) {
            if (newState == mpWindowState.FullscreenWindowed) {
                savedLoc = this.Location;
                savedSize = this.Size;
                savedBorder = this.FormBorderStyle;

                this.FormBorderStyle = FormBorderStyle.None;
                this.Location = new Point(0, -10);
                this.Size = Screen.GetBounds(this.Location).Size;

                menuStrip1.Visible = false;
                MediaController.Fullscreen = true;
            } else {
                this.FormBorderStyle = savedBorder;
                this.Location = savedLoc;
                this.Size = savedSize;

                menuStrip1.Visible = true;
                MediaController.Fullscreen = false;
            }
        }

        private void openMedia() {
            if (fileDiag.ShowDialog() == DialogResult.OK) {
                if (fileDiag.FileNames.Length > 0) {
                    for (int i = 0; i < fileDiag.FileNames.Length; i++) {
                        string filePath = "file:///" + fileDiag.FileNames[i].Replace("\\", "/").Replace(" ", "%20");
                        MediaController.AddMedia(fileDiag.FileNames[i], fileDiag.SafeFileNames[i], null);
                    }
                } else toast.Show("Please select a file.");
            }
        }

        private void closeClientSocket() {
            while (clientSocket != null && clientSocket.Connected == true) clientSocket.Close();
            clientSocket = null;
            serverStream = null;

            changeUsernameToolStripMenuItem.Enabled = true;
        }

        private void sendData(mpMessage s) {
            if (serverStream != null) {
                byte[] outStream = s.ToBytes();
                serverStream.Write(outStream, 0, outStream.Length);
                serverStream.Flush();
            }
        }

        private void sendData(string s) {
            if (serverStream != null) {
                byte[] outStream = Encoding.ASCII.GetBytes(s);
                serverStream.Write(outStream, 0, outStream.Length);
                serverStream.Flush();
            }
        }

        public void connect(IPAddress ip, int port) {
            clientSocket = new TcpClient();

            clientSocket.Connect(ip, port);
            if (clientSocket.Connected == true) {
                changeUsernameToolStripMenuItem.Enabled = false;

                serverStream = clientSocket.GetStream();

                mpMessage joined = new mpMessage(username, mpMessage.Type.cmd, "join;");
                sendData(joined);

                ctThread = new Thread(handleClient);
                ctThread.Start();
            }
        }

        private void handleClient () {
            while (clientSocket != null && clientSocket.Connected == true) {
                joinLobbyToolStripMenuItem.Text = "Disconnect";

                try {
                    int buffSize = 1024;
                    byte[] inStream = new byte[buffSize];
                    string returnString = "";

                    serverStream = clientSocket.GetStream();
                    serverStream.Read(inStream, 0, buffSize);

                    mpMessage data = new mpMessage(inStream);
                    if (data.type != mpMessage.Type.cmd) {
                        readData = data.message;
                        msg();
                    } else {
                        returnString = handleCommands(data);
                    }

                    if (returnString.Length > 0) {
                        readData = "" + returnString;
                        msg();
                    }
                } catch (Exception ex) {
                    if (clientSocket != null && clientSocket.Connected == true) {
                        closeClientSocket();

                        if (ex.ToString().IndexOf("forcibly") > -1) readData = "Disconnected from server: Server closed";
                        else if (ex.ToString().IndexOf("WSACancelBlockingCall") > -1) readData = "Disconnected from server: Disconnect";
                        else readData = "Disconnected from server: " + ex.ToString();
                        msg();
                    }

                    break;
                }
            }

            joinLobbyToolStripMenuItem.Text = "Join lobby";
        }

        private string handleCommands(mpMessage data) {
            string s = "";

            string[] msgArr = data.message.Split(';');
            if (data.type != mpMessage.Type.message) {
                switch (msgArr[0]) {
                    default: //  ===================== Default error message =====================
                        s = "(DEBUG) Server sent unrecognized command: " + data.message;
                        break;

                    case "inUse": // ===================== Username already taken =====================
                        s = "Server rejected connection: Username \"" + username + "\" already in use";
                        closeClientSocket();
                        break;

                    case "play": // ===================== Request media play =====================
                        string mediaPath = MediaController.MediaPlayer.mediaDescription.title;
                        string mediaName = (mediaPath != "" ? mediaPath.Substring(mediaPath.LastIndexOf("\\") + 1) : "");

                        mpMessage msg = new mpMessage();

                        if (mediaName != "") {                          // If a media file was selected
                            if (msgArr[2] != "") {                      // If media name exists
                                if (msgArr[1] != "") {                  // If media name exists
                                    if (msgArr[1] == mediaName) {       // If media names are the same
                                        if (data.from != username) {    // Ask user to play media
                                            DialogResult diagResult = MessageBox.Show("Play media", data.from + " wants to play " + mediaName + "\nClick YES to agree.", MessageBoxButtons.YesNo);

                                            // Send appropriate response
                                            if (diagResult == DialogResult.Yes) msg = new mpMessage(username, mpMessage.Type.cmd, "playOK;");
                                            else msg = new mpMessage(username, mpMessage.Type.cmd, "playDenied;");
                                        } else msg = new mpMessage(username, mpMessage.Type.cmd, "playOK;");
                                    }
                                } else {                                // Client selected wrong media
                                    msg = new mpMessage(username, mpMessage.Type.message, username + " did not select the correct media file");
                                }
                            } else {                                    // If play request was received from server
                                msg = new mpMessage();
                                MediaController.PlayPause();
                            }
                        } else {                                        // Client did not select media yet
                            msg = new mpMessage(username, mpMessage.Type.message, username + " did not select a media file");
                        }

                        if (msg != new mpMessage()) sendData(msg);
                        break;

                    case "pause": // ===================== Request media pause =====================
                        if (msgArr[1] != "") {
                            s = data.message;
                            MediaController.PlayPause();
                        } else {                                        // If pause request is being sent
                            mpMessage msg1 = new mpMessage(username, mpMessage.Type.cmd, "pause;");
                            sendData(msg1);
                        }
                        break;

                    case "stop": // ===================== Request media stop =====================
                        break;
                }
            } else {
                if (data.message.IndexOf(username) > -1) data.message = data.message.Replace(username, username + "(you)");
                s = data.message;
            }

            return s;
        }

        private void msg () {
            try {
                if (this.InvokeRequired)
                    this.Invoke(new MethodInvoker(msg));
                else
                    toast.Show(readData);
            } catch(Exception ex) { MessageBox.Show("Toast invoke failed: " + ex.ToString()); }
        }

        private void btn_playpause_Click(object sender, EventArgs e) {
            playToolStripMenuItem1.PerformClick();
        }

        private void btn_stop_Click(object sender, EventArgs e) {
            stopToolStripMenuItem.PerformClick();
        }

        private void btn_next_Click(object sender, EventArgs e) {
           nextToolStripMenuItem.PerformClick();
        }

        private void btn_prev_Click(object sender, EventArgs e) {
            previousToolStripMenuItem.PerformClick();
        }

        private void Form1_Layout(object sender, LayoutEventArgs e) {
            MediaController.SendToBack();
        }

        private void exitToolStripMenuItem_Click (object sender, EventArgs e) {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click (object sender, EventArgs e) {
            MessageBox.Show("This product was made by Luuk Diederik.\r\n\r\nPlease contact me at:\r\nFlipmil3@hotmail.com", "About");
        }

        private void checkForUpdatesToolStripMenuItem_Click (object sender, EventArgs e) {
            MessageBox.Show("Function not yet implemented.", "Check for updates");
        }

        private void startServerToolStripMenuItem_Click (object sender, EventArgs e) {
            Process.Start(AppDomain.CurrentDomain.BaseDirectory + "Server.exe");
        }

        private void joinLobbyToolStripMenuItem_Click (object sender, EventArgs e) {
            if (joinLobbyToolStripMenuItem.Text == "Join lobby") {
                if (form3 == null) form3 = new Form3(this);
                else {
                    form3.BringToFront();
                    form3.Focus();
                }
            } else {
                mpMessage disconnect = new mpMessage(username, mpMessage.Type.cmd, "disconnect;");
                sendData(disconnect);

                closeClientSocket();
                toast.Show("Disconnected from server: You left");
                joinLobbyToolStripMenuItem.Text = "Join lobby";
            }
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e) {
            if (username == "") {
                changeUsernameToolStripMenuItem.PerformClick();
            }
        }

        private void changeUsernameToolStripMenuItem_Click(object sender, EventArgs e) {
            if (clientSocket == null || clientSocket.Connected == false) {
                if (form2 == null) form2 = new Form2(this);
                else {
                    form2.BringToFront();
                    form2.Focus();
                }
            } else toast.Show("You cannot change your username whilst connected to a server.");
        }

        private void Form1_FormClosing (object sender, FormClosingEventArgs e) {
            if (clientSocket != null && clientSocket.Connected) {
                sendData(new mpMessage(username, mpMessage.Type.cmd, "disconnect;"));
                closeClientSocket();
            }
        }

        private void mediaPlayer1_player_MediaPlayerMediaChanged(object sender, EventArgs e) {
            string title = MediaController.MediaPlayer.mediaDescription.title;

            toast.Show("Now playing: " + title.Substring(title.LastIndexOf("\\") + 1));
        }

        private void playToolStripMenuItem1_Click(object sender, EventArgs e) {
            if ( MediaController.MediaPlayer.playlist.items.count > 0) {
                MediaController.PlayPause();
                
                string mediaPath = MediaController.MediaPlayer.mediaDescription.title;
                string mediaName = (mediaPath != "" ? mediaPath.Substring(mediaPath.LastIndexOf("\\") + 1) : "");

                if (clientSocket != null && clientSocket.Connected) {
                    sendData(new mpMessage(username, mpMessage.Type.cmd, "play;" + mediaName + ";"));
                } else
                    MediaController.PlayPause();
            } else {
                toast.Show("You did not select a media file");
            }

            gmh = new GlobalMouseHandler(); // Instantiate new global MouseEventHandler
            gmh.MouseMoved += new MouseMovedEvent(Mouse_Moved); // Add Mouse_Move event
            gmh.LmbDoubleClick += new MouseMovedEvent(LMB_DoubleClick); // Add Mouse_Move event
            Application.AddMessageFilter(gmh);
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e) {
            MediaController.Stop();
        }

        private void previousToolStripMenuItem_Click(object sender, EventArgs e) {
            MediaController.Prev();
        }

        private void nextToolStripMenuItem_Click(object sender, EventArgs e) {
            MediaController.Next();
        }

        private void loopToolStripMenuItem_Click(object sender, EventArgs e) {
            MediaController.ToggleLoop();
        }

        private void fullscreenToolStripMenuItem_Click(object sender, EventArgs e) {
            if (windowState == mpWindowState.Windowed)
                toggleFullscreen(mpWindowState.FullscreenWindowed);
            else
                toggleFullscreen(mpWindowState.Windowed);
        }

        private void muteToolStripMenuItem_Click(object sender, EventArgs e) {
            MediaController.ToggleMute();
        }

        private void playToolStripMenuItem_Click(object sender, EventArgs e) {
            if ( MediaController.MediaPlayer.playlist.isPlaying == true) playToolStripMenuItem1.Text = "Pause";
            else playToolStripMenuItem1.Text = "Play";
        }

        private void openMediaToolStripMenuItem_Click(object sender, EventArgs e) {
            if (clientSocket != null && clientSocket.Connected == true) {
                if ( MediaController.MediaPlayer.playlist.isPlaying == false) openMedia();
                else toast.Show("You cannot open media while connected to a server and playing media.");
            } else
            {
                if( MediaController.MediaPlayer.playlist.items.count > 0 )
                    MediaController.PlayPause();
                openMedia();
            }
        }

        private void menuStrip1_Leave(object sender, EventArgs e) {
            if ( MediaController.Fullscreen == true) {
                menuStrip1.Visible = false;
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e) {
            MessageBox.Show("Penis");
        }
    }
}
