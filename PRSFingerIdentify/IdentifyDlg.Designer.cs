namespace PRSFingerIdentify
{
    partial class IdentifyDlg
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
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.lblIdentifiedUser = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.SuspendLayout();
            // 
            // pbImage
            // 
            this.pbImage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbImage.Location = new System.Drawing.Point(40, 88);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(237, 263);
            this.pbImage.TabIndex = 0;
            this.pbImage.TabStop = false;
            // 
            // lblInfo
            // 
            this.lblInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo.Location = new System.Drawing.Point(12, 60);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(330, 25);
            this.lblInfo.TabIndex = 1;
            this.lblInfo.Text = "Press finger for identification";
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblIdentifiedUser
            // 
            this.lblIdentifiedUser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblIdentifiedUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIdentifiedUser.Location = new System.Drawing.Point(40, 37);
            this.lblIdentifiedUser.Name = "lblIdentifiedUser";
            this.lblIdentifiedUser.Size = new System.Drawing.Size(237, 23);
            this.lblIdentifiedUser.TabIndex = 2;
            this.lblIdentifiedUser.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblIdentifiedUser.Visible = false;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(121, 12);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // IdentifyDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 363);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.lblIdentifiedUser);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.pbImage);
            this.Name = "IdentifyDlg";
            this.Text = "PRS Identification";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.IdentifyDlg_FormClosed);
            this.Load += new System.EventHandler(this.IdentifyDlg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label lblIdentifiedUser;
        private System.Windows.Forms.Button btnRefresh;
    }
}