namespace CameraApp
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.pb_GrabImage = new System.Windows.Forms.PictureBox();
            this.btn_Connect = new System.Windows.Forms.Button();
            this.btn_Start = new System.Windows.Forms.Button();
            this.btn_Stop = new System.Windows.Forms.Button();
            this.btn_GrabOne = new System.Windows.Forms.Button();
            this.cb_CameraType = new System.Windows.Forms.ComboBox();
            this.tb_Log = new System.Windows.Forms.TextBox();
            this.trackBar_TargetFrame = new System.Windows.Forms.TrackBar();
            this.btn_ShowTargetFrame = new System.Windows.Forms.Button();
            this.nud_TargetFrame = new System.Windows.Forms.NumericUpDown();
            this.lb_BufferCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pb_GrabImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_TargetFrame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_TargetFrame)).BeginInit();
            this.SuspendLayout();
            // 
            // pb_GrabImage
            // 
            this.pb_GrabImage.Location = new System.Drawing.Point(130, 12);
            this.pb_GrabImage.Name = "pb_GrabImage";
            this.pb_GrabImage.Size = new System.Drawing.Size(640, 640);
            this.pb_GrabImage.TabIndex = 0;
            this.pb_GrabImage.TabStop = false;
            // 
            // btn_Connect
            // 
            this.btn_Connect.Location = new System.Drawing.Point(3, 64);
            this.btn_Connect.Name = "btn_Connect";
            this.btn_Connect.Size = new System.Drawing.Size(121, 30);
            this.btn_Connect.TabIndex = 1;
            this.btn_Connect.Text = "Connect";
            this.btn_Connect.UseVisualStyleBackColor = true;
            this.btn_Connect.Click += new System.EventHandler(this.btn_Connect_Click);
            // 
            // btn_Start
            // 
            this.btn_Start.Location = new System.Drawing.Point(3, 136);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(121, 30);
            this.btn_Start.TabIndex = 2;
            this.btn_Start.Text = "StartLive";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // btn_Stop
            // 
            this.btn_Stop.Location = new System.Drawing.Point(3, 172);
            this.btn_Stop.Name = "btn_Stop";
            this.btn_Stop.Size = new System.Drawing.Size(121, 30);
            this.btn_Stop.TabIndex = 3;
            this.btn_Stop.Text = "StopLive";
            this.btn_Stop.UseVisualStyleBackColor = true;
            this.btn_Stop.Click += new System.EventHandler(this.btn_Stop_Click);
            // 
            // btn_GrabOne
            // 
            this.btn_GrabOne.Location = new System.Drawing.Point(3, 100);
            this.btn_GrabOne.Name = "btn_GrabOne";
            this.btn_GrabOne.Size = new System.Drawing.Size(121, 30);
            this.btn_GrabOne.TabIndex = 4;
            this.btn_GrabOne.Text = "Grab";
            this.btn_GrabOne.UseVisualStyleBackColor = true;
            this.btn_GrabOne.Click += new System.EventHandler(this.btn_GrabOne_Click);
            // 
            // cb_CameraType
            // 
            this.cb_CameraType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_CameraType.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_CameraType.FormattingEnabled = true;
            this.cb_CameraType.Items.AddRange(new object[] {
            "Basler"});
            this.cb_CameraType.Location = new System.Drawing.Point(3, 24);
            this.cb_CameraType.Name = "cb_CameraType";
            this.cb_CameraType.Size = new System.Drawing.Size(121, 30);
            this.cb_CameraType.TabIndex = 5;
            // 
            // tb_Log
            // 
            this.tb_Log.Location = new System.Drawing.Point(12, 733);
            this.tb_Log.Multiline = true;
            this.tb_Log.Name = "tb_Log";
            this.tb_Log.Size = new System.Drawing.Size(758, 191);
            this.tb_Log.TabIndex = 6;
            // 
            // trackBar_TargetFrame
            // 
            this.trackBar_TargetFrame.Location = new System.Drawing.Point(130, 658);
            this.trackBar_TargetFrame.Maximum = 200;
            this.trackBar_TargetFrame.Minimum = 1;
            this.trackBar_TargetFrame.Name = "trackBar_TargetFrame";
            this.trackBar_TargetFrame.Size = new System.Drawing.Size(640, 69);
            this.trackBar_TargetFrame.TabIndex = 7;
            this.trackBar_TargetFrame.TickFrequency = 10;
            this.trackBar_TargetFrame.Value = 1;
            this.trackBar_TargetFrame.Scroll += new System.EventHandler(this.trackBar_TargetFrame_Scroll);
            // 
            // btn_ShowTargetFrame
            // 
            this.btn_ShowTargetFrame.Location = new System.Drawing.Point(3, 658);
            this.btn_ShowTargetFrame.Name = "btn_ShowTargetFrame";
            this.btn_ShowTargetFrame.Size = new System.Drawing.Size(121, 30);
            this.btn_ShowTargetFrame.TabIndex = 8;
            this.btn_ShowTargetFrame.Text = "Show";
            this.btn_ShowTargetFrame.UseVisualStyleBackColor = true;
            this.btn_ShowTargetFrame.Click += new System.EventHandler(this.btn_ShowTargetFrame_Click);
            // 
            // nud_TargetFrame
            // 
            this.nud_TargetFrame.Location = new System.Drawing.Point(3, 694);
            this.nud_TargetFrame.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nud_TargetFrame.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nud_TargetFrame.Name = "nud_TargetFrame";
            this.nud_TargetFrame.Size = new System.Drawing.Size(120, 29);
            this.nud_TargetFrame.TabIndex = 9;
            this.nud_TargetFrame.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nud_TargetFrame.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nud_TargetFrame.ValueChanged += new System.EventHandler(this.nud_TargetFrame_ValueChanged);
            // 
            // lb_BufferCount
            // 
            this.lb_BufferCount.AutoSize = true;
            this.lb_BufferCount.Location = new System.Drawing.Point(9, 634);
            this.lb_BufferCount.Name = "lb_BufferCount";
            this.lb_BufferCount.Size = new System.Drawing.Size(92, 18);
            this.lb_BufferCount.TabIndex = 10;
            this.lb_BufferCount.Text = "Buffer: N/A";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1036, 1016);
            this.Controls.Add(this.lb_BufferCount);
            this.Controls.Add(this.nud_TargetFrame);
            this.Controls.Add(this.btn_ShowTargetFrame);
            this.Controls.Add(this.trackBar_TargetFrame);
            this.Controls.Add(this.tb_Log);
            this.Controls.Add(this.cb_CameraType);
            this.Controls.Add(this.btn_GrabOne);
            this.Controls.Add(this.btn_Stop);
            this.Controls.Add(this.btn_Start);
            this.Controls.Add(this.btn_Connect);
            this.Controls.Add(this.pb_GrabImage);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pb_GrabImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_TargetFrame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_TargetFrame)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pb_GrabImage;
        private System.Windows.Forms.Button btn_Connect;
        private System.Windows.Forms.Button btn_Start;
        private System.Windows.Forms.Button btn_Stop;
        private System.Windows.Forms.Button btn_GrabOne;
        private System.Windows.Forms.ComboBox cb_CameraType;
        private System.Windows.Forms.TextBox tb_Log;
        private System.Windows.Forms.TrackBar trackBar_TargetFrame;
        private System.Windows.Forms.Button btn_ShowTargetFrame;
        private System.Windows.Forms.NumericUpDown nud_TargetFrame;
        private System.Windows.Forms.Label lb_BufferCount;
    }
}

