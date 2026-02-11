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
    public partial class FormSalaryDetail : Form
    {
        mgmSalaryDetail mgmSalaryDetail = new mgmSalaryDetail();
        DatabaseConnection conn = DatabaseConnection.Instance;
        public FormSalaryDetail(int salaryID, string TeacherName, string ClassName,decimal salary)
        {
            InitializeComponent();
            this.Text = TeacherName;
            lbClass.Text = ClassName;
            lbSalary.Text = salary.ToString("F2");

            this.dgv = TemplateUI.CreateStyledGrid();
            dgv.Dock = DockStyle.Fill;
            dgv.Width = 780;

            dgv.ScrollBars = ScrollBars.None;

            dgv.MouseWheel += (s, e) =>
            {
                int row = dgv.FirstDisplayedScrollingRowIndex;
                int step = e.Delta > 0 ? -1 : 1; // up or down
                int newRow = Math.Max(0, Math.Min(dgv.RowCount - 1, row + step));
                dgv.FirstDisplayedScrollingRowIndex = newRow;
            };


            loadControls();


            Dictionary<string, object> para = new Dictionary<string, object>
            {
                { "@SalaryID", salaryID }
            };

            mgmSalaryDetail.loadDataFromDB(conn.GetConnection(),null,para);
            SalaryDetail s = mgmSalaryDetail.salaryDetails.FirstOrDefault();
            lbPaid.Text = mgmSalaryDetail.salaryDetails.Where(e => e.IsPaid).Count().ToString();
            lbUnPaid.Text = mgmSalaryDetail.salaryDetails.Where( e => !e.IsPaid).Count().ToString();
            lbTotal.Text = mgmSalaryDetail.salaryDetails.Count().ToString();

            viewDgv(mgmSalaryDetail.salaryDetails);
        }

        private void viewDgv(List<SalaryDetail> salaryDetails)
        {
            dgv.Rows.Clear();
            int i = 1;
            foreach(SalaryDetail detail in salaryDetails)
            {
                System.Drawing.Image image = Properties.Resources.Unpaid;

                if (detail.IsPaid)
                {
                    image = Properties.Resources.PaidNewMonth;
                }
                int index = dgv.Rows.Add(detail.SalaryDetailID, detail.Student.NameEng, detail.Student.NameKh, detail.Student.Sex,"", image, "" );
                dgv.Rows[index].Tag = detail;
                detail.Tag = dgv.Rows[index];
            }
        }

        private void loadControls()
        {
            dgv.Columns.Clear();
            dgv.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn
                {
                    Name ="colSalaryDetailID",
                    HeaderText = "Id",
                    Width = 80,               // 👈 Make sure this matches the icon size (e.g., 16x16 or 24x24)
                    MinimumWidth = 80,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                },
                new DataGridViewTextBoxColumn
                {
                    Name ="colStuNameEng",
                    HeaderText = "NameEng",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                },
                new DataGridViewTextBoxColumn
                {
                    Name ="colStuNameKh",
                    HeaderText = "NameKh"
                },
                new DataGridViewTextBoxColumn
                {
                    Name ="colStuSex",
                    HeaderText = "Sex",
                    Width = 50,               // 👈 Make sure this matches the icon size (e.g., 16x16 or 24x24)
                    MinimumWidth = 50,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                },          
                new DataGridViewTextBoxColumn
                {
                    Name ="colOption",
                    HeaderText = "",
                    Width = 70,               // 👈 Make sure this matches the icon size (e.g., 16x16 or 24x24)
                    MinimumWidth = 70,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                },
                new DataGridViewImageColumn
                {
                    Name = "colStatus",
                    HeaderText = "",
                    ImageLayout = DataGridViewImageCellLayout.Zoom, // or Zoom
                    Width = 60,               // 👈 Make sure this matches the icon size (e.g., 16x16 or 24x24)
                    MinimumWidth = 60,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None,

                    ReadOnly = true,
                },
                new DataGridViewTextBoxColumn
                {
                    Name ="colOption",
                    HeaderText = "",
                    Width = 50,               // 👈 Make sure this matches the icon size (e.g., 16x16 or 24x24)
                    MinimumWidth = 50,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                },

            });
            panelDgv.Controls.Clear();
            panelDgv.Controls.Add(dgv);
        }
    }
}
