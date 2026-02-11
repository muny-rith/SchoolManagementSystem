using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolManagementSystem
{
    public partial class payrollTeacherControl : baseUserControl,IStrategyUserControl<Payroll>
    {
        DatabaseConnection conn = DatabaseConnection.Instance;
        mgmPayroll mgmPayroll = new mgmPayroll();
        PaginationMgm<Payroll> paginator;
        public payrollTeacherControl()
        {
            mgmPayroll.loadDataFromDB(conn.GetConnection());
            //mgmSalary.loadData(conn.GetConnection());
            InitializeComponent();
            doDesignDgv();
            
            paginator = new PaginationMgm<Payroll>(mgmPayroll.payrollTeachers,viewDgv);
            paginator.LoadPage();
            btnNext.Click += (s, e) => paginator.NextPage();
            btnPre.Click += (s, e) => paginator.PreviousPage();
            txtInsert = "Set Payroll";

            btnInformation.Click += (s, e) => { if (dgv.CurrentRow?.Tag is Payroll) { information(); } }; // confirm payroll
            btnInsert.Click += (s, e) => insert();
            btnUpdate.Click += (s, e) => { if (dgv.CurrentRow?.Tag is Payroll) { update(); } };
            btnDelete.Click += (s, e) => { if (dgv.CurrentRow?.Tag is Payroll) { delete(); } };


            txtSearch.TextChanged += (s, e) => search(txtSearch.Text);
            btnFilter.Visible  = false;
            btnExport.Visible = false;
        }


        public void viewDgv(List<Payroll> pageDate)
        {
            dgv.Rows.Clear();
            int i = (paginator.TotalPages-1)*15+1;
            foreach (var payroll in pageDate)
            {
                int index = dgv.Rows.Add(i, payroll.Teacher.NameEng, payroll.Teacher.NameKh, payroll.BaseSalary.ToString("F2"), payroll.Rate+" %", payroll.Other.ToString("F2"));
                dgv.Rows[index].Tag = payroll;
                payroll.Tag = dgv.Rows[index];
                i++;
            }
            lbCurrentPage.Text = paginator.CurrentPage.ToString("D2");
            if(paginator.CurrentPage == 1 ) btnPre.Enabled = false;
            if(paginator.CurrentPage == paginator.TotalPages ) btnNext.Enabled = false;

        }
        public void information() // confirm payroll
        {
            using(FormSalaryConfirm form =  new FormSalaryConfirm((dgv.CurrentRow.Tag as Payroll).TeacherID))
            {
                
                if(form.ShowDialog() == DialogResult.OK)
                {

                }
            }
        }
        public void insert()
        {
            using(FormUpdatePayroll form = new FormUpdatePayroll())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    int teacherID = form.TeacherID;
                    decimal rate = form.Rate;
                    decimal baseSalary = form.BaseSalary;

                    string query = @"
                        INSERT INTO Payroll (TeacherID, Rate, BaseSalary)
                        VALUES (@TeacherID, @Rate , @BaseSalary);
                    ";

                    using (SqlCommand cmd = new SqlCommand(query, conn.GetConnection()))
                    {
                        cmd.Parameters.AddWithValue("@TeacherID", teacherID);
                        cmd.Parameters.AddWithValue("@Rate", rate);
                        cmd.Parameters.AddWithValue("@BaseSalary", baseSalary);


                        cmd.ExecuteNonQuery();
                    }
                    paginator.AllData = mgmPayroll.loadDataFromDB(conn.GetConnection());

                }

            }

        }
        public void update()
        {
            using(FormUpdatePayroll form = new FormUpdatePayroll((dgv.CurrentRow.Tag as Payroll).TeacherID))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    int TeacherID = form.TeacherID;
                    decimal rate = form.Rate;
                    decimal baseSalary = form.BaseSalary;

                    string query = @"
                        UPDATE Payroll
                        SET Rate = @Rate, BaseSalary = @BaseSalary 
                        WHERE TeacherID = @TeacherID
                    ";

                    using (SqlCommand cmd = new SqlCommand(query, conn.GetConnection()))
                    {
                        cmd.Parameters.AddWithValue("@Rate", rate);
                        cmd.Parameters.AddWithValue("@BaseSalary", baseSalary);
                        cmd.Parameters.AddWithValue("@TeacherID", TeacherID);

                        cmd.ExecuteNonQuery();
                    }
                    paginator.AllData = mgmPayroll.loadDataFromDB(conn.GetConnection());

                }

            }

        }
        public void delete()
        {
            using (FormUpdatePayroll form = new FormUpdatePayroll((dgv.CurrentRow.Tag as Payroll).TeacherID))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    int teacID = (dgv.CurrentRow.Tag as Payroll).TeacherID;

                    string query = @"
                        DELETE FROM Payroll
                        WHERE TeacherID = @TeacherID
                    ";

                    using (SqlCommand cmd = new SqlCommand(query, conn.GetConnection()))
                    {
                        cmd.Parameters.AddWithValue("@TeacherID", teacID);
                        cmd.ExecuteNonQuery();
                    }

                }
                paginator.AllData = mgmPayroll.loadDataFromDB(conn.GetConnection());


            }


        }
        public void search(string data)
        {
            var keyword = data?.Trim().ToLower();
            var list = string.IsNullOrEmpty(keyword)
                ? mgmPayroll.payrollTeachers
                : mgmPayroll.payrollTeachers.Where(e =>
                    (e.Teacher.NameEng?.ToLower().Contains(keyword) ?? false) ||
                    (e.Teacher.NameKh?.ToLower().Contains(keyword) ?? false));

            paginator.AllData = list.ToList();
            paginator.Refresh();
        }

        private void doDesignDgv()
        {
            this.dgv.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn
                {
                    Name = "colID",
                    HeaderText = "No",

                },
                new DataGridViewTextBoxColumn
                {
                    Name = "colNameEng",
                    HeaderText = "NameEng",
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "colNameKh",
                    HeaderText = "NameKh"
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "colBaseSalary",
                    HeaderText ="Base Salary"
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "colRate",
                    HeaderText ="Rate"
                },

                new DataGridViewTextBoxColumn
                {
                    Name = "colOtherSalary",
                    HeaderText ="Other"
                },
            });
            dgv.MouseWheel += (s, e) =>
            {
                if (e.Delta < 0)
                    paginator.NextPage();
                else if (e.Delta > 0)
                    paginator.PreviousPage();

                // Delay to avoid rapid paging (adjust as needed)
                Task.Delay(200);
            };
        }

    }
}
