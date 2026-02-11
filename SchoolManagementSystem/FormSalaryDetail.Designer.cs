namespace SchoolManagementSystem
{
    partial class FormSalaryDetail
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
            panel1 = new Panel();
            lbSalary = new Label();
            lbTotal = new Label();
            lbUnPaid = new Label();
            label2 = new Label();
            label7 = new Label();
            label5 = new Label();
            lbPaid = new Label();
            label3 = new Label();
            lbClass = new Label();
            label1 = new Label();
            panelDgv = new Panel();
            dgv = new DataGridView();
            panel1.SuspendLayout();
            panelDgv.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgv).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(lbSalary);
            panel1.Controls.Add(lbTotal);
            panel1.Controls.Add(lbUnPaid);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(lbPaid);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(lbClass);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 59);
            panel1.TabIndex = 0;
            // 
            // lbSalary
            // 
            lbSalary.AutoSize = true;
            lbSalary.Font = new Font("Roboto", 12F);
            lbSalary.Location = new Point(234, 21);
            lbSalary.Name = "lbSalary";
            lbSalary.Size = new Size(67, 19);
            lbSalary.TabIndex = 0;
            lbSalary.Text = "lbSalary";
            // 
            // lbTotal
            // 
            lbTotal.AutoSize = true;
            lbTotal.Font = new Font("Roboto", 12F);
            lbTotal.Location = new Point(718, 21);
            lbTotal.Name = "lbTotal";
            lbTotal.Size = new Size(58, 19);
            lbTotal.TabIndex = 0;
            lbTotal.Text = "lbTotal";
            // 
            // lbUnPaid
            // 
            lbUnPaid.AutoSize = true;
            lbUnPaid.Font = new Font("Roboto", 12F);
            lbUnPaid.Location = new Point(599, 21);
            lbUnPaid.Name = "lbUnPaid";
            lbUnPaid.Size = new Size(73, 19);
            lbUnPaid.TabIndex = 0;
            lbUnPaid.Text = "lbUnPaid";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Roboto", 12F);
            label2.Location = new Point(170, 21);
            label2.Name = "label2";
            label2.Size = new Size(54, 19);
            label2.TabIndex = 0;
            label2.Text = "Salary";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Roboto", 12F);
            label7.Location = new Point(670, 21);
            label7.Name = "label7";
            label7.Size = new Size(45, 19);
            label7.TabIndex = 0;
            label7.Text = "Total";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Roboto", 12F);
            label5.Location = new Point(542, 21);
            label5.Name = "label5";
            label5.Size = new Size(60, 19);
            label5.TabIndex = 0;
            label5.Text = "UnPaid";
            // 
            // lbPaid
            // 
            lbPaid.AutoSize = true;
            lbPaid.Font = new Font("Roboto", 12F);
            lbPaid.Location = new Point(488, 21);
            lbPaid.Name = "lbPaid";
            lbPaid.Size = new Size(54, 19);
            lbPaid.TabIndex = 0;
            lbPaid.Text = "lbPaid";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Roboto", 12F);
            label3.Location = new Point(446, 21);
            label3.Name = "label3";
            label3.Size = new Size(41, 19);
            label3.TabIndex = 0;
            label3.Text = "Paid";
            // 
            // lbClass
            // 
            lbClass.AutoSize = true;
            lbClass.Font = new Font("Roboto", 12F);
            lbClass.Location = new Point(77, 21);
            lbClass.Name = "lbClass";
            lbClass.Size = new Size(61, 19);
            lbClass.TabIndex = 0;
            lbClass.Text = "lbClass";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Roboto", 12F);
            label1.Location = new Point(23, 21);
            label1.Name = "label1";
            label1.Size = new Size(48, 19);
            label1.TabIndex = 0;
            label1.Text = "Class";
            // 
            // panelDgv
            // 
            panelDgv.Controls.Add(dgv);
            panelDgv.Dock = DockStyle.Fill;
            panelDgv.Location = new Point(0, 59);
            panelDgv.Name = "panelDgv";
            panelDgv.Padding = new Padding(10, 0, 10, 15);
            panelDgv.Size = new Size(800, 391);
            panelDgv.TabIndex = 1;
            // 
            // dgv
            // 
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv.Dock = DockStyle.Fill;
            dgv.Location = new Point(10, 0);
            dgv.Name = "dgv";
            dgv.Size = new Size(780, 376);
            dgv.TabIndex = 0;
            // 
            // FormSalaryDetail
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panelDgv);
            Controls.Add(panel1);
            MaximizeBox = false;
            MaximumSize = new Size(816, 489);
            MinimizeBox = false;
            MinimumSize = new Size(816, 489);
            Name = "FormSalaryDetail";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormSalaryDetail";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panelDgv.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgv).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label lbUnPaid;
        private Label label5;
        private Label lbPaid;
        private Label label3;
        private Label lbClass;
        private Label label1;
        private Panel panelDgv;
        private Label lbTotal;
        private Label label7;
        private DataGridView dgv;
        private Label lbSalary;
        private Label label2;
    }
}