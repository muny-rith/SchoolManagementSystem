using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static QuestPDF.Helpers.Colors;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace SchoolManagementSystem
{
    public partial class FormUpdatePayroll : Form
    {
        public int TeacherID { get; set; } = 0;

        public decimal Rate { get; set; } = 0;
        public decimal BaseSalary { get; set; } = 0;

        mgmTeacher mgmTeacher = new mgmTeacher();
        mgmPayroll mgmPayrollTeacher = new mgmPayroll();

        public event EventHandler onSet;
        DatabaseConnection conn = DatabaseConnection.Instance;


        string query;


        Dictionary<string, object> parameters = new Dictionary<string, object>();


        public FormUpdatePayroll(int teaID = 0)
        {
            InitializeComponent();
            if (teaID == 0) // insert PayrollTeacher
            {
                mgmTeacher.loadDataFromDB(conn.GetConnection());
                cbSelectTeacher.Items.Clear();

                if (mgmTeacher.teachers.Count == 0)
                {
                    MessageBox.Show("No teachers available. Please add teachers first.",
                        "No Data",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    this.DialogResult = DialogResult.Cancel;
                    this.Load += (s, e) => this.Close();
                    return;
                }

                foreach (Teacher t in mgmTeacher.teachers)
                {
                    cbSelectTeacher.Items.Add(new Item { ID = t.TeacherID, Name = t.NameEng });
                }
                txtBaseSalary.Text = "0";
                txtRatePayroll.Text = "0";
            }
            else
            {
                TeacherID = teaID;
                query = @"
                SELECT
                       pt.TeacherID, 
                       pt.Rate,
                       pt.BaseSalary,
                       t.NameEng,
                       t.NameKh
                FROM Payroll pt
                INNER JOIN Teacher t
                    ON pt.TeacherID = t.TeacherID
                WHERE pt.TeacherID = @TeacherID
            ";
                parameters.Add("@TeacherID", TeacherID);
                mgmPayrollTeacher.loadDataFromDB(conn.GetConnection(), query, parameters);
                cbSelectTeacher.Items.Clear();

                if (mgmPayrollTeacher.payrollTeachers.Count == 0)
                {
                    MessageBox.Show("Teacher data not found.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.Cancel;
                    this.Load += (s, e) => this.Close();
                    return;
                }

                foreach (Payroll t in mgmPayrollTeacher.payrollTeachers)
                {
                    cbSelectTeacher.Items.Add(new Item { ID = t.TeacherID, Name = t.Teacher.NameEng });
                }
                txtBaseSalary.Text = mgmPayrollTeacher.payrollTeachers.First().BaseSalary.ToString();
                txtRatePayroll.Text = mgmPayrollTeacher.payrollTeachers.First().Rate.ToString();
            }

            // Only set SelectedIndex if there are items
            if (cbSelectTeacher.Items.Count > 0)
            {
                cbSelectTeacher.SelectedIndex = 0;
            }

            btnYes.Click += (s, e) => doSet();
            btnNo.Click += (s, e) => this.Close();
        }
        public class Item
        {
            public int ID { get; set; }
            public string Name { get; set; }

            public override string ToString()
            {
                return Name; // This will be shown in the ComboBox
            }
        }

        private void doSet()
        {
            if (cbSelectTeacher.SelectedItem != null && txtRatePayroll.Text.Length > 0)
            {

                if (cbSelectTeacher.SelectedItem is Item selectedTeacher)
                {
                    TeacherID = selectedTeacher.ID;

                }
                if(decimal.TryParse(txtRatePayroll.Text, out decimal result) && decimal.TryParse(txtBaseSalary.Text,out decimal baseSalary))
                {
                    Rate = result;
                    BaseSalary = baseSalary;
                    this.DialogResult = DialogResult.OK;
                    onSet?.Invoke(this, new EventArgs());
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Pls fill correct format!!");
                }
                //TeacherID = Convert.ToInt32(cbSelectTeacher.SelectedValue);

            }
            else
            {
                MessageBox.Show("Pls fill!");
            }
        }
    }
}