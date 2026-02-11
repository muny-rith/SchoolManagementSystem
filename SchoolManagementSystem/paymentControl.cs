using Microsoft.Data.SqlClient;
using Org.BouncyCastle.Tls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using System.Linq;

namespace SchoolManagementSystem
{
    public partial class paymentControl : baseUserControl, IStrategyUserControl<Payment>
    {
        PaginationMgm<Payment> paginator;
        mgmPayment mgmPayment = new mgmPayment();
        mgmStudent mgmStudent = new mgmStudent();
        mgmClass mgmGrade = new mgmClass();

        DatabaseConnection con = DatabaseConnection.Instance;

        string query = $@"
                        SELECT 
                            p.PaymentID, p.StudentClassTeacherID, p.PayDate, p.Duration, p.StartDate, p.EndDate, p.Bill, p.Discount, p.DiscountValue, p.Amount,
                            s.StudentID, s.NameEng, s.NameKh, s.Sex, s.Photo AS StudentPhoto, 
                            ct.ClassTeacherID,
                            c.ClassID, c.Name AS ClassName ,c.Price
                            
                        FROM {Constants.tbPayment} p
                        JOIN {Constants.tbStudentClassTeacher} sct ON p.StudentClassTeacherID = sct.StudentClassTeacherID
                        JOIN {Constants.tbStudent} s ON sct.StudentID = s.StudentID
                        JOIN {Constants.tbClassTeacher} ct ON sct.ClassTeacherID = ct.ClassTeacherID
                        JOIN {Constants.tbClass} c ON ct.ClassID = c.ClassID
                        WHERE p.IsActive = 1
                        ORDER BY p.PaymentID DESC;
                    ";

        public paymentControl()
        {
            mgmGrade.loadDataFromDB(con.GetConnection());
            mgmStudent.loadDataFromDB(con.GetConnection(),null);
            mgmPayment.loadDataFromDB(con.GetConnection());
            //mgmPayment.Payments.Reverse();
            InitializeComponent();
            customControl();
            //dgv.CellPainting += dgvPayment_CellPainting;
            this.Title = "Payment";
            this.txtInsert = "Pay";
            this.ListTilte = "Invoice";

            this.dgv.Columns.AddRange(new DataGridViewColumn[]
            {

                new DataGridViewTextBoxColumn
                {
                    Name = "colPaymentStuNameEng",
                    HeaderText = "Name English",
                    Width = 175,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                    ReadOnly = true
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "colPaymentStuNameKh",
                    HeaderText = "Name Khmer",
                    Width = 165,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                    ReadOnly = true
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "colPaymentGradeName",
                    HeaderText = "Grade",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                    Width = 125,
                    ReadOnly = true
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "colPaymentAmount",
                    HeaderText = "Amount",
                    Width = 47,
                    ReadOnly = true,
                },

                //new DataGridViewTextBoxColumn
                //{
                //    Name = "colPaymentSign",
                //    HeaderText = "",
                //    Width = 40,
                //    AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                //    DefaultCellStyle = new DataGridViewCellStyle
                //    {
                //        Alignment = DataGridViewContentAlignment.MiddleLeft
                //    },
                //    ReadOnly = true
                //},
                new DataGridViewTextBoxColumn
                {
                    Name = "colPaymentPayDate",
                    HeaderText = "Paydate",
                    Width = 97,
                    ReadOnly = true
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "colPaymentDuration",
                    HeaderText = "Duration",
                    //AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                    Width = 58,
                    ReadOnly = true
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "colPaymentStart",
                    HeaderText = "StartDate",
                    Width = 97,
                    ReadOnly = true
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "colPaymentEnd",
                    HeaderText = "EndDate",
                    Width = 97,
                    ReadOnly = true
                }
            });

            //this.btnFilter.Visible = false;

            paginator = new PaginationMgm<Payment>(mgmPayment.Payments,viewDgv);

            paginator.LoadPage();
            btnNext.Click += (s, e) => paginator.NextPage();
            btnPre.Click += (s, e) => paginator.PreviousPage();
            dgv.MouseWheel += (s, e) =>
            {
                if (e.Delta < 0)
                    paginator.NextPage();
                else if (e.Delta > 0)
                    paginator.PreviousPage();

                // Delay to avoid rapid paging (adjust as needed)
                Task.Delay(200);
            };

            LB2 = mgmPayment.Payments.Count().ToString("D3");

            txtSearch.TextChanged += (s, e) => search(txtSearch.Text);

            btnExport.Click += (s,e) => { if (dgv.CurrentRow?.Tag is Payment p) { doExportInvoice(p,(dgv.CurrentRow.Tag as Payment).StudentClassTeacher); } };
            btnFilter.Click += (s, e) => doShowFilter();
            btnInformation.Click += (s, e) => information();
            btnInsert.Click += (s, e) => insert();
            btnUpdate.Click += (s, e) => update();
            btnDelete.Click += (s, e) => delete();

            //ContextMenuStrip menuTypeGrade = new ContextMenuStrip();
        }

        private void customControl()
        {
            LB1 = "Records";
            LB3 = "";
            LB4 = "";
            LB5 = "";
            LB6 = "";
        }

        private void doShowFilter()
        {
            contextMenuStrip.Items.Clear();
            string ch1 = "Today";
            string ch2 = "This week";
            string ch3 = "This month";
            contextMenuStrip.Items.Add(ch1, null, contextMenu_Click);
            contextMenuStrip.Items.Add(ch2, null, contextMenu_Click);
            contextMenuStrip.Items.Add(ch3, null, contextMenu_Click);
            contextMenuStrip.Items.Add("All",null,contextMenu_Click);
            //throw new NotImplementedException();
            contextMenuStrip.Show(btnFilter, new Point(0, btnFilter.Height + 5));

            void contextMenu_Click(object sender , EventArgs e)
            {
                var item = sender as ToolStripItem;
                //MessageBox.Show(item.ToString());
                if(item != null)
                {
                    txtSearch.Clear();
                    if (item.Text.Contains(ch1,StringComparison.OrdinalIgnoreCase))
                    {
                        mgmPayment.loadDataFromDB(con.GetConnection());
                        mgmPayment.Payments = mgmPayment.Payments.Where(e => e.PayDate == DateTime.Today).ToList();
                        paginator.AllData = mgmPayment.Payments;
                    }
                    else if(item.Text.Contains(ch2, StringComparison.OrdinalIgnoreCase))
                    {
                        mgmPayment.loadDataFromDB(con.GetConnection());

                        var today = DateTime.Today;
                        var startOfWeek = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday); // Monday this week

                        mgmPayment.Payments = mgmPayment.Payments
                            .Where(e => e.PayDate >= startOfWeek && e.PayDate <= today).ToList();
                        paginator.AllData = mgmPayment.Payments;
                    }
                    else if (item.Text.Contains(ch3, StringComparison.OrdinalIgnoreCase))
                    {
                        mgmPayment.loadDataFromDB(con.GetConnection());

                        mgmPayment.Payments = mgmPayment.Payments.Where(e => e.PayDate.Month == DateTime.Now.Month && e.PayDate.Year == DateTime.Now.Year).ToList();
                        paginator.AllData = mgmPayment.Payments;

                    }
                    else
                    {
                        mgmPayment.loadDataFromDB(con.GetConnection());
                        paginator.AllData = mgmPayment.Payments;
                    }
                    LB2 = mgmPayment.Payments.Count().ToString("D3");

                }
            }
        }

        public void viewDgv(List<Payment> pageData)
        {
            dgv.Rows.Clear();
            foreach (Payment payment in pageData)
            {
                int index = dgv.Rows.Add(payment.StudentClassTeacher.Student.NameEng, payment.StudentClassTeacher.Student.NameKh, payment.StudentClassTeacher.ClassTeacher.Class.Name, "$  " + payment.Amount, payment.PayDate.ToString("dd-MMM-yy"), payment.Duration, payment.StartDate.ToString("dd-MMMM-yy"), payment.EndDate.ToString("dd-MMM-yy"));

                //int index = dgv.Rows.Add(payment.findStudent(mgmStudent).NameEng, payment.findStudent(mgmStudent).NameKh, payment.findGrade(mgmGrade).Name, "$  " + payment.Amount, payment.PayDate.ToString("dd-MMM-yy"), payment.Duration, payment.StartDate.ToString("dd-MMMM-yy"), payment.EndDate.ToString("dd-MMM-yy"));
                dgv.Rows[index].Tag = payment;
                payment.Tag = dgv.Rows[index];
            }
            //dgvPayment.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            lbCurrentPage.Text = paginator.CurrentPage.ToString("D2");
            btnPre.Enabled = paginator.CurrentPage > 1;
            btnNext.Enabled = paginator.CurrentPage < paginator.TotalPages;
        }
        #region method
        public void information()
        {
            if (dgv.CurrentRow?.Tag is Payment selected)
            {
                var form = new FormUpdatePayment(selected.StudentClassTeacher,selected.PaymentID);
                form.doReadOnly();
                form.showInfor();
                form.Show();
            }
        }
        public void insert()
        {
            var formSelectStudent = new formSelectSCT();
            if(formSelectStudent.ShowDialog() == DialogResult.OK)
            {
                StudentClassTeacher student = formSelectStudent.SCT;

                var form = new FormUpdatePayment(student,0);
                if(form.ShowDialog() == DialogResult.OK)
                {
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

                    //mgmPayment.Payments.Reverse();

                    mgmPayment.loadDataFromDB(con.GetConnection());
                    paginator.AllData = mgmPayment.Payments;
                    //mgmPayment.Payments.Reverse();
                    updateFile(mgmPayment, Constants.PaymentFile);
                    //paginator.AllData = mgmPayment.Payments.AsEnumerable().Reverse().ToList();
                    doExportInvoice(payment,student);

                    //paginator.LoadPage(paginator.TotalPages);
                }
            }
        }

        public void update()
        {
            if(dgv.CurrentRow?.Tag is Payment selected)
            {
                var form = new FormUpdatePayment(selected.StudentClassTeacher,selected.PaymentID);
                if(form.ShowDialog() == DialogResult.OK)
                {
                    Payment payment = form.dataPayment;

                    string updatePaymentQuery = $@"
                        UPDATE {Constants.tbPayment}
                        SET
                            PayDate = @PayDate,
                            Duration = @Duration,
                            StartDate = @StartDate,
                            EndDate = @EndDate,
                            Bill = @Bill,
                            Discount = @Discount,
                            DiscountValue = @DiscountValue,
                            Amount = @Amount
                        WHERE PaymentID = @PaymentID;
                    ";

                    string updateStudentClassTeacherQuery = $@"
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
                                using (SqlCommand updatePaymentCmd = new SqlCommand(updatePaymentQuery, connection, transaction))
                                {
                                    updatePaymentCmd.Parameters.AddWithValue("@PayDate", payment.PayDate);
                                    updatePaymentCmd.Parameters.AddWithValue("@Duration", payment.Duration);
                                    updatePaymentCmd.Parameters.AddWithValue("@StartDate", payment.StartDate);
                                    updatePaymentCmd.Parameters.AddWithValue("@EndDate", payment.EndDate);
                                    updatePaymentCmd.Parameters.AddWithValue("@Bill", payment.Bill);
                                    updatePaymentCmd.Parameters.AddWithValue("@Discount", payment.Discount);
                                    updatePaymentCmd.Parameters.AddWithValue("@DiscountValue", payment.DiscountValue);
                                    updatePaymentCmd.Parameters.AddWithValue("@Amount", payment.Amount);
                                    updatePaymentCmd.Parameters.AddWithValue("@PaymentID", payment.PaymentID);

                                    updatePaymentCmd.ExecuteNonQuery();
                                }

                                using (SqlCommand updateSCTCmd = new SqlCommand(updateStudentClassTeacherQuery, connection, transaction))
                                {
                                    updateSCTCmd.Parameters.AddWithValue("@EndDate", payment.EndDate);
                                    updateSCTCmd.Parameters.AddWithValue("@StudentClassTeacherID", payment.StudentClassTeacherID);

                                    updateSCTCmd.ExecuteNonQuery();
                                }

                                transaction.Commit();
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                // Handle error or rethrow
                                throw;
                            }
                        }
                    }

                    con.CloseConnection();

                    updateFile(mgmPayment, Constants.PaymentFile);

                    mgmPayment.loadDataFromDB(con.GetConnection());
                    paginator.AllData = mgmPayment.Payments;

                    //paginator.AllData = mgmPayment.Payments.AsEnumerable().Reverse().ToList();
                    //paginator.LoadPage(paginator.CurrentPage);
                }
            }
        }
        public void delete()
        {
            if (dgv.CurrentRow?.Tag is Payment selected)
            {
                var form = new FormUpdatePayment(selected.StudentClassTeacher, selected.PaymentID);
                form.doReadOnly();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    Payment payment = form.dataPayment;


                    string deletePaymentQuery = $"DELETE FROM {Constants.tbPayment} WHERE PaymentID = @PaymentID";
                    string getMaxEndDateQuery = $"SELECT MAX(EndDate) FROM {Constants.tbPayment} WHERE StudentClassTeacherID = @StudentClassTeacherID";
                    string updateStudentClassTeacherQuery = $@"
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
                                // Delete payment
                                using (SqlCommand deleteCmd = new SqlCommand(deletePaymentQuery, connection, transaction))
                                {
                                    deleteCmd.Parameters.AddWithValue("@PaymentID", payment.PaymentID);
                                    deleteCmd.ExecuteNonQuery();
                                }

                                // Get latest EndDate from remaining payments
                                DateTime? newEndDate = null;
                                using (SqlCommand maxDateCmd = new SqlCommand(getMaxEndDateQuery, connection, transaction))
                                {
                                    maxDateCmd.Parameters.AddWithValue("@StudentClassTeacherID", payment.StudentClassTeacherID);
                                    object result = maxDateCmd.ExecuteScalar();
                                    if (result != DBNull.Value)
                                    {
                                        newEndDate = (DateTime)result;
                                    }
                                }

                                // If no payments left, get StartDate from StudentClassTeacher
                                if (!newEndDate.HasValue)
                                {
                                    string getStartDateQuery = @"
                                            SELECT StartDate 
                                            FROM StudentClassTeacher 
                                            WHERE StudentClassTeacherID = @StudentClassTeacherID";

                                    using (SqlCommand getStartDateCmd = new SqlCommand(getStartDateQuery, connection, transaction))
                                    {
                                        getStartDateCmd.Parameters.AddWithValue("@StudentClassTeacherID", payment.StudentClassTeacherID);
                                        object startDateResult = getStartDateCmd.ExecuteScalar();
                                        if (startDateResult != DBNull.Value)
                                        {
                                            newEndDate = (DateTime)startDateResult;
                                        }
                                    }
                                }

                                // Update StudentClassTeacher EndDate with newEndDate (either max EndDate or StartDate)
                                using (SqlCommand updateCmd = new SqlCommand(updateStudentClassTeacherQuery, connection, transaction))
                                {
                                    if (newEndDate.HasValue)
                                        updateCmd.Parameters.AddWithValue("@EndDate", newEndDate.Value);
                                    else
                                        updateCmd.Parameters.AddWithValue("@EndDate", DBNull.Value);

                                    updateCmd.Parameters.AddWithValue("@StudentClassTeacherID", payment.StudentClassTeacherID);
                                    updateCmd.ExecuteNonQuery();
                                }

                                transaction.Commit();
                            }
                            catch
                            {
                                transaction.Rollback();
                                throw;
                            }
                        }
                    }


                    con.CloseConnection();

                    updateFile(mgmPayment, Constants.PaymentFile);

                    mgmPayment.loadDataFromDB(con.GetConnection());
                    paginator.AllData = mgmPayment.Payments;

                    //paginator.AllData = mgmPayment.Payments.AsEnumerable().Reverse().ToList();
                    //paginator.LoadPage(paginator.CurrentPage);
                }
            }
        }

        public void search(string data)
        {
            List<Payment> payFound = new List<Payment>();
            payFound = mgmPayment.Payments.Where(p => p.StudentClassTeacher.Student.NameEng.Contains(data.Trim(), StringComparison.OrdinalIgnoreCase) || p.StudentClassTeacher.Student.NameKh.Contains(data.Trim(), StringComparison.OrdinalIgnoreCase)).ToList();
            paginator.AllData = payFound;
            LB2 = payFound.Count().ToString("D3");

        }
        #endregion
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
        private string GenerateReceiptHtml(Payment p, StudentClassTeacher sct)
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

        private void doExportInvoice(Payment p, StudentClassTeacher sct)
        {
            string folderPath = Path.Combine(System.Windows.Forms.Application.StartupPath, "Data", "Receipt");
            Directory.CreateDirectory(folderPath); // Safe: creates only if not exists

            string safeDate = p.PayDate.ToString("yyyyMMdd");
            string fileName = $"receipt_{p.PaymentID}_{safeDate}.html";
            string fullPath = Path.Combine(folderPath, fileName);

            File.WriteAllText(fullPath, GenerateReceiptHtml(p, sct));

            Process.Start(new ProcessStartInfo(fullPath)
            {
                UseShellExecute = true
            });
        }


        //private void dgvPayment_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
        //{
        //    // Get the column indexes
        //    int amountColumnIndex = dgv.Columns["colPaymentAmount"].Index;
        //    int currencyColumnIndex = dgv.Columns["colPaymentSign"].Index; // Your "$" column name

        //    // Remove the right border of the "Amount" column
        //    if (e.ColumnIndex == amountColumnIndex)
        //    {
        //        e.AdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.None;
        //    }

        //    // Remove the left border of the "$" column
        //    if (e.ColumnIndex == currencyColumnIndex)
        //    {
        //        e.AdvancedBorderStyle.Left = DataGridViewAdvancedCellBorderStyle.None;
        //    }
        //}

    }
}
