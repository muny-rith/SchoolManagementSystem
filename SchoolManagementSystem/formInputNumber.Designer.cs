namespace SchoolManagementSystem
{
    partial class formInputNumber
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
            txtNumber = new TextBox();
            btnOk = new Button();
            lbName = new Label();
            SuspendLayout();
            // 
            // txtNumber
            // 
            txtNumber.BorderStyle = BorderStyle.FixedSingle;
            txtNumber.Font = new Font("Roboto", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtNumber.Location = new Point(134, 22);
            txtNumber.Name = "txtNumber";
            txtNumber.Size = new Size(69, 22);
            txtNumber.TabIndex = 0;
            txtNumber.TextAlign = HorizontalAlignment.Center;
            // 
            // btnOk
            // 
            btnOk.Location = new Point(43, 68);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(142, 32);
            btnOk.TabIndex = 1;
            btnOk.Text = "Ok";
            btnOk.UseVisualStyleBackColor = true;
            // 
            // lbName
            // 
            lbName.AutoSize = true;
            lbName.Location = new Point(29, 22);
            lbName.Name = "lbName";
            lbName.Size = new Size(38, 15);
            lbName.TabIndex = 2;
            lbName.Text = "label1";
            // 
            // formInputNumber
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(230, 126);
            Controls.Add(lbName);
            Controls.Add(btnOk);
            Controls.Add(txtNumber);
            MaximizeBox = false;
            MdiChildrenMinimizedAnchorBottom = false;
            MinimizeBox = false;
            Name = "formInputNumber";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "formInputNumber";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtNumber;
        private Button btnOk;
        private Label lbName;
    }
}