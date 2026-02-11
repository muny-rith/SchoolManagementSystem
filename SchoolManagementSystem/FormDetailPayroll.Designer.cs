namespace SchoolManagementSystem
{
    partial class FormDetailPayroll
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
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            label1 = new Label();
            lbClass = new Label();
            label3 = new Label();
            lbPrice = new Label();
            panel1 = new Panel();
            panel2 = new Panel();
            txtOther = new TextBox();
            txtGift = new TextBox();
            label4 = new Label();
            label2 = new Label();
            btnOk = new Button();
            dgv = new DataGridView();
            colNameEng = new DataGridViewTextBoxColumn();
            colNameKh = new DataGridViewTextBoxColumn();
            colSex = new DataGridViewTextBoxColumn();
            colStatus = new DataGridViewImageColumn();
            colDetail = new DataGridViewTextBoxColumn();
            colActive = new DataGridViewImageColumn();
            colOption = new DataGridViewTextBoxColumn();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgv).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(46, 21);
            label1.TabIndex = 0;
            label1.Text = "Class";
            // 
            // lbClass
            // 
            lbClass.AutoSize = true;
            lbClass.Font = new Font("Segoe UI", 12F);
            lbClass.Location = new Point(68, 9);
            lbClass.Name = "lbClass";
            lbClass.Size = new Size(46, 21);
            lbClass.TabIndex = 0;
            lbClass.Text = "Class";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F);
            label3.Location = new Point(239, 9);
            label3.Name = "label3";
            label3.Size = new Size(44, 21);
            label3.TabIndex = 0;
            label3.Text = "Price";
            // 
            // lbPrice
            // 
            lbPrice.AutoSize = true;
            lbPrice.Font = new Font("Segoe UI", 12F);
            lbPrice.Location = new Point(298, 9);
            lbPrice.Name = "lbPrice";
            lbPrice.Size = new Size(44, 21);
            lbPrice.TabIndex = 0;
            lbPrice.Text = "Price";
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(label1);
            panel1.Controls.Add(lbClass);
            panel1.Controls.Add(lbPrice);
            panel1.Controls.Add(label3);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(791, 36);
            panel1.TabIndex = 2;
            // 
            // panel2
            // 
            panel2.BackColor = Color.WhiteSmoke;
            panel2.Controls.Add(txtOther);
            panel2.Controls.Add(txtGift);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(btnOk);
            panel2.Controls.Add(dgv);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 36);
            panel2.Name = "panel2";
            panel2.Size = new Size(791, 304);
            panel2.TabIndex = 3;
            // 
            // txtOther
            // 
            txtOther.Font = new Font("Roboto", 11.25F);
            txtOther.Location = new Point(261, 258);
            txtOther.Name = "txtOther";
            txtOther.Size = new Size(95, 26);
            txtOther.TabIndex = 3;
            txtOther.Text = "00";
            txtOther.TextAlign = HorizontalAlignment.Center;
            // 
            // txtGift
            // 
            txtGift.Font = new Font("Roboto", 11.25F);
            txtGift.Location = new Point(74, 257);
            txtGift.Name = "txtGift";
            txtGift.Size = new Size(95, 26);
            txtGift.TabIndex = 3;
            txtGift.Text = "00";
            txtGift.TextAlign = HorizontalAlignment.Center;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Roboto", 11.25F);
            label4.Location = new Point(203, 262);
            label4.Name = "label4";
            label4.Size = new Size(44, 18);
            label4.TabIndex = 2;
            label4.Text = "Other";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Roboto", 11.25F);
            label2.Location = new Point(20, 262);
            label2.Name = "label2";
            label2.Size = new Size(32, 18);
            label2.TabIndex = 2;
            label2.Text = "Gift";
            // 
            // btnOk
            // 
            btnOk.Location = new Point(646, 253);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(133, 36);
            btnOk.TabIndex = 1;
            btnOk.Text = "OK";
            btnOk.UseVisualStyleBackColor = true;
            // 
            // dgv
            // 
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeColumns = false;
            dgv.AllowUserToResizeRows = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgv.BackgroundColor = SystemColors.Control;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgv.ColumnHeadersHeight = 30;
            dgv.Columns.AddRange(new DataGridViewColumn[] { colNameEng, colNameKh, colSex, colStatus, colDetail, colActive, colOption });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Window;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(223, 241, 250);
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgv.DefaultCellStyle = dataGridViewCellStyle3;
            dgv.Dock = DockStyle.Top;
            dgv.EnableHeadersVisualStyles = false;
            dgv.Location = new Point(0, 0);
            dgv.Name = "dgv";
            dgv.ReadOnly = true;
            dgv.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = SystemColors.ControlLight;
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.ControlLight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            dgv.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dgv.RowHeadersVisible = false;
            dataGridViewCellStyle5.Padding = new Padding(0, 2, 0, 2);
            dgv.RowsDefaultCellStyle = dataGridViewCellStyle5;
            dgv.RowTemplate.DefaultCellStyle.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dgv.RowTemplate.Height = 60;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.Size = new Size(791, 230);
            dgv.TabIndex = 0;
            // 
            // colNameEng
            // 
            colNameEng.FillWeight = 51.5471954F;
            colNameEng.HeaderText = "NameEng";
            colNameEng.Name = "colNameEng";
            colNameEng.ReadOnly = true;
            // 
            // colNameKh
            // 
            dataGridViewCellStyle2.Font = new Font("Khmer OS Bassac", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            colNameKh.DefaultCellStyle = dataGridViewCellStyle2;
            colNameKh.FillWeight = 5.88550043F;
            colNameKh.HeaderText = "NameKh";
            colNameKh.Name = "colNameKh";
            colNameKh.ReadOnly = true;
            // 
            // colSex
            // 
            colSex.FillWeight = 37.56703F;
            colSex.HeaderText = "Sex";
            colSex.Name = "colSex";
            colSex.ReadOnly = true;
            // 
            // colStatus
            // 
            colStatus.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colStatus.FillWeight = 140F;
            colStatus.HeaderText = "Status";
            colStatus.ImageLayout = DataGridViewImageCellLayout.Zoom;
            colStatus.MinimumWidth = 60;
            colStatus.Name = "colStatus";
            colStatus.ReadOnly = true;
            colStatus.Resizable = DataGridViewTriState.True;
            colStatus.SortMode = DataGridViewColumnSortMode.Automatic;
            colStatus.Width = 60;
            // 
            // colDetail
            // 
            colDetail.FillWeight = 119.502113F;
            colDetail.HeaderText = "    Last Paid";
            colDetail.Name = "colDetail";
            colDetail.ReadOnly = true;
            // 
            // colActive
            // 
            colActive.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colActive.FillWeight = 140F;
            colActive.HeaderText = "";
            colActive.ImageLayout = DataGridViewImageCellLayout.Zoom;
            colActive.MinimumWidth = 60;
            colActive.Name = "colActive";
            colActive.ReadOnly = true;
            colActive.Resizable = DataGridViewTriState.True;
            colActive.SortMode = DataGridViewColumnSortMode.Automatic;
            colActive.Width = 60;
            // 
            // colOption
            // 
            colOption.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colOption.HeaderText = "";
            colOption.Name = "colOption";
            colOption.ReadOnly = true;
            // 
            // FormDetailPayroll
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(791, 340);
            Controls.Add(panel2);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormDetailPayroll";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormDetailPayroll";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgv).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private Label lbClass;
        private Label label3;
        private Label lbPrice;
        private Panel panel1;
        private Panel panel2;
        private Button btnOk;
        private DataGridView dgv;
        private TextBox txtOther;
        private TextBox txtGift;
        private Label label4;
        private Label label2;
        private DataGridViewTextBoxColumn colNameEng;
        private DataGridViewTextBoxColumn colNameKh;
        private DataGridViewTextBoxColumn colSex;
        private DataGridViewImageColumn colStatus;
        private DataGridViewTextBoxColumn colDetail;
        private DataGridViewImageColumn colActive;
        private DataGridViewTextBoxColumn colOption;
    }
}