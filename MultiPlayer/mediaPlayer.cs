using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace MultiPlayer {
    public partial class mediaPlayer : UserControl {
        Thread th;

        private int savedVolume = -1;
        private bool stopped = true;
        private System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();

        public bool fullscreen = false;

        public mediaPlayer() {
            InitializeComponent();

            th = new Thread(keepBack);
        }

        public void addMedia(string filePath, object name, object options) {
            player.playlist.add("file:///" + filePath, name, options);
        }

        public void playPause() {
            if (stopped == true) {
                player.playlist.play();
                stopped = false;
            } else player.playlist.togglePause();
        }

        public void stop() {
            player.playlist.stop();
        }

        public void next() {
            player.playlist.next();
        }

        public void prev() {
            player.playlist.prev();
        }

        public void changeVolume(int volume) {
            if (volume > -1 && volume < 201) {
                player.volume = volume;
                tb_volume.Value = volume;
            } else if (volume > 200) {
                player.volume = 200;
                tb_volume.Value = 200;
            } else if (volume < 0) {
                player.volume = 0;
                tb_volume.Value = 0;
            }
        }

        public void toggleMute() {
            if (savedVolume > -1) {
                changeVolume(savedVolume);
                savedVolume = -1;
            } else {
                savedVolume = player.volume;
                changeVolume(0);
            }

        }

        public void toggleLoop() {
            player.AutoLoop = !player.AutoLoop;
        }

        public void showToolbar() {
            MessageBox.Show("");
            pnl_toolbar.Show();
        }

        public void hideToolbar() {
            pnl_toolbar.Hide();
        }

        private void keepBack() {
            while(true) player.SendToBack();
        }

        private void player_MediaPlayerStopped(object sender, EventArgs e) {
            stopped = true;
        }

        private void tb_volume_ValueChanged(object sender, EventArgs e) {
            player.volume = tb_volume.Value;
        }

        private void player_MediaPlayerPositionChanged(object sender, AxAXVLC.DVLCEvents_MediaPlayerPositionChangedEvent e) {
            //pb_seeker.Value++;
        }

        private void mediaPlayer1_player_MediaPlayerStopped(object sender, EventArgs e) {

        }

        private void btn_playpause_Click(object sender, EventArgs e) {
            if (btn_playpause.Text == "Play")
                btn_playpause.Text = "Pause";
            else
                btn_playpause.Text = "Play";

            playPause();
        }
    }
}
