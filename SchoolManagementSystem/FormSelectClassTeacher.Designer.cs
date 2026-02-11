namespace SchoolManagementSystem
{
    partial class FormSelectClassTeacher
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
            label1 = new Label();
            label2 = new Label();
            cbSelectClass = new ComboBox();
            cbSelectTeacher = new ComboBox();
            btnOK = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(33, 33);
            label1.Name = "label1";
            label1.Size = new Size(42, 20);
            label1.TabIndex = 0;
            label1.Text = "Class";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(274, 33);
            label2.Name = "label2";
            label2.Size = new Size(60, 20);
            label2.TabIndex = 0;
            label2.Text = "Teacher";
            // 
            // cbSelectClass
            // 
            cbSelectClass.DropDownHeight = 172;
            cbSelectClass.DropDownStyle = ComboBoxStyle.DropDownList;
            cbSelectClass.FormattingEnabled = true;
            cbSelectClass.IntegralHeight = false;
            cbSelectClass.ItemHeight = 20;
            cbSelectClass.Location = new Point(33, 85);
            cbSelectClass.Name = "cbSelectClass";
            cbSelectClass.Size = new Size(161, 28);
            cbSelectClass.TabIndex = 1;
            // 
            // cbSelectTeacher
            // 
            cbSelectTeacher.DropDownHeight = 172;
            cbSelectTeacher.DropDownStyle = ComboBoxStyle.DropDownList;
            cbSelectTeacher.FormattingEnabled = true;
            cbSelectTeacher.IntegralHeight = false;
            cbSelectTeacher.Location = new Point(274, 85);
            cbSelectTeacher.MaxDropDownItems = 7;
            cbSelectTeacher.Name = "cbSelectTeacher";
            cbSelectTeacher.Size = new Size(155, 28);
            cbSelectTeacher.TabIndex = 2;
            // 
            // btnOK
            // 
            btnOK.Location = new Point(166, 145);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(115, 33);
            btnOK.TabIndex = 3;
            btnOK.Text = "Ok";
            btnOK.UseVisualStyleBackColor = true;
            // 
            // FormSelectClassTeacher
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(461, 199);
            Controls.Add(btnOK);
            Controls.Add(cbSelectTeacher);
            Controls.Add(cbSelectClass);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "FormSelectClassTeacher";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormSelectClassTeacher";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private ComboBox cbSelectClass;
        private ComboBox cbSelectTeacher;
        private Button btnOK;
    }
}