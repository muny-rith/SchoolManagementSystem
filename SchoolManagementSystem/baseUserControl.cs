using Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolManagementSystem
{
    public partial class baseUserControl : UserControl
    {
        public baseUserControl()
        {
            InitializeComponent();
            //this.Dock = DockStyle.Fill;
            this.dgv = TemplateUI.CreateStyledGrid();
            this.dgv.Width = this.Width;
            //dgv.RowHeadersDefaultCellStyle

            //dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            //dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            //dgv.MultiSelect = false; // Optional: only allow one row to be selected
            //dataGridView1.CellClick += dataGridView1_CellClick;
            //dgv.SelectionChanged += dgv_SelectionChanged;
            //dgv.CellClick += dgv_CellClick;
            this.panelDgv.Controls.Add(this.dgv);

        }



        // Create the KryptonButton
        public virtual string Title { set { lbTitle.Text = value; } }
        //public virtual string ListTilte { set { lbListTitle.Text = value; }  }
        public virtual string ListTilte
        {
            get { return lbListTitle.Text; }
            set { lbListTitle.Text = value; }
        }

        public virtual string txtInsert { set => btnInsert.Text = value; }
        public string LB1 { set { lb1.Text = value; } }
        public string LB2 { set { lb2.Text = value; } }
        public string LB3 { set { lb3.Text = value; } }
        public string LB4 { set { lb4.Text = value; } }
        public string LB5 { set { lb5.Text = value; } }
        public string LB6 { set { lb6.Text = value; } }
        //public abstract void viewDgv(List<T>);

        private void InitializeComponent()
        {
            components = new Container();
            lbTitle = new Label();
            btnFilter = new KryptonButton();
            panelDgv = new Panel();
            lb5 = new Label();
            lb1 = new Label();
            lb3 = new Label();
            lb2 = new Label();
            lb6 = new Label();
            lb4 = new Label();
            Pagination = new Panel();
            btnPre = new Button();
            lbCurrentPage = new Label();
            btnNext = new Button();
            txtSearch = new TextBox();
            lbListTitle = new Label();
            btnUpdate = new Button();
            btnInformation = new Button();
            btnDelete = new Button();
            PaletteBase = new KryptonCustomPaletteBase(components);
            dataGridView1 = new DataGridView();
            btnInsert = new KryptonButton();
            btnExport = new KryptonButton();
            panel1 = new Panel();
            panelContent = new Panel();
            panel4 = new Panel();
            panel6 = new Panel();
            panel5 = new Panel();
            panel3 = new Panel();
            Pagination.SuspendLayout();
            ((ISupportInitialize)dataGridView1).BeginInit();
            panel1.SuspendLayout();
            panelContent.SuspendLayout();
            panel4.SuspendLayout();
            panel6.SuspendLayout();
            panel5.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // lbTitle
            // 
            lbTitle.AutoSize = true;
            lbTitle.Font = new Font("Poppins", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbTitle.ForeColor = SystemColors.ControlText;
            lbTitle.Location = new Point(15, 7);
            lbTitle.Name = "lbTitle";
            lbTitle.Size = new Size(45, 28);
            lbTitle.TabIndex = 12;
            lbTitle.Text = "Title";
            // 
            // btnFilter
            // 
            btnFilter.DialogResult = DialogResult.Cancel;
            btnFilter.Location = new Point(661, 10);
            btnFilter.Margin = new Padding(3, 2, 3, 2);
            btnFilter.Name = "btnFilter";
            btnFilter.OverrideDefault.Back.Color1 = SystemColors.ControlLight;
            btnFilter.OverrideDefault.Back.Color2 = SystemColors.ControlLight;
            btnFilter.OverrideDefault.Border.Color1 = SystemColors.ControlLight;
            btnFilter.OverrideDefault.Border.Color2 = SystemColors.ControlLight;
            btnFilter.OverrideDefault.Border.Rounding = 5F;
            btnFilter.OverrideDefault.Border.Width = 1;
            btnFilter.OverrideDefault.Content.ShortText.Color1 = SystemColors.WindowText;
            btnFilter.OverrideDefault.Content.ShortText.Color2 = SystemColors.WindowText;
            btnFilter.PaletteMode = PaletteMode.ProfessionalSystem;
            btnFilter.Size = new Size(99, 26);
            btnFilter.StateCommon.Back.Color1 = SystemColors.ControlLight;
            btnFilter.StateCommon.Back.Color2 = SystemColors.ControlLight;
            btnFilter.StateCommon.Border.Color1 = SystemColors.ControlLight;
            btnFilter.StateCommon.Border.Color2 = SystemColors.ControlLight;
            btnFilter.StateCommon.Border.GraphicsHint = PaletteGraphicsHint.AntiAlias;
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
            btnFilter.TabIndex = 16;
            btnFilter.Values.DropDownArrowColor = Color.Empty;
            btnFilter.Values.Text = "Filter";
            btnFilter.Click += btnFilter_Click;
            // 
            // panelDgv
            // 
            panelDgv.BackColor = Color.Snow;
            panelDgv.Dock = DockStyle.Fill;
            panelDgv.Location = new Point(0, 0);
            panelDgv.Margin = new Padding(3, 2, 3, 2);
            panelDgv.Name = "panelDgv";
            panelDgv.Size = new Size(942, 475);
            panelDgv.TabIndex = 15;
            // 
            // lb5
            // 
            lb5.AutoSize = true;
            lb5.Font = new Font("Poppins", 12F);
            lb5.Location = new Point(240, 5);
            lb5.Name = "lb5";
            lb5.Size = new Size(39, 28);
            lb5.TabIndex = 18;
            lb5.Text = "LB5";
            // 
            // lb1
            // 
            lb1.AutoSize = true;
            lb1.Font = new Font("Poppins", 12F);
            lb1.Location = new Point(15, 5);
            lb1.Name = "lb1";
            lb1.Size = new Size(34, 28);
            lb1.TabIndex = 23;
            lb1.Text = "LB1";
            // 
            // lb3
            // 
            lb3.AutoSize = true;
            lb3.Font = new Font("Poppins", 12F);
            lb3.Location = new Point(122, 5);
            lb3.Name = "lb3";
            lb3.Size = new Size(38, 28);
            lb3.TabIndex = 19;
            lb3.Text = "LB3";
            // 
            // lb2
            // 
            lb2.AutoSize = true;
            lb2.Font = new Font("Poppins", 12F);
            lb2.Location = new Point(71, 5);
            lb2.Name = "lb2";
            lb2.Size = new Size(38, 28);
            lb2.TabIndex = 22;
            lb2.Text = "LB2";
            // 
            // lb6
            // 
            lb6.AutoSize = true;
            lb6.Font = new Font("Poppins", 12F);
            lb6.Location = new Point(321, 5);
            lb6.Name = "lb6";
            lb6.Size = new Size(39, 28);
            lb6.TabIndex = 20;
            lb6.Text = "LB6";
            // 
            // lb4
            // 
            lb4.AutoSize = true;
            lb4.Font = new Font("Poppins", 12F);
            lb4.Location = new Point(177, 5);
            lb4.Name = "lb4";
            lb4.Size = new Size(39, 28);
            lb4.TabIndex = 21;
            lb4.Text = "LB4";
            // 
            // Pagination
            // 
            Pagination.Controls.Add(btnPre);
            Pagination.Controls.Add(lbCurrentPage);
            Pagination.Controls.Add(btnNext);
            Pagination.Location = new Point(799, 11);
            Pagination.Margin = new Padding(3, 2, 3, 2);
            Pagination.Name = "Pagination";
            Pagination.Size = new Size(132, 43);
            Pagination.TabIndex = 14;
            // 
            // btnPre
            // 
            btnPre.Enabled = false;
            btnPre.Font = new Font("Poppins", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnPre.Location = new Point(13, 6);
            btnPre.Margin = new Padding(3, 2, 3, 2);
            btnPre.Name = "btnPre";
            btnPre.Size = new Size(36, 31);
            btnPre.TabIndex = 12;
            btnPre.Text = "<";
            btnPre.UseVisualStyleBackColor = true;
            // 
            // lbCurrentPage
            // 
            lbCurrentPage.AutoSize = true;
            lbCurrentPage.Font = new Font("Poppins", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbCurrentPage.Location = new Point(55, 11);
            lbCurrentPage.Name = "lbCurrentPage";
            lbCurrentPage.Size = new Size(25, 25);
            lbCurrentPage.TabIndex = 13;
            lbCurrentPage.Text = "01";
            lbCurrentPage.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnNext
            // 
            btnNext.Enabled = false;
            btnNext.Font = new Font("Poppins", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnNext.Location = new Point(85, 6);
            btnNext.Margin = new Padding(3, 2, 3, 2);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(36, 31);
            btnNext.TabIndex = 12;
            btnNext.Text = ">";
            btnNext.UseVisualStyleBackColor = true;
            // 
            // txtSearch
            // 
            txtSearch.BackColor = SystemColors.HighlightText;
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            txtSearch.Font = new Font("Poppins", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtSearch.Location = new Point(799, 10);
            txtSearch.Margin = new Padding(3, 2, 3, 2);
            txtSearch.MaximumSize = new Size(132, 35);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "  Search";
            txtSearch.Size = new Size(132, 25);
            txtSearch.TabIndex = 9;
            // 
            // lbListTitle
            // 
            lbListTitle.AutoSize = true;
            lbListTitle.Font = new Font("Roboto", 13.2000008F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbListTitle.Location = new Point(15, 14);
            lbListTitle.Name = "lbListTitle";
            lbListTitle.Size = new Size(78, 22);
            lbListTitle.TabIndex = 11;
            lbListTitle.Text = "List Title";
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(154, 37);
            btnUpdate.Margin = new Padding(3, 2, 3, 2);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(97, 26);
            btnUpdate.TabIndex = 6;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = true;
            // 
            // btnInformation
            // 
            btnInformation.Location = new Point(0, 37);
            btnInformation.Margin = new Padding(3, 2, 3, 2);
            btnInformation.Name = "btnInformation";
            btnInformation.Size = new Size(113, 26);
            btnInformation.TabIndex = 4;
            btnInformation.Text = "Information";
            btnInformation.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(298, 37);
            btnDelete.Margin = new Padding(3, 2, 3, 2);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(91, 26);
            btnDelete.TabIndex = 7;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            // 
            // PaletteBase
            // 
            PaletteBase.Common.StateCommon.Border.Color1 = Color.FromArgb(224, 224, 224);
            PaletteBase.Common.StateCommon.Border.Color2 = Color.FromArgb(224, 224, 224);
            PaletteBase.Common.StateCommon.Border.Rounding = 5F;
            PaletteBase.Common.StateCommon.Border.Width = 1;
            PaletteBase.FormStyles.FormCommon.StateCommon.Back.Color1 = Color.White;
            PaletteBase.FormStyles.FormCommon.StateCommon.Border.Color1 = Color.Silver;
            PaletteBase.PanelStyles.PanelCommon.StateCommon.Color1 = Color.FromArgb(255, 192, 128);
            PaletteBase.PanelStyles.PanelCommon.StateCommon.Color2 = Color.FromArgb(192, 64, 0);
            PaletteBase.UseThemeFormChromeBorderWidth = InheritBool.True;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(234, 142);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(271, 139);
            dataGridView1.TabIndex = 0;
            // 
            // btnInsert
            // 
            btnInsert.Location = new Point(832, 7);
            btnInsert.Margin = new Padding(3, 2, 3, 2);
            btnInsert.Name = "btnInsert";
            btnInsert.OverrideDefault.Back.Color1 = Color.Blue;
            btnInsert.OverrideDefault.Back.Color2 = Color.Blue;
            btnInsert.OverrideDefault.Border.Color1 = Color.Blue;
            btnInsert.OverrideDefault.Border.Color2 = Color.Blue;
            btnInsert.OverrideDefault.Border.Rounding = 5F;
            btnInsert.OverrideDefault.Border.Width = 1;
            btnInsert.OverrideDefault.Content.ShortText.Color1 = Color.White;
            btnInsert.OverrideDefault.Content.ShortText.Color2 = Color.White;
            btnInsert.PaletteMode = PaletteMode.ProfessionalSystem;
            btnInsert.Size = new Size(99, 26);
            btnInsert.StateCommon.Back.Color1 = Color.Blue;
            btnInsert.StateCommon.Back.Color2 = Color.Blue;
            btnInsert.StateCommon.Border.Color1 = Color.Blue;
            btnInsert.StateCommon.Border.Color2 = Color.Blue;
            btnInsert.StateCommon.Border.GraphicsHint = PaletteGraphicsHint.AntiAlias;
            btnInsert.StateCommon.Border.Rounding = 5F;
            btnInsert.StateCommon.Border.Width = 1;
            btnInsert.StateCommon.Content.ShortText.Color1 = Color.White;
            btnInsert.StateCommon.Content.ShortText.Color2 = Color.White;
            btnInsert.StateCommon.Content.ShortText.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnInsert.StateTracking.Back.Color1 = Color.MediumBlue;
            btnInsert.StateTracking.Back.Color2 = Color.MediumBlue;
            btnInsert.StateTracking.Border.Color1 = Color.MediumBlue;
            btnInsert.StateTracking.Border.Color2 = Color.MediumBlue;
            btnInsert.StateTracking.Border.Rounding = 5F;
            btnInsert.StateTracking.Border.Width = 1;
            btnInsert.TabIndex = 16;
            btnInsert.Values.DropDownArrowColor = Color.Empty;
            btnInsert.Values.Text = "txtInsert";
            // 
            // btnExport
            // 
            btnExport.DialogResult = DialogResult.Cancel;
            btnExport.Location = new Point(697, 7);
            btnExport.Margin = new Padding(3, 2, 3, 2);
            btnExport.Name = "btnExport";
            btnExport.OverrideDefault.Back.Color1 = SystemColors.ControlLight;
            btnExport.OverrideDefault.Back.Color2 = SystemColors.ControlLight;
            btnExport.OverrideDefault.Border.Color1 = SystemColors.ControlLight;
            btnExport.OverrideDefault.Border.Color2 = SystemColors.ControlLight;
            btnExport.OverrideDefault.Border.Rounding = 5F;
            btnExport.OverrideDefault.Border.Width = 1;
            btnExport.OverrideDefault.Content.ShortText.Color1 = SystemColors.WindowText;
            btnExport.OverrideDefault.Content.ShortText.Color2 = SystemColors.WindowText;
            btnExport.PaletteMode = PaletteMode.ProfessionalSystem;
            btnExport.Size = new Size(99, 26);
            btnExport.StateCommon.Back.Color1 = SystemColors.ControlLight;
            btnExport.StateCommon.Back.Color2 = SystemColors.ControlLight;
            btnExport.StateCommon.Border.Color1 = SystemColors.ControlLight;
            btnExport.StateCommon.Border.Color2 = SystemColors.ControlLight;
            btnExport.StateCommon.Border.GraphicsHint = PaletteGraphicsHint.AntiAlias;
            btnExport.StateCommon.Border.Rounding = 5F;
            btnExport.StateCommon.Border.Width = 1;
            btnExport.StateCommon.Content.ShortText.Color1 = SystemColors.WindowText;
            btnExport.StateCommon.Content.ShortText.Color2 = SystemColors.WindowFrame;
            btnExport.StateCommon.Content.ShortText.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnExport.StateTracking.Back.Color1 = SystemColors.ControlDark;
            btnExport.StateTracking.Back.Color2 = SystemColors.ControlDark;
            btnExport.StateTracking.Border.Color1 = SystemColors.ControlDark;
            btnExport.StateTracking.Border.Color2 = SystemColors.ControlDark;
            btnExport.StateTracking.Border.Rounding = 5F;
            btnExport.StateTracking.Border.Width = 1;
            btnExport.TabIndex = 16;
            btnExport.Values.DropDownArrowColor = Color.Empty;
            btnExport.Values.Text = "Export";
            // 
            // panel1
            // 
            panel1.Controls.Add(lbTitle);
            panel1.Controls.Add(btnInsert);
            panel1.Controls.Add(btnExport);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(20, 20);
            panel1.Name = "panel1";
            panel1.Size = new Size(942, 38);
            panel1.TabIndex = 1;
            // 
            // panelContent
            // 
            panelContent.Controls.Add(panel4);
            panelContent.Controls.Add(panel3);
            panelContent.Dock = DockStyle.Fill;
            panelContent.Location = new Point(20, 58);
            panelContent.Name = "panelContent";
            panelContent.Size = new Size(942, 585);
            panelContent.TabIndex = 3;
            // 
            // panel4
            // 
            panel4.Controls.Add(panel6);
            panel4.Controls.Add(panel5);
            panel4.Dock = DockStyle.Fill;
            panel4.Location = new Point(0, 45);
            panel4.Name = "panel4";
            panel4.Size = new Size(942, 540);
            panel4.TabIndex = 18;
            // 
            // panel6
            // 
            panel6.Controls.Add(panelDgv);
            panel6.Dock = DockStyle.Fill;
            panel6.Location = new Point(0, 0);
            panel6.Name = "panel6";
            panel6.Size = new Size(942, 475);
            panel6.TabIndex = 17;
            // 
            // panel5
            // 
            panel5.Controls.Add(lb5);
            panel5.Controls.Add(btnInformation);
            panel5.Controls.Add(lb1);
            panel5.Controls.Add(lb3);
            panel5.Controls.Add(btnUpdate);
            panel5.Controls.Add(lb2);
            panel5.Controls.Add(Pagination);
            panel5.Controls.Add(lb6);
            panel5.Controls.Add(btnDelete);
            panel5.Controls.Add(lb4);
            panel5.Dock = DockStyle.Bottom;
            panel5.Location = new Point(0, 475);
            panel5.Name = "panel5";
            panel5.Size = new Size(942, 65);
            panel5.TabIndex = 16;
            // 
            // panel3
            // 
            panel3.Controls.Add(btnFilter);
            panel3.Controls.Add(txtSearch);
            panel3.Controls.Add(lbListTitle);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(0, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(942, 45);
            panel3.TabIndex = 17;
            // 
            // baseUserControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            Controls.Add(panelContent);
            Controls.Add(panel1);
            Margin = new Padding(3, 2, 3, 2);
            Name = "baseUserControl";
            Padding = new Padding(20);
            Size = new Size(982, 663);
            Pagination.ResumeLayout(false);
            Pagination.PerformLayout();
            ((ISupportInitialize)dataGridView1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panelContent.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel6.ResumeLayout(false);
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ResumeLayout(false);
        }

        private Label lbTitle;
        private Label lbListTitle;
        protected Panel panelDgv;
        protected DataGridView dgv;
        private Panel Pagination;
        protected Label lbCurrentPage;
        protected Button btnPre;
        protected Button btnNext;
        protected TextBox txtSearch;
        protected Krypton.Toolkit.KryptonButton btnInsert;
        protected Button btnUpdate;
        protected Button btnInformation;
        protected Button btnDelete;
        private DataGridView dataGridView1;
        protected Krypton.Toolkit.KryptonButton btnFilter;
        protected Krypton.Toolkit.KryptonButton btnExport;
        protected KryptonContextMenuItems menuItems = new KryptonContextMenuItems();
        private Krypton.Toolkit.KryptonCustomPaletteBase PaletteBase;

        private void btnFilter_Click(object sender, EventArgs e)
        {

        }
    }
}

