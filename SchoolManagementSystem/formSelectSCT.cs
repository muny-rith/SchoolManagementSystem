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
    public partial class formSelectSCT : Form
    {
        //private mgmPayment payments;
        //private mgmStudent mgmStudent = new mgmStudent();
        //private mgmClass grades;
        mgmStudentClassTeacher mgmStudentClassTeacher = new mgmStudentClassTeacher();
        private StudentClassTeacher student = new StudentClassTeacher();
        public StudentClassTeacher SCT { get { return student; } set { student = value; } }
        DatabaseConnection con = DatabaseConnection.Instance;
        public formSelectSCT()
        {
            InitializeComponent();

            mgmStudentClassTeacher.loadDataFromDB(con.GetConnection());
            doViewDgv(mgmStudentClassTeacher.studentClassTeachers);

            btnContinue.Click += doContnue;
            btnDetail.Click += doShowInformationStudent;
            txtFindStudent.TextChanged += doFindstudents;
        }

        private void doFindstudents(object? sender, EventArgs e)
        {
            if(txtFindStudent.Text == string.Empty)
            {
                doViewDgv(mgmStudentClassTeacher.studentClassTeachers);
            }
            else
                doViewDgv(mgmStudentClassTeacher.studentClassTeachers
                            .Where(sct =>
                            (string.IsNullOrEmpty(txtFindStudent.Text) ||
                            sct.Student.NameEng.ToLower().Contains(txtFindStudent.Text) ||
                             sct.Student.NameKh.ToLower().Contains(txtFindStudent.Text))
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

        private void doViewDgv(List<StudentClassTeacher> listStudents)
        {
            dgvView.Rows.Clear();
            foreach(StudentClassTeacher stu in listStudents)
            {
                int i = dgvView.Rows.Add(stu.StudentID,stu.Student.NameEng,stu.ClassTeacher.Class.Name);
                dgvView.Rows[i].Tag = stu;
                stu.Tag = dgvView.Rows[i];
            }
        }
        public FormUpdatePayment formUpdatePayment;

        private void doContnue(object? sender, EventArgs e)
        {
            if (dgvView.CurrentRow != null && dgvView.CurrentRow.Tag is StudentClassTeacher selected) {
                SCT = selected;
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
