using System;
using System.Windows.Forms;

namespace MultiPlayer {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose (bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            try {
                base.Dispose(disposing);
            } catch(Exception ex) {
                MessageBox.Show("Disposing failed: " + ex.ToString());
            }
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent () {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openMediaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.joinLobbyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.changeUsernameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_chat = new System.Windows.Forms.ToolStripMenuItem();
            this.playToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.previousToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fullscreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.muteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_send = new System.Windows.Forms.Button();
            this.btn_openChat = new System.Windows.Forms.Button();
            this.spl_container = new System.Windows.Forms.SplitContainer();
            this.tb_receive = new System.Windows.Forms.RichTextBox();
            this.tb_send = new System.Windows.Forms.TextBox();
            this.MediaController = new MultiPlayer.MediaController();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spl_container)).BeginInit();
            this.spl_container.Panel1.SuspendLayout();
            this.spl_container.Panel2.SuspendLayout();
            this.spl_container.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.connectToolStripMenuItem,
            this.btn_chat,
            this.playToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1407, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.Leave += new System.EventHandler(this.menuStrip1_Leave);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openMediaToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openMediaToolStripMenuItem
            // 
            this.openMediaToolStripMenuItem.Name = "openMediaToolStripMenuItem";
            this.openMediaToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openMediaToolStripMenuItem.Size = new System.Drawing.Size(219, 26);
            this.openMediaToolStripMenuItem.Text = "Open media";
            this.openMediaToolStripMenuItem.Click += new System.EventHandler(this.openMediaToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(219, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startServerToolStripMenuItem,
            this.joinLobbyToolStripMenuItem,
            this.toolStripSeparator1,
            this.changeUsernameToolStripMenuItem});
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(75, 24);
            this.connectToolStripMenuItem.Text = "Connect";
            // 
            // startServerToolStripMenuItem
            // 
            this.startServerToolStripMenuItem.Name = "startServerToolStripMenuItem";
            this.startServerToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.startServerToolStripMenuItem.Size = new System.Drawing.Size(226, 26);
            this.startServerToolStripMenuItem.Text = "Start server";
            this.startServerToolStripMenuItem.Click += new System.EventHandler(this.startServerToolStripMenuItem_Click);
            // 
            // joinLobbyToolStripMenuItem
            // 
            this.joinLobbyToolStripMenuItem.Name = "joinLobbyToolStripMenuItem";
            this.joinLobbyToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.joinLobbyToolStripMenuItem.Size = new System.Drawing.Size(226, 26);
            this.joinLobbyToolStripMenuItem.Text = "Join lobby";
            this.joinLobbyToolStripMenuItem.Click += new System.EventHandler(this.joinLobbyToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(223, 6);
            // 
            // changeUsernameToolStripMenuItem
            // 
            this.changeUsernameToolStripMenuItem.Name = "changeUsernameToolStripMenuItem";
            this.changeUsernameToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.changeUsernameToolStripMenuItem.Size = new System.Drawing.Size(226, 26);
            this.changeUsernameToolStripMenuItem.Text = "Change username";
            this.changeUsernameToolStripMenuItem.Click += new System.EventHandler(this.changeUsernameToolStripMenuItem_Click);
            // 
            // btn_chat
            // 
            this.btn_chat.Name = "btn_chat";
            this.btn_chat.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.btn_chat.Size = new System.Drawing.Size(99, 24);
            this.btn_chat.Text = "Toggle &chat";
            this.btn_chat.Click += new System.EventHandler(this.btn_chat_Click);
            // 
            // playToolStripMenuItem
            // 
            this.playToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playToolStripMenuItem1,
            this.stopToolStripMenuItem,
            this.previousToolStripMenuItem,
            this.nextToolStripMenuItem,
            this.loopToolStripMenuItem,
            this.fullscreenToolStripMenuItem,
            this.muteToolStripMenuItem});
            this.playToolStripMenuItem.Name = "playToolStripMenuItem";
            this.playToolStripMenuItem.Size = new System.Drawing.Size(48, 24);
            this.playToolStripMenuItem.Text = "Play";
            this.playToolStripMenuItem.Click += new System.EventHandler(this.playToolStripMenuItem_Click);
            // 
            // playToolStripMenuItem1
            // 
            this.playToolStripMenuItem1.Name = "playToolStripMenuItem1";
            this.playToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.playToolStripMenuItem1.Size = new System.Drawing.Size(198, 26);
            this.playToolStripMenuItem1.Text = "Play";
            this.playToolStripMenuItem1.Click += new System.EventHandler(this.playToolStripMenuItem1_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(198, 26);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // previousToolStripMenuItem
            // 
            this.previousToolStripMenuItem.Name = "previousToolStripMenuItem";
            this.previousToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.previousToolStripMenuItem.Size = new System.Drawing.Size(198, 26);
            this.previousToolStripMenuItem.Text = "Back";
            this.previousToolStripMenuItem.Click += new System.EventHandler(this.previousToolStripMenuItem_Click);
            // 
            // nextToolStripMenuItem
            // 
            this.nextToolStripMenuItem.Name = "nextToolStripMenuItem";
            this.nextToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.nextToolStripMenuItem.Size = new System.Drawing.Size(198, 26);
            this.nextToolStripMenuItem.Text = "Next";
            this.nextToolStripMenuItem.Click += new System.EventHandler(this.nextToolStripMenuItem_Click);
            // 
            // loopToolStripMenuItem
            // 
            this.loopToolStripMenuItem.CheckOnClick = true;
            this.loopToolStripMenuItem.Name = "loopToolStripMenuItem";
            this.loopToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.loopToolStripMenuItem.Size = new System.Drawing.Size(198, 26);
            this.loopToolStripMenuItem.Text = "Loop";
            this.loopToolStripMenuItem.Click += new System.EventHandler(this.loopToolStripMenuItem_Click);
            // 
            // fullscreenToolStripMenuItem
            // 
            this.fullscreenToolStripMenuItem.Name = "fullscreenToolStripMenuItem";
            this.fullscreenToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.fullscreenToolStripMenuItem.Size = new System.Drawing.Size(198, 26);
            this.fullscreenToolStripMenuItem.Text = "Fullscreen";
            this.fullscreenToolStripMenuItem.Click += new System.EventHandler(this.fullscreenToolStripMenuItem_Click);
            // 
            // muteToolStripMenuItem
            // 
            this.muteToolStripMenuItem.Name = "muteToolStripMenuItem";
            this.muteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.muteToolStripMenuItem.Size = new System.Drawing.Size(198, 26);
            this.muteToolStripMenuItem.Text = "Mute";
            this.muteToolStripMenuItem.Click += new System.EventHandler(this.muteToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.checkForUpdatesToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(203, 26);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // checkForUpdatesToolStripMenuItem
            // 
            this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
            this.checkForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(203, 26);
            this.checkForUpdatesToolStripMenuItem.Text = "Check for updates";
            this.checkForUpdatesToolStripMenuItem.Click += new System.EventHandler(this.checkForUpdatesToolStripMenuItem_Click);
            // 
            // btn_send
            // 
            this.btn_send.Location = new System.Drawing.Point(196, 652);
            this.btn_send.Margin = new System.Windows.Forms.Padding(4);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(100, 28);
            this.btn_send.TabIndex = 4;
            this.btn_send.Text = "Send";
            this.btn_send.UseVisualStyleBackColor = true;
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // btn_openChat
            // 
            this.btn_openChat.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_openChat.Location = new System.Drawing.Point(-416, -32);
            this.btn_openChat.Margin = new System.Windows.Forms.Padding(4);
            this.btn_openChat.Name = "btn_openChat";
            this.btn_openChat.Size = new System.Drawing.Size(100, 28);
            this.btn_openChat.TabIndex = 4;
            this.btn_openChat.Text = "Open chat";
            this.btn_openChat.UseVisualStyleBackColor = true;
            this.btn_openChat.Click += new System.EventHandler(this.btn_openChat_Click);
            // 
            // spl_container
            // 
            this.spl_container.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spl_container.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.spl_container.Location = new System.Drawing.Point(0, 0);
            this.spl_container.Margin = new System.Windows.Forms.Padding(4);
            this.spl_container.Name = "spl_container";
            // 
            // spl_container.Panel1
            // 
            this.spl_container.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.spl_container.Panel1.Controls.Add(this.tb_receive);
            this.spl_container.Panel1.Controls.Add(this.btn_send);
            this.spl_container.Panel1.Controls.Add(this.tb_send);
            this.spl_container.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // spl_container.Panel2
            // 
            this.spl_container.Panel2.Controls.Add(this.btn_openChat);
            this.spl_container.Panel2.Controls.Add(this.MediaController);
            this.spl_container.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.spl_container.Size = new System.Drawing.Size(1407, 713);
            this.spl_container.SplitterDistance = 225;
            this.spl_container.TabIndex = 3;
            this.spl_container.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.spl_container_SplitterMoved);
            // 
            // tb_receive
            // 
            this.tb_receive.BackColor = System.Drawing.SystemColors.Window;
            this.tb_receive.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_receive.Location = new System.Drawing.Point(0, 30);
            this.tb_receive.Margin = new System.Windows.Forms.Padding(4);
            this.tb_receive.Name = "tb_receive";
            this.tb_receive.ReadOnly = true;
            this.tb_receive.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.tb_receive.Size = new System.Drawing.Size(299, 655);
            this.tb_receive.TabIndex = 5;
            this.tb_receive.Text = "";
            // 
            // tb_send
            // 
            this.tb_send.AcceptsReturn = true;
            this.tb_send.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_send.Location = new System.Drawing.Point(0, 688);
            this.tb_send.Margin = new System.Windows.Forms.Padding(4);
            this.tb_send.Name = "tb_send";
            this.tb_send.Size = new System.Drawing.Size(224, 22);
            this.tb_send.TabIndex = 1;
            // 
            // MediaController
            // 
            this.MediaController.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MediaController.Location = new System.Drawing.Point(0, 0);
            this.MediaController.Margin = new System.Windows.Forms.Padding(5);
            this.MediaController.Name = "MediaController";
            this.MediaController.Size = new System.Drawing.Size(1178, 713);
            this.MediaController.TabIndex = 2;
            // 
            // Form1
            // 
            this.AcceptButton = this.btn_send;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_openChat;
            this.ClientSize = new System.Drawing.Size(1407, 713);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.spl_container);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(594, 419);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MultiPlayer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.Form1_Layout);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.spl_container.Panel1.ResumeLayout(false);
            this.spl_container.Panel1.PerformLayout();
            this.spl_container.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spl_container)).EndInit();
            this.spl_container.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MediaController MediaController;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openMediaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startServerToolStripMenuItem;
        public  System.Windows.Forms.ToolStripMenuItem joinLobbyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem previousToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem changeUsernameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fullscreenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem muteToolStripMenuItem;
        private ToolStripMenuItem btn_chat;
        private SplitContainer spl_container;
        private TextBox tb_send;
        private Button btn_send;
        private Button btn_openChat;
        private RichTextBox tb_receive;
    }
}

