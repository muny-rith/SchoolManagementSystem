using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolManagementSystem
{
    public partial class formSelectStudent : Form
    {
        //private mgmPayment payments;
        //private mgmStudent mgmStudent = new mgmStudent();
        //private mgmClass grades;
        mgmStudent mgmStudent = new mgmStudent();
        private Student student = new Student();
        public DateTime StartDate { get; set; } = new DateTime();
        public Student S { get { return student; } set { student = value; } }
        DatabaseConnection con = DatabaseConnection.Instance;
        public formSelectStudent()
        {
            InitializeComponent();

            mgmStudent.loadDataFromDB(con.GetConnection(), null);
            doViewDgv(mgmStudent.Students);

            btnContinue.Click += doContnue;
            btnDetail.Click += doShowInformationStudent;
            txtFindStudent.TextChanged += doFindstudents;
        }

        private void doFindstudents(object? sender, EventArgs e)
        {
            if (txtFindStudent.Text == string.Empty)
            {
                doViewDgv(mgmStudent.Students);
            }
            else
                doViewDgv(mgmStudent.Students
                            .Where(s =>
                            (string.IsNullOrEmpty(txtFindStudent.Text) ||
                            s.NameEng.ToLower().Contains(txtFindStudent.Text) ||
                             s.NameKh.ToLower().Contains(txtFindStudent.Text))
                        )
                        .ToList());
        }

        private void doShowInformationStudent(object? sender, EventArgs e)
        {
            if (dgvView.CurrentRow != null && dgvView.CurrentRow.Tag is Student selected)
            {
                var form = new FormUpdateStudent(selected);
                form.showInfor();
                form.doReadOnly();
                form.Show();
            }
            else
            {
                MessageBox.Show("Please select a valid student row.", "Invalid Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void doViewDgv(List<Student> listStudents)
        {
            dgvView.Rows.Clear();
            foreach (Student stu in listStudents)
            {
                int i = dgvView.Rows.Add(stu.StudentID, stu.NameEng,stu.NameKh , stu.Sex);
                dgvView.Rows[i].Tag = stu;
                stu.Tag = dgvView.Rows[i];
            }
        }
        public FormUpdatePayment formUpdatePayment;

        private void doContnue(object? sender, EventArgs e)
        {
            if (dgvView.CurrentRow != null && dgvView.CurrentRow.Tag is Student selected)
            {
                StartDate = dtStartDate.Value;
                S = selected;
                DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Please select a valid student row.", "Invalid Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

    }
}
