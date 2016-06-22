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
            this.btn_playpause = new System.Windows.Forms.Button();
            this.pnl_toolbar = new System.Windows.Forms.Panel();
            this.pb_seeker = new System.Windows.Forms.ProgressBar();
            this.tb_volume = new System.Windows.Forms.TrackBar();
            this.btn_next = new System.Windows.Forms.Button();
            this.btn_prev = new System.Windows.Forms.Button();
            this.btn_stop = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.MediaPlayer)).BeginInit();
            this.pnl_toolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tb_volume)).BeginInit();
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
            // btn_playpause
            // 
            this.btn_playpause.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_playpause.Location = new System.Drawing.Point(4, 2);
            this.btn_playpause.Name = "btn_playpause";
            this.btn_playpause.Size = new System.Drawing.Size(45, 40);
            this.btn_playpause.TabIndex = 0;
            this.btn_playpause.Text = "Play";
            this.btn_playpause.UseVisualStyleBackColor = true;
            this.btn_playpause.Click += new System.EventHandler(this.btn_playpause_Click);
            // 
            // pnl_toolbar
            // 
            this.pnl_toolbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl_toolbar.Controls.Add(this.pb_seeker);
            this.pnl_toolbar.Controls.Add(this.tb_volume);
            this.pnl_toolbar.Controls.Add(this.btn_next);
            this.pnl_toolbar.Controls.Add(this.btn_prev);
            this.pnl_toolbar.Controls.Add(this.btn_stop);
            this.pnl_toolbar.Controls.Add(this.btn_playpause);
            this.pnl_toolbar.Location = new System.Drawing.Point(0, 347);
            this.pnl_toolbar.Name = "pnl_toolbar";
            this.pnl_toolbar.Size = new System.Drawing.Size(538, 45);
            this.pnl_toolbar.TabIndex = 8;
            // 
            // pb_seeker
            // 
            this.pb_seeker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pb_seeker.Location = new System.Drawing.Point(196, 17);
            this.pb_seeker.MarqueeAnimationSpeed = 0;
            this.pb_seeker.Name = "pb_seeker";
            this.pb_seeker.Size = new System.Drawing.Size(246, 11);
            this.pb_seeker.Step = 1;
            this.pb_seeker.TabIndex = 5;
            // 
            // tb_volume
            // 
            this.tb_volume.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_volume.Location = new System.Drawing.Point(444, 9);
            this.tb_volume.Maximum = 200;
            this.tb_volume.Name = "tb_volume";
            this.tb_volume.Size = new System.Drawing.Size(94, 45);
            this.tb_volume.TabIndex = 4;
            // 
            // btn_next
            // 
            this.btn_next.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_next.Location = new System.Drawing.Point(145, 2);
            this.btn_next.Name = "btn_next";
            this.btn_next.Size = new System.Drawing.Size(45, 40);
            this.btn_next.TabIndex = 3;
            this.btn_next.Text = "Next";
            this.btn_next.UseVisualStyleBackColor = true;
            // 
            // btn_prev
            // 
            this.btn_prev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_prev.Location = new System.Drawing.Point(98, 2);
            this.btn_prev.Name = "btn_prev";
            this.btn_prev.Size = new System.Drawing.Size(45, 40);
            this.btn_prev.TabIndex = 2;
            this.btn_prev.Text = "Back";
            this.btn_prev.UseVisualStyleBackColor = true;
            // 
            // btn_stop
            // 
            this.btn_stop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_stop.Location = new System.Drawing.Point(51, 2);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(45, 40);
            this.btn_stop.TabIndex = 1;
            this.btn_stop.Text = "Stop";
            this.btn_stop.UseVisualStyleBackColor = true;
            this.btn_stop.Click += new System.EventHandler(this.btn_stop_Click);
            // 
            // MediaController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnl_toolbar);
            this.Controls.Add(this.MediaPlayer);
            this.Name = "MediaController";
            this.Size = new System.Drawing.Size(538, 392);
            ((System.ComponentModel.ISupportInitialize)(this.MediaPlayer)).EndInit();
            this.pnl_toolbar.ResumeLayout(false);
            this.pnl_toolbar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tb_volume)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        public AxAXVLC.AxVLCPlugin2 MediaPlayer;
        public System.Windows.Forms.Button btn_playpause;
        public System.Windows.Forms.Panel pnl_toolbar;
        public System.Windows.Forms.ProgressBar pb_seeker;
        public System.Windows.Forms.TrackBar tb_volume;
        public System.Windows.Forms.Button btn_next;
        public System.Windows.Forms.Button btn_prev;
        public System.Windows.Forms.Button btn_stop;
    }
}
