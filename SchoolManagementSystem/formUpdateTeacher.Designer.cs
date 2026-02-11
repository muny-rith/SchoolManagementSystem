namespace SchoolManagementSystem
{
    partial class formUpdateTeacher
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
            pic = new PictureBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            btnYes = new Button();
            btnOk = new Button();
            btnCancel = new Button();
            txtNameEng = new TextBox();
            txtNameKh = new TextBox();
            txtAddress = new TextBox();
            txtPhone = new TextBox();
            cbSex = new ComboBox();
            panelPic = new Panel();
            btnSelectImage = new Button();
            ((System.ComponentModel.ISupportInitialize)pic).BeginInit();
            panelPic.SuspendLayout();
            SuspendLayout();
            // 
            // pic
            // 
            pic.Location = new Point(4, 5);
            pic.Name = "pic";
            pic.Size = new Size(186, 228);
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
            pic.TabIndex = 0;
            pic.TabStop = false;
            pic.MouseHover += doShowBtnSelect;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(237, 12);
            label1.Name = "label1";
            label1.Size = new Size(74, 20);
            label1.TabIndex = 0;
            label1.Text = "NameEng";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(237, 66);
            label2.Name = "label2";
            label2.Size = new Size(66, 20);
            label2.TabIndex = 0;
            label2.Text = "NameKh";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(237, 111);
            label3.Name = "label3";
            label3.Size = new Size(32, 20);
            label3.TabIndex = 0;
            label3.Text = "Sex";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(237, 167);
            label4.Name = "label4";
            label4.Size = new Size(62, 20);
            label4.TabIndex = 0;
            label4.Text = "Address";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(237, 223);
            label5.Name = "label5";
            label5.Size = new Size(50, 20);
            label5.TabIndex = 0;
            label5.Text = "Phone";
            // 
            // btnYes
            // 
            btnYes.Location = new Point(49, 283);
            btnYes.Name = "btnYes";
            btnYes.Size = new Size(116, 31);
            btnYes.TabIndex = 6;
            btnYes.Text = "Yes";
            btnYes.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            btnOk.Location = new Point(200, 283);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(116, 31);
            btnOk.TabIndex = 7;
            btnOk.Text = "Ok";
            btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(350, 283);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(116, 31);
            btnCancel.TabIndex = 8;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // txtNameEng
            // 
            txtNameEng.Location = new Point(328, 9);
            txtNameEng.Name = "txtNameEng";
            txtNameEng.Size = new Size(199, 27);
            txtNameEng.TabIndex = 1;
            // 
            // txtNameKh
            // 
            txtNameKh.Location = new Point(328, 63);
            txtNameKh.Name = "txtNameKh";
            txtNameKh.Size = new Size(199, 27);
            txtNameKh.TabIndex = 2;
            // 
            // txtAddress
            // 
            txtAddress.Location = new Point(328, 164);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(199, 27);
            txtAddress.TabIndex = 4;
            // 
            // txtPhone
            // 
            txtPhone.Location = new Point(328, 220);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(199, 27);
            txtPhone.TabIndex = 5;
            // 
            // cbSex
            // 
            cbSex.DropDownStyle = ComboBoxStyle.DropDownList;
            cbSex.FormattingEnabled = true;
            cbSex.Items.AddRange(new object[] { "male", "female" });
            cbSex.Location = new Point(328, 108);
            cbSex.Name = "cbSex";
            cbSex.Size = new Size(199, 28);
            cbSex.TabIndex = 3;
            // 
            // panelPic
            // 
            panelPic.BackColor = Color.SteelBlue;
            panelPic.Controls.Add(btnSelectImage);
            panelPic.Controls.Add(pic);
            panelPic.Location = new Point(25, 9);
            panelPic.Name = "panelPic";
            panelPic.Size = new Size(194, 238);
            panelPic.TabIndex = 21;
            // 
            // btnSelectImage
            // 
            btnSelectImage.BackColor = SystemColors.Control;
            btnSelectImage.FlatAppearance.BorderColor = Color.Silver;
            btnSelectImage.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnSelectImage.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnSelectImage.FlatStyle = FlatStyle.Flat;
            btnSelectImage.Font = new Font("Noto Sans", 9F);
            btnSelectImage.Location = new Point(4, 202);
            btnSelectImage.Name = "btnSelectImage";
            btnSelectImage.Size = new Size(186, 31);
            btnSelectImage.TabIndex = 21;
            btnSelectImage.TabStop = false;
            btnSelectImage.Text = "Select File";
            btnSelectImage.UseVisualStyleBackColor = false;
            btnSelectImage.Visible = false;
            // 
            // formUpdateTeacher
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(556, 341);
            Controls.Add(panelPic);
            Controls.Add(cbSex);
            Controls.Add(txtPhone);
            Controls.Add(txtAddress);
            Controls.Add(txtNameKh);
            Controls.Add(txtNameEng);
            Controls.Add(btnCancel);
            Controls.Add(btnOk);
            Controls.Add(btnYes);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "formUpdateTeacher";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "formUpdateTeachercs";
            DoubleClick += doShowChangeFile;
            ((System.ComponentModel.ISupportInitialize)pic).EndInit();
            panelPic.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pic;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Button btnYes;
        private Button btnOk;
        private Button btnCancel;
        private TextBox txtNameEng;
        private TextBox txtNameKh;
        private TextBox txtAddress;
        private TextBox txtPhone;
        private ComboBox cbSex;
        private Panel panelPic;
        private Button btnSelectImage;
    }
}