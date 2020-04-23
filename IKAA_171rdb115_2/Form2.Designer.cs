namespace IKAA_171rdb115_2
{
    partial class Form2
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.softLightButton = new System.Windows.Forms.Button();
            this.hardLightButton = new System.Windows.Forms.Button();
            this.differenceButton = new System.Windows.Forms.Button();
            this.overlayButton = new System.Windows.Forms.Button();
            this.subtractButton = new System.Windows.Forms.Button();
            this.additionButton = new System.Windows.Forms.Button();
            this.multiplyButton = new System.Windows.Forms.Button();
            this.lightenButton = new System.Windows.Forms.Button();
            this.darkenButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.splitVerticalRadio = new System.Windows.Forms.RadioButton();
            this.splitHorizontalRadio = new System.Windows.Forms.RadioButton();
            this.wipeVerticalRadio = new System.Windows.Forms.RadioButton();
            this.wipeHorizontalRadio = new System.Windows.Forms.RadioButton();
            this.fadeRadio = new System.Windows.Forms.RadioButton();
            this.transitionButton = new System.Windows.Forms.Button();
            this.screenButton = new System.Windows.Forms.Button();
            this.opacityButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.LightCyan;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(300, 300);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.LightCyan;
            this.pictureBox2.Location = new System.Drawing.Point(318, 12);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(300, 300);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseClick);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.LightCyan;
            this.pictureBox3.Location = new System.Drawing.Point(13, 318);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(300, 300);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 2;
            this.pictureBox3.TabStop = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog2";
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.softLightButton);
            this.panel1.Controls.Add(this.hardLightButton);
            this.panel1.Controls.Add(this.differenceButton);
            this.panel1.Controls.Add(this.overlayButton);
            this.panel1.Controls.Add(this.subtractButton);
            this.panel1.Controls.Add(this.additionButton);
            this.panel1.Controls.Add(this.multiplyButton);
            this.panel1.Controls.Add(this.lightenButton);
            this.panel1.Controls.Add(this.darkenButton);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.transitionButton);
            this.panel1.Controls.Add(this.screenButton);
            this.panel1.Controls.Add(this.opacityButton);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.trackBar1);
            this.panel1.Location = new System.Drawing.Point(319, 318);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(362, 300);
            this.panel1.TabIndex = 3;
            // 
            // softLightButton
            // 
            this.softLightButton.Location = new System.Drawing.Point(100, 224);
            this.softLightButton.Name = "softLightButton";
            this.softLightButton.Size = new System.Drawing.Size(87, 32);
            this.softLightButton.TabIndex = 16;
            this.softLightButton.Text = "Soft Light";
            this.softLightButton.UseVisualStyleBackColor = true;
            this.softLightButton.Click += new System.EventHandler(this.softLightButton_Click);
            // 
            // hardLightButton
            // 
            this.hardLightButton.Location = new System.Drawing.Point(7, 224);
            this.hardLightButton.Name = "hardLightButton";
            this.hardLightButton.Size = new System.Drawing.Size(87, 32);
            this.hardLightButton.TabIndex = 15;
            this.hardLightButton.Text = "Hard Light";
            this.hardLightButton.UseVisualStyleBackColor = true;
            this.hardLightButton.Click += new System.EventHandler(this.hardLightButton_Click);
            // 
            // differenceButton
            // 
            this.differenceButton.Location = new System.Drawing.Point(100, 188);
            this.differenceButton.Name = "differenceButton";
            this.differenceButton.Size = new System.Drawing.Size(87, 32);
            this.differenceButton.TabIndex = 14;
            this.differenceButton.Text = "Difference";
            this.differenceButton.UseVisualStyleBackColor = true;
            this.differenceButton.Click += new System.EventHandler(this.differenceButton_Click);
            // 
            // overlayButton
            // 
            this.overlayButton.Location = new System.Drawing.Point(7, 188);
            this.overlayButton.Name = "overlayButton";
            this.overlayButton.Size = new System.Drawing.Size(87, 32);
            this.overlayButton.TabIndex = 13;
            this.overlayButton.Text = "Overlay";
            this.overlayButton.UseVisualStyleBackColor = true;
            this.overlayButton.Click += new System.EventHandler(this.overlayButton_Click);
            // 
            // subtractButton
            // 
            this.subtractButton.Location = new System.Drawing.Point(100, 152);
            this.subtractButton.Name = "subtractButton";
            this.subtractButton.Size = new System.Drawing.Size(87, 32);
            this.subtractButton.TabIndex = 10;
            this.subtractButton.Text = "Subtract";
            this.subtractButton.UseVisualStyleBackColor = true;
            this.subtractButton.Click += new System.EventHandler(this.subtractButton_Click);
            // 
            // additionButton
            // 
            this.additionButton.Location = new System.Drawing.Point(6, 152);
            this.additionButton.Name = "additionButton";
            this.additionButton.Size = new System.Drawing.Size(87, 32);
            this.additionButton.TabIndex = 9;
            this.additionButton.Text = "Addition";
            this.additionButton.UseVisualStyleBackColor = true;
            this.additionButton.Click += new System.EventHandler(this.additionButton_Click);
            // 
            // multiplyButton
            // 
            this.multiplyButton.Location = new System.Drawing.Point(100, 80);
            this.multiplyButton.Name = "multiplyButton";
            this.multiplyButton.Size = new System.Drawing.Size(87, 32);
            this.multiplyButton.TabIndex = 8;
            this.multiplyButton.Text = "Multiply";
            this.multiplyButton.UseVisualStyleBackColor = true;
            this.multiplyButton.Click += new System.EventHandler(this.multiplyButton_Click);
            // 
            // lightenButton
            // 
            this.lightenButton.Location = new System.Drawing.Point(6, 116);
            this.lightenButton.Name = "lightenButton";
            this.lightenButton.Size = new System.Drawing.Size(87, 32);
            this.lightenButton.TabIndex = 7;
            this.lightenButton.Text = "Lighten";
            this.lightenButton.UseVisualStyleBackColor = true;
            this.lightenButton.Click += new System.EventHandler(this.lightenButton_Click);
            // 
            // darkenButton
            // 
            this.darkenButton.Location = new System.Drawing.Point(100, 116);
            this.darkenButton.Name = "darkenButton";
            this.darkenButton.Size = new System.Drawing.Size(87, 32);
            this.darkenButton.TabIndex = 6;
            this.darkenButton.Text = "Darken";
            this.darkenButton.UseVisualStyleBackColor = true;
            this.darkenButton.Click += new System.EventHandler(this.darkenButton_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.splitVerticalRadio);
            this.panel2.Controls.Add(this.splitHorizontalRadio);
            this.panel2.Controls.Add(this.wipeVerticalRadio);
            this.panel2.Controls.Add(this.wipeHorizontalRadio);
            this.panel2.Controls.Add(this.fadeRadio);
            this.panel2.Location = new System.Drawing.Point(193, 76);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(156, 142);
            this.panel2.TabIndex = 5;
            // 
            // swipeVerticalRadio
            // 
            this.splitVerticalRadio.AutoSize = true;
            this.splitVerticalRadio.Location = new System.Drawing.Point(3, 114);
            this.splitVerticalRadio.Name = "swipeVerticalRadio";
            this.splitVerticalRadio.Size = new System.Drawing.Size(127, 21);
            this.splitVerticalRadio.TabIndex = 4;
            this.splitVerticalRadio.TabStop = true;
            this.splitVerticalRadio.Text = "Swipe (Vertical)";
            this.splitVerticalRadio.UseVisualStyleBackColor = true;
            // 
            // swipeHorizontalRadio
            // 
            this.splitHorizontalRadio.AutoSize = true;
            this.splitHorizontalRadio.Location = new System.Drawing.Point(3, 86);
            this.splitHorizontalRadio.Name = "swipeHorizontalRadio";
            this.splitHorizontalRadio.Size = new System.Drawing.Size(144, 21);
            this.splitHorizontalRadio.TabIndex = 3;
            this.splitHorizontalRadio.TabStop = true;
            this.splitHorizontalRadio.Text = "Swipe (Horizontal)";
            this.splitHorizontalRadio.UseVisualStyleBackColor = true;
            // 
            // wipeVerticalRadio
            // 
            this.wipeVerticalRadio.AutoSize = true;
            this.wipeVerticalRadio.Location = new System.Drawing.Point(3, 58);
            this.wipeVerticalRadio.Name = "wipeVerticalRadio";
            this.wipeVerticalRadio.Size = new System.Drawing.Size(122, 21);
            this.wipeVerticalRadio.TabIndex = 2;
            this.wipeVerticalRadio.TabStop = true;
            this.wipeVerticalRadio.Text = "Wipe (Vertical)";
            this.wipeVerticalRadio.UseVisualStyleBackColor = true;
            // 
            // wipeHorizontalRadio
            // 
            this.wipeHorizontalRadio.AutoSize = true;
            this.wipeHorizontalRadio.Location = new System.Drawing.Point(3, 30);
            this.wipeHorizontalRadio.Name = "wipeHorizontalRadio";
            this.wipeHorizontalRadio.Size = new System.Drawing.Size(139, 21);
            this.wipeHorizontalRadio.TabIndex = 1;
            this.wipeHorizontalRadio.Text = "Wipe (Horizontal)";
            this.wipeHorizontalRadio.UseVisualStyleBackColor = true;
            // 
            // fadeRadio
            // 
            this.fadeRadio.AutoSize = true;
            this.fadeRadio.Checked = true;
            this.fadeRadio.Location = new System.Drawing.Point(3, 3);
            this.fadeRadio.Name = "fadeRadio";
            this.fadeRadio.Size = new System.Drawing.Size(61, 21);
            this.fadeRadio.TabIndex = 0;
            this.fadeRadio.TabStop = true;
            this.fadeRadio.Text = "Fade";
            this.fadeRadio.UseVisualStyleBackColor = true;
            // 
            // transitionButton
            // 
            this.transitionButton.Location = new System.Drawing.Point(262, 224);
            this.transitionButton.Name = "transitionButton";
            this.transitionButton.Size = new System.Drawing.Size(87, 32);
            this.transitionButton.TabIndex = 4;
            this.transitionButton.Text = "Transition";
            this.transitionButton.UseVisualStyleBackColor = true;
            this.transitionButton.Click += new System.EventHandler(this.transitionButton_Click);
            // 
            // screenButton
            // 
            this.screenButton.Location = new System.Drawing.Point(7, 80);
            this.screenButton.Name = "screenButton";
            this.screenButton.Size = new System.Drawing.Size(87, 32);
            this.screenButton.TabIndex = 3;
            this.screenButton.Text = "Screen";
            this.screenButton.UseVisualStyleBackColor = true;
            this.screenButton.Click += new System.EventHandler(this.screenButton_Click);
            // 
            // opacityButton
            // 
            this.opacityButton.Location = new System.Drawing.Point(7, 44);
            this.opacityButton.Name = "opacityButton";
            this.opacityButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.opacityButton.Size = new System.Drawing.Size(87, 32);
            this.opacityButton.TabIndex = 2;
            this.opacityButton.Text = "Opacity";
            this.opacityButton.UseVisualStyleBackColor = true;
            this.opacityButton.Click += new System.EventHandler(this.opacityButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(100, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Opacity = 50%";
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(6, 3);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(286, 56);
            this.trackBar1.TabIndex = 0;
            this.trackBar1.Value = 50;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(13, 629);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(668, 23);
            this.progressBar1.TabIndex = 4;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 664);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form2";
            this.Text = "Transitions";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton wipeHorizontalRadio;
        private System.Windows.Forms.RadioButton fadeRadio;
        private System.Windows.Forms.Button transitionButton;
        private System.Windows.Forms.Button screenButton;
        private System.Windows.Forms.Button opacityButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button overlayButton;
        private System.Windows.Forms.Button subtractButton;
        private System.Windows.Forms.Button additionButton;
        private System.Windows.Forms.Button multiplyButton;
        private System.Windows.Forms.Button lightenButton;
        private System.Windows.Forms.Button darkenButton;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button softLightButton;
        private System.Windows.Forms.Button hardLightButton;
        private System.Windows.Forms.Button differenceButton;
        private System.Windows.Forms.RadioButton splitVerticalRadio;
        private System.Windows.Forms.RadioButton splitHorizontalRadio;
        private System.Windows.Forms.RadioButton wipeVerticalRadio;
    }
}