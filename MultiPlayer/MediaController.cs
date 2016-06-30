using System;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using Brush = System.Drawing.Brushes;
using Point = System.Drawing.Point;

namespace MultiPlayer
{
    public partial class MediaController : UserControl
    {
        public class pbSeeker : ProgressBar
        {
            public pbSeeker()
            {
                this.SetStyle( ControlStyles.UserPaint, true );
            }

            protected override void OnPaint( PaintEventArgs e )
            {
                Rectangle rec = e.ClipRectangle;

                rec.Width = ( int )( rec.Width*( ( double )Value/Maximum ) ) - 4;
                if ( ProgressBarRenderer.IsSupported )
                    ProgressBarRenderer.DrawHorizontalBar( e.Graphics, e.ClipRectangle );
                rec.Height -= 4;
                e.Graphics.FillRectangle( Brush.DodgerBlue, 2, 2, rec.Width, rec.Height );
            }
        }

        private int _savedVolume = -1;
        private bool _stopped = true;

        public bool Fullscreen = false;
        private readonly pbSeeker _pb;

        public MediaController()
        {
            InitializeComponent();

            _pb = new pbSeeker
            {
                Width = 243,
                Height = 45,
                Location = new Point( 193, 2 ),
                Maximum = 100,
                Anchor = ( AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right )
            };
            _pb.MouseDown += pb_MouseDown;
            pnl_toolbar.Controls.Add( _pb );
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
            if ( _stopped )
            {
                MediaPlayer.playlist.play();

                _pb.Value = 0;
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
            if ( volume > -1 && volume < 201 )
            {
                MediaPlayer.volume = volume;
                //_toolbar.tb_volume.Value = volume;
            }
            else if ( volume > 200 )
            {
                MediaPlayer.volume = 200;
                //_toolbar.tb_volume.Value = 200;
            }
            else if ( volume < 0 )
            {
                MediaPlayer.volume = 0;
                //_toolbar.tb_volume.Value = 0;
            }
        }

        public void ToggleMute()
        {
            if ( _savedVolume > -1 )
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

        private void tb_volume_ValueChanged( object sender, EventArgs e )
        {
            MediaPlayer.volume = tb_volume.Value;
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

        private void pb_MouseDown( object sender, MouseEventArgs e )
        {
            bool playing = isPlaying;
            if ( !playing )
                PlayPause();

            var newValue = ( e.X + 4 ) / ( double )_pb.Width * ( _pb.Maximum - _pb.Minimum );
            _pb.Value = Convert.ToInt32( newValue );

            if( MediaPlayer.playlist.itemCount != 0 && isPlaying )
                MediaPlayer.input.time = newValue;

            if(!playing)
                PlayPause();
        }

        private void MediaPlayer_MediaPlayerTimeChanged( object sender, AxAXVLC.DVLCEvents_MediaPlayerTimeChangedEvent e )
        {
            if ( MediaPlayer.playlist.itemCount != 0 && isPlaying )
                _pb.Value = e.time;
        }

        private void MediaPlayer_MediaPlayerOpening( object sender, EventArgs e )
        {
            while ( MediaPlayer.input.length <= 0 )
            {}
            _pb.Maximum = ( int )Math.Round( MediaPlayer.input.length );
        }

        bool isPlaying => MediaPlayer.playlist.isPlaying;
    }
}
