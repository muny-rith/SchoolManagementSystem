namespace SchoolManagementSystem
{
    partial class FormSalaryControl
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
            lbTotal = new Label();
            label3 = new Label();
            lbTeacher = new Label();
            label1 = new Label();
            btnDetail = new Button();
            panel2 = new Panel();
            panelDgv = new Panel();
            dgv = new DataGridView();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panelDgv.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgv).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(lbTotal);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(lbTeacher);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(btnDetail);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 52);
            panel1.TabIndex = 0;
            // 
            // lbTotal
            // 
            lbTotal.AutoSize = true;
            lbTotal.Font = new Font("Roboto", 11.25F);
            lbTotal.Location = new Point(295, 19);
            lbTotal.Name = "lbTotal";
            lbTotal.Size = new Size(54, 18);
            lbTotal.TabIndex = 4;
            lbTotal.Text = "lbTotal";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Roboto", 11.25F);
            label3.Location = new Point(244, 19);
            label3.Name = "label3";
            label3.Size = new Size(42, 18);
            label3.TabIndex = 3;
            label3.Text = "Total";
            // 
            // lbTeacher
            // 
            lbTeacher.AutoSize = true;
            lbTeacher.Font = new Font("Roboto", 11.25F);
            lbTeacher.Location = new Point(103, 19);
            lbTeacher.Name = "lbTeacher";
            lbTeacher.Size = new Size(73, 18);
            lbTeacher.TabIndex = 2;
            lbTeacher.Text = "lbTeacher";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Roboto", 11.25F);
            label1.Location = new Point(31, 19);
            label1.Name = "label1";
            label1.Size = new Size(61, 18);
            label1.TabIndex = 1;
            label1.Text = "Teacher";
            // 
            // btnDetail
            // 
            btnDetail.Location = new Point(675, 12);
            btnDetail.Name = "btnDetail";
            btnDetail.Size = new Size(113, 28);
            btnDetail.TabIndex = 0;
            btnDetail.Text = "Detail";
            btnDetail.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            panel2.Controls.Add(panelDgv);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 52);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(10, 0, 10, 15);
            panel2.Size = new Size(800, 398);
            panel2.TabIndex = 1;
            // 
            // panelDgv
            // 
            panelDgv.Controls.Add(dgv);
            panelDgv.Dock = DockStyle.Fill;
            panelDgv.Location = new Point(10, 0);
            panelDgv.Name = "panelDgv";
            panelDgv.Size = new Size(780, 383);
            panelDgv.TabIndex = 0;
            // 
            // dgv
            // 
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv.Dock = DockStyle.Fill;
            dgv.Location = new Point(0, 0);
            dgv.Name = "dgv";
            dgv.Size = new Size(780, 383);
            dgv.TabIndex = 0;
            // 
            // FormSalaryControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panel2);
            Controls.Add(panel1);
            MaximizeBox = false;
            MaximumSize = new Size(816, 489);
            MinimizeBox = false;
            MinimumSize = new Size(816, 489);
            Name = "FormSalaryControl";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormSalaryControl";
            TransparencyKey = Color.FromArgb(192, 192, 0);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panelDgv.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgv).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button btnDetail;
        private Panel panel2;
        private Panel panelDgv;
        private Label lbTotal;
        private Label label3;
        private Label lbTeacher;
        private Label label1;
        private DataGridView dgv;
    }
}