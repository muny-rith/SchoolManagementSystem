
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Linq.Expressions;
using System.Net.Security;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using Krypton.Toolkit;
using System.Security.Policy;
using System.Windows.Forms;
using System.Threading;
using Google.Protobuf.Reflection;
using System.ComponentModel;
using System.Reflection;
using Mysqlx.Crud;


//using ComponentFactory.Krypton.Toolkit;

namespace SchoolManagementSystem
{
    public partial class mainForm : Form
    {
        public event EventHandler PageChanged;
        List<object> allData;
        Color firstColor = Color.FromArgb(223, 241, 250);
        Color secondColor = ColorTranslator.FromHtml("#C4EBF7");
        Color thirdColor = ColorTranslator.FromHtml("#E5A865");
        Color color4 = Color.FromArgb(252, 232, 141);
        Color color5 = ColorTranslator.FromHtml("#F9F9FB");


        private bool mouseDown;
        private Point lastLocation;

        private int currentPage = 1;

        public int CurrentPage
        {
            get => currentPage;
            set
            {
                if (currentPage != value)
                {
                    currentPage = value;
                    PageChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private int pageSize = 15;
        private int totalRecords = 0;
        private int totalPages = 0;

        //string fileName = "data.txt";
        //string fileGradeName = "dataGrade.txt";
        //string filePayment = "dataPayment.txt";
        string fileResultMonthly = "dataResultMonthly.txt";
        //List<Student> listStudents = new List<Student>();

        mgmStudent students = new mgmStudent();
        mgmClass grades = new mgmClass();
        mgmPayment payments = new mgmPayment();

        mgmStudent mgmStudentNotPay = new mgmStudent();

        //region form
        public formSelectSCT formPay;
        public FormUpdatePayment formUpdatePayment;


        DataGridView dgvPayment = TemplateUI.CreateStyledGrid();
        DataGridView dgvGrade = TemplateUI.CreateStyledGrid();

        public DatabaseConnection con = DatabaseConnection.Instance;
        //UserControl userControl = new dashboardControl();

        ~mainForm() { }
        public mainForm()
        {
            doLoadData();
            InitializeComponent();

            //userControl.Location = new Point((this.Size.Width*3)/10,0);
            //this.Controls.Add(userControl);
            loadControl(new dashboardControl());

            doCustom();

            btnDashboard.Click += (s, e) => loadControl(new dashboardControl());
            btnStudent.Click += (s, e) => loadControl(new studentControl());
            btnTeacher.Click += (s, e) => loadControl(new teacherControl());
            btnGrade.Click += (s, e) => loadControl(new classControl());
            btnTeacherClass.Click += (s, e) => loadControl(new classTeacherControl());
            btnGradeStudents.Click += (s, e) => loadControl(new classTeacherStudentsControls());
            btnInvoiceStudent.Click += (s, e) => loadControl(new paymentControl());
            btnPaymentStudents.Click += (s, e) => loadControl(new paymentStudentsControl());
            btnPayroll.Click += (s, e) => loadControl(new payrollTeacherControl());
            btnSalary.Click += (s, e) => loadControl(new teacherSalarySummaryControl());

            this.MouseDown += new MouseEventHandler(Form1_MouseDown);
            this.MouseMove += new MouseEventHandler(Form1_MouseMove);
            this.MouseUp += new MouseEventHandler(Form1_MouseUp);


            btnLogout.Click += doLogOut;
            btnBackup.Click += (s, e) => doBackUp();
            //btnLogout.Image = doResizeImage(Properties.Resources.logout,32,32);
            btnLogout.ImageAlign = ContentAlignment.MiddleRight;
            btnLogout.FlatAppearance.MouseOverBackColor = firstColor;
            btnLogout.FlatAppearance.MouseDownBackColor = secondColor;

            doSaveRecord();


        }


        private void loadControl(UserControl control)
        {
            panelMain.Controls.Clear();
            panelMain.Controls.Add(control);
            control.Dock = DockStyle.Fill;
            control.Margin = new Padding(0);

            panelMain.Padding = new Padding(0);
            panelMain.Controls.Add(control);
        }
        #region 1
        private void doCustom()
        {
            //dgv.ColumnHeadersDefaultCellStyle.BackColor = color5;
            //dgv.RowTemplate.Height = 30;
            ////panelContent.Paint += panel_Paint;
            ////dgvPayment.RowPrePaint += dataGridView_RowPrePaint;
            //dgv.GridColor = Color.LightGray;
        }
        private void panel_Paint(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            Color borderColor = color5;
            int borderWidth = 1;

            ControlPaint.DrawBorder(e.Graphics, p.ClientRectangle,
                borderColor, borderWidth, ButtonBorderStyle.Solid,   // Top
                borderColor, borderWidth, ButtonBorderStyle.Solid,   // Left
                borderColor, borderWidth, ButtonBorderStyle.Solid,   // Bottom
                borderColor, borderWidth, ButtonBorderStyle.Solid
                );  // Right
        }
        private void doLoadData()
        {
            //students.loadDataFromFile(Constants.StudentFile);
            students.loadDataFromDB(con.GetConnection(), null);
            grades.loadDataFromDB(con.GetConnection());
            payments.loadDataFromDB(con.GetConnection());
        }

        public static string SanitizeFileName(string filename)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                filename = filename.Replace(c, '_');
            }
            return filename;
        }
        private void doBackUp()
        {
            //students.updateFile()
            //string sourcePath = Path.Combine(Application.StartupPath, "Picture\\System\\unknow.jpeg");
            //string targetFolder = Path.Combine(Application.StartupPath, "Picture\\Student");

            //string sourcePath = Path.Combine(Application.StartupPath, "Resource\\System\\unknow.jpeg");
            //string targetFolder = Path.Combine(Application.StartupPath, "Data\\Student");

            //////st
            //////mgmStudent 
            //int counter = 1;

            //foreach (var student in students.Students)
            //{
            //    string safeName = student.NameEng.Replace(" ", "_");
            //    string filename = $"{student.StudentID}_{safeName}.jpg";
            //    //string targetPath = Path.Combine(targetFolder, filename);

            //    //// Copy the file and overwrite if needed
            //    //File.Copy(sourcePath, targetPath, overwrite: true);

            //    //// Optionally update the student's Photo path
            //    //student.Photo = $"{filename}";
            //    //string q = $"UPDATE Student SET Photo = '{student.Photo}' WHERE StudentID = {student.StudentID};";
            //    using (var cmd = new SqlCommand("UPDATE tbStudent SET Photo = @Photo WHERE StudentID = @ID", con.GetConnection()))
            //    {
            //        cmd.Parameters.AddWithValue("@Photo", filename);
            //        cmd.Parameters.AddWithValue("@ID", student.StudentID);
            //        cmd.ExecuteNonQuery();
            //    }


            //    //counter++;
            //}

            //mgmGrade mgmGrade = new mgmGrade();
            //mgmStudent mgmStudent = new mgmStudent();
            //mgmPayment mgmPayment = new mgmPayment();

            //mgmGrade.loadDataFromFile(Constants.GradeFile);
            //mgmStudent.loadDataFromFile(Constants.StudentFile);
            //mgmPayment.loadDataFromFile(Constants.PaymentFile);



            //foreach (Grade newGrade in mgmGrade.Grades)
            //{
            //    string query = $"Insert into tbGrade (GradeID,Name,Type,Price) values ('{newGrade.GradeID}','{newGrade.Name}','{newGrade.Type}',{newGrade.Price})";
            //    SqlCommand cmd = new SqlCommand(query, con.GetConnection());
            //    cmd.ExecuteNonQuery();
            //}
            //foreach (Student student in mgmStudent.Students)
            //{

            //    //Insert into tbStudent values(0001,'Sok Vanten','សុខ វណ្ណថេន','male','E-P5','3/18/2025','P.P','P.P','Muny','Chenda',2,'3/6/2025','070788913','Unknow')
            //    string query = $"Insert into tbStudent values ('{student.StudentID}','{student.NameEng}', N'{student.NameKh}' ,'{student.Sex}','{mgmGrade.Grades.Where(e => e.Name == student.GradeName).First().GradeID}','{student.DOB.ToShortDateString().Replace('/', '-')}','{student.POB}','{student.CurrentPlace}','{student.Parrent[0]}','{student.Parrent[1]}',{student.Member},'{student.StartDate.ToShortDateString().Replace('/', '-')}','{student.Phone}','{student.Photo}')";
            //    SqlCommand cmd = new SqlCommand(query, con.GetConnection());
            //    cmd.ExecuteNonQuery();
            //}

            //fix file photo on db

            //foreach (Student student in students.Students)
            //{
            //    string gradeId = mgmGrade.Grades.FirstOrDefault(e => e.Name == student.GradeName)?.GradeID ?? "Unknown";

            //    // Sanitize photo name if needed
            //    //string safePhoto = SanitizeFileName(student.Photo);

            //    string query = $@"
            //        UPDATE tbStudent SET 
            //            NameEng = '{student.NameEng}',
            //            NameKh = N'{student.NameKh}',
            //            Sex = '{student.Sex}',
            //            GradeID = '{gradeId}',
            //            DOB = '{student.DOB:yyyy-MM-dd}',
            //            POB = '{student.POB}',
            //            CurrentPlace = '{student.CurrentPlace}',
            //            Father = '{student.Parrent[0]}',
            //            Mother = '{student.Parrent[1]}',
            //            Member = {student.Member},
            //            StartDate = '{student.StartDate:yyyy-MM-dd}',
            //            Phone = '{student.Phone}',
            //            Photo = '{student.Photo}'
            //        WHERE StudentID = '{student.StudentID}'
            //    ";

            //    SqlCommand cmd = new SqlCommand(query, con.GetConnection());
            //    cmd.ExecuteNonQuery();
            //}


            //foreach (Payment payment in mgmPayment.Payments)
            //{
            //    //con.GetConnection();
            //    string query = $"Insert into tbPayment values ('{payment.PaymentID}','{payment.StudentID}','{payment.GradeID}','{payment.PayDate.ToShortDateString().Replace('/', '-')}',{payment.Duration},'{payment.StartDate.ToShortDateString().Replace('/', '-')}','{payment.EndDate.ToShortDateString().Replace('/', '-')}',{payment.Bill},{payment.Discount},{payment.Amount})";
            //    SqlCommand cmd = new SqlCommand(query, con.GetConnection());
            //    cmd.ExecuteNonQuery();
            //}

            //con.CloseConnection();
            //0196___Phanna Phary______female___0013___3 / 20 / 2025 12:00:00 AM___P.P___P.P___unkonw___ - ___0___3 / 18 / 2025 12:00:00 AM___6 / 21 / 2025 12:00:00 AM______Picture / Student / Phanna_Phary_0196.jpg
            //    string query = $"Insert into tbStudent values ('0196','Phanna Phary', N'ផាន់ណា ផារី' ,'female','0013','03-20-2025','P.P','P.P','','',0,'03-18-2025','','Picture/Student/Phanna_Phary_0196.jpg')";
            //SqlCommand cmd = new SqlCommand(query, con.GetConnection());
            //cmd.ExecuteNonQuery();
            MessageBox.Show("Completed!");
        }

        private void doLogOut(object? sender, EventArgs e)
        {
            //this.FormBorderStyle = FormBorderStyle.None;
            this.Hide();
            loginForm loginForm = new loginForm();
            loginForm.Show();

        }

        #endregion

        #region About Form
        private const int WM_SYSCOMMAND = 0x112;
        private const int SC_MINIMIZE = 0xF020;
        private const int SC_RESTORE = 0xF120;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_SYSCOMMAND)
            {
                int command = m.WParam.ToInt32();

                // If the taskbar icon is clicked (Minimize)
                if (command == SC_MINIMIZE)
                {
                    this.WindowState = FormWindowState.Minimized;
                    return;
                }
                // If the taskbar icon is clicked again (Restore)
                else if (command == SC_RESTORE)
                {
                    this.WindowState = FormWindowState.Normal;
                    return;
                }
            }

            base.WndProc(ref m);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X,
                    (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }


        private void doSaveRecord()
        {
            string lastline = "";
            DateTime dateLastRecord = new DateTime();
            if (File.ReadAllLines(Constants.ResultMonthlyFile).ToList().Count() != 0)
            {
                lastline = (File.ReadAllLines(Constants.ResultMonthlyFile).ToList()).Last();
                string[] arr = lastline.Split("___");
                dateLastRecord = DateTime.Parse(arr[0]);
            }
            //MessageBox.Show(lastline);


            //DateTime firstDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            //if (DateTime.Now.ToShortDateString() == firstDay.ToShortDateString())
            //{
            //    string Info = null;
            //    Info = DateTime.Now.ToShortDateString() + "___" + students.Students.Count(e => e.Sex == "female") + "___" + students.Students.Count(e => e.Sex == "male") + "___" + students.Students.Count();
            //    MessageBox.Show(Info);
            //    File.AppendAllText(fileResultMonthly, Info + "\n");
            //}
            //MessageBox.Show(dateLastRecord.ToShortDateString());
            if (dateLastRecord.Month < DateTime.Now.Month || dateLastRecord.Year < DateTime.Now.Year)
            {
                string Info = null;
                Info = DateTime.Now.ToShortDateString() + "___" + students.Students.Count(e => e.Sex == "female") + "___" + students.Students.Count(e => e.Sex == "male") + "___" + students.Students.Count();
                File.AppendAllText(Constants.ResultMonthlyFile, Info + "\n");
            }
        }

        private Image doResizeImage(Image original, int width, int height)
        {
            Image resized = new Bitmap(original, new Size(width, height));
            return resized;
        }


        #endregion


        private void mainForm_Load(object sender, EventArgs e)
        {

        }

        private void panelSidbar_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
