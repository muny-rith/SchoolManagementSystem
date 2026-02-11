namespace SchoolManagementSystem
{
    partial class FormEditGrade
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
            lbOldGrade = new Label();
            label2 = new Label();
            btnOk = new Button();
            btnCancel = new Button();
            btnFilter = new Krypton.Toolkit.KryptonButton();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(25, 17);
            label1.Name = "label1";
            label1.Size = new Size(77, 20);
            label1.TabIndex = 0;
            label1.Text = "Old Grade";
            // 
            // lbOldGrade
            // 
            lbOldGrade.AutoSize = true;
            lbOldGrade.Location = new Point(25, 71);
            lbOldGrade.Name = "lbOldGrade";
            lbOldGrade.Size = new Size(77, 20);
            lbOldGrade.TabIndex = 0;
            lbOldGrade.Text = "Old Grade";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(223, 17);
            label2.Name = "label2";
            label2.Size = new Size(83, 20);
            label2.TabIndex = 0;
            label2.Text = "New Grade";
            // 
            // btnOk
            // 
            btnOk.Location = new Point(47, 117);
            btnOk.MaximumSize = new Size(103, 26);
            btnOk.MinimumSize = new Size(103, 26);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(103, 26);
            btnOk.TabIndex = 2;
            btnOk.Text = "Ok";
            btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(190, 117);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(103, 26);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnFilter
            // 
            btnFilter.DialogResult = DialogResult.Cancel;
            btnFilter.Location = new Point(207, 67);
            btnFilter.Name = "btnFilter";
            btnFilter.OverrideDefault.Back.Color1 = SystemColors.ControlLight;
            btnFilter.OverrideDefault.Back.Color2 = SystemColors.ControlLight;
            btnFilter.OverrideDefault.Border.Color1 = SystemColors.ControlLight;
            btnFilter.OverrideDefault.Border.Color2 = SystemColors.ControlLight;
            btnFilter.OverrideDefault.Border.Rounding = 5F;
            btnFilter.OverrideDefault.Border.Width = 1;
            btnFilter.OverrideDefault.Content.ShortText.Color1 = SystemColors.WindowText;
            btnFilter.OverrideDefault.Content.ShortText.Color2 = SystemColors.WindowText;
            btnFilter.PaletteMode = Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            btnFilter.Size = new Size(113, 26);
            btnFilter.StateCommon.Back.Color1 = SystemColors.ControlLight;
            btnFilter.StateCommon.Back.Color2 = SystemColors.ControlLight;
            btnFilter.StateCommon.Border.Color1 = SystemColors.ControlLight;
            btnFilter.StateCommon.Border.Color2 = SystemColors.ControlLight;
            btnFilter.StateCommon.Border.GraphicsHint = Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            btnFilter.StateCommon.Border.Rounding = 5F;
            btnFilter.StateCommon.Border.Width = 1;
            btnFilter.StateCommon.Content.ShortText.Color1 = SystemColors.WindowText;
            btnFilter.StateCommon.Content.ShortText.Color2 = SystemColors.WindowFrame;
            btnFilter.StateCommon.Content.ShortText.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnFilter.StateTracking.Back.Color1 = SystemColors.ControlDark;
            btnFilter.StateTracking.Back.Color2 = SystemColors.ControlDark;
            btnFilter.StateTracking.Border.Color1 = SystemColors.ControlDark;
            btnFilter.StateTracking.Border.Color2 = SystemColors.ControlDark;
            btnFilter.StateTracking.Border.Rounding = 5F;
            btnFilter.StateTracking.Border.Width = 1;
            btnFilter.TabIndex = 17;
            btnFilter.Values.DropDownArrowColor = Color.Empty;
            btnFilter.Values.Text = "pls Select!";
            // 
            // FormEditGrade
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(343, 165);
            Controls.Add(btnFilter);
            Controls.Add(btnCancel);
            Controls.Add(btnOk);
            Controls.Add(lbOldGrade);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MaximumSize = new Size(359, 181);
            MinimizeBox = false;
            MinimumSize = new Size(359, 181);
            Name = "FormEditGrade";
            PaletteMode = Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            StartPosition = FormStartPosition.CenterScreen;
            StateCommon.Back.Color1 = Color.White;
            StateCommon.Back.Color2 = Color.White;
            StateCommon.Border.Color1 = Color.FromArgb(224, 224, 224);
            StateCommon.Border.Color2 = Color.FromArgb(224, 224, 224);
            StateCommon.Border.Rounding = 12F;
            StateCommon.Header.Back.Color1 = Color.White;
            StateCommon.Header.Back.Color2 = Color.White;
            StateCommon.Header.Border.Color1 = Color.White;
            StateCommon.Header.Border.Color2 = Color.White;
            StateCommon.Header.Border.Rounding = 12F;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label lbOldGrade;
        private Label label2;
        private Button btnOk;
        private Button btnCancel;
        protected Krypton.Toolkit.KryptonButton btnFilter;
    }
}