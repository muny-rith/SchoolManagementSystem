namespace SchoolManagementSystem
{
    partial class FormUpdatePayroll
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
            btnYes = new Button();
            cbSelectTeacher = new ComboBox();
            label2 = new Label();
            label3 = new Label();
            txtRatePayroll = new TextBox();
            btnNo = new Button();
            label1 = new Label();
            txtBaseSalary = new TextBox();
            SuspendLayout();
            // 
            // btnYes
            // 
            btnYes.Location = new Point(83, 147);
            btnYes.Name = "btnYes";
            btnYes.Size = new Size(115, 33);
            btnYes.TabIndex = 8;
            btnYes.Text = "Yes";
            btnYes.UseVisualStyleBackColor = true;
            // 
            // cbSelectTeacher
            // 
            cbSelectTeacher.DropDownHeight = 172;
            cbSelectTeacher.DropDownStyle = ComboBoxStyle.DropDownList;
            cbSelectTeacher.FormattingEnabled = true;
            cbSelectTeacher.IntegralHeight = false;
            cbSelectTeacher.Location = new Point(25, 89);
            cbSelectTeacher.MaxDropDownItems = 7;
            cbSelectTeacher.Name = "cbSelectTeacher";
            cbSelectTeacher.Size = new Size(155, 28);
            cbSelectTeacher.TabIndex = 7;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(25, 37);
            label2.Name = "label2";
            label2.Size = new Size(60, 20);
            label2.TabIndex = 4;
            label2.Text = "Teacher";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(333, 42);
            label3.Name = "label3";
            label3.Size = new Size(79, 20);
            label3.TabIndex = 9;
            label3.Text = "RateSalary";
            // 
            // txtRatePayroll
            // 
            txtRatePayroll.Location = new Point(333, 90);
            txtRatePayroll.Name = "txtRatePayroll";
            txtRatePayroll.Size = new Size(110, 27);
            txtRatePayroll.TabIndex = 10;
            // 
            // btnNo
            // 
            btnNo.Location = new Point(250, 147);
            btnNo.Name = "btnNo";
            btnNo.Size = new Size(115, 33);
            btnNo.TabIndex = 8;
            btnNo.Text = "No";
            btnNo.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(200, 42);
            label1.Name = "label1";
            label1.Size = new Size(80, 20);
            label1.TabIndex = 9;
            label1.Text = "BaseSalary";
            // 
            // txtBaseSalary
            // 
            txtBaseSalary.Location = new Point(200, 90);
            txtBaseSalary.Name = "txtBaseSalary";
            txtBaseSalary.Size = new Size(110, 27);
            txtBaseSalary.TabIndex = 10;
            // 
            // FormUpdatePayroll
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(463, 216);
            Controls.Add(txtBaseSalary);
            Controls.Add(txtRatePayroll);
            Controls.Add(label1);
            Controls.Add(label3);
            Controls.Add(btnNo);
            Controls.Add(btnYes);
            Controls.Add(cbSelectTeacher);
            Controls.Add(label2);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormUpdatePayroll";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ForUpdatePayrollTeacher";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnYes;
        private ComboBox cbSelectTeacher;
        private Label label2;
        private Label label3;
        private TextBox txtRatePayroll;
        private Button btnNo;
        private Label label1;
        private TextBox txtBaseSalary;
    }
}