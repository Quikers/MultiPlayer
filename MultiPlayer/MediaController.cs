using System;
using System.Threading;
using System.Windows.Forms;

namespace MultiPlayer
{
    public partial class MediaController : UserControl
    {
        private int _savedVolume = -1;
        private bool _stopped = true;

        public bool Fullscreen = false;

        public MediaController()
        {
            InitializeComponent();
        }
        
        public void ShowToolbar()
        {
            pnl_toolbar.Show();
        }

        public void HideToolbar()
        {
            pnl_toolbar.Hide();
        }

        /// <summary>
        /// Adds media to the playlist
        /// </summary>
        /// <param name="filePath">Path of the file</param>
        /// <param name="name">Name of the media</param>
        /// <param name="options"></param>
        public void AddMedia( string filePath, object name, object options )
        {
            MediaPlayer.playlist.add( "file:///" + filePath, name, options );
        }

        public void PlayPause()
        {
            if( _stopped )
            {
                MediaPlayer.playlist.play();
                _stopped = false;
            }
            else
                MediaPlayer.playlist.togglePause();
        }

        public void Stop()
        {
            _stopped = true;
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
                //_toolbar.tb_volume.Value = volume;
            }
            else if( volume > 200 )
            {
                MediaPlayer.volume = 200;
               //_toolbar.tb_volume.Value = 200;
            }
            else if( volume < 0 )
            {
                MediaPlayer.volume = 0;
                //_toolbar.tb_volume.Value = 0;
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
        private void tb_volume_ValueChanged( object sender, EventArgs e )
        {
            MediaPlayer.volume = tb_volume.Value;
        }

        private void player_MediaPlayerPositionChanged( object sender, AxAXVLC.DVLCEvents_MediaPlayerPositionChangedEvent e )
        {
            pb_seeker.Value++;
        }
        private void btn_playpause_Click( object sender, EventArgs e )
        {
            btn_playpause.Text = btn_playpause.Text == "Play" ? "Pause" : "Play";

            PlayPause();
        }

        private void btn_stop_Click( object sender, EventArgs e )
        {
            Stop();
        }
    }
}
