using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;
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
using System.Windows.Forms.DataVisualization.Charting;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SchoolManagementSystem
{
    public partial class dashboardControl : UserControl
    {
        Color colorBlue = Color.FromArgb(223, 241, 250); // blue
        Color secondColor = ColorTranslator.FromHtml("#C4EBF7"); // blue
        Color thirdColor = ColorTranslator.FromHtml("#E5A865"); // brown
        Color colorYellow = Color.FromArgb(252, 232, 141); // yellow
        Color color5 = ColorTranslator.FromHtml("#F9F9FB"); // gray sral
        Color colorPuple = Color.FromArgb(199, 179, 245); // blue


        mgmPayment payments = new mgmPayment();
        mgmClass mgmC = new mgmClass();
        mgmTeacher mgmTeacher = new mgmTeacher();
        mgmStudentClassTeacher mgmSCT = new mgmStudentClassTeacher();

        DatabaseConnection con = DatabaseConnection.Instance;

        string queryLoadData = $@"
                    SELECT 
                        s.StudentID,
                        s.NameEng,
                        s.NameKh,
                        s.Sex,
                        ct.ClassID,
                        c.Name AS ClassName,
                        s.Phone,
                        p.EndDate,
                        s.Photo,

                        -- Difference in days
                        DATEDIFF(DAY, GETDATE(), p.EndDate) AS DateDiff,

                        -- DaysRemaining: only if payment still valid
                        CASE 
                            WHEN DATEDIFF(DAY, GETDATE(), p.EndDate) >= 0 THEN DATEDIFF(DAY, GETDATE(), p.EndDate)
                            ELSE NULL
                        END AS DaysRemaining,

                        -- DaysOverdue: only if payment expired
                        CASE 
                            WHEN DATEDIFF(DAY, GETDATE(), p.EndDate) < 0 THEN ABS(DATEDIFF(DAY, GETDATE(), p.EndDate))
                            ELSE NULL
                        END AS DaysOverdue

                    FROM {Constants.tbStudent} s
                    JOIN StudentClassTeacher sct ON s.StudentID = sct.StudentID
                    JOIN ClassTeacher ct ON sct.ClassTeacherID = ct.ClassTeacherID
                    JOIN {Constants.tbClass} c ON ct.ClassID = c.ClassID
                    LEFT JOIN (
                        SELECT StudentClassTeacherID, MAX(EndDate) AS EndDate
                        FROM {Constants.tbPayment}
                        GROUP BY StudentClassTeacherID
                    ) p ON sct.StudentClassTeacherID = p.StudentClassTeacherID
                ";



        public dashboardControl()
        {
            mgmSCT.loadDataFromDB(con.GetConnection());
            mgmC.loadDataFromDB(con.GetConnection());
            mgmTeacher.loadDataFromDB(con.GetConnection());

            InitializeComponent();
            this.Dock = DockStyle.Fill;
            //this.Size = new Size(930, 700);
            doOnDashboard();

        }


        private void doOnDashboard()
        {
            DateTime now = DateTime.Now;

            #region part Top

            lbNumberPay.Text = mgmSCT.studentClassTeachers.Where(e => e.DateDiff >= 0).ToList().Count().ToString();
            lbNumberStudents.Text = mgmSCT.studentClassTeachers.Count().ToString();
            lbNumberGrades.Text = mgmC.Classes.Count().ToString();
            lbNumberTeachers.Text = mgmTeacher.teachers.Count().ToString();

            #endregion

            #region part1 about student

            int female = mgmSCT.studentClassTeachers.Where(e => e.Student.Sex.ToLower() == "female").Count();
            int male = mgmSCT.studentClassTeachers.Where(e => e.Student.Sex.ToLower() == "male").Count();

            chartFemaleMale.Series.Clear();
            chartFemaleMale.ChartAreas.Clear();
            chartFemaleMale.ChartAreas.Add("Default");

            var s = new Series("Series1");
            s.ChartType = SeriesChartType.Column;
            s.IsValueShownAsLabel = true;
            s.XValueType = ChartValueType.String;
            s.IsXValueIndexed = true;
            s["PointWidth"] = "0.70";
            //s.

            chartFemaleMale.Series.Add(s);

            // add points and set color per point
            int idx = 0;
            idx = s.Points.AddXY("Female", female);
            s.Points[idx].Color = colorYellow;

            idx = s.Points.AddXY("Male", male);
            s.Points[idx].Color = colorBlue;

            // 👇 Optional better visuals
            chartFemaleMale.ChartAreas[0].AxisX.Interval = 1;
            chartFemaleMale.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chartFemaleMale.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.White;
            chartFemaleMale.ChartAreas[0].AxisX.LabelStyle.Angle = 0;
            chartFemaleMale.ChartAreas[0].AxisX.IsMarginVisible = true;
            mgmStudentClassTeacher studentStopped = new mgmStudentClassTeacher();
            string query = $@"
                SELECT 
                    sct.StudentClassTeacherID,
                    sct.StudentID,
                    sct.ClassTeacherID,
                    sct.StartDate,
                    sct.EndDate,

                    s.NameEng,
                    s.NameKh,
                    s.Sex,
                    s.Phone,
                    s.Photo AS StudentPhoto,

                    ct.ClassID,
                    ct.TeacherID,

                    c.Name AS ClassName,
                    c.LanguageID,
                    c.Price,

                    t.NameEng AS TeacherNameEng,
                    t.NameKh AS TeacherNameKh,
                    t.Sex AS TeacherSex,
                    t.Phone AS TeacherPhone

                FROM {Constants.tbStudentClassTeacher} sct
                INNER JOIN {Constants.tbStudent} s ON s.StudentID = sct.StudentID
                INNER JOIN {Constants.tbClassTeacher} ct ON ct.ClassTeacherID = sct.ClassTeacherID
                INNER JOIN {Constants.tbClass} c ON c.ClassID = ct.ClassID
                INNER JOIN {Constants.tbTeacher} t ON t.TeacherID = ct.TeacherID
                WHERE sct.IsActive = 0";
            studentStopped.loadDataFromDB(con.GetConnection(), query, null);

            lbNewStudent.Text = mgmSCT.studentClassTeachers.Where(e => e.StartDate?.Month == now.Date.Month && e.StartDate?.Year == now.Year).ToList().Count().ToString("D3");
            lbStopStudent.Text = studentStopped.studentClassTeachers.Count.ToString("D3");

            #endregion

            #region Payment


            //int amountStudents = students.Students.Count(); // fixed typo: 'amoung' ➜ 'amount'
            DateTime newMonth = new DateTime(now.Year, now.Month, 1).AddMonths(1);


            int countStudents = mgmSCT.studentClassTeachers.Count();
            int countActive = mgmSCT?.studentClassTeachers.Where(e => e.DateDiff>=0).Count() ?? 0;

            int countPaid = mgmSCT.studentClassTeachers
                .Where(e => e.EndDate >= newMonth)
                .Count();
            int countNotPayed = mgmSCT.studentClassTeachers.Where(e => e.DateDiff < 0).ToList().Count(); ;


            chartPaidIsActive.Series.Clear();
            chartPaidIsActive.ChartAreas.Clear();
            chartPaidIsActive.ChartAreas.Add("Default");

            var seriesPaid = new Series("Series2");
            seriesPaid.ChartType = SeriesChartType.Column;
            seriesPaid.IsValueShownAsLabel = true;
            seriesPaid.XValueType = ChartValueType.String;
            seriesPaid.IsXValueIndexed = true;
            seriesPaid["PointWidth"] = "0.70";
            seriesPaid.Color = colorYellow;


            chartPaidIsActive.Series.Add(seriesPaid);
            
            // add points and set color per point
            idx = 0;
            idx = seriesPaid.Points.AddXY("Paid", countPaid);
            seriesPaid.Points[idx].Color = Color.LimeGreen;

            idx = seriesPaid.Points.AddXY("Active", countActive);
            seriesPaid.Points[idx].Color = Color.DodgerBlue;

            chartPaidIsActive.ChartAreas[0].AxisX.Interval = 1;
            chartPaidIsActive.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chartPaidIsActive.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            chartPaidIsActive.ChartAreas[0].AxisY.Maximum = countStudents;


            //// Update other labels regardless
            lbDPaid2.Text = countPaid.ToString();
            lbActive.Text = countActive.ToString();
            lbDTotalPaid.Text = countStudents.ToString();

            #endregion




            #region part2 Buttom
            //DateTime now = DateTime.Now;
            List<StudentClassTeacher> studentList = mgmSCT.studentClassTeachers;
            var studentsLast3 = studentList.Where(e => e.StartDate?.Month <= now.AddMonths(-3).Month && e.StartDate?.Year == now.AddMonths(-3).Year);
            var studentsLast2 = studentList.Where(e => e.StartDate?.Month <= now.AddMonths(-2).Month && e.StartDate?.Year == now.AddMonths(-3).Year);
            var studentsLast1 = studentList.Where(e => e.StartDate?.Month <= now.AddMonths(-1).Month && e.StartDate?.Year == now.AddMonths(-3).Year);
            //DateTime now = DateTime.Now;
            //List<StudentClassTeacher> studentList = mgmSCT.studentClassTeachers;

            // define each target month
            DateTime month3 = now.AddMonths(-3);
            DateTime month2 = now.AddMonths(-2);
            DateTime month1 = now.AddMonths(-1);
            DateTime month = now;

            chartResultMonthly.Series.Clear();
            chartResultMonthly.ChartAreas.Clear();
            chartResultMonthly.ChartAreas.Add("Default");

            var s1 = new Series("female");
            var s2 = new Series("male");
            var s3 = new Series("total");
            s1.ChartType = SeriesChartType.Column;
            s1.Color = colorYellow;
            s1.IsValueShownAsLabel = true;
            s1.XValueType = ChartValueType.String; s1.ChartType = SeriesChartType.Column;
            s1["PointWidth"] = "0.7";

            s2.ChartType = SeriesChartType.Column;
            s2.Color = colorBlue;
            s2.IsValueShownAsLabel = true;
            s2.XValueType = ChartValueType.String; s1.ChartType = SeriesChartType.Column;
            s2["PointWidth"] = "0.7";


            s3.ChartType = SeriesChartType.Column;
            s3.Color = colorPuple;
            s3.IsValueShownAsLabel = true;
            s3.XValueType = ChartValueType.String;
            s3["PointWidth"] = "0.7";

            // 👇 The key line that fixes same-location issue
            s1.IsXValueIndexed = true;
            s2.IsXValueIndexed = true;
            s3.IsXValueIndexed = true;

            chartResultMonthly.Series.Add(s1);
            chartResultMonthly.Series.Add(s2);
            chartResultMonthly.Series.Add(s3);

            // add labels for each month
            string[] monthLabels = { month3.ToString("MMM"), month2.ToString("MMM"), month1.ToString("MMM"), month.ToString("MMM") };

            // add last 3 months' data
            chartResultMonthly.Series["female"].Points.AddXY(monthLabels[0], studentsLast3.Count(e => e.Student.Sex?.ToLower() == "female"));
            chartResultMonthly.Series["male"].Points.AddXY(monthLabels[0], studentsLast3.Count(e => e.Student.Sex?.ToLower() == "male"));
            chartResultMonthly.Series["total"].Points.AddXY(monthLabels[0], studentsLast3.Count());

            // last 2 months
            chartResultMonthly.Series["female"].Points.AddXY(monthLabels[1], studentsLast2.Count(e => e.Student.Sex?.ToLower() == "female"));
            chartResultMonthly.Series["male"].Points.AddXY(monthLabels[1], studentsLast2.Count(e => e.Student.Sex?.ToLower() == "male"));
            chartResultMonthly.Series["total"].Points.AddXY(monthLabels[1], studentsLast2.Count());

            // last 1 month
            chartResultMonthly.Series["female"].Points.AddXY(monthLabels[2], studentsLast1.Count(e => e.Student.Sex?.ToLower() == "female"));
            chartResultMonthly.Series["male"].Points.AddXY(monthLabels[2], studentsLast1.Count(e => e.Student.Sex?.ToLower() == "male"));
            chartResultMonthly.Series["total"].Points.AddXY(monthLabels[2], studentsLast1.Count());

            // this month

            chartResultMonthly.Series["female"].Points.AddXY(monthLabels[3], studentList.Count(e => e.Student.Sex?.ToLower() == "female"));
            chartResultMonthly.Series["male"].Points.AddXY(monthLabels[3], studentList.Count(e => e.Student.Sex?.ToLower() == "male"));
            chartResultMonthly.Series["total"].Points.AddXY(monthLabels[3], studentList.Count());


            chartResultMonthly.ChartAreas[0].AxisX.Interval = 1;
            chartResultMonthly.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chartResultMonthly.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
            #endregion

        }
    }
}
