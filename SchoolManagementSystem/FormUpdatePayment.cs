using Google.Protobuf.WellKnownTypes;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolManagementSystem
{
    public partial class FormUpdatePayment : Form
    {
        DatabaseConnection con = DatabaseConnection.Instance;
        Payment payment = new Payment();
        mgmPayment mgmPayment = new mgmPayment();
        mgmStudent mgmStudentClassTeacher = new mgmStudent();
        public Payment dataPayment { get => this.payment; }
        int sctID;

        ~FormUpdatePayment() { }
        public FormUpdatePayment(StudentClassTeacher sct, int payID =0)
        {
            sctID = sct.StudentClassTeacherID;
            mgmPayment.loadDataFromDB(con.GetConnection());
            InitializeComponent();

            if (payID != 0 && sct != null) // update
            {
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
                        WHERE p.IsActive = 1 AND p.PaymentID = @PaymentID
                        ORDER BY p.PaymentID DESC;
                    ";

                var parameters = new Dictionary<string, object>()
                {
                    { "@PaymentID", payID  }
                };

                mgmPayment.loadDataFromDB(con.GetConnection(), query, parameters);

                loadDataPayFromMain(mgmPayment.Payments.FirstOrDefault());
                loadDataStuGraFromMain(sct);

            }
            if (payID == 0 && sct != null)// insert
            {
                string query = $"SELECT ISNULL(MAX(PaymentID), 0) + 1 FROM {Constants.tbPayment}";
                SqlCommand cmd = new SqlCommand(query, con.GetConnection());
                int nextID = (int)cmd.ExecuteScalar();
                txtPaymentId.Text = nextID.ToString();
                query = $@"
                    SELECT EndDate
                    FROM {Constants.tbStudentClassTeacher}
                    WHERE StudentClassTeacherID = @StudentClassTeacherID;
";

                using (cmd = new SqlCommand(query, con.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@StudentClassTeacherID", sctID);
                    object result = cmd.ExecuteScalar();

                    if (result != DBNull.Value && result != null)
                        dtStartPaymentNew.Value = (DateTime)result;
                    else
                        dtStartPaymentNew.Value = DateTime.Today; // or some default
                }

                loadDataStuGraFromMain(sct);
            }
            //MessageBox.Show(sct.ClassTeacher.Class.Price.ToString());
            txtDurationNew.TextChanged += (s, e) => doChange_Start_End_Date(sct);
            dtStartPaymentNew.ValueChanged += (s, e) => doChange_Start_End_Date(sct);
            txtOtherPayment.TextChanged += (s,e)=> doChange_Start_End_Date(sct);
            txtOtherPaymentDollar.TextChanged += (s, e) => doChange_Start_End_Date(sct);
            txtDurationNew.KeyPress += txtDurationNew_KeyPress;
            txtOtherPayment.KeyPress += txtOthrePayment_KeyPress;

            btnYes.Click += (s, e) => doClickSave();
            btnNo.Click += (s, e) => this.Close();
        }
        private void doClickSave()
        {
            //setData();
            if (setData())
            {
                DialogResult = DialogResult.OK;
                this.Close();
            }

        }


        #region method
        private bool validationPayment()
        {
            //if (txtOthrePayment.Text == null || txtOthrePayment.Text == string.Empty) txtOthrePayment.Text = "0";
            foreach (Control c in this.panelPayment.Controls)
            {
                if (c is TextBox)
                {
                    if (c.Text == null || c.Text == string.Empty)
                    {
                        MessageBox.Show("Please Enter all requirement!", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                        return false;

                    }
                }
            }
            if (!int.TryParse(txtDurationNew.Text, out int d) || int.Parse(txtDurationNew.Text) <= 0)
            {
                MessageBox.Show("Duration must be interger!", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return false;
            }
            else if (!float.TryParse(txtBillPayment.Text, out float b) || float.Parse(txtBillPayment.Text) == 0)
            {
                MessageBox.Show("Bill Payment must has value!", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return false;
            }
            else if (!float.TryParse(txtOtherPayment.Text, out float o))
            {
                MessageBox.Show("Discount Payment must be number!", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        #endregion

        #region getData
        private bool setData()
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(txtPaymentId.Text) ||
                string.IsNullOrWhiteSpace(txtStudentId.Text) ||
                string.IsNullOrWhiteSpace(txtGradeId.Text) ||
                string.IsNullOrWhiteSpace(txtDurationNew.Text) ||
                string.IsNullOrWhiteSpace(txtBillPayment.Text) ||
                string.IsNullOrWhiteSpace(txtAmountPayment.Text))
            {
                MessageBox.Show("Please fill in all required fields.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Validate Duration
            if (!int.TryParse(txtDurationNew.Text, out int duration) || duration <= 0 || duration > 1200)
            {
                MessageBox.Show("Duration must be a number between 1 and 1200.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDurationNew.Focus();
                return false;
            }

            // Validate Bill
            if (!decimal.TryParse(txtBillPayment.Text, out decimal bill) || bill < 0)
            {
                MessageBox.Show("Invalid bill amount.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBillPayment.Focus();
                return false;
            }

            // Validate Amount
            if (!decimal.TryParse(txtAmountPayment.Text, out decimal amount) || amount < 0)
            {
                MessageBox.Show("Invalid amount.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAmountPayment.Focus();
                return false;
            }

            // Validate Discount (optional)
            decimal discount = 0;
            if (!string.IsNullOrWhiteSpace(txtOtherPayment.Text))
            {
                if (!decimal.TryParse(txtOtherPayment.Text, out discount) || discount < 0 || discount > 100)
                {
                    MessageBox.Show("Discount must be a number between 0 and 100.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtOtherPayment.Focus();
                    return false;
                }
            }
            // Validate DiscountValue (optional)
            decimal discountValue = 0;
            if (!string.IsNullOrWhiteSpace(txtOtherPaymentDollar.Text))
            {
                if (!decimal.TryParse(txtOtherPaymentDollar.Text, out discountValue) || discountValue < 0 || discountValue > bill)
                {
                    MessageBox.Show("DiscountValue must be a number and less Bill.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtOtherPayment.Focus();
                    return false;
                }
            }

            payment.PaymentID = int.Parse(txtPaymentId.Text);
            payment.StudentClassTeacherID = sctID;
            payment.PayDate = dtPaymentNew.Value;

            payment.Duration = duration;
            payment.StartDate = dtStartPaymentNew.Value;
            payment.EndDate = dtEndPaymentNew.Value;

            payment.Bill = bill;
            payment.Amount = amount;
            payment.Discount = discount;
            payment.DiscountValue = discountValue;
            

            return true;
        }


        private void loadDataStuGraFromMain(StudentClassTeacher sct)
        {
            txtStudentId.Text = sct.StudentID.ToString("D3");
            txtSudentName.Text = sct.Student.NameEng;
            picStudentPayment.ImageLocation = "Data/Student/" + sct.Student.Photo;

            //Class grade = student.findGrade(mgmGrade.Grades);
            txtGradeId.Text = sct.ClassTeacher.ClassID.ToString("D3");
            txtGradeName.Text = sct.ClassTeacher.Class.Name;
            txtGradePrice.Text = sct.ClassTeacher.Class.Price.ToString();
        }
        private void loadDataPayFromMain(Payment payment)
        {
            txtPaymentId.Text = payment.PaymentID.ToString();

            dtPaymentNew.Value = payment.PayDate;
            txtDurationNew.Text = payment.Duration.ToString();
            dtStartPaymentNew.Value = payment.StartDate;
            dtEndPaymentNew.Value = payment.EndDate;

            txtBillPayment.Text = payment.Bill.ToString();
            txtOtherPayment.Text = payment.Discount.ToString();
            txtOtherPaymentDollar.Text = payment.DiscountValue.ToString();
            txtAmountPayment.Text = payment.Amount.ToString();
        }
        #endregion

        #region option
        public void showInfor()
        {
            btnYes.Visible = false;
            btnNo.Visible = false;
            btnOk.Visible = true;
            btnOk.Click += (s, e) => this.Close();
        }
        #endregion

        #region validation
        private void doChange_Start_End_Date(StudentClassTeacher sct)
        {
            // Default values
            int duration = 0;
            decimal discountPercent = 0;
            decimal discountDollar = 0;
            decimal bill;
            decimal amount;

            // Validate Duration
            bool isDurationValid = int.TryParse(txtDurationNew.Text, out duration);
            if (!isDurationValid || duration <= 0 || duration > 1200) // Prevent out-of-range (e.g., 100 years)
            {
                duration = 0;
                txtDurationNew.Text = "";
                dtEndPaymentNew.Value = dtStartPaymentNew.Value; // Reset to start
                txtBillPayment.Text = "0.00";
                txtAmountPayment.Text = "0.00";
                return;
            }

            try
            {
                // Set End Date safely
                dtEndPaymentNew.Value = dtStartPaymentNew.Value.AddMonths(duration);
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Invalid duration. Please enter a smaller number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Calculate Bill (Make sure grade.Price is defined)
            if (sct.ClassTeacher.Class.Price == null || sct.ClassTeacher.Class.Price <= 0)
            {
                MessageBox.Show("Invalid grade price. Cannot calculate payment.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtBillPayment.Text = "0.00";
                txtAmountPayment.Text = "0.00";
                return;
            }

            bill = sct.ClassTeacher.Class.Price * duration;
            txtBillPayment.Text = bill.ToString("0.00");

            // Validate Discount (as percentage)
            if (!decimal.TryParse(txtOtherPayment.Text, out discountPercent) || discountPercent < 0 || discountPercent > 100)
            {
                discountPercent = 0;
                txtOtherPayment.Text = "";
            }       
            // Validate Discount (as dollar)
            
            if (!decimal.TryParse(txtOtherPaymentDollar.Text, out discountDollar) || discountDollar < 0 || discountDollar > bill)
            {
                discountDollar = 0;
                txtOtherPaymentDollar.Text = "";
            }

            // Final Amount
            amount = bill - (bill * discountPercent / 100) - discountDollar;
            txtAmountPayment.Text = amount.ToString("0.00");
        }

        private void txtDurationNew_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar);
        }

        private void txtOthrePayment_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.';
        }

        private void FormUpdatePayment_Load(object sender, EventArgs e)
        {
            this.BeginInvoke(new Action(() => txtDurationNew.Focus()));

        }



        public void doReadOnly()
        {
            SetControlsReadOnly(this);
            SetTabStopFalseRecursive(this);
            SetTabIndexZeroRecursive(this);
            btnOk.TabIndex = 0;
            btnOk.TabStop = true;
            btnOk.Focus();

        }

        private void SetControlsReadOnly(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                switch (control)
                {
                    case TextBox tb:
                        tb.ReadOnly = true;
                        tb.BorderStyle = BorderStyle.None;
                        tb.BackColor = this.BackColor;
                        break;

                    case ComboBox cb:
                        cb.Enabled = false;
                        break;

                    case DateTimePicker dtp:
                        dtp.Enabled = false;
                        break;

                    case Button btn:
                        // leave enabled or disable here if needed
                        break;

                    case Label lbl:
                        // do nothing
                        break;

                    default:
                        control.Enabled = false;
                        break;
                }

                // Recurse if control has children
                if (control.HasChildren)
                {
                    SetControlsReadOnly(control);
                }
            }
        }

        private void SetTabStopFalseRecursive(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                control.TabStop = false;

                if (control.HasChildren)
                {
                    SetTabStopFalseRecursive(control);
                }
            }

        }
        private void SetTabIndexZeroRecursive(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                control.TabIndex = 0;

                if (control.HasChildren)
                {
                    SetTabIndexZeroRecursive(control);
                }
            }
        }


        #endregion

    }
}
