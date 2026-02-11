using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SchoolManagementSystem.mgmSalary;

namespace SchoolManagementSystem
{
    public partial class teacherSalarySummaryControl : baseUserControl , IStrategyUserControl<TeacherSalarySummary>
    {

        DatabaseConnection conn = DatabaseConnection.Instance;
        PaginationMgm<TeacherSalarySummary> paginator;
        mgmSalary mgmSalary = new mgmSalary();
        List<TeacherSalarySummary> list;

        public teacherSalarySummaryControl()
        {
            InitializeComponent();

            dgv.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn
                {
                    Name = "No",
                    HeaderText = "No",
                    
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "TeacherName",
                    HeaderText = "Teacher",
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "Month",
                    HeaderText = "Month",

                },
                new DataGridViewTextBoxColumn
                {
                    Name = "Year",
                    HeaderText = "Year",

                },
                new DataGridViewTextBoxColumn
                {
                    Name = "BaseSalary",
                    HeaderText = "BaseSalary",

                },
                new DataGridViewTextBoxColumn
                {
                    Name = "Gift",
                    HeaderText = "Gift",

                },
                new DataGridViewTextBoxColumn
                {
                    Name = "Other",
                    HeaderText = "Other",

                },
                new DataGridViewTextBoxColumn
                {
                    Name = "Total",
                    HeaderText = "Total",

                },

            });


            Title = "Salary Record";
            ListTilte = "List";
            btnInsert.Visible = false;
            btnUpdate.Visible = false;
            btnExport.Visible = false;
            btnFilter.Visible = false;
            txtSearch.Visible = false;



            mgmSalary.LoadData(conn.GetConnection());
            list = mgmSalary.LoadGroupedByTeacher(conn.GetConnection());
            paginator = new PaginationMgm<TeacherSalarySummary>(list, viewDgv);
            paginator.LoadPage();
            btnNext.Click += (s,e)=>  paginator.NextPage();
            btnPre.Click += (s, e) => paginator.PreviousPage();

            btnInformation.Click += (s, e) =>
            {
                if (dgv.CurrentRow?.Tag is TeacherSalarySummary)
                    information();
            };

            //btnInformation.Click += (s, e) => { if (dgv.CurrentRow != null &&  dgv.CurrentRow.Tag is TeacherSalarySummary) { information(); } };
            btnDelete.Click += (s, e) => { if (dgv.CurrentRow?.Tag is TeacherSalarySummary) { delete(); } };
        }

        public void viewDgv(List<TeacherSalarySummary> salaries)
        {
            dgv.Rows.Clear();
            int i = 1;

            foreach (TeacherSalarySummary salary in salaries)
            {
                int index  = dgv.Rows.Add(i,salary.TeacherNameEng,salary.Month,salary.Year,salary.TotalBaseSalary,salary.TotalGift,salary.TotalOther,salary.TotalSalary);
                dgv.Rows[index].Tag = salary;
                salary.Tag = dgv.Rows[index];
                i++;
            }
            lbCurrentPage.Text = paginator.CurrentPage.ToString("D2");
            btnPre.Enabled = paginator.CurrentPage > 1;
            btnNext.Enabled = paginator.CurrentPage < paginator.TotalPages;
        }
        public void information()
        {
            //MessageBox.Show("Informtion");
            FormSalaryControl form = new FormSalaryControl((dgv.CurrentRow.Tag as TeacherSalarySummary));
            //{
                form.Show();
            //}
        }
        public void insert()
        {

        }
        public void update()
        {

        }

        public void delete()
        {
            TeacherSalarySummary t = (dgv.CurrentRow.Tag as TeacherSalarySummary);

            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete this teacher's salary record(s)?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                using (conn.GetConnection())
                {
                    //conn.Open();

                    // First, find SalaryID(s) to delete details
                    string selectSql = @"
                        SELECT s.SalaryID
                        FROM Salary s
                        INNER JOIN ClassTeacher ct ON s.ClassTeacherID = ct.ClassTeacherID
                        WHERE ct.TeacherID = @TeacherID
                          AND s.[Month] = @Month
                          AND s.[Year] = @Year;";

                    List<int> salaryIds = new List<int>();
                    using (SqlCommand cmd = new SqlCommand(selectSql, conn.GetConnection()))
                    {
                        cmd.Parameters.AddWithValue("@TeacherID", t.TeacherID);
                        cmd.Parameters.AddWithValue("@Month", t.Month);
                        cmd.Parameters.AddWithValue("@Year", t.Year);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                salaryIds.Add(reader.GetInt32(0));
                            }
                        }
                    }

                    // Delete SalaryDetail first
                    foreach (int sid in salaryIds)
                    {
                        string deleteDetailSql = "DELETE FROM SalaryDetail WHERE SalaryID = @SalaryID";
                        using (SqlCommand cmd = new SqlCommand(deleteDetailSql, conn.GetConnection()))
                        {
                            cmd.Parameters.AddWithValue("@SalaryID", sid);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // Delete Salary
                    string deleteSalarySql = @"
                        DELETE s
                        FROM Salary s
                        INNER JOIN ClassTeacher ct ON s.ClassTeacherID = ct.ClassTeacherID
                        WHERE ct.TeacherID = @TeacherID
                            AND s.[Month] = @Month
                            AND s.[Year] = @Year;";

                    using (SqlCommand cmd = new SqlCommand(deleteSalarySql, conn.GetConnection()))
                    {
                        cmd.Parameters.AddWithValue("@TeacherID", t.TeacherID);
                        cmd.Parameters.AddWithValue("@Month", t.Month);
                        cmd.Parameters.AddWithValue("@Year", t.Year);

                        int rows = cmd.ExecuteNonQuery();

                        if (rows > 0)
                        {

                            MessageBox.Show("Salary record(s) deleted successfully.", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            list.RemoveAll(e => e.TeacherID == t.TeacherID && e.Month == t.Month && e.Year == t.Year);
                            paginator.Refresh();
                        }
                        else
                        {
                            MessageBox.Show("No salary records found for this teacher.", "Not Found",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }

                    }
                }
            }
        }

        //public void delete()
        //{
        //    TeacherSalarySummary t = (dgv.CurrentRow.Tag as TeacherSalarySummary);
        //    DialogResult result = MessageBox.Show(
        //        "Are you sure you want to delete this teacher's salary record(s)?",
        //        "Confirm Delete",
        //        MessageBoxButtons.YesNo,
        //        MessageBoxIcon.Warning
        //    );

        //    if (result == DialogResult.Yes)
        //    {
        //        using (conn.GetConnection())
        //        {
        //            //conn.Open();

        //            string sql = @"
        //        DELETE s
        //        FROM Salary s
        //        INNER JOIN ClassTeacher ct ON s.ClassTeacherID = ct.ClassTeacherID
        //        WHERE ct.TeacherID = @TeacherID
        //          AND s.[Month] = @Month
        //          AND s.[Year] = @Year";

        //            using (SqlCommand cmd = new SqlCommand(sql, conn.GetConnection()))
        //            {
        //                cmd.Parameters.AddWithValue("@TeacherID", t.TeacherID);
        //                cmd.Parameters.AddWithValue("@Month", t.Month);
        //                cmd.Parameters.AddWithValue("@Year", t.Year);

        //                int rows = cmd.ExecuteNonQuery();

        //                if (rows > 0)
        //                {

        //                    MessageBox.Show("Salary record(s) deleted successfully.", "Success",
        //                        MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                    list.RemoveAll(e => e.TeacherID == t.TeacherID);
        //                    paginator.Refresh();
        //                }
        //                else
        //                {
        //                    MessageBox.Show("No salary records found for this teacher.", "Not Found",
        //                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //                }
        //            }
        //        }
        //    }
        //}

        public void search(string query)
        {

        }
        //public class TeacherSalarySummary
        //{
        //    public int TeacherID { get; set; }
        //    public string TeacherNameEng { get; set; }
        //    public decimal TotalBaseSalary { get; set; }
        //    public decimal TotalGift { get; set; }
        //    public decimal TotalOther { get; set; }
        //    public decimal TotalSalary { get; set; }
        //    public object Tag { get; set; } = null;  // can default to null
        //}

    }
}
