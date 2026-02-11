using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Runtime.InteropServices;


namespace SchoolManagementSystem
{
    public partial class FormSalaryConfirm : Form
    {
        Payroll Payroll { get; set; }
        mgmClassTeacher mgmClassTeacher = new mgmClassTeacher();
        DatabaseConnection conn = DatabaseConnection.Instance;
        ClassTeacher ct = new ClassTeacher();

        List<Salary> salaries = new List<Salary>();
        decimal Rate { get; set; }

        int totalStudent = 0;
        int totalSctsPaid = 0;
        decimal totalBaseSalary = 0;

        decimal totalSalary = 0;
        int month = DateTime.Now.Day < 10 ? DateTime.Now.Month - 1 : DateTime.Now.Month;
        int year = DateTime.Now.Year;
        public FormSalaryConfirm(int TeacherID)
        {

            InitializeComponent();
            lbMonth.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@TeacherID", TeacherID);

            string query = $@"
                SELECT 
                    ct.ClassTeacherID,
                    ct.ClassID,
                    ct.TeacherID,

                    c.Name AS ClassName,
                    c.Price,
                    c.Other,

                    l.LanguageID,
                    l.Title,

                    t.NameEng AS TeacherNameEng,
                    t.NameKh AS TeacherNameKh,
                    t.Sex,

                    p.BaseSalary,
                    p.Rate  -- Add payroll info from Payroll table

                FROM {Constants.tbClassTeacher} ct
                INNER JOIN {Constants.tbClass} c 
                    ON ct.ClassID = c.ClassID
                INNER JOIN {Constants.tbTeacher} t 
                    ON ct.TeacherID = t.TeacherID
                INNER JOIN {Constants.tbLanguage} l
                    ON c.LanguageID = l.LanguageID
                LEFT JOIN Payroll p  -- Join payroll (optional if not every teacher has payroll)
                    ON t.TeacherID = p.TeacherID

                WHERE ct.TeacherID = @TeacherID
                ORDER BY l.Title DESC, c.Price;
            ";
            mgmClassTeacher.loadDataFromDB(conn.GetConnection(), query, parameters);

            this.Text = mgmClassTeacher.classTeachers.First().Teacher.NameEng;
            lbPayrollBase.Text = mgmClassTeacher.classTeachers.First().Teacher.Payroll.BaseSalary.ToString() + " $";
            lbPayrollRate.Text = mgmClassTeacher.classTeachers.First().Teacher.Payroll.Rate.ToString() + " %";

            Rate = mgmClassTeacher.classTeachers.First().Teacher.Payroll.Rate;
            viewDgv(mgmClassTeacher.classTeachers);



            txtBonus.TextChanged += (s, e) => doCalculateTotalSalary();
            txtOther.TextChanged += (s, e) => doCalculateTotalSalary();

            btnDetail.Click += (s, e) => { if (dgv.CurrentRow.Tag is ClassTeacher) doShowDetail(dgv.CurrentRow.Tag as ClassTeacher); };
            btnConfirm.Click += (s, e) => SaveSalary(conn.GetConnection(),salaries);
            btnCancel.Click +=(s,e) =>  this.Close();


        }
        public void SaveSalary(SqlConnection conn, List<Salary> salaries)
        {
            DialogResult result = MessageBox.Show(
                "Do you want to confirm?",
                "Confirm",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result != DialogResult.Yes)
                return;

            foreach (Salary salary in salaries)
            {
                // 1️⃣ Insert Salary
                string querySalary = @"
                    INSERT INTO Salary (ClassTeacherID, Year, Month, BaseSalary, Gift, Other, Total)
                    VALUES (@ClassTeacherID, @Year, @Month, @BaseSalary, @Gift, @Other, @Total);
                    SELECT CAST(SCOPE_IDENTITY() AS INT);";

                int newSalaryID;
                using (var cmd = new SqlCommand(querySalary, conn))
                {
                    cmd.Parameters.AddWithValue("@ClassTeacherID", salary.ClassTeacherID);
                    cmd.Parameters.AddWithValue("@Year", salary.Year);
                    cmd.Parameters.AddWithValue("@Month", salary.Month);
                    cmd.Parameters.AddWithValue("@BaseSalary", salary.BaseSalary);
                    cmd.Parameters.AddWithValue("@Gift", salary.Gift);
                    cmd.Parameters.AddWithValue("@Other", salary.Other);
                    cmd.Parameters.AddWithValue("@Total", salary.Total);

                    newSalaryID = (int)cmd.ExecuteScalar(); // get new SalaryID
                    salary.SalaryID = newSalaryID;
                }



                if (salary.SalaryDetails.Any())
                {
                    string queryDetail = @"
                                INSERT INTO SalaryDetail (SalaryID, StudentID, IsPaid, Amount)
                                VALUES (@SalaryID, @StudentID, @IsPaid, 0);";

                    foreach (var salaryDetail in salary.SalaryDetails)
                    {

                        using (var cmd = new SqlCommand(queryDetail, conn))
                        {
                            cmd.Parameters.AddWithValue("@SalaryID", newSalaryID);
                            cmd.Parameters.AddWithValue("@StudentID", salaryDetail.StudentID);
                            cmd.Parameters.AddWithValue("@IsPaid", salaryDetail.IsPaid);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }

            MessageBox.Show("Salary and details saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }



        private void doShowDetail(ClassTeacher ct)
        {
            using(FormDetailPayroll form = new FormDetailPayroll(ct,month,year))
            {
                if(form.ShowDialog() == DialogResult.OK)
                {
                    viewDgv(mgmClassTeacher.classTeachers);
                    //doCalculateTotalSalary();
                }
            }
        }

        private void doCalculateTotalSalary()
        {
            lbBaseSalary.Text = totalBaseSalary.ToString("F2");
            decimal bonus = 0, other = 0, totalOther = 0;

            // TryParse for each input
            decimal.TryParse(txtBonus.Text, out bonus);
            //decimal.TryParse(txtGift.Text, out gift);
            decimal.TryParse(txtOther.Text, out other);

            // Final calculation
            totalOther = bonus + other;
            lbTotalOther.Text = totalOther.ToString("F2");

            totalSalary = totalBaseSalary + totalOther;
            lbTotalSalary.Text = totalSalary.ToString("F2");

        }

        public void viewDgv(List<ClassTeacher> classTeachers)
        {
            salaries.Clear();
            dgv.Rows.Clear();
            totalBaseSalary = 0;

            foreach (ClassTeacher classTeacher in classTeachers)
            {
                FormDetailPayroll form = new FormDetailPayroll(classTeacher,month,year);
                form.doSave();

                int index = dgv.Rows.Add(classTeacher.Class.Name, classTeacher.Class.Price, form.stuPaid, form.stuUnPaid, form.Salary.BaseSalary.ToString("F2"), classTeacher.Gift, classTeacher.Other,form.Salary.Total.ToString("F2"));
                dgv.Rows[index].Tag = classTeacher; //ស្គាល់Row គេដឹងយកGradeមួយណាមកបង្ហាញ
                classTeacher.Tag = dgv.Rows[index]; //ស្គាល់Student គេដឹងយកRowមួយណា

                totalSctsPaid += form.stuPaid;
                totalBaseSalary += form.Salary.Total;
                Salary salary = new Salary
                {
                    ClassTeacherID = classTeacher.ClassTeacherID,
                    ClassTeacher = classTeacher,
                    Month = month,
                    Year = year,
                    BaseSalary = form.Salary.BaseSalary,
                    Gift = form.ClassTeacher.Gift,
                    Other = form.ClassTeacher.Other,
                    Total = form.Salary.Total,
                    SalaryDetails = form.Salary.SalaryDetails
                };

                salaries.Add(salary);
            }
            doCalculateTotalSalary();
        }
    }
}
