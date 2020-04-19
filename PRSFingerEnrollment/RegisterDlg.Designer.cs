namespace PRSFingerIdentify
{
    partial class RegisterDlg
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.pb1 = new System.Windows.Forms.PictureBox();
            this.pb2 = new System.Windows.Forms.PictureBox();
            this.pb3 = new System.Windows.Forms.PictureBox();
            this.pb4 = new System.Windows.Forms.PictureBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.rbLeftIndex = new System.Windows.Forms.RadioButton();
            this.rbRightIndex = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnReset = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pb1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb4)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Computer #:";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(79, 27);
            this.tbName.MaxLength = 7;
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(192, 20);
            this.tbName.TabIndex = 1;
            this.tbName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbName_KeyPress);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(277, 23);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(96, 27);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "ENROLL";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // pb1
            // 
            this.pb1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pb1.Location = new System.Drawing.Point(11, 108);
            this.pb1.Name = "pb1";
            this.pb1.Size = new System.Drawing.Size(160, 187);
            this.pb1.TabIndex = 3;
            this.pb1.TabStop = false;
            // 
            // pb2
            // 
            this.pb2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pb2.Location = new System.Drawing.Point(191, 108);
            this.pb2.Name = "pb2";
            this.pb2.Size = new System.Drawing.Size(160, 187);
            this.pb2.TabIndex = 4;
            this.pb2.TabStop = false;
            // 
            // pb3
            // 
            this.pb3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pb3.Location = new System.Drawing.Point(371, 108);
            this.pb3.Name = "pb3";
            this.pb3.Size = new System.Drawing.Size(160, 187);
            this.pb3.TabIndex = 5;
            this.pb3.TabStop = false;
            // 
            // pb4
            // 
            this.pb4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pb4.Location = new System.Drawing.Point(551, 108);
            this.pb4.Name = "pb4";
            this.pb4.Size = new System.Drawing.Size(160, 187);
            this.pb4.TabIndex = 6;
            this.pb4.TabStop = false;
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo.Location = new System.Drawing.Point(241, 76);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(290, 17);
            this.lblInfo.TabIndex = 7;
            this.lblInfo.Text = "Press the same finger 4 or more times.";
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rbLeftIndex
            // 
            this.rbLeftIndex.AutoSize = true;
            this.rbLeftIndex.Location = new System.Drawing.Point(11, 56);
            this.rbLeftIndex.Name = "rbLeftIndex";
            this.rbLeftIndex.Size = new System.Drawing.Size(72, 17);
            this.rbLeftIndex.TabIndex = 8;
            this.rbLeftIndex.TabStop = true;
            this.rbLeftIndex.Text = "Left Index";
            this.rbLeftIndex.UseVisualStyleBackColor = true;
            this.rbLeftIndex.Visible = false;
            // 
            // rbRightIndex
            // 
            this.rbRightIndex.AutoSize = true;
            this.rbRightIndex.Location = new System.Drawing.Point(92, 56);
            this.rbRightIndex.Name = "rbRightIndex";
            this.rbRightIndex.Size = new System.Drawing.Size(79, 17);
            this.rbRightIndex.TabIndex = 9;
            this.rbRightIndex.TabStop = true;
            this.rbRightIndex.Text = "Right Index";
            this.rbRightIndex.UseVisualStyleBackColor = true;
            this.rbRightIndex.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnReset);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.lblInfo);
            this.groupBox1.Controls.Add(this.tbName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.rbRightIndex);
            this.groupBox1.Controls.Add(this.rbLeftIndex);
            this.groupBox1.Controls.Add(this.pb1);
            this.groupBox1.Controls.Add(this.pb2);
            this.groupBox1.Controls.Add(this.pb3);
            this.groupBox1.Controls.Add(this.pb4);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(723, 371);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Finger Enrollment";
            // 
            // btnReset
            // 
            this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Location = new System.Drawing.Point(389, 23);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(96, 27);
            this.btnReset.TabIndex = 10;
            this.btnReset.Text = "RESET";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // RegisterDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 387);
            this.Controls.Add(this.groupBox1);
            this.Name = "RegisterDlg";
            this.Text = "Fingerprint Enrollment";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RegisterDlg_FormClosed);
            this.Load += new System.EventHandler(this.Register_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pb1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb4)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.PictureBox pb1;
        private System.Windows.Forms.PictureBox pb2;
        private System.Windows.Forms.PictureBox pb3;
        private System.Windows.Forms.PictureBox pb4;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.RadioButton rbLeftIndex;
        private System.Windows.Forms.RadioButton rbRightIndex;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnReset;
    }
}