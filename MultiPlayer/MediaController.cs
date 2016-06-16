using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiPlayer
{
    public partial class MediaController : UserControl
    {
        private Toolbar _toolbar;
        Thread _th;

        private System.Windows.Forms.Timer _t = new System.Windows.Forms.Timer();

        private int _savedVolume = -1;
        private bool _stopped = true;

        public bool Fullscreen = false;

        public MediaController()
        {
            InitializeComponent();

            _th = new Thread( KeepBack );
            _toolbar = new Toolbar();
        }
        
        public void ShowToolbar()
        {
            _toolbar.pnl_toolbar.Show();
        }

        public void HideToolbar()
        {
            _toolbar.pnl_toolbar.Hide();
        }

        public void AddMedia( string filePath, object name, object options )
        {
            MediaPlayer.playlist.add( "file:///" + filePath, name, options );
        }

        public void PlayPause()
        {
            if( _stopped == true )
            {
                MediaPlayer.playlist.play();
                _stopped = false;
            }
            else
                MediaPlayer.playlist.togglePause();
        }

        public void Stop()
        {
            MediaPlayer.playlist.stop();
        }

        public void Next()
        {
            MediaPlayer.playlist.next();
        }

        public void Prev()
        {
            MediaPlayer.playlist.prev();
        }

        public void ChangeVolume( int volume )
        {
            if( volume > -1 && volume < 201 )
            {
                MediaPlayer.volume = volume;
                _toolbar.tb_volume.Value = volume;
            }
            else if( volume > 200 )
            {
                MediaPlayer.volume = 200;
                _toolbar.tb_volume.Value = 200;
            }
            else if( volume < 0 )
            {
                MediaPlayer.volume = 0;
                _toolbar.tb_volume.Value = 0;
            }
        }

        public void ToggleMute()
        {
            if( _savedVolume > -1 )
            {
                ChangeVolume( _savedVolume );
                _savedVolume = -1;
            }
            else
            {
                _savedVolume = MediaPlayer.volume;
                ChangeVolume( 0 );
            }

        }

        public void ToggleLoop()
        {
            MediaPlayer.AutoLoop = !MediaPlayer.AutoLoop;
        }

        private void KeepBack()
        {
            while( true )
                MediaPlayer.SendToBack();
        }

        private void player_MediaPlayerStopped( object sender, EventArgs e )
        {
            _stopped = true;
        }

        private void tb_volume_ValueChanged( object sender, EventArgs e )
        {
            MediaPlayer.volume = _toolbar.tb_volume.Value;
        }

        private void player_MediaPlayerPositionChanged( object sender, AxAXVLC.DVLCEvents_MediaPlayerPositionChangedEvent e )
        {
            //pb_seeker.Value++;
        }

        private void mediaPlayer1_player_MediaPlayerStopped( object sender, EventArgs e )
        {

        }

        private void btn_playpause_Click( object sender, EventArgs e )
        {
            if( _toolbar.btn_playpause.Text == "Play" )
                _toolbar.btn_playpause.Text = "Pause";
            else
                _toolbar.btn_playpause.Text = "Play";

            PlayPause();
        }
    }
}
