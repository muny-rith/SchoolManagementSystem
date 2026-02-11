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
    public partial class FormSalaryControl : Form
    {
        mgmSalary mgmSalary = new mgmSalary();
        DatabaseConnection conn = DatabaseConnection.Instance;

        string queryLoadData = @$"
                SELECT 
                    s.SalaryID,
                    s.ClassTeacherID,
                    s.Year,
                    s.Month,
                    s.BaseSalary,
                    s.Gift,
                    s.Other,
                    s.Total,

                    ct.ClassID,
                    ct.TeacherID,

                    c.Name AS ClassName,
                    c.Price AS ClassPrice,

                    t.NameEng AS TeacherNameEng,
                    t.NameKh AS TeacherNameKh,
                    t.Sex AS TeacherSex
                FROM Salary s
                INNER JOIN ClassTeacher ct ON s.ClassTeacherID = ct.ClassTeacherID
                INNER JOIN Class c ON ct.ClassID = c.ClassID
                INNER JOIN Teacher t ON ct.TeacherID = t.TeacherID
                WHERE s.Year = @Year AND s.Month = @Month
                AND t.TeacherID = @TeacherID
                ORDER BY s.SalaryID;
            ";

        public FormSalaryControl(TeacherSalarySummary teacherSalarySummary)
        {
            InitializeComponent();
            this.dgv = TemplateUI.CreateStyledGrid();

            loadControls();
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@Year", teacherSalarySummary.Year },
                { "@Month", teacherSalarySummary.Month },

            };
            parameters.Add("@TeacherID",teacherSalarySummary.TeacherID);
            mgmSalary.LoadData(conn.GetConnection(),queryLoadData,parameters);
            viewDgv(mgmSalary.Salaries);
            lbTeacher.Text = teacherSalarySummary.TeacherNameEng;
            lbTotal.Text = teacherSalarySummary.TotalSalary.ToString("F2")+" $";


            btnDetail.Click += (s, e) => { if (dgv.CurrentRow != null && dgv.CurrentRow.Tag is Salary sa) { showSalaryDetail(sa.SalaryID,sa.ClassTeacher.Teacher.NameEng,sa.ClassTeacher.Class.Name,sa.Total); } };


        }

        private void showSalaryDetail(int salaryID,string TeacherName,string ClassName,decimal salary)
        {
            FormSalaryDetail form = new FormSalaryDetail(salaryID,  TeacherName, ClassName,salary);
            
            form.Show();
            //if (form.DialogResult == DialogResult.OK)
            //    {
            //        //mes
            //    }
            //}
        }

        private void viewDgv(List<Salary> salaries)
        {
            int i = 1;
            dgv.Rows.Clear();
            foreach (Salary s in salaries)
            {
                int index = dgv.Rows.Add(s.SalaryID,s.ClassTeacher.Class.Name,s.BaseSalary,s.Gift,s.Other,s.Total);
                dgv.Rows[index].Tag = s ;
                s.Tag = dgv.Rows[index];
            }
        }
        private void loadControls()
        {
            dgv.Columns.Clear();
            dgv.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn
                {
                    Name ="colSalaryID",
                    HeaderText = "Id"
                },
                new DataGridViewTextBoxColumn
                {
                    Name ="colSalaryClass",
                    HeaderText = "Class"
                },
                new DataGridViewTextBoxColumn
                {
                    Name ="colSalaryBaseSalary",
                    HeaderText = "BaseSalary"
                },
                new DataGridViewTextBoxColumn
                {
                    Name ="colSalaryGift",
                    HeaderText = "Gift"
                },
                new DataGridViewTextBoxColumn
                {
                    Name ="colSalaryOther",
                    HeaderText = "Other"
                },
                new DataGridViewTextBoxColumn
                {
                    Name ="colSalaryTotal",
                    HeaderText = "Total",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                    Width = 200
                },
            });
            panelDgv.Controls.Clear();
            panelDgv.Controls.Add(dgv);
        }
    }
}
