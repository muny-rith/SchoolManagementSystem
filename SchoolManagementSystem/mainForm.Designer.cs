using System.Runtime.CompilerServices;
//using ComponentFactory.Krypton.Toolkit;

namespace SchoolManagementSystem
{
    partial class mainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            panelSidbar = new Panel();
            btnBackup = new Button();
            lbSection4 = new Label();
            label1 = new Label();
            lbSection2 = new Label();
            lbSection1 = new Label();
            btnLogout = new Button();
            panel4 = new Panel();
            panel2 = new Panel();
            panel1 = new Panel();
            pLine1 = new Panel();
            lbNameSchool = new Label();
            pictureLogo = new PictureBox();
            btnDashboard = new Button();
            btnSalary = new Button();
            btnPayroll = new Button();
            btnPaymentStudents = new Button();
            btnInvoiceStudent = new Button();
            btnTeacherClass = new Button();
            btnTeacher = new Button();
            btnGradeStudents = new Button();
            btnGrade = new Button();
            btnStudent = new Button();
            panelMain = new Panel();
            panelSidbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureLogo).BeginInit();
            SuspendLayout();
            // 
            // panelSidbar
            // 
            panelSidbar.AutoSize = true;
            panelSidbar.BackColor = Color.White;
            panelSidbar.Controls.Add(btnBackup);
            panelSidbar.Controls.Add(lbSection4);
            panelSidbar.Controls.Add(label1);
            panelSidbar.Controls.Add(lbSection2);
            panelSidbar.Controls.Add(lbSection1);
            panelSidbar.Controls.Add(btnLogout);
            panelSidbar.Controls.Add(panel4);
            panelSidbar.Controls.Add(panel2);
            panelSidbar.Controls.Add(panel1);
            panelSidbar.Controls.Add(pLine1);
            panelSidbar.Controls.Add(lbNameSchool);
            panelSidbar.Controls.Add(pictureLogo);
            panelSidbar.Controls.Add(btnDashboard);
            panelSidbar.Controls.Add(btnSalary);
            panelSidbar.Controls.Add(btnPayroll);
            panelSidbar.Controls.Add(btnPaymentStudents);
            panelSidbar.Controls.Add(btnInvoiceStudent);
            panelSidbar.Controls.Add(btnTeacherClass);
            panelSidbar.Controls.Add(btnTeacher);
            panelSidbar.Controls.Add(btnGradeStudents);
            panelSidbar.Controls.Add(btnGrade);
            panelSidbar.Controls.Add(btnStudent);
            panelSidbar.Dock = DockStyle.Left;
            panelSidbar.Font = new Font("Roboto Black", 9F, FontStyle.Bold);
            panelSidbar.ForeColor = Color.Black;
            panelSidbar.Location = new Point(0, 0);
            panelSidbar.MinimumSize = new Size(250, 663);
            panelSidbar.Name = "panelSidbar";
            panelSidbar.Size = new Size(250, 663);
            panelSidbar.TabIndex = 1;
            panelSidbar.Paint += panelSidbar_Paint;
            // 
            // btnBackup
            // 
            btnBackup.FlatAppearance.BorderSize = 0;
            btnBackup.FlatStyle = FlatStyle.Flat;
            btnBackup.Location = new Point(37, 133);
            btnBackup.Name = "btnBackup";
            btnBackup.Padding = new Padding(50, 0, 50, 0);
            btnBackup.Size = new Size(176, 25);
            btnBackup.TabIndex = 8;
            btnBackup.Text = "BackUp";
            btnBackup.TextAlign = ContentAlignment.MiddleLeft;
            btnBackup.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnBackup.UseVisualStyleBackColor = true;
            btnBackup.UseWaitCursor = true;
            btnBackup.Visible = false;
            // 
            // lbSection4
            // 
            lbSection4.AutoSize = true;
            lbSection4.Font = new Font("Roboto Black", 9F, FontStyle.Bold);
            lbSection4.ForeColor = Color.Black;
            lbSection4.Location = new Point(14, 480);
            lbSection4.Name = "lbSection4";
            lbSection4.Size = new Size(55, 14);
            lbSection4.TabIndex = 9;
            lbSection4.Text = "Financial";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Roboto Black", 9F, FontStyle.Bold);
            label1.ForeColor = Color.Black;
            label1.Location = new Point(14, 293);
            label1.Name = "label1";
            label1.Size = new Size(92, 14);
            label1.TabIndex = 9;
            label1.Text = "Class - Teacher";
            // 
            // lbSection2
            // 
            lbSection2.AutoSize = true;
            lbSection2.BackColor = Color.Transparent;
            lbSection2.Font = new Font("Roboto Black", 9F, FontStyle.Bold);
            lbSection2.ForeColor = Color.Black;
            lbSection2.Location = new Point(14, 227);
            lbSection2.Name = "lbSection2";
            lbSection2.Size = new Size(51, 14);
            lbSection2.TabIndex = 9;
            lbSection2.Text = "Student";
            // 
            // lbSection1
            // 
            lbSection1.AutoSize = true;
            lbSection1.BackColor = Color.Transparent;
            lbSection1.Font = new Font("Roboto Black", 9F, FontStyle.Bold);
            lbSection1.ForeColor = Color.Black;
            lbSection1.Location = new Point(14, 161);
            lbSection1.Margin = new Padding(0);
            lbSection1.Name = "lbSection1";
            lbSection1.Size = new Size(34, 14);
            lbSection1.TabIndex = 9;
            lbSection1.Text = "Main";
            // 
            // btnLogout
            // 
            btnLogout.BackColor = Color.Gainsboro;
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.Image = (Image)resources.GetObject("btnLogout.Image");
            btnLogout.Location = new Point(164, 133);
            btnLogout.Name = "btnLogout";
            btnLogout.Padding = new Padding(50, 0, 50, 0);
            btnLogout.Size = new Size(74, 27);
            btnLogout.TabIndex = 8;
            btnLogout.Text = "    Log Out";
            btnLogout.TextAlign = ContentAlignment.MiddleLeft;
            btnLogout.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnLogout.UseVisualStyleBackColor = false;
            btnLogout.Visible = false;
            // 
            // panel4
            // 
            panel4.BackColor = Color.Black;
            panel4.Location = new Point(90, 489);
            panel4.Name = "panel4";
            panel4.Size = new Size(150, 2);
            panel4.TabIndex = 7;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Black;
            panel2.Location = new Point(134, 301);
            panel2.Name = "panel2";
            panel2.Size = new Size(106, 1);
            panel2.TabIndex = 7;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Black;
            panel1.Location = new Point(90, 234);
            panel1.Name = "panel1";
            panel1.Size = new Size(150, 2);
            panel1.TabIndex = 7;
            // 
            // pLine1
            // 
            pLine1.BackColor = Color.Black;
            pLine1.Location = new Point(90, 170);
            pLine1.Name = "pLine1";
            pLine1.Size = new Size(150, 2);
            pLine1.TabIndex = 7;
            // 
            // lbNameSchool
            // 
            lbNameSchool.Font = new Font("Poppins", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbNameSchool.Location = new Point(46, 115);
            lbNameSchool.Name = "lbNameSchool";
            lbNameSchool.Size = new Size(156, 22);
            lbNameSchool.TabIndex = 6;
            lbNameSchool.Text = "SIS Management System";
            lbNameSchool.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pictureLogo
            // 
            pictureLogo.Dock = DockStyle.Top;
            pictureLogo.Image = Properties.Resources.logo;
            pictureLogo.Location = new Point(0, 0);
            pictureLogo.Name = "pictureLogo";
            pictureLogo.Size = new Size(250, 109);
            pictureLogo.SizeMode = PictureBoxSizeMode.Zoom;
            pictureLogo.TabIndex = 5;
            pictureLogo.TabStop = false;
            // 
            // btnDashboard
            // 
            btnDashboard.FlatAppearance.BorderSize = 0;
            btnDashboard.FlatAppearance.MouseDownBackColor = Color.LightSkyBlue;
            btnDashboard.FlatAppearance.MouseOverBackColor = Color.LightBlue;
            btnDashboard.FlatStyle = FlatStyle.Flat;
            btnDashboard.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnDashboard.ForeColor = Color.FromArgb(64, 64, 64);
            btnDashboard.Location = new Point(8, 187);
            btnDashboard.Name = "btnDashboard";
            btnDashboard.Size = new Size(230, 35);
            btnDashboard.TabIndex = 1;
            btnDashboard.Text = "   Dashborad";
            btnDashboard.TextAlign = ContentAlignment.MiddleLeft;
            btnDashboard.UseVisualStyleBackColor = true;
            // 
            // btnSalary
            // 
            btnSalary.FlatAppearance.BorderSize = 0;
            btnSalary.FlatAppearance.MouseDownBackColor = Color.SkyBlue;
            btnSalary.FlatAppearance.MouseOverBackColor = Color.LightBlue;
            btnSalary.FlatStyle = FlatStyle.Flat;
            btnSalary.Font = new Font("Roboto", 12F);
            btnSalary.ForeColor = Color.FromArgb(64, 64, 64);
            btnSalary.Location = new Point(8, 617);
            btnSalary.Name = "btnSalary";
            btnSalary.Size = new Size(230, 35);
            btnSalary.TabIndex = 4;
            btnSalary.Text = "   Salary";
            btnSalary.TextAlign = ContentAlignment.MiddleLeft;
            btnSalary.UseVisualStyleBackColor = true;
            // 
            // btnPayroll
            // 
            btnPayroll.FlatAppearance.BorderSize = 0;
            btnPayroll.FlatAppearance.MouseDownBackColor = Color.SkyBlue;
            btnPayroll.FlatAppearance.MouseOverBackColor = Color.LightBlue;
            btnPayroll.FlatStyle = FlatStyle.Flat;
            btnPayroll.Font = new Font("Roboto", 12F);
            btnPayroll.ForeColor = Color.FromArgb(64, 64, 64);
            btnPayroll.Location = new Point(8, 579);
            btnPayroll.Name = "btnPayroll";
            btnPayroll.Size = new Size(230, 35);
            btnPayroll.TabIndex = 4;
            btnPayroll.Text = "   Payroll";
            btnPayroll.TextAlign = ContentAlignment.MiddleLeft;
            btnPayroll.UseVisualStyleBackColor = true;
            // 
            // btnPaymentStudents
            // 
            btnPaymentStudents.FlatAppearance.BorderSize = 0;
            btnPaymentStudents.FlatAppearance.MouseDownBackColor = Color.SkyBlue;
            btnPaymentStudents.FlatAppearance.MouseOverBackColor = Color.LightBlue;
            btnPaymentStudents.FlatStyle = FlatStyle.Flat;
            btnPaymentStudents.Font = new Font("Roboto", 12F);
            btnPaymentStudents.ForeColor = Color.FromArgb(64, 64, 64);
            btnPaymentStudents.Location = new Point(8, 543);
            btnPaymentStudents.Name = "btnPaymentStudents";
            btnPaymentStudents.Size = new Size(230, 35);
            btnPaymentStudents.TabIndex = 4;
            btnPaymentStudents.Text = "   Students";
            btnPaymentStudents.TextAlign = ContentAlignment.MiddleLeft;
            btnPaymentStudents.UseVisualStyleBackColor = true;
            // 
            // btnInvoiceStudent
            // 
            btnInvoiceStudent.FlatAppearance.BorderSize = 0;
            btnInvoiceStudent.FlatAppearance.MouseDownBackColor = Color.SkyBlue;
            btnInvoiceStudent.FlatAppearance.MouseOverBackColor = Color.LightBlue;
            btnInvoiceStudent.FlatStyle = FlatStyle.Flat;
            btnInvoiceStudent.Font = new Font("Roboto", 12F);
            btnInvoiceStudent.ForeColor = Color.FromArgb(64, 64, 64);
            btnInvoiceStudent.Location = new Point(8, 508);
            btnInvoiceStudent.Name = "btnInvoiceStudent";
            btnInvoiceStudent.Size = new Size(230, 35);
            btnInvoiceStudent.TabIndex = 4;
            btnInvoiceStudent.Text = "   Invoice";
            btnInvoiceStudent.TextAlign = ContentAlignment.MiddleLeft;
            btnInvoiceStudent.UseVisualStyleBackColor = true;
            // 
            // btnTeacherClass
            // 
            btnTeacherClass.FlatAppearance.BorderSize = 0;
            btnTeacherClass.FlatAppearance.MouseDownBackColor = Color.SkyBlue;
            btnTeacherClass.FlatAppearance.MouseOverBackColor = Color.LightBlue;
            btnTeacherClass.FlatStyle = FlatStyle.Flat;
            btnTeacherClass.Font = new Font("Roboto", 12F);
            btnTeacherClass.ForeColor = Color.FromArgb(64, 64, 64);
            btnTeacherClass.Location = new Point(8, 402);
            btnTeacherClass.Name = "btnTeacherClass";
            btnTeacherClass.Size = new Size(230, 35);
            btnTeacherClass.TabIndex = 3;
            btnTeacherClass.Text = "   Class && Teacher";
            btnTeacherClass.TextAlign = ContentAlignment.MiddleLeft;
            btnTeacherClass.UseVisualStyleBackColor = true;
            // 
            // btnTeacher
            // 
            btnTeacher.FlatAppearance.BorderSize = 0;
            btnTeacher.FlatAppearance.MouseDownBackColor = Color.SkyBlue;
            btnTeacher.FlatAppearance.MouseOverBackColor = Color.LightBlue;
            btnTeacher.FlatStyle = FlatStyle.Flat;
            btnTeacher.Font = new Font("Roboto", 12F);
            btnTeacher.ForeColor = Color.FromArgb(64, 64, 64);
            btnTeacher.Location = new Point(8, 363);
            btnTeacher.Name = "btnTeacher";
            btnTeacher.Size = new Size(230, 35);
            btnTeacher.TabIndex = 3;
            btnTeacher.Text = "   Teacher";
            btnTeacher.TextAlign = ContentAlignment.MiddleLeft;
            btnTeacher.UseVisualStyleBackColor = true;
            // 
            // btnGradeStudents
            // 
            btnGradeStudents.FlatAppearance.BorderSize = 0;
            btnGradeStudents.FlatAppearance.MouseDownBackColor = Color.SkyBlue;
            btnGradeStudents.FlatAppearance.MouseOverBackColor = Color.LightBlue;
            btnGradeStudents.FlatStyle = FlatStyle.Flat;
            btnGradeStudents.Font = new Font("Roboto", 12F);
            btnGradeStudents.ForeColor = Color.FromArgb(64, 64, 64);
            btnGradeStudents.Location = new Point(8, 442);
            btnGradeStudents.Name = "btnGradeStudents";
            btnGradeStudents.Size = new Size(230, 35);
            btnGradeStudents.TabIndex = 3;
            btnGradeStudents.Text = "   Students";
            btnGradeStudents.TextAlign = ContentAlignment.MiddleLeft;
            btnGradeStudents.UseVisualStyleBackColor = true;
            // 
            // btnGrade
            // 
            btnGrade.FlatAppearance.BorderSize = 0;
            btnGrade.FlatAppearance.MouseDownBackColor = Color.SkyBlue;
            btnGrade.FlatAppearance.MouseOverBackColor = Color.LightBlue;
            btnGrade.FlatStyle = FlatStyle.Flat;
            btnGrade.Font = new Font("Roboto", 12F);
            btnGrade.ForeColor = Color.FromArgb(64, 64, 64);
            btnGrade.Location = new Point(8, 323);
            btnGrade.Name = "btnGrade";
            btnGrade.Size = new Size(230, 35);
            btnGrade.TabIndex = 3;
            btnGrade.Text = "   Class";
            btnGrade.TextAlign = ContentAlignment.MiddleLeft;
            btnGrade.UseVisualStyleBackColor = true;
            // 
            // btnStudent
            // 
            btnStudent.FlatAppearance.BorderSize = 0;
            btnStudent.FlatAppearance.MouseDownBackColor = Color.SkyBlue;
            btnStudent.FlatAppearance.MouseOverBackColor = Color.LightBlue;
            btnStudent.FlatStyle = FlatStyle.Flat;
            btnStudent.Font = new Font("Roboto", 12F);
            btnStudent.ForeColor = Color.FromArgb(64, 64, 64);
            btnStudent.Location = new Point(8, 255);
            btnStudent.Name = "btnStudent";
            btnStudent.Size = new Size(230, 35);
            btnStudent.TabIndex = 2;
            btnStudent.Text = "   Students";
            btnStudent.TextAlign = ContentAlignment.MiddleLeft;
            btnStudent.UseVisualStyleBackColor = true;
            // 
            // panelMain
            // 
            panelMain.BackColor = SystemColors.AppWorkspace;
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(250, 0);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(982, 663);
            panelMain.TabIndex = 2;
            // 
            // mainForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = SystemColors.Control;
            ClientSize = new Size(1232, 663);
            Controls.Add(panelMain);
            Controls.Add(panelSidbar);
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ImeMode = ImeMode.On;
            MaximizeBox = false;
            MinimumSize = new Size(1248, 702);
            Name = "mainForm";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Load += mainForm_Load;
            panelSidbar.ResumeLayout(false);
            panelSidbar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureLogo).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Panel panelSidbar;
        private Button btnStudent;
        private Button btnGrade;
        private Button btnInvoiceStudent;
        private Button btnDashboard;
        private Label lbNameSchool;
        private PictureBox pictureLogo;
        private Button btnLogout;
        private Button btnBackup;
        private Panel pLine1;
        private Label lbSection4;
        private Label lbSection2;
        private Label lbSection1;
        private Button btnPaymentStudents;
        private Button btnGradeStudents;
        private Label label1;
        private Button btnTeacher;
        private Panel panel4;
        private Panel panel2;
        private Panel panel1;
        private Button btnTeacherClass;
        private Button btnPayroll;
        private Button btnSalary;
        private Panel panelMain;
    }
}
