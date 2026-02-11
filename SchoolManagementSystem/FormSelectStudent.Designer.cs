namespace SchoolManagementSystem
{
    partial class formSelectStudent
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
            txtFindStudent = new TextBox();
            btnContinue = new Button();
            dgvView = new DataGridView();
            btnDetail = new Button();
            textBox1 = new TextBox();
            label3 = new Label();
            dtStartDate = new DateTimePicker();
            colStuId = new DataGridViewTextBoxColumn();
            colStudentName = new DataGridViewTextBoxColumn();
            colStudentNameKh = new DataGridViewTextBoxColumn();
            colGradeName = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgvView).BeginInit();
            SuspendLayout();
            // 
            // txtFindStudent
            // 
            txtFindStudent.Location = new Point(422, 20);
            txtFindStudent.Name = "txtFindStudent";
            txtFindStudent.Size = new Size(181, 27);
            txtFindStudent.TabIndex = 0;
            // 
            // btnContinue
            // 
            btnContinue.Location = new Point(422, 365);
            btnContinue.Name = "btnContinue";
            btnContinue.Size = new Size(203, 42);
            btnContinue.TabIndex = 2;
            btnContinue.Text = "Continue";
            btnContinue.UseVisualStyleBackColor = true;
            // 
            // dgvView
            // 
            dgvView.AllowUserToAddRows = false;
            dgvView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvView.BackgroundColor = Color.Gainsboro;
            dgvView.BorderStyle = BorderStyle.None;
            dgvView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvView.Columns.AddRange(new DataGridViewColumn[] { colStuId, colStudentName, colStudentNameKh, colGradeName });
            dgvView.Location = new Point(31, 64);
            dgvView.Name = "dgvView";
            dgvView.RightToLeft = RightToLeft.No;
            dgvView.RowHeadersVisible = false;
            dgvView.RowHeadersWidth = 51;
            dgvView.ShowEditingIcon = false;
            dgvView.Size = new Size(594, 282);
            dgvView.TabIndex = 3;
            // 
            // btnDetail
            // 
            btnDetail.Location = new Point(31, 18);
            btnDetail.Name = "btnDetail";
            btnDetail.Size = new Size(59, 31);
            btnDetail.TabIndex = 4;
            btnDetail.Text = "Detail";
            btnDetail.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.BackColor = SystemColors.Control;
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Location = new Point(362, 23);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(54, 20);
            textBox1.TabIndex = 5;
            textBox1.Text = "Search";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(31, 349);
            label3.Name = "label3";
            label3.Size = new Size(87, 20);
            label3.TabIndex = 25;
            label3.Text = "Start Date : ";
            // 
            // dtStartDate
            // 
            dtStartDate.CustomFormat = "dd- MMM -yyyy";
            dtStartDate.Font = new Font("Roboto", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtStartDate.Format = DateTimePickerFormat.Custom;
            dtStartDate.ImeMode = ImeMode.NoControl;
            dtStartDate.Location = new Point(31, 374);
            dtStartDate.Name = "dtStartDate";
            dtStartDate.Size = new Size(175, 26);
            dtStartDate.TabIndex = 24;
            // 
            // colStuId
            // 
            colStuId.FillWeight = 64.17112F;
            colStuId.HeaderText = "Id";
            colStuId.MinimumWidth = 6;
            colStuId.Name = "colStuId";
            // 
            // colStudentName
            // 
            colStudentName.FillWeight = 117.914444F;
            colStudentName.HeaderText = "Name";
            colStudentName.MinimumWidth = 6;
            colStudentName.Name = "colStudentName";
            // 
            // colStudentNameKh
            // 
            colStudentNameKh.HeaderText = "NameKh";
            colStudentNameKh.MinimumWidth = 6;
            colStudentNameKh.Name = "colStudentNameKh";
            // 
            // colGradeName
            // 
            colGradeName.FillWeight = 117.914444F;
            colGradeName.HeaderText = "Grade";
            colGradeName.MinimumWidth = 6;
            colGradeName.Name = "colGradeName";
            // 
            // formSelectStudent
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(637, 419);
            Controls.Add(label3);
            Controls.Add(dtStartDate);
            Controls.Add(textBox1);
            Controls.Add(btnDetail);
            Controls.Add(dgvView);
            Controls.Add(btnContinue);
            Controls.Add(txtFindStudent);
            Name = "formSelectStudent";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form SelectStudent";
            ((System.ComponentModel.ISupportInitialize)dgvView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtFindStudent;
        private Button btnContinue;
        private DataGridView dgvView;
        private Button btnDetail;
        private TextBox textBox1;
        private Label label3;
        private DateTimePicker dtStartDate;
        private DataGridViewTextBoxColumn colStuId;
        private DataGridViewTextBoxColumn colStudentName;
        private DataGridViewTextBoxColumn colStudentNameKh;
        private DataGridViewTextBoxColumn colGradeName;
    }
}