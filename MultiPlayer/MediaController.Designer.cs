namespace MultiPlayer
{
    partial class MediaController
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MediaController));
            this.MediaPlayer = new AxAXVLC.AxVLCPlugin2();
            this.toolbar2 = new MultiPlayer.Toolbar();
            this.toolbar1 = new MultiPlayer.Toolbar();
            ((System.ComponentModel.ISupportInitialize)(this.MediaPlayer)).BeginInit();
            this.SuspendLayout();
            // 
            // MediaPlayer
            // 
            this.MediaPlayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MediaPlayer.Enabled = true;
            this.MediaPlayer.Location = new System.Drawing.Point(0, 0);
            this.MediaPlayer.Name = "MediaPlayer";
            this.MediaPlayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("MediaPlayer.OcxState")));
            this.MediaPlayer.Size = new System.Drawing.Size(538, 392);
            this.MediaPlayer.TabIndex = 2;
            // 
            // toolbar2
            // 
            this.toolbar2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toolbar2.Location = new System.Drawing.Point(0, 341);
            this.toolbar2.Name = "toolbar2";
            this.toolbar2.Size = new System.Drawing.Size(538, 51);
            this.toolbar2.TabIndex = 3;
            // 
            // toolbar1
            // 
            this.toolbar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toolbar1.Location = new System.Drawing.Point(3, 341);
            this.toolbar1.Name = "toolbar1";
            this.toolbar1.Size = new System.Drawing.Size(535, 51);
            this.toolbar1.TabIndex = 1;
            // 
            // MediaController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolbar2);
            this.Controls.Add(this.MediaPlayer);
            this.Controls.Add(this.toolbar1);
            this.Name = "MediaController";
            this.Size = new System.Drawing.Size(538, 392);
            ((System.ComponentModel.ISupportInitialize)(this.MediaPlayer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Toolbar toolbar1;
        public AxAXVLC.AxVLCPlugin2 MediaPlayer;
        private Toolbar toolbar2;
    }
}
