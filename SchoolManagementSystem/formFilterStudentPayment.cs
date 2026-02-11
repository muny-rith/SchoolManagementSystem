using Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolManagementSystem
{
    public partial class formFilterStudentPayment : KryptonForm
    {
        DatabaseConnection conn = DatabaseConnection.Instance;
        public int isPaid { get; set; }
        public event EventHandler applied;

        public string ?StudentName {  get; set; }
        public string ?Status { get; set; }
        public string ?Cls { get; set; }
        public int ?TimeStatus { get; set; }
        public formFilterStudentPayment()
        {
            InitializeComponent();
            Initial();
            btnApply.Click += (s, e) => doApply();
            btnReset.Click += (s, e) => doReset();
            //btnPaid.Click += (s, e) => doShowPaid();
            //btnNotPaid.Click += (s, e) => doShowNotPaid();
            //btnDefault.Click += (s, e) => doShowAll();
            this.Deactivate += (s, e) => this.Close();


        }

        private void doReset()
        {
            txtName.Clear();
            cbStatus.SelectedIndex = -1;
            cbGrade.SelectedIndex = -1;
            cbTimeStatus.SelectedIndex = -1;
            DialogResult = DialogResult.No;
        }

        private void doApply()
        {
            StudentName = txtName.Text;
            Status = GetComboBoxText(cbStatus);
            Cls = GetComboBoxText(cbGrade);
            TimeStatus = cbTimeStatus.SelectedIndex >= 0 ? cbTimeStatus.SelectedIndex : (int?)null;
            DialogResult = DialogResult.OK;
            applied?.Invoke(this, EventArgs.Empty);

            this.Close();
        }
        private string GetComboBoxText(ComboBox cb)
        {
            return cb.SelectedItem?.ToString();
        }

        private void Initial()
        {
            cbStatus.Items.Add("Paid");
            cbStatus.Items.Add("Unpaid");
            mgmClass mgmGrade = new mgmClass();
            mgmGrade.loadDataFromDB(conn.GetConnection());
            cbGrade.Items.AddRange(mgmGrade.Classes.Select(e => e.Name).ToArray());
            object[] items = new object[] {"1 days","3 days","5 dyas","7 day and more"};
            cbTimeStatus.Items.AddRange(items);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            cbStatus = new ComboBox();
            cbGrade = new ComboBox();
            label4 = new Label();
            cbTimeStatus = new ComboBox();
            txtName = new KryptonTextBox();
            pLine1 = new Panel();
            btnReset = new KryptonButton();
            btnApply = new KryptonButton();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(2, 0);
            label1.Name = "label1";
            label1.Size = new Size(49, 20);
            label1.TabIndex = 0;
            label1.Text = "Name";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(2, 67);
            label2.Name = "label2";
            label2.Size = new Size(49, 20);
            label2.TabIndex = 0;
            label2.Text = "Status";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(110, 67);
            label3.Name = "label3";
            label3.Size = new Size(49, 20);
            label3.TabIndex = 0;
            label3.Text = "Grade";
            // 
            // cbStatus
            // 
            cbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cbStatus.FormattingEnabled = true;
            cbStatus.IntegralHeight = false;
            cbStatus.ItemHeight = 20;
            cbStatus.Location = new Point(2, 103);
            cbStatus.Name = "cbStatus";
            cbStatus.Size = new Size(90, 28);
            cbStatus.TabIndex = 1;
            // 
            // cbGrade
            // 
            cbGrade.DropDownStyle = ComboBoxStyle.DropDownList;
            cbGrade.FormattingEnabled = true;
            cbGrade.IntegralHeight = false;
            cbGrade.Location = new Point(110, 103);
            cbGrade.MaxDropDownItems = 5;
            cbGrade.Name = "cbGrade";
            cbGrade.Size = new Size(129, 28);
            cbGrade.TabIndex = 1;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(2, 139);
            label4.Name = "label4";
            label4.Size = new Size(146, 20);
            label4.TabIndex = 0;
            label4.Text = "payment time status.";
            // 
            // cbTimeStatus
            // 
            cbTimeStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cbTimeStatus.FormattingEnabled = true;
            cbTimeStatus.Location = new Point(2, 165);
            cbTimeStatus.Name = "cbTimeStatus";
            cbTimeStatus.Size = new Size(237, 28);
            cbTimeStatus.TabIndex = 1;
            // 
            // txtName
            // 
            txtName.Location = new Point(2, 31);
            txtName.Name = "txtName";
            txtName.Size = new Size(237, 27);
            txtName.TabIndex = 2;
            // 
            // pLine1
            // 
            pLine1.BackColor = Color.FromArgb(101, 101, 105);
            pLine1.Location = new Point(-8, 205);
            pLine1.Name = "pLine1";
            pLine1.Size = new Size(258, 1);
            pLine1.TabIndex = 8;
            // 
            // btnReset
            // 
            btnReset.DialogResult = DialogResult.Cancel;
            btnReset.Location = new Point(93, 223);
            btnReset.Name = "btnReset";
            btnReset.OverrideDefault.Back.Color1 = SystemColors.ControlLight;
            btnReset.OverrideDefault.Back.Color2 = SystemColors.ControlLight;
            btnReset.OverrideDefault.Border.Color1 = SystemColors.ControlLight;
            btnReset.OverrideDefault.Border.Color2 = SystemColors.ControlLight;
            btnReset.OverrideDefault.Border.Rounding = 5F;
            btnReset.OverrideDefault.Border.Width = 1;
            btnReset.OverrideDefault.Content.ShortText.Color1 = SystemColors.WindowText;
            btnReset.OverrideDefault.Content.ShortText.Color2 = SystemColors.WindowText;
            btnReset.PaletteMode = PaletteMode.ProfessionalSystem;
            btnReset.Size = new Size(63, 35);
            btnReset.StateCommon.Back.Color1 = SystemColors.ControlLight;
            btnReset.StateCommon.Back.Color2 = SystemColors.ControlLight;
            btnReset.StateCommon.Border.Color1 = SystemColors.ControlLight;
            btnReset.StateCommon.Border.Color2 = SystemColors.ControlLight;
            btnReset.StateCommon.Border.GraphicsHint = PaletteGraphicsHint.AntiAlias;
            btnReset.StateCommon.Border.Rounding = 5F;
            btnReset.StateCommon.Border.Width = 1;
            btnReset.StateCommon.Content.ShortText.Color1 = SystemColors.WindowText;
            btnReset.StateCommon.Content.ShortText.Color2 = SystemColors.WindowFrame;
            btnReset.StateCommon.Content.ShortText.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnReset.StateTracking.Back.Color1 = SystemColors.ControlDark;
            btnReset.StateTracking.Back.Color2 = SystemColors.ControlDark;
            btnReset.StateTracking.Border.Color1 = SystemColors.ControlDark;
            btnReset.StateTracking.Border.Color2 = SystemColors.ControlDark;
            btnReset.StateTracking.Border.Rounding = 5F;
            btnReset.StateTracking.Border.Width = 1;
            btnReset.TabIndex = 17;
            btnReset.Values.DropDownArrowColor = Color.Empty;
            btnReset.Values.Text = "Reset";
            // 
            // btnApply
            // 
            btnApply.Location = new Point(175, 223);
            btnApply.Name = "btnApply";
            btnApply.OverrideDefault.Back.Color1 = Color.Blue;
            btnApply.OverrideDefault.Back.Color2 = Color.Blue;
            btnApply.OverrideDefault.Border.Color1 = Color.Blue;
            btnApply.OverrideDefault.Border.Color2 = Color.Blue;
            btnApply.OverrideDefault.Border.Rounding = 5F;
            btnApply.OverrideDefault.Border.Width = 1;
            btnApply.OverrideDefault.Content.ShortText.Color1 = Color.White;
            btnApply.OverrideDefault.Content.ShortText.Color2 = Color.White;
            btnApply.PaletteMode = PaletteMode.ProfessionalSystem;
            btnApply.Size = new Size(64, 35);
            btnApply.StateCommon.Back.Color1 = Color.Blue;
            btnApply.StateCommon.Back.Color2 = Color.Blue;
            btnApply.StateCommon.Border.Color1 = Color.Blue;
            btnApply.StateCommon.Border.Color2 = Color.Blue;
            btnApply.StateCommon.Border.GraphicsHint = PaletteGraphicsHint.AntiAlias;
            btnApply.StateCommon.Border.Rounding = 5F;
            btnApply.StateCommon.Border.Width = 1;
            btnApply.StateCommon.Content.ShortText.Color1 = Color.White;
            btnApply.StateCommon.Content.ShortText.Color2 = Color.White;
            btnApply.StateCommon.Content.ShortText.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnApply.StateTracking.Back.Color1 = Color.MediumBlue;
            btnApply.StateTracking.Back.Color2 = Color.MediumBlue;
            btnApply.StateTracking.Border.Color1 = Color.MediumBlue;
            btnApply.StateTracking.Border.Color2 = Color.MediumBlue;
            btnApply.StateTracking.Border.Rounding = 5F;
            btnApply.StateTracking.Border.Width = 1;
            btnApply.TabIndex = 18;
            btnApply.Values.DropDownArrowColor = Color.Empty;
            btnApply.Values.Text = "Apply";
            // 
            // formFilterStudentPayment
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.White;
            ClientSize = new Size(244, 278);
            Controls.Add(btnApply);
            Controls.Add(btnReset);
            Controls.Add(pLine1);
            Controls.Add(txtName);
            Controls.Add(cbGrade);
            Controls.Add(cbTimeStatus);
            Controls.Add(cbStatus);
            Controls.Add(label3);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MaximumSize = new Size(260, 294);
            MinimizeBox = false;
            MinimumSize = new Size(260, 294);
            Name = "formFilterStudentPayment";
            PaletteMode = PaletteMode.ProfessionalSystem;
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            StateCommon.Back.Color1 = Color.White;
            StateCommon.Back.Color2 = Color.White;
            StateCommon.Border.Color1 = Color.Silver;
            StateCommon.Border.Color2 = Color.Silver;
            StateCommon.Border.Draw = InheritBool.True;
            StateCommon.Border.DrawBorders = PaletteDrawBorders.Top | PaletteDrawBorders.Bottom | PaletteDrawBorders.Left | PaletteDrawBorders.Right;
            StateCommon.Border.Rounding = 12F;
            StateCommon.Header.Back.Color1 = Color.White;
            StateCommon.Header.Back.Color2 = Color.White;
            StateCommon.Header.Border.Color1 = Color.White;
            StateCommon.Header.Border.Color2 = Color.White;
            StateCommon.Header.Border.Width = 0;
            ResumeLayout(false);
            PerformLayout();
        }

        private Microsoft.Data.SqlClient.SqlCommand sqlCommand1;
        private Krypton.Toolkit.KryptonCustomPaletteBase PaletteBase;
        private Label label1;
        private Label label2;
        private Label label3;
        private ComboBox cbStatus;
        private ComboBox cbGrade;
        private ComboBox cbTimeStatus;
        private Label label4;
        private Krypton.Toolkit.KryptonTextBox txtName;
        private Panel pLine1;
        protected Krypton.Toolkit.KryptonButton btnReset;
        protected Krypton.Toolkit.KryptonButton btnApply;

    }
}
