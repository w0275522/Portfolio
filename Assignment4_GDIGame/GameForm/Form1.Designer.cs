namespace GameForm
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tmrRefreshRate = new System.Windows.Forms.Timer(this.components);
            this.tmrSpawnBricks = new System.Windows.Forms.Timer(this.components);
            this.tmrBrickMove = new System.Windows.Forms.Timer(this.components);
            this.tmrRestartGame = new System.Windows.Forms.Timer(this.components);
            this.tmrWinTracker = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // tmrRefreshRate
            // 
            this.tmrRefreshRate.Interval = 1;
            this.tmrRefreshRate.Tick += new System.EventHandler(this.tmrRefreshRate_Tick);
            // 
            // tmrSpawnBricks
            // 
            this.tmrSpawnBricks.Interval = 300;
            this.tmrSpawnBricks.Tick += new System.EventHandler(this.tmrSpawnBricks_Tick);
            // 
            // tmrBrickMove
            // 
            this.tmrBrickMove.Tick += new System.EventHandler(this.tmrBrickMove_Tick);
            // 
            // tmrRestartGame
            // 
            this.tmrRestartGame.Interval = 5000;
            this.tmrRestartGame.Tick += new System.EventHandler(this.tmrRestartGame_Tick);
            // 
            // tmrWinTracker
            // 
            this.tmrWinTracker.Interval = 1000;
            this.tmrWinTracker.Tick += new System.EventHandler(this.tmrWinTracker_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BackgroundImage = global::GameForm.Properties.Resources.rsz_ntoikpy;
            this.ClientSize = new System.Drawing.Size(653, 273);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Dodge Brick!";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer tmrRefreshRate;
        private System.Windows.Forms.Timer tmrSpawnBricks;
        private System.Windows.Forms.Timer tmrBrickMove;
        private System.Windows.Forms.Timer tmrRestartGame;
        private System.Windows.Forms.Timer tmrWinTracker;
    }
}

