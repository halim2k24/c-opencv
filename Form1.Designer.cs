namespace PylonGrabSampleStream
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.PortOpen = new System.Windows.Forms.Button();
            this.PortClose = new System.Windows.Forms.Button();
            this.StatusMessage = new System.Windows.Forms.Label();
            this.Grab = new System.Windows.Forms.Button();
            this.Display = new System.Windows.Forms.PictureBox();
            this.ExitApp = new System.Windows.Forms.Button();
            this.GrabCont = new System.Windows.Forms.Button();
            this.Display1 = new System.Windows.Forms.PictureBox();
            this.GrabCont1 = new System.Windows.Forms.Button();
            this.Display2 = new System.Windows.Forms.PictureBox();
            this.btPixForm = new System.Windows.Forms.Button();
            this.btMotor = new System.Windows.Forms.Button();
            this.lblSizeMode = new System.Windows.Forms.Label();
            this.lblPixForm = new System.Windows.Forms.Label();
            this.RedL = new System.Windows.Forms.NumericUpDown();
            this.RedH = new System.Windows.Forms.NumericUpDown();
            this.GreenL = new System.Windows.Forms.NumericUpDown();
            this.BlueL = new System.Windows.Forms.NumericUpDown();
            this.GreenH = new System.Windows.Forms.NumericUpDown();
            this.BlueH = new System.Windows.Forms.NumericUpDown();
            this.takeSnap = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Display)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Display1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Display2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RedL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RedH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GreenL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BlueL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GreenH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BlueH)).BeginInit();
            this.SuspendLayout();
            // 
            // PortOpen
            // 
            this.PortOpen.Location = new System.Drawing.Point(13, 680);
            this.PortOpen.Name = "PortOpen";
            this.PortOpen.Size = new System.Drawing.Size(77, 37);
            this.PortOpen.TabIndex = 0;
            this.PortOpen.Text = "カメラオープン";
            this.PortOpen.UseVisualStyleBackColor = true;
            this.PortOpen.Click += new System.EventHandler(this.PortOpen_Click);
            // 
            // PortClose
            // 
            this.PortClose.Enabled = false;
            this.PortClose.Location = new System.Drawing.Point(96, 680);
            this.PortClose.Name = "PortClose";
            this.PortClose.Size = new System.Drawing.Size(77, 37);
            this.PortClose.TabIndex = 2;
            this.PortClose.Text = "カメラクローズ";
            this.PortClose.UseVisualStyleBackColor = true;
            this.PortClose.Click += new System.EventHandler(this.PortClose_Click);
            // 
            // StatusMessage
            // 
            this.StatusMessage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.StatusMessage.Font = new System.Drawing.Font("Meiryo", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.StatusMessage.Location = new System.Drawing.Point(12, 10);
            this.StatusMessage.Name = "StatusMessage";
            this.StatusMessage.Size = new System.Drawing.Size(630, 16);
            this.StatusMessage.TabIndex = 3;
            this.StatusMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Grab
            // 
            this.Grab.Enabled = false;
            this.Grab.Location = new System.Drawing.Point(179, 680);
            this.Grab.Name = "Grab";
            this.Grab.Size = new System.Drawing.Size(77, 37);
            this.Grab.TabIndex = 4;
            this.Grab.Text = "1枚取込";
            this.Grab.UseVisualStyleBackColor = true;
            this.Grab.Click += new System.EventHandler(this.Grab_Click);
            // 
            // Display
            // 
            this.Display.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Display.Location = new System.Drawing.Point(12, 29);
            this.Display.Name = "Display";
            this.Display.Size = new System.Drawing.Size(920, 634);
            this.Display.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Display.TabIndex = 5;
            this.Display.TabStop = false;
            // 
            // ExitApp
            // 
            this.ExitApp.Location = new System.Drawing.Point(1313, 670);
            this.ExitApp.Name = "ExitApp";
            this.ExitApp.Size = new System.Drawing.Size(77, 47);
            this.ExitApp.TabIndex = 7;
            this.ExitApp.Text = "Exit";
            this.ExitApp.UseVisualStyleBackColor = true;
            this.ExitApp.Click += new System.EventHandler(this.ExitApp_Click);
            // 
            // GrabCont
            // 
            this.GrabCont.Enabled = false;
            this.GrabCont.Location = new System.Drawing.Point(262, 680);
            this.GrabCont.Name = "GrabCont";
            this.GrabCont.Size = new System.Drawing.Size(77, 37);
            this.GrabCont.TabIndex = 4;
            this.GrabCont.Text = "連続取込";
            this.GrabCont.UseVisualStyleBackColor = true;
            this.GrabCont.Click += new System.EventHandler(this.GrabCont_Click);
            // 
            // Display1
            // 
            this.Display1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Display1.Location = new System.Drawing.Point(954, 29);
            this.Display1.Name = "Display1";
            this.Display1.Size = new System.Drawing.Size(431, 307);
            this.Display1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Display1.TabIndex = 8;
            this.Display1.TabStop = false;
            // 
            // GrabCont1
            // 
            this.GrabCont1.Enabled = false;
            this.GrabCont1.Location = new System.Drawing.Point(345, 680);
            this.GrabCont1.Name = "GrabCont1";
            this.GrabCont1.Size = new System.Drawing.Size(77, 37);
            this.GrabCont1.TabIndex = 4;
            this.GrabCont1.Text = "Size Mode";
            this.GrabCont1.UseVisualStyleBackColor = true;
            this.GrabCont1.Click += new System.EventHandler(this.GrabCont1_Click);
            // 
            // Display2
            // 
            this.Display2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Display2.Location = new System.Drawing.Point(954, 342);
            this.Display2.Name = "Display2";
            this.Display2.Size = new System.Drawing.Size(431, 321);
            this.Display2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Display2.TabIndex = 10;
            this.Display2.TabStop = false;
            // 
            // btPixForm
            // 
            this.btPixForm.Enabled = false;
            this.btPixForm.Location = new System.Drawing.Point(428, 680);
            this.btPixForm.Name = "btPixForm";
            this.btPixForm.Size = new System.Drawing.Size(77, 37);
            this.btPixForm.TabIndex = 4;
            this.btPixForm.Text = "Pixel Format";
            this.btPixForm.UseVisualStyleBackColor = true;
            this.btPixForm.Click += new System.EventHandler(this.btPixForm_Click);
            // 
            // btMotor
            // 
            this.btMotor.Enabled = false;
            this.btMotor.Location = new System.Drawing.Point(772, 679);
            this.btMotor.Name = "btMotor";
            this.btMotor.Size = new System.Drawing.Size(77, 37);
            this.btMotor.TabIndex = 4;
            this.btMotor.Text = "Motor OK";
            this.btMotor.UseVisualStyleBackColor = true;
            this.btMotor.Click += new System.EventHandler(this.btMotor_Click);
            // 
            // lblSizeMode
            // 
            this.lblSizeMode.AutoSize = true;
            this.lblSizeMode.Location = new System.Drawing.Point(350, 667);
            this.lblSizeMode.Name = "lblSizeMode";
            this.lblSizeMode.Size = new System.Drawing.Size(35, 12);
            this.lblSizeMode.TabIndex = 11;
            this.lblSizeMode.Text = "label1";
            // 
            // lblPixForm
            // 
            this.lblPixForm.AutoSize = true;
            this.lblPixForm.Location = new System.Drawing.Point(428, 667);
            this.lblPixForm.Name = "lblPixForm";
            this.lblPixForm.Size = new System.Drawing.Size(35, 12);
            this.lblPixForm.TabIndex = 12;
            this.lblPixForm.Text = "label2";
            // 
            // RedL
            // 
            this.RedL.Location = new System.Drawing.Point(954, 670);
            this.RedL.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.RedL.Name = "RedL";
            this.RedL.Size = new System.Drawing.Size(101, 19);
            this.RedL.TabIndex = 13;
            // 
            // RedH
            // 
            this.RedH.Location = new System.Drawing.Point(954, 698);
            this.RedH.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.RedH.Name = "RedH";
            this.RedH.Size = new System.Drawing.Size(101, 19);
            this.RedH.TabIndex = 13;
            this.RedH.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // GreenL
            // 
            this.GreenL.Location = new System.Drawing.Point(1080, 670);
            this.GreenL.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.GreenL.Name = "GreenL";
            this.GreenL.Size = new System.Drawing.Size(101, 19);
            this.GreenL.TabIndex = 13;
            this.GreenL.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // BlueL
            // 
            this.BlueL.Location = new System.Drawing.Point(1206, 669);
            this.BlueL.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.BlueL.Name = "BlueL";
            this.BlueL.Size = new System.Drawing.Size(101, 19);
            this.BlueL.TabIndex = 13;
            // 
            // GreenH
            // 
            this.GreenH.Location = new System.Drawing.Point(1080, 698);
            this.GreenH.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.GreenH.Name = "GreenH";
            this.GreenH.Size = new System.Drawing.Size(101, 19);
            this.GreenH.TabIndex = 13;
            this.GreenH.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // BlueH
            // 
            this.BlueH.Location = new System.Drawing.Point(1206, 698);
            this.BlueH.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.BlueH.Name = "BlueH";
            this.BlueH.Size = new System.Drawing.Size(101, 19);
            this.BlueH.TabIndex = 13;
            this.BlueH.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // takeSnap
            // 
            this.takeSnap.Enabled = false;
            this.takeSnap.Location = new System.Drawing.Point(855, 669);
            this.takeSnap.Name = "takeSnap";
            this.takeSnap.Size = new System.Drawing.Size(77, 47);
            this.takeSnap.TabIndex = 14;
            this.takeSnap.Text = "Snapshot";
            this.takeSnap.UseVisualStyleBackColor = true;
            this.takeSnap.Click += new System.EventHandler(this.takeSnap_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1402, 729);
            this.Controls.Add(this.takeSnap);
            this.Controls.Add(this.BlueH);
            this.Controls.Add(this.GreenH);
            this.Controls.Add(this.BlueL);
            this.Controls.Add(this.GreenL);
            this.Controls.Add(this.RedH);
            this.Controls.Add(this.RedL);
            this.Controls.Add(this.lblPixForm);
            this.Controls.Add(this.lblSizeMode);
            this.Controls.Add(this.btMotor);
            this.Controls.Add(this.btPixForm);
            this.Controls.Add(this.Display2);
            this.Controls.Add(this.GrabCont1);
            this.Controls.Add(this.Display1);
            this.Controls.Add(this.ExitApp);
            this.Controls.Add(this.Display);
            this.Controls.Add(this.GrabCont);
            this.Controls.Add(this.Grab);
            this.Controls.Add(this.StatusMessage);
            this.Controls.Add(this.PortClose);
            this.Controls.Add(this.PortOpen);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "シンチレータ素子画像検査　＜R2　事業再構築＞";
            ((System.ComponentModel.ISupportInitialize)(this.Display)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Display1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Display2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RedL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RedH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GreenL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BlueL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GreenH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BlueH)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button PortOpen;
        private System.Windows.Forms.Button PortClose;
        private System.Windows.Forms.Label StatusMessage;
        private System.Windows.Forms.Button Grab;
        private System.Windows.Forms.PictureBox Display;
        private System.Windows.Forms.Button ExitApp;
        private System.Windows.Forms.Button GrabCont;
        private System.Windows.Forms.PictureBox Display1;
        private System.Windows.Forms.Button GrabCont1;
        private System.Windows.Forms.PictureBox Display2;
        private System.Windows.Forms.Button btPixForm;
        private System.Windows.Forms.Label lblSizeMode;
        private System.Windows.Forms.Label lblPixForm;
        private System.Windows.Forms.NumericUpDown RedL;
        private System.Windows.Forms.NumericUpDown RedH;
        private System.Windows.Forms.NumericUpDown GreenL;
        private System.Windows.Forms.NumericUpDown BlueL;
        private System.Windows.Forms.NumericUpDown GreenH;
        private System.Windows.Forms.NumericUpDown BlueH;
        public System.Windows.Forms.Button btMotor;
        public System.Windows.Forms.Button takeSnap;
    }
}

