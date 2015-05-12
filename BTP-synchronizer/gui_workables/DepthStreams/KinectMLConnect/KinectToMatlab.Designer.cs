namespace KinectMLConnect
{
    partial class KinectToMatlab
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
            this.StartButton = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.StatusBox = new System.Windows.Forms.TextBox();
            this.Options = new System.Windows.Forms.GroupBox();
            this.IRRadio = new System.Windows.Forms.RadioButton();
            this.DepthRadio = new System.Windows.Forms.RadioButton();
            this.depthImage = new System.Windows.Forms.RadioButton();
            this.depthFrame = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pathText = new System.Windows.Forms.TextBox();
            this.minText = new System.Windows.Forms.TextBox();
            this.maxText = new System.Windows.Forms.TextBox();
            this.submitButton = new System.Windows.Forms.Button();
            this.browseButton = new System.Windows.Forms.Button();
            this.Options.SuspendLayout();
            this.SuspendLayout();
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(12, 12);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(95, 23);
            this.StartButton.TabIndex = 0;
            this.StartButton.Text = "Start capture";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // StopButton
            // 
            this.StopButton.Location = new System.Drawing.Point(113, 12);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(92, 23);
            this.StopButton.TabIndex = 1;
            this.StopButton.Text = "Stop capture";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // StatusBox
            // 
            this.StatusBox.BackColor = System.Drawing.SystemColors.Info;
            this.StatusBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.StatusBox.Location = new System.Drawing.Point(223, 17);
            this.StatusBox.Name = "StatusBox";
            this.StatusBox.ReadOnly = true;
            this.StatusBox.Size = new System.Drawing.Size(218, 13);
            this.StatusBox.TabIndex = 2;
            this.StatusBox.Text = "Ready...";
            // 
            // Options
            // 
            this.Options.Controls.Add(this.IRRadio);
            this.Options.Controls.Add(this.DepthRadio);
            this.Options.Location = new System.Drawing.Point(12, 42);
            this.Options.Name = "Options";
            this.Options.Size = new System.Drawing.Size(200, 67);
            this.Options.TabIndex = 3;
            this.Options.TabStop = false;
            this.Options.Text = "Options";
            // 
            // IRRadio
            // 
            this.IRRadio.AutoSize = true;
            this.IRRadio.Location = new System.Drawing.Point(7, 44);
            this.IRRadio.Name = "IRRadio";
            this.IRRadio.Size = new System.Drawing.Size(72, 17);
            this.IRRadio.TabIndex = 1;
            this.IRRadio.TabStop = true;
            this.IRRadio.Text = "Extract IR";
            this.IRRadio.UseVisualStyleBackColor = true;
            // 
            // DepthRadio
            // 
            this.DepthRadio.AutoSize = true;
            this.DepthRadio.Location = new System.Drawing.Point(7, 20);
            this.DepthRadio.Name = "DepthRadio";
            this.DepthRadio.Size = new System.Drawing.Size(90, 17);
            this.DepthRadio.TabIndex = 0;
            this.DepthRadio.TabStop = true;
            this.DepthRadio.Text = "Extract Depth";
            this.DepthRadio.UseVisualStyleBackColor = true;
            // 
            // depthImage
            // 
            this.depthImage.AutoSize = true;
            this.depthImage.Checked = true;
            this.depthImage.Location = new System.Drawing.Point(257, 105);
            this.depthImage.Name = "depthImage";
            this.depthImage.Size = new System.Drawing.Size(78, 17);
            this.depthImage.TabIndex = 4;
            this.depthImage.TabStop = true;
            this.depthImage.Text = "Depth Map";
            this.depthImage.UseVisualStyleBackColor = true;
            this.depthImage.CheckedChanged += new System.EventHandler(this.depthImage_CheckedChanged);
            // 
            // depthFrame
            // 
            this.depthFrame.AutoSize = true;
            this.depthFrame.Location = new System.Drawing.Point(257, 143);
            this.depthFrame.Name = "depthFrame";
            this.depthFrame.Size = new System.Drawing.Size(93, 17);
            this.depthFrame.TabIndex = 5;
            this.depthFrame.TabStop = true;
            this.depthFrame.Text = "Frame Timings";
            this.depthFrame.UseVisualStyleBackColor = true;
            this.depthFrame.CheckedChanged += new System.EventHandler(this.depthFrame_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 112);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Directory Path";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(56, 166);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Min Frame";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(56, 228);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Max Frame";
            // 
            // pathText
            // 
            this.pathText.Location = new System.Drawing.Point(223, 112);
            this.pathText.Name = "pathText";
            this.pathText.Size = new System.Drawing.Size(218, 20);
            this.pathText.TabIndex = 8;
            // 
            // minText
            // 
            this.minText.Location = new System.Drawing.Point(223, 166);
            this.minText.Name = "minText";
            this.minText.Size = new System.Drawing.Size(218, 20);
            this.minText.TabIndex = 9;
            // 
            // maxText
            // 
            this.maxText.Location = new System.Drawing.Point(223, 228);
            this.maxText.Name = "maxText";
            this.maxText.Size = new System.Drawing.Size(218, 20);
            this.maxText.TabIndex = 10;
            // 
            // submitButton
            // 
            this.submitButton.Location = new System.Drawing.Point(444, 284);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(75, 23);
            this.submitButton.TabIndex = 11;
            this.submitButton.Text = "Submit";
            this.submitButton.UseVisualStyleBackColor = true;
            this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(495, 110);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(75, 23);
            this.browseButton.TabIndex = 12;
            this.browseButton.Text = "Browse";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // KinectToMatlab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(595, 337);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.submitButton);
            this.Controls.Add(this.maxText);
            this.Controls.Add(this.minText);
            this.Controls.Add(this.pathText);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.depthFrame);
            this.Controls.Add(this.depthImage);
            this.Controls.Add(this.Options);
            this.Controls.Add(this.StatusBox);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.StartButton);
            this.Name = "KinectToMatlab";
            this.Text = "Depth to MatLab";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Options.ResumeLayout(false);
            this.Options.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.TextBox StatusBox;
        private System.Windows.Forms.GroupBox Options;
        private System.Windows.Forms.RadioButton IRRadio;
        private System.Windows.Forms.RadioButton DepthRadio;
        private System.Windows.Forms.RadioButton depthImage;
        private System.Windows.Forms.RadioButton depthFrame;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox pathText;
        private System.Windows.Forms.TextBox minText;
        private System.Windows.Forms.TextBox maxText;
        private System.Windows.Forms.Button submitButton;
        private System.Windows.Forms.Button browseButton;
    }
}

