using Krypton.Toolkit;
using Microsoft.Data.SqlClient;
using Mysqlx.Crud;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;


namespace SchoolManagementSystem
{
    public partial class studentControl : baseUserControl,IStrategyUserControl<Student>
    {
        DatabaseConnection conn = DatabaseConnection.Instance;
        WebBrowser webBrowser = new WebBrowser();
        PaginationMgm<Student> paginator;
        mgmStudent MgmStudent = new mgmStudent();
        string receiptText;
        DatabaseConnection con = DatabaseConnection.Instance;

        public studentControl()
        {
            MgmStudent.loadDataFromDB(conn.GetConnection(), null);

            InitializeComponent();
            this.dgv.Columns.AddRange(new DataGridViewColumn[]
            {
            new DataGridViewTextBoxColumn
            {
                Name = "colStuId",
                HeaderText = "ID",
                Width = 50,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                ReadOnly = true
            },
            new DataGridViewTextBoxColumn
            {
                Name = "colStuNameEng",
                HeaderText = "NameEnglish",
                ReadOnly = true,
                Width = 200
            },
            new DataGridViewTextBoxColumn
            {
                Name = "colStuNameKh",
                HeaderText = "NameKhmer",
                ReadOnly = true,
                Width = 200
            },
            new DataGridViewTextBoxColumn
            {
                Name = "colStuSex",
                HeaderText = "Sex",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                Width = 80
            },
            new DataGridViewTextBoxColumn
            {
                Name = "colStuFather",
                HeaderText = "Father",
                Width = 100,
                //MinimumWidth = 200,
                ReadOnly = true
            },
            new DataGridViewTextBoxColumn {
                Name = "colStuMother",
                HeaderText = "Mother",
                Width=100,
                ReadOnly=true
            },
            new DataGridViewTextBoxColumn
            {
                Name = "colStuPhone",
                HeaderText = "Phone",
                ReadOnly = true,
                Width = 200
            },




            //new DataGridViewImageColumn
            //{
            //    Name = "colStuActionEdit",
            //    HeaderText = "",
            //    Image = Properties.Resources.edit,
            //    ImageLayout = DataGridViewImageCellLayout.Zoom, // or Zoom
            //    Width = 48,               // 👈 Make sure this matches the icon size (e.g., 16x16 or 24x24)
            //    MinimumWidth = 48,
            //    AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
            //    ReadOnly = true,
            //},
            //new DataGridViewImageColumn
            //{
            //    Name = "colStuActionDelete",
            //    HeaderText = "",
            //    Image = Properties.Resources.trash,
            //    ImageLayout = DataGridViewImageCellLayout.Zoom, // or Zoom
            //    Width = 48,               // 👈 Make sure this matches the icon size (e.g., 16x16 or 24x24)
            //    MinimumWidth = 48,
            //    AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
            //    ReadOnly = true,
            //}

            });




            ListTilte = "List Students";
            this.Title = "Student";
            this.txtInsert = "Add New";


            LB1 = "Total:";
            LB2 = MgmStudent.Students.Count().ToString("D2");
            LB3 = "Male:";
            LB4 = MgmStudent.Students.Where(e => e.Sex == "male").ToList().Count().ToString("D2");
            LB5 = "Female:";
            LB6 = MgmStudent.Students.Where(e => e.Sex == "female").ToList().Count().ToString("D2");

            // Pagination setup
            paginator = new PaginationMgm<Student>(
                MgmStudent.Students,viewDgv
            );
            paginator.LoadPage();
            btnNext.Click += (s, e) => paginator.NextPage();
            btnPre.Click += (s, e) => paginator.PreviousPage();

            menuItems.Items.AddRange(new KryptonContextMenuItem[]
            {
                new KryptonContextMenuItem("Filter by Date"),
                new KryptonContextMenuItem("Clear Filters")
            });
            //contextMenu.Items.Add(menuItems);
            btnFilter.Click += (s, e) => doShowFilter();
            //btnExport.Click += (s, e) => { if (dgv.CurrentRow.Tag is Student stu) { doExport(stu); } };
            btnExport.Click += (s, e) => {
                if (dgv.CurrentRow?.Tag is Student stu)
                    doExport(stu);
            };

            txtSearch.TextChanged += (s,e) => search(txtSearch.Text);
            btnInsert.Click += (s, e) => insert();
            btnUpdate.Click += (s,e) => update();
            btnDelete.Click += (s, e) => delete();

            btnInformation.Click += (s, e) => information();



            dgv.MouseWheel += (s, e) =>
            {
                if (e.Delta < 0)
                    paginator.NextPage();
                else if (e.Delta > 0)
                    paginator.PreviousPage();

                // Delay to avoid rapid paging (adjust as needed)
                Task.Delay(200);
            };

            #region students stopped

            int rowCount;
            string queryLoadStuStopped = $@"
                SELECT COUNT(*) 
                FROM {Constants.tbStudent} s
                WHERE s.IsActive = 0";

            using (SqlCommand cmd = new SqlCommand(queryLoadStuStopped, conn.GetConnection()))
            {
                rowCount = (int)cmd.ExecuteScalar();
                // now you can use rowCount
            }

            lbStopped.Text = rowCount.ToString();

            Image dot = Properties.Resources.menu_dots_horizontal;
            Image resized = new Bitmap(dot, new Size(24, 24));
            btnStudentStop.Image = resized;
            btnStudentStop.Click += (s, e) => doShowStopped();
            #endregion
        }

        private void doShowStopped()
        {
            Title = "Students Stopped";
            MgmStudent.LoadDataStopFromDB(conn.GetConnection());
            paginator.AllData = MgmStudent.Students;
            paginator.LoadPage();

            LB2 = MgmStudent.Students.Count().ToString("D2");
            LB4 = MgmStudent.Students.Where(e => e.Sex == "male").ToList().Count().ToString("D2");
            LB6 = MgmStudent.Students.Where(e => e.Sex == "female").ToList().Count().ToString("D2");
            lbStopped.Visible = false;
            labelStopped.Visible = false;
            btnStudentStop.Visible = false;
            btnInsert.Visible = false;
            btnRecovery.Visible = true;
            btnRecovery.Location = btnInsert.Location;
            btnRecovery.Click += (s, e) => { if (dgv.CurrentRow.Tag is Student stu) doRecovery(stu); };

        }

        private void doRecovery(Student stu)
        {
            int studentId = stu.StudentID;
            string query = $@"
                UPDATE {Constants.tbStudent} 
                SET IsActive = 1
                WHERE StudentId = @StudentId;
            ";

            using (SqlCommand cmd = new SqlCommand(query, conn.GetConnection()))
            {
                cmd.Parameters.AddWithValue("@StudentId", studentId);

                int rowsAffected = cmd.ExecuteNonQuery();

                MessageBox.Show(rowsAffected > 0
                    ? $"Student {stu.NameEng} recovered successfully."
                    : $"No student found with ID {stu.NameEng}.");
            }
            paginator.AllData.RemoveAll(e => e.StudentID == studentId);
            paginator.Refresh();
        }

        #region export StudentCard
        private string GenerateHtml(Student stu)
        {
            string fallbackPath = Path.Combine(Application.StartupPath,"Data", "StudentCard", "image", "pf.jpg");
            string fallbackUri = new Uri(fallbackPath).AbsoluteUri;
            string studentPath = Path.Combine(Application.StartupPath, "Data", "Student", stu.Photo);

            string photoPath = File.Exists(studentPath) ? new Uri(studentPath).AbsoluteUri : fallbackUri;

            //string fallbackPath = Path.Combine(Application.StartupPath, "StudentCard", "image", "pf.jpg");
            //string photoPath = File.Exists(studentPath) ? studentPath : fallbackPath;
            //pictureBox1.Image = Image.FromFile(photoPath);

            //string photoPath = File.Exists(stu.Photo) ? stu.Photo : "StudentCard/image/pf.jpg";

            string[] khmerMonthNames = {
                "", "មករា", "កុម្ភៈ", "មីនា", "មេសា", "ឧសភា", "មិថុនា",
                "កក្កដា", "សីហា", "កញ្ញា", "តុលា", "វិច្ឆិកា", "ធ្នូ"
            };
            //string logoPath = Path.Combine(System.Windows.Forms.Application.StartupPath, "Picture\\System\\logo.png");
            string htmll = $$""""
                <html lang="en">

                <head>
                  <meta charset="UTF-8">
                  <meta name="viewport" content="width=device-width, initial-scale=1.0">
                  <title>Student card</title>
                  <!-- <link rel="stylesheet" href="card.css"> -->
                  <link rel="preconnect" href="https://fonts.googleapis.com">
                  <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
                  <style>
                    @import url('https://fonts.googleapis.com/css2?family=Figtree:ital,wght@0,300..900;1,300..900&family=Moul&family=Nokora:wght@100;300;400;700;900&family=Rubik:ital,wght@0,300..900;1,300..900&display=swap');

                    * {
                      margin: 0;
                      padding: 0;
                    }
                    body {
                      /* background-color: black; */
                      padding: 0;
                      margin: 0;
                    }
                    .content {
                      width: 100vw;
                      height: 100vh;
                      display: flex;
                    }
                    .card-fornt {
                      margin: 20px;
                      width: 294px;
                      height: 450px;
                      background-color: rgb(255, 255, 255);
                      display: flex;
                      flex-direction: column;
                      align-content: space-between;
                    }
                    .card-fornt .top {
                      padding: 0px 10px;
                      background-image: url(./image/head-shap.png);
                      background-size: contain;
                      background-repeat: no-repeat;
                      width: auto;
                      height: 100px;
                      display: flex;
                      align-items: center;
                      justify-content: space-between;
                      transform: translate(0, -1px);
                    }
                    .top .logo-top {
                      padding-bottom: 10px;
                      width: 22%;
                    }
                    .logo-top img {
                      filter: drop-shadow(-5px 5px 4px rgba(0, 0, 0, 0.5));
                    }
                    .school-name > h2 {
                      font-family: "Moul", serif;
                      /* color: white; */
                      font-size: 14px;
                      color: #1c336c; /* dark blue */
                      text-shadow: -1px -1px 0 #ffffff, 1px -1px 0 #ffffff, -1px 1px 0 #ffffff,
                        1px 1px 0 #ffffff, -3px 3px 3px rgb(255, 255, 255); /* extra shadow for depth */
                    }

                    .school-name > h4 {
                      font-family: "Moul", serif;
                      font-weight: 200;
                      padding-bottom: 10px;
                      font-size: 10px;
                      color: #1c336c; /* dark blue */
                      text-shadow: -1px -1px 0 #ffffff, 1px -1px 0 #ffffff, -1px 1px 0 #ffffff,
                        1px 1px 0 #ffffff, -3px 3px 3px rgb(255, 255, 255); /* extra shadow for depth */
                    }

                    .center {
                      width: 100%;
                      height: 280px;
                      /* background-color: rgb(255, 11, 11); */
                      background-size: contain;
                      text-align: center;
                    }

                    .center .pf {
                      width: 40%;
                      border-radius: 50%;
                      z-index: 1;
                      position: relative;
                      outline: 2.5px solid #1c336c;
                      height: 40%;   /* make height same as width */
                      object-fit: cover; /* crop image if needed */
                    }
                    .blue-box {
                      /* background-color: aqua; */
                      width: 100%;
                      height: 270px;
                      position: relative;
                      top: -60px;
                      padding-top: 25px;
                      background-image: url(./image/blue-box2.png);
                      background-size: contain;
                      background-repeat: no-repeat;
                      background-position: center;
                      text-align: center;
                      display: flex;
                      flex-direction: column;
                      align-items: center;
                      justify-content: center;
                      color: rgb(255, 255, 255);
                      font-family: "Suwannaphum", serif;
                    }
                    .info {
                      padding: 3px 0px;
                      width: 80%;
                      /* background-color: rgb(0, 68, 255); */
                      text-align: left;
                    }
                    p {
                      font-size: 12px;
                    }

                    .button {
                      height: 70px;
                      background-color: #ff6c00;
                    }

                    /* back */
                    .back {
                      padding: 0;
                      /* background-color: gold; */
                      display: flex;
                      flex-direction: column;
                      justify-content: space-between;

                    }
                    .back .back-top {
                      width: auto;
                      /* background-color: #ff6c00; */
                    }
                    .back-top img {
                      background-image: none;
                      transform: translate(-18px, 8px);
                    }
                    .center-back {
                      display: flex;
                      align-items: center;
                      justify-content: center;
                      flex-direction: column;
                      /* background-color: crimson; */
                      width: 100%;
                      height: auto;
                      position: relative;
                      top: -82px;
                    }
                    .text-center {
                      z-index: 1;
                      width: 90%;
                      height: 416px;
                      background-color: #23336c;
                      border-radius: 13px;
                      text-align: center;
                      color: white;

                    }
                    .text-center p{
                      margin: 10px;
                    }
                    .button-back {
                      transform: translateY(-135px);
                      background-color: #ff6600;
                      position: relative;
                      height: 100px;

                    }

                  </style>
                  <link rel="preconnect" href="https://fonts.googleapis.com">
                  <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
                  <link
                    href="https://fonts.googleapis.com/css2?family=Figtree:ital,wght@0,300..900;1,300..900&family=Lilita+One&family=Moul&family=Nokora:wght@100;300;400;700;900&family=Rubik:ital,wght@0,300..900;1,300..900&family=Suwannaphum:wght@100;300;400;700;900&display=swap"
                    rel="stylesheet">



                </head>

                <body>
                  <section class="content">
                    <div class="card-fornt">
                      <div class="top">
                        <div class="logo-top">
                          <img src="./image/logo-school.png" alt="logo" width="100%">
                        </div>
                        <div class="school-name">
                          <h2>សាលារៀន អន្ដរជាតិ ស្មាយលីង</h2>
                          <h4>SMILING INTERNATIONAL SCHOOL</h4>
                        </div>
                      </div>

                      <div class="center">
                        <img class="pf" src= "{{photoPath}}" alt="pf">
                        <div class="blue-box">
                          <h3>{{stu.NameKh}}</h3>
                          <h4>{{stu.NameEng}}</h4>
                          <div class="info">
                            <p>
                              អត្ដលេខ​ &nbsp;| ID &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;៖ &nbsp;&nbsp;{{stu.StudentID}} <br>
                              ថ្នាក់ទី &nbsp;&nbsp;&nbsp;&nbsp;| Grade ៖ &nbsp;&nbsp;{{stu.Sex}}<br>
                              វេនសិក្សា  | Time&nbsp; ៖ &nbsp;&nbsp; <br>
                              លេខទូរសព្ទ័ | Phone ៖&nbsp;&nbsp; {{stu.Phone}}
                            </p>

                          </div>
                          <br>
                          <p>ថ្ងៃផុតកំណត់ ៖ ២៨ សីហា ២០២៥</p>
                        </div>

                      </div>
                      <div class="button">

                      </div>
                    </div>

                    <!-- back  -->
                    <div class="card-fornt back">
                      <div class="back-top top">
                        <img src="./image/back-top.png" alt="" width="113%">
                      </div>

                      <div class="center-back">
                        <div class="text-center">
                          <img src="./image/logo-school.png" alt="" width="30%">
                          <div class="school-name">
                            <h2>សាលារៀន អន្ដរជាតិ ស្មាយលីង</h2>
                            <h4>SMILING INTERNATIONAL SCHOOL</h4>
                          </div>
                          <p>
                            <br>
                            <br>
                            <br>
                            <br>
                            <br>
                            <br>
                            &nbsp;&nbsp;ធ្វើនៅថ្ងៃទី&nbsp;{{KhmerUtils.ToKhmerNumber(DateTime.Today.Day)}}&nbsp;ខែ&nbsp;{{KhmerUtils.ToKhmerMonth(DateTime.Today.Month)}}&nbsp;ឆ្នាំ&nbsp;{{KhmerUtils.ToKhmerNumber(DateTime.Now.Year)}}
                            <br>
                            <br>
                            <br>
                            <br>
                            <br>
                            <br>
                            <div style="margin-right: 50px;"><p style="text-align:end;font-size: 12px;">លេខា</p></div>  
                          </p>

                        </div>


                      </div>
                      <div class="button-back">
                        <p style="height: 70px;"> </p>
                      </div>
                    </div>

                  </section>

                </body>

                </html>
                """";
            return htmll;
        }
        private void doExport(Student stu)
        {
            string folderPath = Path.Combine(System.Windows.Forms.Application.StartupPath, "Data\\StudentCard");
            Directory.CreateDirectory(folderPath); // Safe: creates only if not exists

            //string safeDate = p.PayDate.ToString("yyyyMMdd");
            string fileName = $"{stu.StudentID}_{stu.NameEng}.html";
            string fullPath = Path.Combine(folderPath, fileName);
            File.WriteAllText(fullPath, GenerateHtml(stu));
            Process.Start(new ProcessStartInfo(fullPath)
            {
                UseShellExecute = true
            });
        }
        #endregion
        public void viewDgv(List<Student> pageData)
        {
            dgv.Rows.Clear();
            foreach (Student s in pageData)
            {
                int index = dgv.Rows.Add(s.StudentID, s.NameEng, s.NameKh, s.Sex, s.Father,s.Mother, s.Phone);
                dgv.Rows[index].Tag = s;
                s.Tag = dgv.Rows[index];
            }

            lbCurrentPage.Text = paginator.CurrentPage.ToString("D2");
            btnPre.Enabled = paginator.CurrentPage > 1;
            btnNext.Enabled = paginator.CurrentPage < paginator.TotalPages;
        }
        public void information()
        {
            if (dgv.CurrentRow?.Tag is Student selected)
            {
                var form = new FormUpdateStudent(selected);
                form.showInfor();
                form.doReadOnly();
                form.Show();
            }
        }

        public void insert()
        {
            mgmClassTeacher clstea = new mgmClassTeacher();
            clstea.loadDataFromDB(conn.GetConnection());
            if (clstea.classTeachers.Count() > 0)
            {
                FormUpdateStudent form = new FormUpdateStudent(null);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    Student newStudent = form.StudentData;


                    string sourcePath = newStudent.Photo;
                    //MessageBox.Show(sourcePath);
                    string safeName = newStudent.NameEng.Replace(" ", "_"); // "Sok_Dara"
                    string fileName = $"{newStudent.StudentID}_{safeName}.jpg";
                    newStudent.Photo = fileName;


                    string folderPath = Path.Combine(Application.StartupPath, "Data", "Student");
                    Directory.CreateDirectory(folderPath); // safe even if already exists

                    string destinationPath = Path.Combine("Data/Student/", fileName);
                    File.Copy(sourcePath, destinationPath, true);

                    // Assume newStudent is filled but NOT inserted yet
                    int nextStudentID = 1; // default if no data

                    string lastIdQuery = $@"SELECT ISNULL(MAX(StudentID), 0) + 1 
                        FROM {Constants.tbStudent}";

                    using (SqlCommand cmd = new SqlCommand(lastIdQuery, con.GetConnection()))
                    {
                        object result = cmd.ExecuteScalar();
                        nextStudentID = Convert.ToInt32(result);
                    }



                    DateTime startDate = form.StartDate;  // your start date from UI
                    bool selectionMade = false;

                    using (FormSelectClassTeacher formSelect = new FormSelectClassTeacher(nextStudentID, 0, 0, 0))
                    {
                        if (formSelect.ShowDialog() == DialogResult.OK)
                        {
                            int classID = formSelect.ClassID;
                            int teacherID = formSelect.TeacherID;

                            if (classID == 0 || teacherID == 0)
                            {
                                MessageBox.Show("Please select valid Class and Teacher.");
                                return;
                            }

                            selectionMade = true;

                            int classTeacherID;

                            // Get ClassTeacherID
                            string getClassTeacherQuery = @"
                                        SELECT ClassTeacherID FROM ClassTeacher WHERE ClassID = @ClassID AND TeacherID = @TeacherID";

                            using (SqlCommand cmd = new SqlCommand(getClassTeacherQuery, con.GetConnection()))
                            {
                                cmd.Parameters.AddWithValue("@ClassID", classID);
                                cmd.Parameters.AddWithValue("@TeacherID", teacherID);
                                object result = cmd.ExecuteScalar();
                                if (result == null)
                                {
                                    MessageBox.Show("ClassTeacher not found.");
                                    return;
                                }
                                classTeacherID = Convert.ToInt32(result);
                            }

                            // Insert Student now that Class & Teacher selected
                            string studentInsertQuery = $@"
                                    INSERT INTO {Constants.tbStudent} 
                                        (NameEng, NameKh, Sex, DOB, POB, CurrentPlace, Father, Mother, Member, Phone, Photo)
                                    VALUES 
                                        (@NameEng, @NameKh, @Sex, @DOB, @POB, @CurrentPlace, @Father, @Mother, @Member, @Phone, @Photo);
                                    SELECT SCOPE_IDENTITY();
                                ";

                            int insertedStudentID;
                            using (SqlCommand insertCmd = new SqlCommand(studentInsertQuery, con.GetConnection()))
                            {
                                insertCmd.Parameters.AddWithValue("@NameEng", newStudent.NameEng);
                                insertCmd.Parameters.AddWithValue("@NameKh", newStudent.NameKh);
                                insertCmd.Parameters.AddWithValue("@Sex", newStudent.Sex);
                                insertCmd.Parameters.AddWithValue("@DOB", newStudent.DOB);
                                insertCmd.Parameters.AddWithValue("@POB", newStudent.POB);
                                insertCmd.Parameters.AddWithValue("@CurrentPlace", newStudent.CurrentPlace);
                                insertCmd.Parameters.AddWithValue("@Father", newStudent.Father);
                                insertCmd.Parameters.AddWithValue("@Mother", newStudent.Mother);
                                insertCmd.Parameters.AddWithValue("@Member", newStudent.Member);
                                insertCmd.Parameters.AddWithValue("@Phone", newStudent.Phone);
                                insertCmd.Parameters.AddWithValue("@Photo", newStudent.Photo);

                                object result = insertCmd.ExecuteScalar();
                                insertedStudentID = Convert.ToInt32(result);
                            }

                            newStudent.StudentID = insertedStudentID;


                            int newSCTID;
                            // Insert StudentClassTeacher
                            string insertSCTQuery = @"
                                INSERT INTO StudentClassTeacher (StudentID, ClassTeacherID, StartDate, EndDate)
                                VALUES (@StudentID, @ClassTeacherID, @StartDate, @EndDate);
                                SELECT CAST(SCOPE_IDENTITY() AS INT);
                            ";

                            using (SqlCommand insertSCTCmd = new SqlCommand(insertSCTQuery, con.GetConnection()))
                            {
                                insertSCTCmd.Parameters.AddWithValue("@StudentID", insertedStudentID);
                                insertSCTCmd.Parameters.AddWithValue("@ClassTeacherID", classTeacherID);
                                insertSCTCmd.Parameters.AddWithValue("@StartDate", startDate);
                                insertSCTCmd.Parameters.AddWithValue("@EndDate", startDate);

                                newSCTID = (int)insertSCTCmd.ExecuteScalar();
                                // Now newSCTID holds the inserted StudentClassTeacherID
                            }


                            MgmStudent.addStudent(newStudent);
                            paginator.LoadPage(paginator.TotalPages);
                            //paginator.Refresh();

                            StudentClassTeacher sct = new StudentClassTeacher();
                            string querySelectSCT = $@"
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
                                    WHERE sct.IsActive = 1 AND StudentClassTeacherID = @SCTID";
                            //string querySelectSCT = $"SELECT * FROM {Constants.tbStudentClassTeacher} WHERE StudentClassTeacherID = @SCTID";

                            Dictionary<string, object?> parameters = new Dictionary<string, object?>();
                            parameters.Add("@SCTID", newSCTID);


                            mgmStudentClassTeacher mgmStudentClassTeacher = new mgmStudentClassTeacher();
                            mgmStudentClassTeacher.loadDataFromDB(con.GetConnection(), null, parameters);

                            using (FormUpdatePayment formPay = new FormUpdatePayment(mgmStudentClassTeacher.studentClassTeachers.First(), 0))
                            {
                                if (formPay.ShowDialog() == DialogResult.OK)
                                {
                                    Payment payment = formPay.dataPayment;
                                    string insertQuery = $@"
                                        INSERT INTO {Constants.tbPayment}
                                            (StudentClassTeacherID, PayDate, Duration, StartDate, EndDate, Bill, Discount, Amount)
                                        VALUES
                                            (@StudentClassTeacherID, @PayDate, @Duration, @StartDate, @EndDate, @Bill, @Discount, @Amount);
                                    ";

                                    string updateQuery = $@"
                                        UPDATE {Constants.tbStudentClassTeacher}
                                        SET EndDate = @EndDate
                                        WHERE StudentClassTeacherID = @StudentClassTeacherID;
                                    ";

                                    using (SqlConnection connection = con.GetConnection())
                                    {
                                        using (SqlTransaction transaction = connection.BeginTransaction())
                                        {
                                            try
                                            {
                                                using (SqlCommand insertCmd = new SqlCommand(insertQuery, connection, transaction))
                                                {
                                                    insertCmd.Parameters.AddWithValue("@StudentClassTeacherID", payment.StudentClassTeacherID);
                                                    insertCmd.Parameters.AddWithValue("@PayDate", payment.PayDate);
                                                    insertCmd.Parameters.AddWithValue("@Duration", payment.Duration);
                                                    insertCmd.Parameters.AddWithValue("@StartDate", payment.StartDate);
                                                    insertCmd.Parameters.AddWithValue("@EndDate", payment.EndDate);
                                                    insertCmd.Parameters.AddWithValue("@Bill", payment.Bill);
                                                    insertCmd.Parameters.AddWithValue("@Discount", payment.Discount);
                                                    insertCmd.Parameters.AddWithValue("@Amount", payment.Amount);

                                                    insertCmd.ExecuteNonQuery();
                                                }

                                                using (SqlCommand updateCmd = new SqlCommand(updateQuery, connection, transaction))
                                                {
                                                    updateCmd.Parameters.AddWithValue("@EndDate", payment.EndDate);
                                                    updateCmd.Parameters.AddWithValue("@StudentClassTeacherID", payment.StudentClassTeacherID);

                                                    updateCmd.ExecuteNonQuery();
                                                }

                                                transaction.Commit();
                                            }
                                            catch (Exception ex)
                                            {
                                                transaction.Rollback();
                                                // Handle or rethrow exception as needed
                                                throw;
                                            }
                                        }
                                    }
                                }
                            }
                            
                        };

                        formSelect.FormClosed += (s, e) =>
                        {
                            if (!selectionMade)
                            {
                                MessageBox.Show("No Class and Teacher selected, student was not saved.");
                                // No DB insertion happened
                            }
                        };

                    }



                    updateFile(MgmStudent.Students, Constants.StudentFile);



                    //paginator.LoadPage(paginator.TotalPages);
                }
                MgmStudent.loadDataFromDB(con.GetConnection());
                paginator.AllData = MgmStudent.Students;
                paginator.LoadPage(paginator.TotalPages);

            }
            else
            {
                MessageBox.Show("Pls insert Class AND Teacher first!!");
            }

        }
        public void update()
        {
            if (dgv.CurrentRow?.Tag is Student selected)
            {

                //MessageBox.Show(selected.Id);
                var form = new FormUpdateStudent(selected);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    Student student = form.StudentData;

                    // Compare only the filename, not full path
                    string currentFilename = Path.GetFileName(student.Photo); // e.g., "temp.jpg"
                    if (!string.Equals(currentFilename?.Trim(), selected.Photo?.Trim(), StringComparison.OrdinalIgnoreCase))
                    {
                        // Generate new filename like: 123_Sok_Dara.jpg
                        string safeName = student.NameEng.Replace(" ", "_");
                        string fileName = $"{student.StudentID}_{safeName}.jpg";
                        student.Photo = fileName;

                        // Prepare paths
                        string folderPath = Path.Combine(Application.StartupPath, "Data", "Student");
                        Directory.CreateDirectory(folderPath); // Create if not exists

                        string destinationPath = Path.Combine(folderPath, fileName);
                        File.Copy(student.Photo, destinationPath, true);
                    }
                    else
                    {
                        student.Photo = currentFilename;
                    }

                    var con = DatabaseConnection.Instance;

                    string query = $@"
                            UPDATE {Constants.tbStudent} SET 
                                NameEng = @NameEng,
                                NameKh = @NameKh,
                                Sex = @Sex,
                                DOB = @DOB,
                                POB = @POB,
                                CurrentPlace = @CurrentPlace,
                                Father = @Father,
                                Mother = @Mother,
                                Member = @Member,
                                Phone = @Phone,
                                Photo = @Photo
                            WHERE StudentID = @StudentID";

                    SqlCommand cmd = new SqlCommand(query, con.GetConnection());

                    cmd.Parameters.AddWithValue("@NameEng", student.NameEng);
                    cmd.Parameters.AddWithValue("@NameKh", student.NameKh);
                    cmd.Parameters.AddWithValue("@Sex", student.Sex);
                    cmd.Parameters.AddWithValue("@DOB", student.DOB);
                    cmd.Parameters.AddWithValue("@POB", student.POB);
                    cmd.Parameters.AddWithValue("@CurrentPlace", student.CurrentPlace);
                    cmd.Parameters.AddWithValue("@Father", student.Father);
                    cmd.Parameters.AddWithValue("@Mother", student.Mother);
                    cmd.Parameters.AddWithValue("@Member", student.Member);
                    cmd.Parameters.AddWithValue("@Phone", student.Phone);
                    cmd.Parameters.AddWithValue("@Photo", student.Photo);
                    cmd.Parameters.AddWithValue("@StudentID", student.StudentID);

                    cmd.ExecuteNonQuery();
                    con.CloseConnection();

                    MgmStudent.updateStudent(student);
                    paginator.LoadPage(paginator.CurrentPage);

                    updateFile(MgmStudent.Students, Constants.StudentFile);
                }
            }
        }

        public void delete()
        {
            if(dgv.CurrentRow?.Tag is Student selected)
            {
                //MessageBox.Show(selected.Id);
                FormUpdateStudent form = new FormUpdateStudent(selected);
                form.doReadOnly();
                if(form.ShowDialog() == DialogResult.OK)
                {
                    // 1️⃣ Check if student is already soft-deleted
                    string checkQuery = $"SELECT IsActive FROM {Constants.tbStudent} WHERE StudentID = @StudentID";
                    using (SqlCommand cmdCheck = new SqlCommand(checkQuery, conn.GetConnection()))
                    {
                        cmdCheck.Parameters.AddWithValue("@StudentID", selected.StudentID);
                        var isActiveObj = cmdCheck.ExecuteScalar();

                        if (isActiveObj != null && Convert.ToInt32(isActiveObj) == 0)
                        {
                            var result = MessageBox.Show(
                            "Are you sure you want to permanently delete this student and all related records?",
                            "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                            if (result != DialogResult.Yes) return;
                            else
                            {
                                // Get all StudentClassTeacherIDs for this student
                                List<int> sctIds = new List<int>();
                                string querySCTIds = $"SELECT StudentClassTeacherID FROM {Constants.tbStudentClassTeacher} WHERE StudentID = @StudentID";

                                using (SqlCommand cmd = new SqlCommand(querySCTIds, conn.GetConnection()))
                                {
                                    cmd.Parameters.AddWithValue("@StudentID", selected.StudentID);
                                    using (SqlDataReader reader = cmd.ExecuteReader())
                                    {
                                        while (reader.Read())
                                        {
                                            sctIds.Add(reader.GetInt32(0));
                                        }
                                    }
                                }

                                // Delete payments for all those StudentClassTeacherIDs
                                foreach (int sctId in sctIds)
                                {
                                    string deletePayment = $"DELETE FROM {Constants.tbPayment} WHERE StudentClassTeacherID = @SCTID";
                                    using (SqlCommand cmdDeletePayment = new SqlCommand(deletePayment, conn.GetConnection()))
                                    {
                                        cmdDeletePayment.Parameters.AddWithValue("@SCTID", sctId);
                                        cmdDeletePayment.ExecuteNonQuery();
                                    }
                                }

                                // Delete StudentClassTeacher records
                                string deleteSCT = $"DELETE FROM {Constants.tbStudentClassTeacher} WHERE StudentID = @StudentID";
                                using (SqlCommand cmdDeleteSCT = new SqlCommand(deleteSCT, conn.GetConnection()))
                                {
                                    cmdDeleteSCT.Parameters.AddWithValue("@StudentID", selected.StudentID);
                                    cmdDeleteSCT.ExecuteNonQuery();
                                }

                                // Delete Student record
                                string deleteStudent = $"DELETE FROM {Constants.tbStudent} WHERE StudentID = @StudentID";
                                using (SqlCommand cmdDeleteStudent = new SqlCommand(deleteStudent, conn.GetConnection()))
                                {
                                    cmdDeleteStudent.Parameters.AddWithValue("@StudentID", selected.StudentID);
                                    cmdDeleteStudent.ExecuteNonQuery();
                                }

                                // Show confirmation
                                MessageBox.Show("Student and all related records (including payments) have been permanently deleted.",
                                    "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }
                        }
                        else
                        {
                            // Soft-delete
                            string queryStudent = $"UPDATE {Constants.tbStudent} SET IsActive = 0 WHERE StudentID = @StudentID";
                            using (SqlCommand cmdStudent = new SqlCommand(queryStudent, conn.GetConnection()))
                            {
                                cmdStudent.Parameters.AddWithValue("@StudentID", selected.StudentID);
                                cmdStudent.ExecuteNonQuery();
                            }

                            string querySCT = $"UPDATE {Constants.tbStudentClassTeacher} SET IsActive = 0 WHERE StudentID = @StudentID";
                            using (SqlCommand cmdSCT = new SqlCommand(querySCT, conn.GetConnection()))
                            {
                                cmdSCT.Parameters.AddWithValue("@StudentID", selected.StudentID);
                                cmdSCT.ExecuteNonQuery();
                            }
                            MessageBox.Show("Student soft-deleted.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                    }



                    conn.CloseConnection();

                    MgmStudent.removeStudent(selected);
                    paginator.LoadPage(paginator.CurrentPage);
                    updateFile(MgmStudent.Students, Constants.StudentFile);
                }
            }
        }
        public void search(string data)
        {
            paginator.AllData = MgmStudent.searchSudents(data);
            //paginator.LoadPage(1);
        }

        private void doShowFilter()
        {
            var formPopup = new formFilterStudent();
            formPopup.Location = btnFilter.PointToScreen(new Point(0, btnFilter.Height + 5));


            formPopup.FilterApplied += (s, e) =>
            {
                string name = formPopup.Name;
                string sex = formPopup.Sex;
                string classID = formPopup.ClassID;

                var db = DatabaseConnection.Instance;
                string query = $@"
                    SELECT 
                        s.StudentID,
                        s.NameEng,
                        s.NameKh,
                        s.Sex,
                        s.DOB,
                        s.POB,
                        s.CurrentPlace,
                        s.Father,
                        s.Mother,
                        s.Member,
                        s.Phone,
                        s.Photo
                    FROM {Constants.tbStudent} s
                    WHERE 
                        EXISTS (
                            SELECT 1
                            FROM {Constants.tbStudentClassTeacher} sct
                            WHERE 
                                sct.StudentID = s.StudentID AND
                                sct.IsActive = 1 AND
                                (@Year = 0 OR YEAR(sct.StartDate) = @Year) AND 
                                (@Month = 0 OR MONTH(sct.StartDate) >= @Month)
                        ) AND
                        (@Name IS NULL OR s.NameEng LIKE '%' + @Name + '%' OR s.NameKh LIKE '%' + @Name + '%') AND 
                        (@Sex IS NULL OR s.Sex = @Sex)";




                int selectedYear = 0;
                int selectedMonth = 0;

                if (formPopup.Date == "This Month")
                {
                    selectedYear = DateTime.Now.Year;
                    selectedMonth = DateTime.Now.Month; // Current month
                }
                else if (formPopup.Date == "Last 2 Months")
                {
                    selectedYear = DateTime.Now.Year;
                    selectedMonth = DateTime.Now.AddMonths(-2).Month; // 2 months back
                }
                else if (formPopup.Date == "Last 3 Months")
                {
                    selectedYear = DateTime.Now.Year;
                    selectedMonth = DateTime.Now.AddMonths(-3).Month; // 3 months back
                }

                var students = new List<Student>();

                using (SqlConnection conn = db.GetConnection())
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", string.IsNullOrWhiteSpace(name) ? (object)DBNull.Value : name);
                    cmd.Parameters.AddWithValue("@Sex", string.IsNullOrWhiteSpace(sex) ? (object)DBNull.Value : sex);
                    cmd.Parameters.AddWithValue("@ClassID", string.IsNullOrWhiteSpace(classID) ? (object)DBNull.Value : classID);
                    cmd.Parameters.AddWithValue("@Year", selectedYear);
                    cmd.Parameters.AddWithValue("@Month", selectedMonth);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            students.Add(new Student
                            {
                                StudentID = reader.GetInt32(reader.GetOrdinal("StudentId")),
                                NameEng = reader.GetString(reader.GetOrdinal("NameEng")),
                                NameKh = reader.GetString(reader.GetOrdinal("Namekh")),
                                Sex = reader.GetString(reader.GetOrdinal("Sex")),
                                //GradeID = reader.GetString(reader.GetOrdinal("GradeID")),
                                //GradeName = reader["GradeName"].ToString(),
                                DOB = reader.GetDateTime(reader.GetOrdinal("DOB")),
                                POB = reader.GetString(reader.GetOrdinal("POB")),
                                CurrentPlace = reader.GetString(reader.GetOrdinal("CurrentPlace")),
                                Father = reader.GetString(reader.GetOrdinal("Father")),
                                Mother = reader.GetString(reader.GetOrdinal("Mother")),
                                Member = reader.GetInt32(reader.GetOrdinal("Member")),
                                Phone = reader.GetString(reader.GetOrdinal("Phone")),
                                Photo = reader.GetString(reader.GetOrdinal("Photo"))
                            });
                        }
                    }
                }

                MgmStudent.Students = students;
                paginator.AllData = MgmStudent.Students;
                //lb2.Text = students.Count.ToString();
                //lb4.Text = students.Where(e => e.Sex == "male").Count().ToString();
                //lb6.Text = students.Where(e => e.Sex.Contains("female",StringComparison.OrdinalIgnoreCase)).ToList().Count.ToString();
                //paginator = new PaginationMgm<Student>(students, viewDgv);
                //paginator.LoadPage(1);
                //viewDgv(students);
            };
            formPopup.Show();
        }


        #region method file
        private void updateFile(List<Student> students, string fileName)
        {
            int count = students.Count;
            string[] lines = new string[count];
            int i = 0;
            foreach (Student student in students)
            {
                //string line = student.getInfoForFile;
                string line = "";
                lines[i] = line;
                i++;
            }
            File.Delete(fileName);
            File.WriteAllLines(fileName, lines);
        }
        private void moveTempory(Student s)
        {
            string fileName = "fileTempory.txt";
            //string line = s.getInfoForFile;
            string line = "";
            if (!File.Exists(fileName))
            {
                File.Create(fileName);
            }
            File.AppendAllText(fileName, line);

        }

        #endregion
        private void DgvStudent_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
        {
            // Get the column indexes
            //int editColumnIndex = dgvStudent.Columns["colStuActionEdit"].Index;
            //int deleteColumnIndex = dgvStudent.Columns["colStuActionDelete"].Index; // Your "$" column name

            //// Remove the right border of the "Amount" column
            //if (e.ColumnIndex == editColumnIndex)
            //{
            //    e.AdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.None;
            //    //e.CellStyle.SelectionBackColor = Color.White;
            //}

            //// Remove the left border of the "$" column
            //if (e.ColumnIndex == deleteColumnIndex)
            //{
            //    e.AdvancedBorderStyle.Left = DataGridViewAdvancedCellBorderStyle.None;
            //    //e.CellStyle.SelectionBackColor = Color.White;
            //}
        }

    }
}
