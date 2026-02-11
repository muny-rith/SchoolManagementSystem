namespace SchoolManagementSystem
{
    partial class FormSalaryConfirm
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            panelTop = new Panel();
            panel2 = new Panel();
            dgv = new DataGridView();
            colClassName = new DataGridViewTextBoxColumn();
            colClassPrice = new DataGridViewTextBoxColumn();
            colStudentPaid = new DataGridViewTextBoxColumn();
            colStuPaid = new DataGridViewTextBoxColumn();
            colBaseSalary = new DataGridViewTextBoxColumn();
            colGirft = new DataGridViewTextBoxColumn();
            colOther = new DataGridViewTextBoxColumn();
            colTotal = new DataGridViewTextBoxColumn();
            panel6 = new Panel();
            lbMonth = new Label();
            label1 = new Label();
            btnDetail = new Button();
            lbTotalSalary = new Label();
            label10 = new Label();
            label9 = new Label();
            label8 = new Label();
            lbPayrollRate = new Label();
            lbPayrollBase = new Label();
            txtOther = new TextBox();
            label4 = new Label();
            txtBonus = new TextBox();
            label2 = new Label();
            btnCancel = new Button();
            btnConfirm = new Button();
            lbTotalOther = new Label();
            label5 = new Label();
            lbBaseSalary = new Label();
            label6 = new Label();
            panel3 = new Panel();
            panel4 = new Panel();
            panelTop.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgv).BeginInit();
            panel6.SuspendLayout();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.Controls.Add(panel2);
            panelTop.Controls.Add(panel6);
            panelTop.Dock = DockStyle.Fill;
            panelTop.Location = new Point(0, 0);
            panelTop.Margin = new Padding(3, 2, 3, 2);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(883, 342);
            panelTop.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Controls.Add(dgv);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 58);
            panel2.Name = "panel2";
            panel2.Size = new Size(883, 284);
            panel2.TabIndex = 4;
            // 
            // dgv
            // 
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeColumns = false;
            dgv.AllowUserToResizeRows = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgv.BackgroundColor = SystemColors.ButtonFace;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.ControlLight;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.ControlLight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgv.ColumnHeadersHeight = 30;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.Columns.AddRange(new DataGridViewColumn[] { colClassName, colClassPrice, colStudentPaid, colStuPaid, colBaseSalary, colGirft, colOther, colTotal });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(223, 241, 250);
            dataGridViewCellStyle2.SelectionForeColor = Color.Black;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgv.DefaultCellStyle = dataGridViewCellStyle2;
            dgv.Dock = DockStyle.Fill;
            dgv.EnableHeadersVisualStyles = false;
            dgv.GridColor = SystemColors.Control;
            dgv.Location = new Point(0, 0);
            dgv.Margin = new Padding(3, 2, 3, 2);
            dgv.Name = "dgv";
            dgv.ReadOnly = true;
            dgv.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgv.RowHeadersVisible = false;
            dgv.RowHeadersWidth = 50;
            dataGridViewCellStyle3.Padding = new Padding(0, 3, 0, 3);
            dgv.RowsDefaultCellStyle = dataGridViewCellStyle3;
            dgv.RowTemplate.DefaultCellStyle.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dgv.RowTemplate.Height = 40;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.Size = new Size(883, 284);
            dgv.TabIndex = 2;
            // 
            // colClassName
            // 
            colClassName.FillWeight = 120.661407F;
            colClassName.HeaderText = "ClassName";
            colClassName.MinimumWidth = 6;
            colClassName.Name = "colClassName";
            colClassName.ReadOnly = true;
            // 
            // colClassPrice
            // 
            colClassPrice.FillWeight = 120.661407F;
            colClassPrice.HeaderText = "Price";
            colClassPrice.MinimumWidth = 6;
            colClassPrice.Name = "colClassPrice";
            colClassPrice.ReadOnly = true;
            // 
            // colStudentPaid
            // 
            colStudentPaid.HeaderText = "Paid";
            colStudentPaid.MinimumWidth = 6;
            colStudentPaid.Name = "colStudentPaid";
            colStudentPaid.ReadOnly = true;
            // 
            // colStuPaid
            // 
            colStuPaid.FillWeight = 120.661407F;
            colStuPaid.HeaderText = "Unpaid";
            colStuPaid.MinimumWidth = 6;
            colStuPaid.Name = "colStuPaid";
            colStuPaid.ReadOnly = true;
            // 
            // colBaseSalary
            // 
            colBaseSalary.HeaderText = "Salary";
            colBaseSalary.MinimumWidth = 6;
            colBaseSalary.Name = "colBaseSalary";
            colBaseSalary.ReadOnly = true;
            // 
            // colGirft
            // 
            colGirft.HeaderText = "Girft";
            colGirft.Name = "colGirft";
            colGirft.ReadOnly = true;
            // 
            // colOther
            // 
            colOther.HeaderText = "Other";
            colOther.Name = "colOther";
            colOther.ReadOnly = true;
            // 
            // colTotal
            // 
            colTotal.HeaderText = "Total";
            colTotal.MinimumWidth = 6;
            colTotal.Name = "colTotal";
            colTotal.ReadOnly = true;
            // 
            // panel6
            // 
            panel6.Controls.Add(lbMonth);
            panel6.Controls.Add(label1);
            panel6.Controls.Add(btnDetail);
            panel6.Controls.Add(lbTotalSalary);
            panel6.Controls.Add(label10);
            panel6.Controls.Add(label9);
            panel6.Controls.Add(label8);
            panel6.Controls.Add(lbPayrollRate);
            panel6.Controls.Add(lbPayrollBase);
            panel6.Dock = DockStyle.Top;
            panel6.Location = new Point(0, 0);
            panel6.Name = "panel6";
            panel6.Size = new Size(883, 58);
            panel6.TabIndex = 3;
            // 
            // lbMonth
            // 
            lbMonth.AutoSize = true;
            lbMonth.Font = new Font("Roboto", 12F);
            lbMonth.Location = new Point(627, 18);
            lbMonth.Name = "lbMonth";
            lbMonth.Size = new Size(68, 19);
            lbMonth.TabIndex = 2;
            lbMonth.Text = "lbMonth";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Roboto", 12F);
            label1.Location = new Point(566, 18);
            label1.Name = "label1";
            label1.Size = new Size(55, 19);
            label1.TabIndex = 2;
            label1.Text = "Month";
            // 
            // btnDetail
            // 
            btnDetail.Location = new Point(735, 11);
            btnDetail.Margin = new Padding(3, 2, 3, 2);
            btnDetail.Name = "btnDetail";
            btnDetail.Size = new Size(112, 34);
            btnDetail.TabIndex = 1;
            btnDetail.Text = "Detail";
            btnDetail.UseVisualStyleBackColor = true;
            // 
            // lbTotalSalary
            // 
            lbTotalSalary.AutoSize = true;
            lbTotalSalary.Font = new Font("Roboto", 12F);
            lbTotalSalary.Location = new Point(439, 18);
            lbTotalSalary.Name = "lbTotalSalary";
            lbTotalSalary.Size = new Size(27, 19);
            lbTotalSalary.TabIndex = 0;
            lbTotalSalary.Text = "00";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Roboto", 12F);
            label10.Location = new Point(153, 18);
            label10.Name = "label10";
            label10.Size = new Size(45, 19);
            label10.TabIndex = 0;
            label10.Text = "Rate:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Roboto", 12F);
            label9.Location = new Point(14, 18);
            label9.Name = "label9";
            label9.Size = new Size(48, 19);
            label9.TabIndex = 0;
            label9.Text = "Base:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Roboto", 12F);
            label8.Location = new Point(329, 18);
            label8.Name = "label8";
            label8.Size = new Size(98, 19);
            label8.TabIndex = 0;
            label8.Text = "Total Salary:";
            // 
            // lbPayrollRate
            // 
            lbPayrollRate.AutoSize = true;
            lbPayrollRate.Font = new Font("Roboto", 12F);
            lbPayrollRate.Location = new Point(210, 18);
            lbPayrollRate.Name = "lbPayrollRate";
            lbPayrollRate.Size = new Size(48, 19);
            lbPayrollRate.TabIndex = 0;
            lbPayrollRate.Text = "Base:";
            // 
            // lbPayrollBase
            // 
            lbPayrollBase.AutoSize = true;
            lbPayrollBase.Font = new Font("Roboto", 12F);
            lbPayrollBase.Location = new Point(71, 18);
            lbPayrollBase.Name = "lbPayrollBase";
            lbPayrollBase.Size = new Size(48, 19);
            lbPayrollBase.TabIndex = 0;
            lbPayrollBase.Text = "Base:";
            // 
            // txtOther
            // 
            txtOther.Font = new Font("Roboto", 12F);
            txtOther.Location = new Point(60, 22);
            txtOther.Margin = new Padding(3, 2, 3, 2);
            txtOther.Name = "txtOther";
            txtOther.Size = new Size(27, 27);
            txtOther.TabIndex = 1;
            txtOther.Visible = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Roboto", 12F);
            label4.Location = new Point(3, 25);
            label4.Name = "label4";
            label4.Size = new Size(51, 19);
            label4.TabIndex = 0;
            label4.Text = "Other:";
            label4.Visible = false;
            // 
            // txtBonus
            // 
            txtBonus.Font = new Font("Roboto", 12F);
            txtBonus.Location = new Point(154, 22);
            txtBonus.Margin = new Padding(3, 2, 3, 2);
            txtBonus.Name = "txtBonus";
            txtBonus.Size = new Size(34, 27);
            txtBonus.TabIndex = 1;
            txtBonus.Visible = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Roboto", 12F);
            label2.Location = new Point(93, 25);
            label2.Name = "label2";
            label2.Size = new Size(66, 19);
            label2.TabIndex = 0;
            label2.Text = "Bonus : ";
            label2.Visible = false;
            // 
            // btnCancel
            // 
            btnCancel.Font = new Font("Segoe UI", 11.25F);
            btnCancel.Location = new Point(518, 16);
            btnCancel.Margin = new Padding(3, 2, 3, 2);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(135, 37);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnConfirm
            // 
            btnConfirm.Font = new Font("Segoe UI", 11.25F);
            btnConfirm.Location = new Point(197, 16);
            btnConfirm.Margin = new Padding(3, 2, 3, 2);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(135, 37);
            btnConfirm.TabIndex = 1;
            btnConfirm.Text = "Confirm";
            btnConfirm.UseVisualStyleBackColor = true;
            // 
            // lbTotalOther
            // 
            lbTotalOther.AutoSize = true;
            lbTotalOther.Font = new Font("Roboto", 12F);
            lbTotalOther.Location = new Point(583, 78);
            lbTotalOther.Name = "lbTotalOther";
            lbTotalOther.Size = new Size(27, 19);
            lbTotalOther.TabIndex = 0;
            lbTotalOther.Text = "00";
            lbTotalOther.Visible = false;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Roboto", 12F);
            label5.Location = new Point(419, 78);
            label5.Name = "label5";
            label5.Size = new Size(55, 19);
            label5.TabIndex = 0;
            label5.Text = "Other :";
            label5.Visible = false;
            // 
            // lbBaseSalary
            // 
            lbBaseSalary.AutoSize = true;
            lbBaseSalary.Font = new Font("Roboto", 12F);
            lbBaseSalary.Location = new Point(374, 78);
            lbBaseSalary.Name = "lbBaseSalary";
            lbBaseSalary.Size = new Size(27, 19);
            lbBaseSalary.TabIndex = 0;
            lbBaseSalary.Text = "00";
            lbBaseSalary.Visible = false;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Roboto", 12F);
            label6.Location = new Point(210, 78);
            label6.Name = "label6";
            label6.Size = new Size(97, 19);
            label6.TabIndex = 0;
            label6.Text = "Base Salary:";
            label6.Visible = false;
            // 
            // panel3
            // 
            panel3.Controls.Add(txtOther);
            panel3.Controls.Add(btnCancel);
            panel3.Controls.Add(label4);
            panel3.Controls.Add(label6);
            panel3.Controls.Add(btnConfirm);
            panel3.Controls.Add(txtBonus);
            panel3.Controls.Add(lbBaseSalary);
            panel3.Controls.Add(label5);
            panel3.Controls.Add(label2);
            panel3.Controls.Add(lbTotalOther);
            panel3.Dock = DockStyle.Bottom;
            panel3.Location = new Point(0, 342);
            panel3.Name = "panel3";
            panel3.Size = new Size(883, 68);
            panel3.TabIndex = 2;
            // 
            // panel4
            // 
            panel4.Controls.Add(panelTop);
            panel4.Dock = DockStyle.Fill;
            panel4.Location = new Point(0, 0);
            panel4.Name = "panel4";
            panel4.Size = new Size(883, 342);
            panel4.TabIndex = 3;
            // 
            // FormSalaryConfirm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(883, 410);
            Controls.Add(panel4);
            Controls.Add(panel3);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormSalaryConfirm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormPayroll";
            panelTop.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgv).EndInit();
            panel6.ResumeLayout(false);
            panel6.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel4.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelTop;
        private Button btnCancel;
        private Button btnConfirm;
        private Label label5;
        private Label label4;
        private Label label2;
        private Label label8;
        private Label label6;
        private TextBox txtOther;
        private TextBox txtBonus;
        private Label label10;
        private Label lbPayrollRate;
        private Label lbPayrollBase;
        private Label label9;
        private DataGridView dgv;
        private Button btnDetail;
        private Label lbTotalOther;
        private Label lbTotalSalary;
        private Label lbBaseSalary;
        private Panel panel6;
        private Panel panel3;
        private Panel panel4;
        private Panel panel2;
        private DataGridViewTextBoxColumn colClassName;
        private DataGridViewTextBoxColumn colClassPrice;
        private DataGridViewTextBoxColumn colStudentPaid;
        private DataGridViewTextBoxColumn colStuPaid;
        private DataGridViewTextBoxColumn colBaseSalary;
        private DataGridViewTextBoxColumn colGirft;
        private DataGridViewTextBoxColumn colOther;
        private DataGridViewTextBoxColumn colTotal;
        private Label label1;
        private Label lbMonth;
    }
}