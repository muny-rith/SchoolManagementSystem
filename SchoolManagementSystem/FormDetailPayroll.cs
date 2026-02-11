using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using Microsoft.Identity.Client.Extensibility;




namespace SchoolManagementSystem
{
    public partial class FormDetailPayroll : Form
    {
        DatabaseConnection conn = DatabaseConnection.Instance;
        public mgmStudentClassTeacher mgmSCT { get; } = new mgmStudentClassTeacher();
        //StudentClassTeacher sct = new StudentClassTeacher();

        public ClassTeacher ClassTeacher;
        public Salary Salary { get; } = new Salary();
        public int stuPaid { get; set; }
        public int stuUnPaid { get; set; }

        public FormDetailPayroll(ClassTeacher classTeacher,int month,int year)
        {
            InitializeComponent();

            doCustom();
            ClassTeacher = classTeacher;
            this.Text = classTeacher.Teacher.NameEng;
            txtGift.Text = ClassTeacher.Gift.ToString();
            txtOther.Text = classTeacher.Other.ToString();

            Dictionary<string, object?> parameters = new Dictionary<string, object?>();
            mgmSCT.GetStudentsPaidForMonth(new DateTime(year, month, 1), classTeacher.ClassTeacherID);
            //MessageBox.Show(year.ToString() + month.ToString());

            viewDgv(mgmSCT.studentClassTeachers);
            lbClass.Text = classTeacher.Class.Name;
            lbPrice.Text = classTeacher.Class.Price.ToString("F2")+"$";


            btnOk.Click += (s, e) => doSave();
        }

        private void doCustom()
        {
            colNameEng.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colNameEng.Width = 200;
            colNameKh.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colNameKh.Width = 170;
            colSex.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colStatus.Width = 40;
            colStatus.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colDetail.Width = 150;
            colDetail.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colActive.Width = 40;
            colActive.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        }

        public void doSave()
        {
            if (decimal.TryParse(txtGift.Text, out decimal gift) && decimal.TryParse(txtOther.Text, out decimal other)) 
            {
                ClassTeacher.Gift = gift;
                ClassTeacher.Other = other;

                stuPaid = mgmSCT.studentClassTeachers.Where(e => e.IsPaid == true).Count();
                stuUnPaid = mgmSCT.studentClassTeachers.Where(e => e.IsPaid == false).Count();

                Salary.BaseSalary = stuPaid * ClassTeacher.Class.Price *  (ClassTeacher.Teacher.Payroll.Rate/100);
                Salary.ClassTeacherID = ClassTeacher.ClassTeacherID;
                Salary.ClassTeacher = ClassTeacher;
                Salary.Gift = gift;
                Salary.Other = other;
                Salary.Total = Salary.BaseSalary - Salary.Gift + Salary.Other;
                Salary.SalaryDetails = new List<SalaryDetail>();
                foreach(StudentClassTeacher sct in mgmSCT.studentClassTeachers)
                {
                    Salary.SalaryDetails.Add(
                    new SalaryDetail
                    {
                        Student = sct.Student,
                        StudentID = sct.StudentID,
                        IsPaid = sct.IsPaid,
                    });
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                txtGift.Text = "00";
                txtOther.Text = "00";
                txtGift.Focus();
                MessageBox.Show("Please fill coreect format!!");
            }

        }



        public void viewDgv(List<StudentClassTeacher> SCTs)
        {
            dgv.Rows.Clear();

            foreach (StudentClassTeacher sct in SCTs)
            {
                System.Drawing.Image imagePaid = Properties.Resources.Unpaid;
                //if (sct.IsActive == true && sct.IsPaid == false) imagePaid = Properties.Resources.Paid;
                if (sct.IsPaid) imagePaid = Properties.Resources.PaidNewMonth;

                //if (s.EndDate == DateTime.Now.Date) image = Properties.Resources.PaidToday;
                else if (sct.EndDate > DateTime.Now.Date && sct.EndDate < new DateTime(DateTime.Now.Year,DateTime.Now.Month,1).AddMonths(1))
                {
                    imagePaid = Properties.Resources.Paid;
                }

                System.Drawing.Image imageActive = Properties.Resources.Stopped;
                if (sct.IsActive) imageActive = Properties.Resources.Active;
                // 3️⃣ Add row
                int index = dgv.Rows.Add(
                    sct.Student.NameEng,
                    sct.Student.NameKh,
                    sct.Student.Sex,
                    imagePaid,
                    GetPaymentStatus((DateTime?)sct.Tag),
                    imageActive
                );

                dgv.Rows[index].Tag = sct;
                sct.Tag = dgv.Rows[index];
            }
        }

        public string GetPaymentStatus(DateTime? dateDiff)
        {
            if (!dateDiff.HasValue)
                return "    Not Paid";

            DateTime date = dateDiff.Value;
            string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(date.Month);
            string shortMonth = monthName.Substring(0, 3);
            return $"    {date.Day:D2} - {shortMonth} - {date.Year}";
        }
    }
}
