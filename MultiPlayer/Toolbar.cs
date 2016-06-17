using System;
using System.Windows.Forms;

namespace MultiPlayer
{
    public partial class Toolbar : UserControl
    {
        //private MediaController _mediaController = new MediaController();

        public Toolbar()
        {
            InitializeComponent();
        }
        private void btn_playpause_Click( object sender, EventArgs e )
        {
            if( btn_playpause.Text == "Play" )
                btn_playpause.Text = "Pause";
            else
                btn_playpause.Text = "Play";

            //_mediaController.PlayPause();
        }
    }
}
