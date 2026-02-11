using Krypton.Navigator;
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
using MaterialSkin;
using MaterialSkin.Controls;
using Krypton.Toolkit;
using static Azure.Core.HttpHeader;

namespace SchoolManagementSystem
{
    public partial class formFilterStudent : Krypton.Toolkit.KryptonForm
    {

        string name = "";
        string sex = "";
        string classID = "";
        string date = "";

        public string Name { get => name; set => name = value; }
        public string Sex { get => sex; set => sex = value; }
        public string ClassID { get => classID; set => classID = value; }
        public string Date { get => date; set => date = value; }
        public event EventHandler FilterApplied;
        mgmClass mgmGrades = new mgmClass();
        DatabaseConnection con = DatabaseConnection.Instance;
        public formFilterStudent()
        {

            InitializeComponent();
            //cbSex.SelectedItem = cbSex.Items[0];
            mgmGrades.loadDataFromDB(con.GetConnection());
            if (mgmGrades.Classes.Any())
            {
                cbGrade.Items.Clear();
                var names = mgmGrades.Classes
                    .Where(g => !string.IsNullOrWhiteSpace(g.Name))
                    .Select(g => g.Name)
                    .ToArray();
                cbGrade.Items.AddRange(names);
                //cbGrade.SelectedItem = cbGrade.Items[0];
            }
            string[] array = new string[4] { "This Month", "Last 2 Months", "Last 3 Months", "All Times" };
            cbDate.Items.AddRange(array);
            //cbDate.SelectedItem = cbDate.Items[0];
            btnApply.Click += (s, e) => doApply();
            btnReset.Click += (s, e) => doReset();
            btnReset.DialogResult = DialogResult.None;
            this.Deactivate += (s, e) => this.Close();


        }

        private void doApply()
        {
            if (cbSex.SelectedItem != null)
            {
                Sex = cbSex.SelectedItem.ToString();
            }
            if (cbGrade.SelectedItem != null)
            {
                string gname = cbGrade.SelectedItem.ToString();
                var grade = mgmGrades.Classes.FirstOrDefault(e => e.Name == gname);
                if (grade != null)
                {
                    ClassID = grade.ClassID.ToString();
                }
            }
            if (cbDate.SelectedItem != null)
            {
                Date = cbDate.SelectedItem.ToString();
            }
            Name = txtName.Text;
            this.DialogResult = DialogResult.Yes;
            FilterApplied.Invoke(this, new EventArgs());
            this.Close();
        }
        private void doReset()
        {
            txtName.Clear();
            cbSex.SelectedIndex = -1;
            cbGrade.SelectedIndex = -1;
            cbDate.SelectedIndex = -1;
        }
        private void InitializeComponent()
        {
            components = new Container();
            sqlCommand1 = new Microsoft.Data.SqlClient.SqlCommand();
            PaletteBase = new KryptonCustomPaletteBase(components);
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            cbSex = new ComboBox();
            cbGrade = new ComboBox();
            label4 = new Label();
            cbDate = new ComboBox();
            txtName = new KryptonTextBox();
            pLine1 = new Panel();
            btnReset = new KryptonButton();
            btnApply = new KryptonButton();
            SuspendLayout();
            // 
            // sqlCommand1
            // 
            sqlCommand1.CommandTimeout = 30;
            sqlCommand1.EnableOptimizedParameterBinding = false;
            // 
            // PaletteBase
            // 
            PaletteBase.FormStyles.FormCommon.StateCommon.Back.Color1 = Color.White;
            PaletteBase.FormStyles.FormCommon.StateCommon.Back.Color2 = Color.White;
            PaletteBase.FormStyles.FormCommon.StateCommon.Border.Color1 = Color.Silver;
            PaletteBase.FormStyles.FormCommon.StateCommon.Border.Color2 = Color.Silver;
            PaletteBase.FormStyles.FormCommon.StateCommon.Border.GraphicsHint = PaletteGraphicsHint.AntiAlias;
            PaletteBase.FormStyles.FormCommon.StateCommon.Border.Rounding = 12F;
            PaletteBase.FormStyles.FormCommon.StateCommon.Border.Width = 1;
            PaletteBase.UseThemeFormChromeBorderWidth = InheritBool.True;
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
            label2.Size = new Size(32, 20);
            label2.TabIndex = 0;
            label2.Text = "Sex";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(128, 67);
            label3.Name = "label3";
            label3.Size = new Size(49, 20);
            label3.TabIndex = 0;
            label3.Text = "Grade";
            // 
            // cbSex
            // 
            cbSex.DropDownStyle = ComboBoxStyle.DropDownList;
            cbSex.FormattingEnabled = true;
            cbSex.Items.AddRange(new object[] { "Male", "Female" });
            cbSex.Location = new Point(2, 103);
            cbSex.Name = "cbSex";
            cbSex.Size = new Size(90, 28);
            cbSex.TabIndex = 1;
            // 
            // cbGrade
            // 
            cbGrade.DropDownStyle = ComboBoxStyle.DropDownList;
            cbGrade.FormattingEnabled = true;
            cbGrade.Location = new Point(128, 103);
            cbGrade.Name = "cbGrade";
            cbGrade.Size = new Size(111, 28);
            cbGrade.TabIndex = 1;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(2, 139);
            label4.Name = "label4";
            label4.Size = new Size(41, 20);
            label4.TabIndex = 0;
            label4.Text = "Date";
            // 
            // cbDate
            // 
            cbDate.DropDownStyle = ComboBoxStyle.DropDownList;
            cbDate.FormattingEnabled = true;
            cbDate.Location = new Point(2, 165);
            cbDate.Name = "cbDate";
            cbDate.Size = new Size(237, 28);
            cbDate.TabIndex = 1;
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
            // formFilterStudent
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
            Controls.Add(cbDate);
            Controls.Add(cbSex);
            Controls.Add(label3);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MaximumSize = new Size(260, 294);
            MinimizeBox = false;
            MinimumSize = new Size(260, 294);
            Name = "formFilterStudent";
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
        private ComboBox cbSex;
        private ComboBox cbGrade;
        private ComboBox cbDate;
        private Label label4;
        private Krypton.Toolkit.KryptonTextBox txtName;
        private Panel pLine1;
        protected Krypton.Toolkit.KryptonButton btnReset;
        protected Krypton.Toolkit.KryptonButton btnApply;

    }
}
