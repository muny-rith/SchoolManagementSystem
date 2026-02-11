    using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolManagementSystem
{
    public partial class paymentStudentsControl : baseUserControl
    {
        DatabaseConnection con = DatabaseConnection.Instance;
        mgmStudent MgmStudent = new mgmStudent();
        PaginationMgm<StudentClassTeacher> paginator;
        mgmStudent listStudents = new mgmStudent();
        //StudentClassTeacher mgmSCT = new StudentClassTeacher();
        mgmStudentClassTeacher mgmSCT = new mgmStudentClassTeacher();
        string queryLoadData = $@"
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
                WHERE sct.IsActive = 1";


        Dictionary<string,object> parameters = null;
        string option;
        public paymentStudentsControl()
        {
            mgmSCT.loadDataFromDB(con.GetConnection());
            //MgmStudent.Students = LoadData(queryLoadData, parameters);
            option = "All";
            InitializeComponent();

            Title = "Payment";
            ListTilte = $"{option}";
            txtInsert = "Pay";
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
                Name = "colStuGrade",
                HeaderText = "Grade",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                Width = 125
            },
            new DataGridViewTextBoxColumn
            {
                Name = "colStuPhone",
                HeaderText = "Phone",
                ReadOnly = true,
                Width = 200
            },
            new DataGridViewImageColumn
            {
                Name = "colStuActionPaid",
                HeaderText = "",
                ImageLayout = DataGridViewImageCellLayout.Zoom, // or Zoom
                Width = 60,               // 👈 Make sure this matches the icon size (e.g., 16x16 or 24x24)
                MinimumWidth = 60,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,

                ReadOnly = true,
            },

            new DataGridViewTextBoxColumn
            {
                Name = "colOther",
                HeaderText = "",
                ReadOnly = true,
                Width = 30,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
            },

            new DataGridViewTextBoxColumn
            {
                Name = "colStatus",
                HeaderText = "",
                ReadOnly = true,
                Width = 150,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
            }

         });
            customControl();
            paginator = new PaginationMgm<StudentClassTeacher>(mgmSCT.studentClassTeachers, viewDgv);
            paginator.LoadPage();
            btnNext.Click += (s, e) => paginator.NextPage();
            btnPre.Click += (s, e) => paginator.PreviousPage();
            dgv.MouseWheel += (s, e) =>
            {
                if (e.Delta < 0)
                    paginator.NextPage();
                else if (e.Delta > 0)
                    paginator.PreviousPage();

                Task.Delay(200);
            };



            LB2 = mgmSCT.studentClassTeachers.Where(e => e.DateDiff >= 0).Count().ToString("D2");
            LB4 = mgmSCT.studentClassTeachers.Where(e => e.DateDiff < 0).Count().ToString("D2");

            txtSearch.TextChanged += (s, e) => search(txtSearch.Text);
            btnInsert.Click += (s, e) => insert();

            btnFilter.Click += (s, e) => doShowFilter();
            btnDelete.Visible = false;
            btnUpdate.Visible = false;
            btnInformation.Visible = false;

            btnExport.Click += (s, e) => { if (dgv.CurrentRow?.Tag is StudentClassTeacher sct) doExportInvitation(sct); };
        }

        private void customControl()
        {
            LB5 = "";
            LB6 = "";
            LB1 = "Paid";
            LB3 = "UnPaid";
        }

        #region export invoice && invitaion
        private string GenerateInvitationHtml(StudentClassTeacher sct)
        {
            mgmClass MgmGrade = new mgmClass();
            MgmGrade.loadDataFromDB(con.GetConnection());
            string gradetype = "khmer";
            //gradetype = stu.findGrade(MgmGrade.Grades).Type;
            string html = $$""""
                <!DOCTYPE html>
                <html lang="km">
                <head>
                    <meta charset="UTF-8">
                    <style>
                        *{
                            margin: 0px;
                            padding: 0px;
                            box-sizing: border-box;
                        }
                        body {
                            height: 148mm;
                            width: 210mm;
                            /* display: flex; */
                        }

                        @media print {
                            body {
                            height: 148mm;
                            width: 210mm;
                            margin: 0;
                            }
                        }
                        body {
                            font-family: 'Khmer OS Battambang', 'Khmer OS', sans-serif;
                            line-height: 1.8;
                            padding: 30px;
                            font-size: 12px;
                            /* border: 1px solid black; */
                            display: flex;
                            flex-direction: column;
                        }
                        .logocontent{
                            position: absolute;
                            display: flex;
                            flex-direction: column;
                            justify-content: center;
                            align-items: center;
                            margin-left: 20px;
                        }
                        .logo {
                            width: 80px;
                            height: 80px;
                        }
                        .logotext{
                            font-size: 12px;
                            text-align: center;
                        }
                        .header {
                            display: flex;
                            justify-content: space-between;
                            align-items: center;
                        }

                        .center-text {
                            text-align: center;
                        }
                        .section {
                            margin-top: 25px;
                        }
                        .footer{
                            display: flex;
                            justify-content: space-between;
                            padding: 0 20px;
                        }
                        .signature {
                            margin-right: 20px;
                            text-align: center;
                        }
                    </style>
                </head>
                <body>
                    <div class="logocontent">
                        <img src="../image/logo.png" class="logo" alt="School Logo">
                        <div class="logotext">
                            <strong>សាលាអន្តរជាតិ ស្មាយលីង</strong><br>
                            <small>SMILING INTERNATIONAL SCHOOL</small>
                        </div>
                    </div>


                    <div class="header">

                        <div class="center-text" style="flex: 1;">
                            <h3>ព្រះរាជាណាចក្រកម្ពុជា<br>
                            ជាតិ សាសនា ព្រះមហាក្សត្រ</h3>
                            <br>
                        </div>
                    </div>

                    <div class="center-text" style="margin-top: 30px;">
                        <h2>លិខិតអញ្ជើញ</h2>
                        <p>
                            <b>
                            សូមជម្រាបជូន ឯកឧត្តម លោកជំទាវ លោក លោកស្រី ដែលត្រូវជាមាតា បិតា អាណាព្យាបាលសិស្សកំពុងរៀននៅ<br> សាលារៀន អន្តរជាតិ ស្មាយលីង ទាំងអស់សូមជ្រាប់៖
                            </b>
                        </p>
                    </div>

                    <div class="section">
                        <p style="margin-bottom: 10px;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;សាលារៀន អន្តរជាតិ ស្មាយលីង មានកិត្តិយសសូមអញ្ជើញ ឯកឧត្តម លោកជំទាវ លោក លោកស្រី ដែលត្រូវជាមាតា-បិតា អាណាព្យាបាល សិស្សឈ្មោះ&nbsp;&nbsp;<b>{{sct.Student.NameKh}}</b>&nbsp; សិក្សាផ្នែក&nbsp;&nbsp;<b>{{char.ToUpper(gradetype[0]) + gradetype.Substring(1)}}&nbsp;</b> កម្រិតថ្នាក់ទី&nbsp;&nbsp;<span style="font-family: 'Arial Black'; font-weight: bold";>{{sct.Student.NameKh}} </span> ។ សូមអញ្ជើញមកកាន់ ការិយាល័យបង់ប្រាក់ នៃសាលារៀន អន្តរជាតិ ស្មាយលីង ដើម្បីបង់ប្រាក់នៅថ្ងៃទីខែឆ្នាំ ដើម្បីបន្តសុពលភាពការសិក្សារបស់បុត្រ ធីតាលោកអ្នកទៅខែក្រោយៗទៀត។
                        </p>
                        <p style="margin-bottom: 10px;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;អាស្រ័យហេតុនេះ សូម ឯកឧត្តម លោកជំទាវ លោក លោកស្រី ដែលត្រូវជាមាតា-បិតា អាណាព្យាបាលសិស្ស ខាងលើជួយចាត់ចែងសម្របសម្រួលដើម្បីមកបង់ថ្លៃសិក្សាបុត្រធីតា របស់លោកអ្នក តាមការគួរ។
                        </p>
                        <p style="margin-bottom: 10px;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;សូម ឯកឧត្តម លោកជំទាវ លោក លោកស្រី ដែលត្រូវជាមាតា បិតា អាណាព្យាបាល ទទួលនូវការគោរពរាប់អានដ៏ជ្រាលជ្រៅពីខ្ញុំបាទ/នាងខ្ញុំដែលជា បុគ្គលិក នៃសាលារៀន អន្តរជាតិ ស្មាយលឹងទាំងអស់។
                        </p>
                    </div>

                    <div class="footer">
                        <div class="qr">
                            <p>ABA: 001 206 423</p>
                            <p>ACLEDA: 087 402 220</p>
                            <p>LORS PHALLA</p>
                        </div>
                        <div class="signature">
                            <br>
                            <p>ភ្នំពេញ, ថ្ងៃ&nbsp;{{KhmerUtils.ToKhmerNumber(DateTime.Today.Day)}}&nbsp;ខែ&nbsp;{{KhmerUtils.ToKhmerMonth(DateTime.Today.Month)}}&nbsp;ឆ្នាំ&nbsp;{{KhmerUtils.ToKhmerNumber(DateTime.Today.Year)}}</p>
                            <p>ហត្ថលេខា និងឈ្មោះ</p>
                        </div>
                    </div>
                </body>
                </html>
                
                """";

            return html;
        }
        private void doExportInvitation(StudentClassTeacher sct)
        {
            //string folderPath = Path.Combine(System.Windows.Forms.Application.StartupPath, "Invitaion");
            string folderPath = Path.Combine(System.Windows.Forms.Application.StartupPath, "Data/Invitation");
            Directory.CreateDirectory(folderPath); // Safe: creates only if not exists

            string fileName = $"invitaon_{sct.StudentID}_{sct.Student.NameEng}.html";
            string fullPath = Path.Combine(folderPath, fileName);

            File.WriteAllText(fullPath, GenerateInvitationHtml(sct));

            Process.Start(new ProcessStartInfo(fullPath)
            {
                UseShellExecute = true
            });
        }

        private string GenerateReceiptHtml(Payment p,StudentClassTeacher sct)
        {
            //string logoPath = Path.Combine(System.Windows.Forms.Application.StartupPath, "Picture\\System\\logo.png");
            string logoPath = Path.Combine(System.Windows.Forms.Application.StartupPath, "Resource\\System\\logo.png");
            byte[] imageBytes = File.ReadAllBytes(logoPath);
            string base64Logo = Convert.ToBase64String(imageBytes);

            string htmll = $$"""
                <!DOCTYPE html>
                <html lang="en">
                <head>
                    <meta charset="UTF-8">
                    <title>Official Receipt</title>
                    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css" rel="stylesheet">
                    <style>
                        * { font - family: 'Noto Sans Khmer', 'Segoe UI', sans-serif;
                            font-size: 14px;
                            margin: 0;
                            padding: 0;
                        }
                        body {
                            width: 148mm;
                            height: 210mm;
                            display: flex;
                        }

                        @media print {
                            body {
                                width: 148mm;
                                height: 210mm;
                                margin: 0;
                            }
                        }
                
                        .main{
                            display: flex;
                            flex-direction: column;
                            align-items: center;
                            justify-items: center;
                            height: 100%;
                            width: 562px;
                            padding : 0 10px;
                        }
                        .main .header{
                            display: flex;
                            flex-direction: row;
                            justify-content: space-between;
                            height: 18%;
                            width: 100%;
                        }
                        .main .header .logo{
                            display: flex;
                            flex-direction: column;
                            justify-content: center;
                            align-items: center;
                            width: 50%;
                        }
                        .main .header .logo .img{
                            background-image: url(data:image/png;base64,{{base64Logo}});
                            background-position: center;
                            background-size: 100px auto;
                            background-repeat: no-repeat;
                            height: 50%;
                            width: 50%;
                        }
                        .main .header .logo .name-logo{
                            text-align: center;

                        }
                        .main .header .date{
                            display: flex;
                            flex-direction: column;
                            height: 100%;

                        }
                        .main .header .date p{
                            margin-top: auto;
                            margin-bottom: 10px;
                            /* height: 15px; */
                        }


                        .main .section{
                            display: flex;
                            flex-direction: column;
                            align-items: center;
                            height: 70%;
                            width: 100%;
                        }
                        .section .title{
                            margin: 30px;
                            p{
                                font-size: large;
                                font-weight: 600;
                                text-align: center;
                            }
                        }
                        .section .discription{
                            width: 100%;
                            .line{
                                margin: 10px 0;
                                font-size: 16px;
                                display: flex;
                                justify-content: space-between;
                                .name{
                                    width: 45%;
                                    display: flex;
                                    justify-content: space-between;
                                }
                                .sex{
                                    width: 15%;
                                    display: flex;
                                    justify-content: space-between;
                                }
                                .grade{
                                    width: 20%;
                                    display: flex;
                                    justify-content: space-between;
                                }
                                .month{
                                    width: 12%;
                                    display: flex;
                                    justify-content: space-between;
                                }
                                .year{
                                    width: 12%;
                                    display: flex;
                                    justify-content: space-between;
                                }
                            }
                        }
                        .section .option{
                            margin: 10px;
                            width: 100%;
                            display: flex;
                            flex-direction: column;
                            .line{
                                display: flex;
                                justify-content: space-between;
                                span{
                                font-size: 11px;
                                }
                            }
                        }
                        .section .payment{
                            width: 100%;
                            .line{
                                margin: 10px 0;
                                font-size: 16px;
                                display: flex;
                                justify-content: space-between;
                            }
                        }
                        .section .accountant{
                            padding: 30px 0;
                            display: flex;
                            flex-direction: row;
                            align-items: center;
                            justify-content: centers;
                            width: 100%;
                            .QR{
                                width: 50%;
                            }
                            .acc{
                                width: 50%;
                                display: flex;
                                flex-direction: column;
                                align-items: center;
                                justify-content: center;
                                text-align: center;
                            }
                        }
                        .main .footer{
                            display: flex;
                            flex-direction: column;
                            /* height: 15%; */
                            width: 100%;
                        }

                    </style>
                </head>
                <body>
                    <div class="main">
                        <div class="header">
                            <div class="logo">
                                <div class="img"></div>
                                <div class="name-logo">សាលារៀន អន្តរជាតិ ស្មាយលីង<br>SMILING INTERNATIONAL SCHOOL</div>
                            </div>
                            <div class="date">
                                <p>ល.រ&nbsp;&nbsp;{{KhmerUtils.ToKhmerNumber(p.PaymentID)}}<br>ថ្ងៃ&nbsp;{{KhmerUtils.ToKhmerNumber(p.PayDate.Day)}}&nbsp;&nbsp;ខែ&nbsp;{{KhmerUtils.ToKhmerMonth(p.PayDate.Month)}}&nbsp;&nbsp; ឆ្នាំ&nbsp;{{KhmerUtils.ToKhmerNumber(p.PayDate.Year)}}</p>
                            </div>
                        </div>
                        <div class="section">
                            <div class="title">
                                <p>បង្កាន់ដៃបង់ប្រាក់<br>OFFICIAL RECEIPT</p>
                            </div>
                            <div class="discription">
                                <div class="line">
                                    <span class="name"><span>ឈ្មោះសិស្ស</span><span>{{sct.Student.NameKh}}</span></span>
                                    <span class="sex"><span>ភេទ</span><span>{{KhmerUtils.ToKhmerGender(sct.Student.Sex)}}</span></span>
                                    <span class="grade"><span>ថ្នាក់</span><span>{{sct.ClassTeacher.Class.Name}}</span></span>
                                </div>
                                <div class="line">
                                    <span class="name"><span>Student's Name</span><span>{{sct.Student.NameEng}}</span></span>
                                    <span class="sex"><span>Sex</span><span>{{sct.Student.Sex}}</span></span>
                                    <span class="grade"><span>Grade</span><span>{{sct.ClassTeacher.Class.Name}}</span></span>
                                </div>
                                <div class="line">
                                    <span style="width: 27%;">សុពលភាពពីថ្ងៃទី&nbsp;&nbsp;{{KhmerUtils.ToKhmerNumber(p.StartDate.Day)}}</span>
                                    <span class="month">ខែ&nbsp;&nbsp;{{KhmerUtils.ToKhmerMonth(p.StartDate.Month)}}</span>
                                    <span class="year">ឆ្នាំ&nbsp;&nbsp;{{KhmerUtils.ToKhmerNumber(p.StartDate.Year)}}</span>
                                    <span style="width: 17%;">ដល់ថ្ងៃទី&nbsp;&nbsp;{{KhmerUtils.ToKhmerNumber(p.EndDate.Day)}}</span>
                                    <span class="month">ខែ&nbsp;&nbsp;{{KhmerUtils.ToKhmerMonth(p.EndDate.Month)}}</span>
                                    <span class="year">ឆ្នាំ&nbsp;&nbsp;{{KhmerUtils.ToKhmerNumber(p.EndDate.Year)}}</span>
                                </div>
                                <div class="line">
                                    <span>Validate:&nbsp;&nbsp;{{p.StartDate.ToString("dd - MMMM - yyyy")}}</span>
                                    <span style="width:50%;">Until:&nbsp;&nbsp;{{p.EndDate.ToString("dd - MMMM - yyyy")}}</span>
                                </div>
                            </div>
                            <div class="option">
                                <div class="line">
                                    <span>សៀវភៅភាសាខ្មែរ <i class="fa-regular fa-square"></i></span>
                                    <span>សៀវភៅភាសាអង់គ្លេស <i class="fa-regular fa-square"></i></span>
                                    <span>សៀវភៅភាសាចិន <i class="fa-regular fa-square"></i></span>
                                    <span>សៀវភៅតាមដាន <i class="fa-regular fa-square"></i></span>
                                    <span>ឯកសណ្ឋាន <i class="fa-regular fa-square"></i></span>
                                </div>
                                <div class="line">
                                    <span>Khmer Book <i class="fa-regular fa-square"></i></span>
                                    <span>English Book <i class="fa-regular fa-square"></i></span>
                                    <span>Chinese Book <i class="fa-regular fa-square"></i></span>
                                    <span>Observation Book <i class="fa-regular fa-square"></i></span>
                                    <span>Uniform <i class="fa-regular fa-square"></i></span>
                                </div>
                            </div>
                            <div class="payment">
                                <div class="line">
                                    <span>ចំនួនៈ&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{{KhmerUtils.ToKhmerNumber(p.Duration)}}&nbsp;ខែ</span>
                                    <span style="width: 50%;">តម្លៃសរុបៈ&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{{KhmerUtils.ToKhmerNumber(p.Amount)}} $</span>
                                </div>
                                <div class="line">
                                    <span style="width: 50%;">Amont:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{{p.Duration.ToString("D2")}}&nbsp;month</span>
                                    <span style="width: 50%;">Total Amount:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{{p.Amount}} $</span>
                                </div>
                            </div>
                            <div class="accountant">
                                 <div class="QR"></div>
                                 <div class="acc">
                                    <p>គណនេយ្យករ / Accountant</p>
                                    <br><br><br>
                                    <p>..................</p>
                                 </div>
                            </div>
                        </div>
                        <div class="footer">
                            <p>ទំនាក់ទំនងតាមទូរសព្ទ័លេខ 087 402 220 / 066 442 220 / 095 372 220 / 096 600 8002</p>
                            <p>អាសយដ្ឋាន ភូមិភ្លើងឆេះរទេះលិច សង្កាត់ភ្លើងឆេះរទេះ ខណ្ឌកំបូល រាជធានីភ្នំពេញ</p>
                            <p><b><u>ចំណាំ</u>៖ បង់ប្រាក់ហើយមិនអាចដក់វិញបាននោះទេ។</b></p>
                        </div>
                    </div>
  
                    </div>
                </body>
                </html>
                
                """;
            return htmll;
        }

        private void doExportInvoice(Payment p,StudentClassTeacher sct)
        {
            string folderPath = Path.Combine(System.Windows.Forms.Application.StartupPath, "Data", "Receipt");
            Directory.CreateDirectory(folderPath); // Safe: creates only if not exists

            string safeDate = p.PayDate.ToString("yyyyMMdd");
            string fileName = $"receipt_{p.PaymentID}_{safeDate}.html";
            string fullPath = Path.Combine(folderPath, fileName);

            File.WriteAllText(fullPath, GenerateReceiptHtml(p,sct));

            Process.Start(new ProcessStartInfo(fullPath)
            {
                UseShellExecute = true
            });
        }

        #endregion
        private void doShowFilter()
        {
            formFilterStudentPayment form = new formFilterStudentPayment();
            form.Location = btnFilter.PointToScreen(new Point(0, btnFilter.Height + 5));
            form.applied += (s, e) =>
            {
                txtSearch.Clear();

                string name = form.StudentName;
                string status = form.Status;
                string cls = form.Cls;
                int? timeStatus = form.TimeStatus;

                if (!string.IsNullOrWhiteSpace(cls))
                {
                    ListTilte = cls;
                }

                queryLoadData = $@"
                            WITH SCTData AS (
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
                                    t.Phone AS TeacherPhone,

                                    DATEDIFF(DAY, GETDATE(), sct.EndDate) AS DateDiff,

                                    CASE 
                                        WHEN DATEDIFF(DAY, GETDATE(), sct.EndDate) >= 0 
                                            THEN DATEDIFF(DAY, GETDATE(), sct.EndDate)
                                    END AS DaysRemaining,

                                    CASE 
                                        WHEN DATEDIFF(DAY, GETDATE(), sct.EndDate) < 0 
                                            THEN ABS(DATEDIFF(DAY, GETDATE(), sct.EndDate))
                                    END AS DaysOverdue
                                FROM {Constants.tbStudentClassTeacher} sct
                                INNER JOIN {Constants.tbStudent} s 
                                    ON sct.StudentID = s.StudentID
                                INNER JOIN ClassTeacher ct 
                                    ON sct.ClassTeacherID = ct.ClassTeacherID
                                INNER JOIN {Constants.tbClass} c 
                                    ON ct.ClassID = c.ClassID
                                INNER JOIN {Constants.tbTeacher} t 
                                    ON ct.TeacherID = t.TeacherID
                                WHERE sct.IsActive = 1 
                            )
                            SELECT *
                            FROM SCTData
                            WHERE         
                                (@name IS NULL OR NameEng LIKE '%' + @name + '%' OR NameKh LIKE '%' + @name + '%')
                                AND (
                                    @status IS NULL 
                                    OR (@status = 'Paid' AND DateDiff >= 0)
                                    OR (@status = 'Unpaid' AND DateDiff < 0)
                                )
                                AND (@class IS NULL OR ClassName = @class)
                                AND (
                                    @timeStatus IS NULL
                                    OR (
                                        @timeStatus = 0 AND (
                                            (DaysRemaining IS NOT NULL AND DaysRemaining <= 1) 
                                            OR (DaysOverdue IS NOT NULL AND DaysOverdue <= 1)
                                        )
                                    )
                                    OR (
                                        @timeStatus = 1 AND (
                                            (DaysRemaining IS NOT NULL AND DaysRemaining <= 3) 
                                            OR (DaysOverdue IS NOT NULL AND DaysOverdue <= 3)
                                        )
                                    )
                                    OR (
                                        @timeStatus = 2 AND (
                                            (DaysRemaining IS NOT NULL AND DaysRemaining <= 5) 
                                            OR (DaysOverdue IS NOT NULL AND DaysOverdue <= 5)
                                        )
                                    )
                                    OR (
                                        @timeStatus = 3 AND (
                                            (DaysRemaining IS NOT NULL AND DaysRemaining >= 7) 
                                            OR (DaysOverdue IS NOT NULL AND DaysOverdue >= 7)
                                        )
                                    )
                                )
                        ";



                parameters = new Dictionary<string, object>
                {
                    { "@name", string.IsNullOrWhiteSpace(name) ? DBNull.Value : name },
                    { "@status", string.IsNullOrWhiteSpace(status) ? DBNull.Value : status },
                    { "@class", string.IsNullOrWhiteSpace(cls) ? DBNull.Value : cls },
                    { "@timeStatus", timeStatus.HasValue ? (object)timeStatus.Value : DBNull.Value }
                };

                mgmSCT.loadDataFromDB(con.GetConnection(), queryLoadData, parameters);

                paginator.AllData = mgmSCT.studentClassTeachers;
                LB2 = mgmSCT.studentClassTeachers.Where(e => e.DateDiff >= 0).Count().ToString("D2");
                LB4 = mgmSCT.studentClassTeachers.Where(e => e.DateDiff < 0).Count().ToString("D2");
            }; 
            form.Show();
        }


        private List<Student> LoadData(string query, Dictionary<string, object> parameters)
        {
            List<Student> students = new List<Student>();

            using (SqlCommand cmd = new SqlCommand(query, con.GetConnection()))
            {
                // Add parameters safely
                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        cmd.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                    }
                }

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Student s = new Student
                        {
                            StudentID = Convert.ToInt32(reader["StudentId"]),

                            NameEng = reader["NameEng"]?.ToString(),
                            NameKh = reader["NameKh"]?.ToString(),
                            Sex = reader["Sex"]?.ToString(),

                            Phone = reader["Phone"]?.ToString(),
                            Photo = reader["Photo"]?.ToString(),

                            //DateDiff = reader.IsDBNull(reader.GetOrdinal("DateDiff")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("DateDiff")),

                        };

                        students.Add(s);
                    }
                }
            }

            return students;
        }

        public void viewDgv(List<StudentClassTeacher> pageData)
        {


            // Bind to DataGridView
            dgv.Rows.Clear();
            foreach (StudentClassTeacher s in pageData)
            {
                DateTime newMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1);


                System.Drawing.Image image = Properties.Resources.Unpaid;


                if (s.EndDate == DateTime.Now.Date) image = Properties.Resources.PaidToday;
                else if (s.EndDate > DateTime.Now.Date && s.EndDate < newMonth)
                {
                    image = Properties.Resources.Paid;
                }
                else if (s.EndDate >= newMonth)
                {
                    image = Properties.Resources.PaidNewMonth;
                }

                int index = dgv.Rows.Add(s.StudentID, s.Student.NameEng, s.Student.NameKh, s.Student.Sex, s.ClassTeacher.Class.Name, s.Student.Phone, image, "", GetPaymentStatus(s.DateDiff));
                //int index = dgv.Rows.Add(s.StudentID, s.NameEng, s.NameKh, s.Sex, "", s.Phone, image, "", GetPaymentStatus(1));
                dgv.Rows[index].Tag = s;
                s.Tag = dgv.Rows[index]; // Optional: if you're tracking it in object
            }
            lbCurrentPage.Text = paginator.CurrentPage.ToString("D2");
            btnPre.Enabled = paginator.CurrentPage > 1;
            btnNext.Enabled = paginator.CurrentPage < paginator.TotalPages;


        }
        public string GetPaymentStatus(int? dateDiff)
        {
            if (!dateDiff.HasValue)
                return "No payment record";

            if (dateDiff > 0)
                return $"{dateDiff} days remain";

            if (dateDiff == 0)
                return "Expires today";

            // dateDiff < 0 means overdue, so take absolute value
            int overdueDays = Math.Abs(dateDiff.Value);
            return $"{overdueDays} days overdue";
        }
        public void information()
        {

        }
        public void insert()
        {
            if (dgv.CurrentRow != null && dgv.CurrentRow.Tag is StudentClassTeacher sct)
            {
                FormUpdatePayment form = new FormUpdatePayment(sct, 0);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    //Payment p = form.dataPayment;
                    Payment payment = form.dataPayment;

                    string insertQuery = $@"
                        INSERT INTO {Constants.tbPayment}
                            (StudentClassTeacherID, PayDate, Duration, StartDate, EndDate, Bill, Discount, DiscountValue, Amount)
                        VALUES
                            (@StudentClassTeacherID, @PayDate, @Duration, @StartDate, @EndDate, @Bill, @Discount, @DiscountValue, @Amount);
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
                                    insertCmd.Parameters.AddWithValue("@DiscountValue", payment.DiscountValue);
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


                    con.CloseConnection();

                    mgmPayment mgmPayment = new mgmPayment();
                    mgmPayment.loadDataFromDB(con.GetConnection());

                    doExportInvoice(payment,sct);
                    updateFile(mgmPayment, Constants.PaymentFile);

                    mgmSCT.loadDataFromDB(con.GetConnection(),queryLoadData,parameters);
                    paginator.AllData = mgmSCT.studentClassTeachers;
                    LB2 = mgmSCT.studentClassTeachers.Where(e => e.DateDiff >= 0).Count().ToString("D2");
                    LB4 = mgmSCT.studentClassTeachers.Where(e => e.DateDiff < 0).Count().ToString("D2");

                }
            }
        }
        public void update()
        {

        }
        public void delete()
        {

        }

        public void search(string data)
        {
            string keyword = data.Trim().ToLower();

            paginator.AllData = mgmSCT.studentClassTeachers
                .Where(e =>
                    e.Student.NameEng.ToLower().Contains(keyword) ||
                    e.Student.NameKh.ToLower().Contains(keyword)
                )
                .ToList();
            LB2 = paginator.AllData.Where(e => e.DateDiff >= 0).Count().ToString("D2");
            LB4 = paginator.AllData.Where(e => e.DateDiff < 0).Count().ToString("D2");

            //paginator.Refresh(); // Optional: If your paginator needs to re-render
        }


        private void updateFile(mgmPayment payments, string fileName)
        {
            int count = payments.Payments.Count;
            string[] lines = new string[count];
            int i = 0;
            foreach (Payment payment in payments.Payments)
            {
                //lines[i] = payment.getInfoForFile;
                i++;
            }
            File.Delete(fileName);
            File.WriteAllLines(fileName, lines);
        }


        private void paymentStudentsControl_Load(object sender, EventArgs e)
        {
        }
    }
}