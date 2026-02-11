namespace SchoolManagementSystem
{
    partial class FormUpdateStudent
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
            picStudent = new PictureBox();
            panel1 = new Panel();
            labelStartDate = new Label();
            btnOk = new Button();
            cbStatus = new ComboBox();
            cbSex = new ComboBox();
            panelPic = new Panel();
            btnSelectImage = new Button();
            btnNo = new Button();
            btnYes = new Button();
            dtStartDate = new DateTimePicker();
            dtimeDOB = new DateTimePicker();
            txtPhone = new TextBox();
            txtFather = new TextBox();
            txtMother = new TextBox();
            txtMember = new TextBox();
            txtCurrentPlace = new TextBox();
            txtNameKh = new TextBox();
            txtNameEng = new TextBox();
            label9 = new Label();
            label12 = new Label();
            label14 = new Label();
            label10 = new Label();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label2 = new Label();
            label1 = new Label();
            lbNameEng = new Label();
            cbPOB = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)picStudent).BeginInit();
            panel1.SuspendLayout();
            panelPic.SuspendLayout();
            SuspendLayout();
            // 
            // picStudent
            // 
            picStudent.Cursor = Cursors.Hand;
            picStudent.ImageLocation = "";
            picStudent.Location = new Point(2, 2);
            picStudent.Margin = new Padding(3, 2, 3, 2);
            picStudent.Name = "picStudent";
            picStudent.Size = new Size(194, 212);
            picStudent.SizeMode = PictureBoxSizeMode.StretchImage;
            picStudent.TabIndex = 22;
            picStudent.TabStop = false;
            picStudent.DoubleClick += doChangeFilePic;
            picStudent.MouseHover += doShowBtnSelect;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Transparent;
            panel1.Controls.Add(cbPOB);
            panel1.Controls.Add(labelStartDate);
            panel1.Controls.Add(btnOk);
            panel1.Controls.Add(cbStatus);
            panel1.Controls.Add(cbSex);
            panel1.Controls.Add(panelPic);
            panel1.Controls.Add(btnNo);
            panel1.Controls.Add(btnYes);
            panel1.Controls.Add(dtStartDate);
            panel1.Controls.Add(dtimeDOB);
            panel1.Controls.Add(txtPhone);
            panel1.Controls.Add(txtFather);
            panel1.Controls.Add(txtMother);
            panel1.Controls.Add(txtMember);
            panel1.Controls.Add(txtCurrentPlace);
            panel1.Controls.Add(txtNameKh);
            panel1.Controls.Add(txtNameEng);
            panel1.Controls.Add(label9);
            panel1.Controls.Add(label12);
            panel1.Controls.Add(label14);
            panel1.Controls.Add(label10);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(lbNameEng);
            panel1.Location = new Point(24, 27);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(553, 434);
            panel1.TabIndex = 1;
            panel1.MouseHover += doCloseBtnSelect;
            // 
            // labelStartDate
            // 
            labelStartDate.AutoSize = true;
            labelStartDate.Location = new Point(225, 129);
            labelStartDate.Name = "labelStartDate";
            labelStartDate.Size = new Size(67, 15);
            labelStartDate.TabIndex = 23;
            labelStartDate.Text = "Start Date : ";
            // 
            // btnOk
            // 
            btnOk.FlatStyle = FlatStyle.System;
            btnOk.Font = new Font("Noto Sans", 9F);
            btnOk.Location = new Point(187, 362);
            btnOk.Margin = new Padding(3, 2, 3, 2);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(154, 36);
            btnOk.TabIndex = 21;
            btnOk.Text = "Ok";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Visible = false;
            // 
            // cbStatus
            // 
            cbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cbStatus.Font = new Font("Noto Sans", 9F);
            cbStatus.FormattingEnabled = true;
            cbStatus.Items.AddRange(new object[] { "own", "rent" });
            cbStatus.Location = new Point(490, 237);
            cbStatus.Margin = new Padding(3, 2, 3, 2);
            cbStatus.Name = "cbStatus";
            cbStatus.Size = new Size(61, 26);
            cbStatus.TabIndex = 8;
            cbStatus.Visible = false;
            // 
            // cbSex
            // 
            cbSex.DropDownStyle = ComboBoxStyle.DropDownList;
            cbSex.Font = new Font("Noto Sans", 9F);
            cbSex.FormattingEnabled = true;
            cbSex.Items.AddRange(new object[] { "male", "female" });
            cbSex.Location = new Point(290, 89);
            cbSex.Margin = new Padding(3, 2, 3, 2);
            cbSex.Name = "cbSex";
            cbSex.Size = new Size(77, 26);
            cbSex.TabIndex = 3;
            // 
            // panelPic
            // 
            panelPic.BackColor = Color.SteelBlue;
            panelPic.Controls.Add(btnSelectImage);
            panelPic.Controls.Add(picStudent);
            panelPic.Location = new Point(9, 9);
            panelPic.Margin = new Padding(3, 2, 3, 2);
            panelPic.Name = "panelPic";
            panelPic.Size = new Size(198, 215);
            panelPic.TabIndex = 20;
            panelPic.MouseLeave += doCloseBtnSelect;
            panelPic.MouseHover += doShowBtnSelect;
            // 
            // btnSelectImage
            // 
            btnSelectImage.BackColor = SystemColors.Control;
            btnSelectImage.FlatAppearance.BorderColor = Color.Silver;
            btnSelectImage.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnSelectImage.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnSelectImage.FlatStyle = FlatStyle.Flat;
            btnSelectImage.Font = new Font("Noto Sans", 9F);
            btnSelectImage.Location = new Point(2, 190);
            btnSelectImage.Margin = new Padding(3, 2, 3, 2);
            btnSelectImage.Name = "btnSelectImage";
            btnSelectImage.Size = new Size(194, 23);
            btnSelectImage.TabIndex = 21;
            btnSelectImage.Text = "Select File";
            btnSelectImage.UseVisualStyleBackColor = false;
            btnSelectImage.Visible = false;
            btnSelectImage.Click += doChangeFilePic;
            btnSelectImage.MouseLeave += doCloseEffect;
            btnSelectImage.MouseHover += doShowEffect;
            // 
            // btnNo
            // 
            btnNo.Font = new Font("Noto Sans", 9F);
            btnNo.Location = new Point(333, 368);
            btnNo.Margin = new Padding(3, 2, 3, 2);
            btnNo.Name = "btnNo";
            btnNo.Size = new Size(154, 36);
            btnNo.TabIndex = 15;
            btnNo.Text = "Cancel";
            btnNo.UseVisualStyleBackColor = true;
            // 
            // btnYes
            // 
            btnYes.Font = new Font("Noto Sans", 9F);
            btnYes.Location = new Point(62, 368);
            btnYes.Margin = new Padding(3, 2, 3, 2);
            btnYes.Name = "btnYes";
            btnYes.Size = new Size(154, 36);
            btnYes.TabIndex = 14;
            btnYes.Text = "Yes";
            btnYes.UseVisualStyleBackColor = true;
            // 
            // dtStartDate
            // 
            dtStartDate.CustomFormat = "dd- MMMM -yyyy";
            dtStartDate.Font = new Font("Noto Sans", 9F);
            dtStartDate.Format = DateTimePickerFormat.Custom;
            dtStartDate.ImeMode = ImeMode.NoControl;
            dtStartDate.Location = new Point(333, 124);
            dtStartDate.Margin = new Padding(3, 2, 3, 2);
            dtStartDate.Name = "dtStartDate";
            dtStartDate.Size = new Size(191, 24);
            dtStartDate.TabIndex = 5;
            // 
            // dtimeDOB
            // 
            dtimeDOB.CustomFormat = "dd- MMMM -yyyy";
            dtimeDOB.Font = new Font("Noto Sans", 9F);
            dtimeDOB.Format = DateTimePickerFormat.Custom;
            dtimeDOB.ImeMode = ImeMode.NoControl;
            dtimeDOB.Location = new Point(333, 162);
            dtimeDOB.Margin = new Padding(3, 2, 3, 2);
            dtimeDOB.Name = "dtimeDOB";
            dtimeDOB.Size = new Size(191, 24);
            dtimeDOB.TabIndex = 5;
            // 
            // txtPhone
            // 
            txtPhone.Font = new Font("Noto Sans", 9F);
            txtPhone.Location = new Point(114, 323);
            txtPhone.Margin = new Padding(3, 2, 3, 2);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(178, 24);
            txtPhone.TabIndex = 13;
            // 
            // txtFather
            // 
            txtFather.Font = new Font("Noto Sans", 9F);
            txtFather.Location = new Point(65, 281);
            txtFather.Margin = new Padding(3, 2, 3, 2);
            txtFather.Name = "txtFather";
            txtFather.Size = new Size(122, 24);
            txtFather.TabIndex = 9;
            // 
            // txtMother
            // 
            txtMother.Font = new Font("Noto Sans", 9F);
            txtMother.Location = new Point(293, 281);
            txtMother.Margin = new Padding(3, 2, 3, 2);
            txtMother.Name = "txtMother";
            txtMother.Size = new Size(122, 24);
            txtMother.TabIndex = 9;
            // 
            // txtMember
            // 
            txtMember.Font = new Font("Noto Sans", 9F);
            txtMember.Location = new Point(500, 281);
            txtMember.Margin = new Padding(3, 2, 3, 2);
            txtMember.Name = "txtMember";
            txtMember.Size = new Size(50, 24);
            txtMember.TabIndex = 10;
            // 
            // txtCurrentPlace
            // 
            txtCurrentPlace.Font = new Font("Noto Sans", 9F);
            txtCurrentPlace.Location = new Point(108, 237);
            txtCurrentPlace.Margin = new Padding(3, 2, 3, 2);
            txtCurrentPlace.Name = "txtCurrentPlace";
            txtCurrentPlace.Size = new Size(205, 24);
            txtCurrentPlace.TabIndex = 7;
            // 
            // txtNameKh
            // 
            txtNameKh.Font = new Font("Noto Sans", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtNameKh.Location = new Point(290, 51);
            txtNameKh.Margin = new Padding(3, 2, 3, 2);
            txtNameKh.Name = "txtNameKh";
            txtNameKh.Size = new Size(166, 24);
            txtNameKh.TabIndex = 2;
            // 
            // txtNameEng
            // 
            txtNameEng.Font = new Font("Noto Sans", 9F);
            txtNameEng.Location = new Point(290, 8);
            txtNameEng.Margin = new Padding(3, 2, 3, 2);
            txtNameEng.Name = "txtNameEng";
            txtNameEng.Size = new Size(166, 24);
            txtNameEng.TabIndex = 1;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Noto Sans", 9F);
            label9.Location = new Point(3, 326);
            label9.Name = "label9";
            label9.Size = new Size(96, 18);
            label9.TabIndex = 1;
            label9.Text = "Phone number :";
            label9.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Noto Sans", 9F);
            label12.Location = new Point(427, 284);
            label12.Name = "label12";
            label12.Size = new Size(62, 18);
            label12.TabIndex = 1;
            label12.Text = "Member :";
            label12.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Noto Sans", 9F);
            label14.Location = new Point(427, 239);
            label14.Name = "label14";
            label14.Size = new Size(49, 18);
            label14.TabIndex = 1;
            label14.Text = "Status :";
            label14.TextAlign = ContentAlignment.MiddleCenter;
            label14.Visible = false;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Noto Sans", 9F);
            label10.Location = new Point(225, 284);
            label10.Name = "label10";
            label10.Size = new Size(55, 18);
            label10.TabIndex = 1;
            label10.Text = "Mother :";
            label10.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Noto Sans", 9F);
            label7.Location = new Point(3, 284);
            label7.Name = "label7";
            label7.Size = new Size(50, 18);
            label7.TabIndex = 1;
            label7.Text = "Father :";
            label7.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Noto Sans", 9F);
            label6.Location = new Point(3, 239);
            label6.Name = "label6";
            label6.Size = new Size(84, 18);
            label6.TabIndex = 1;
            label6.Text = "Current Place";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Noto Sans", 9F);
            label5.Location = new Point(225, 202);
            label5.Name = "label5";
            label5.Size = new Size(88, 18);
            label5.TabIndex = 1;
            label5.Text = "Place of Birth :";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Noto Sans", 9F);
            label4.Location = new Point(225, 166);
            label4.Name = "label4";
            label4.Size = new Size(85, 18);
            label4.TabIndex = 1;
            label4.Text = "Date of Birth :";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Noto Sans", 9F);
            label2.Location = new Point(225, 92);
            label2.Name = "label2";
            label2.Size = new Size(34, 18);
            label2.TabIndex = 1;
            label2.Text = "Sex :";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Noto Sans", 9F);
            label1.Location = new Point(225, 52);
            label1.Name = "label1";
            label1.Size = new Size(46, 18);
            label1.TabIndex = 1;
            label1.Text = "ឈ្មោះ ៖";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            label1.Click += label1_Click;
            // 
            // lbNameEng
            // 
            lbNameEng.AutoSize = true;
            lbNameEng.Font = new Font("Noto Sans", 9F);
            lbNameEng.Location = new Point(225, 10);
            lbNameEng.Name = "lbNameEng";
            lbNameEng.Size = new Size(48, 18);
            lbNameEng.TabIndex = 1;
            lbNameEng.Text = "Name :";
            lbNameEng.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cbPOB
            // 
            cbPOB.FormattingEnabled = true;
            cbPOB.Location = new Point(333, 199);
            cbPOB.Name = "cbPOB";
            cbPOB.Size = new Size(191, 23);
            cbPOB.TabIndex = 24;
            // 
            // FormUpdateStudent
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(602, 474);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            Name = "FormUpdateStudent";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Update Information";
            ((System.ComponentModel.ISupportInitialize)picStudent).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panelPic.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private Panel panel1;
        private Label label1;
        private Label lbNameEng;
        private Label label5;
        private Label label4;
        private Label label2;
        private Label label9;
        private Label label12;
        private Label label10;
        private Label label7;
        private Label label6;
        private TextBox txtPhone;
        private TextBox txtFather;
        private TextBox txtMother;
        private TextBox txtMember;
        private TextBox txtCurrentPlace;
        private TextBox txtNameKh;
        private TextBox txtNameEng;
        private Label label14;
        private DateTimePicker dtimeDOB;
        private Button btnNo;
        private Button btnYes;
        public PictureBox picStudent;
        private Button btnSelectImage;
        private Panel panelPic;
        private ComboBox comboBox1;
        private ComboBox cbSex;
        private ComboBox cbStatus;
        private Button btnOk;
        private Label labelStartDate;
        private DateTimePicker dtStartDate;
        private ComboBox cbPOB;
    }
}