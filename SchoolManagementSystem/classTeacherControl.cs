using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;
using Org.BouncyCastle.Asn1.Cmp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace SchoolManagementSystem
{
    public partial class classTeacherControl : baseUserControl, IStrategyUserControl<ClassTeacher>
    {
        mgmClassTeacher mgmClassTeacher = new mgmClassTeacher();
        DatabaseConnection conn = DatabaseConnection.Instance;
        PaginationMgm<ClassTeacher> paginator;
        public classTeacherControl()
        {
            mgmClassTeacher.loadDataFromDB(conn.GetConnection());
            InitializeComponent();
            this.dgv.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn
                {
                    Name = "colNo",
                    HeaderText = "ID",
                    Width = 50,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                    ReadOnly = true
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "colClassName",
                    HeaderText = "Class",
                    Width = 50,
                    ReadOnly = true
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "colClassOther",
                    HeaderText = "Other",
                    Width = 50,
                    ReadOnly = true
                },

                new DataGridViewTextBoxColumn
                {
                    Name = "colTeacherNameEng",
                    HeaderText = "Teacher(Eng)",
                    Width = 50,
                    ReadOnly = true
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "colTeacherNameKh",
                    HeaderText = "Teacher(Kh)",
                    Width = 50,
                    ReadOnly = true
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "colTeacherSex",
                    HeaderText = "Sex",
                    Width = 150,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                    ReadOnly = true
                },

            });
            Title = "Teacher";
            ListTilte = "List Class & Teacher";
            txtInsert = "Insert";

            txtSearch.TextChanged += (s, e) => search(txtSearch.Text);
            btnInformation.Visible = false;
            //btnInformation.Click += (s, e) => { if (dgv.CurrentRow != null) information(); };
            btnInsert.Click += (s, e) => { insert(); };
            btnUpdate.Click += (s, e) => { if (dgv.CurrentRow != null) update(); };
            //btnDalete.Click += (s, e) => { if (dgv.CurrentRow != null) delete(); };
            btnDelete.Visible = false;



            paginator = new PaginationMgm<ClassTeacher>(mgmClassTeacher.classTeachers, viewDgv);
            paginator.LoadPage();
            btnNext.Click += (s, e) => paginator.NextPage();
            btnPre.Click += (s, e) => paginator.PreviousPage();

            LB1 = "Total:";
            LB2 = mgmClassTeacher.classTeachers.Count.ToString("D2");
            LB3 = "Male";
            LB4 = mgmClassTeacher.classTeachers.Where(e => e.Teacher.Sex.Contains("male", StringComparison.OrdinalIgnoreCase)).Count().ToString("D2");
            LB5 = "Female";
            LB6 = mgmClassTeacher.classTeachers.Where(e => e.Teacher.Sex.Contains("fe", StringComparison.OrdinalIgnoreCase)).Count().ToString("D2");

            btnExport.Visible = false;
            btnFilter.Visible = false;

            dgv.MouseWheel += (s, e) =>
            {
                if (e.Delta < 0)
                    paginator.NextPage();
                else if (e.Delta > 0)
                    paginator.PreviousPage();

                // Delay to avoid rapid paging (adjust as needed)
                Task.Delay(200);
            };
        }
        public void viewDgv(List<ClassTeacher> pageData)
        {
            dgv.Rows.Clear();
            int i = (paginator.CurrentPage-1)*15 +1;
            foreach (ClassTeacher ct in pageData)
            {
                int index = dgv.Rows.Add(i, ct.Class.Name, ct.Class.Other, ct.Teacher.NameEng, ct.Teacher.NameKh, ct.Teacher.Sex);
                dgv.Rows[index].Tag = ct;
                ct.Tag = dgv.Rows[index];
                i++;
            }
            lbCurrentPage.Text = paginator.CurrentPage.ToString("D2");
            btnPre.Enabled = paginator.CurrentPage > 1;
            btnNext.Enabled = paginator.CurrentPage < paginator.TotalPages;
        }
        public void information()
        {
            formUpdateTeacher form = new formUpdateTeacher(dgv.CurrentRow.Tag as Teacher);
            form.showInfo();
            form.Show();
        }
        public void insert()
        {
            FormSelectClassTeacher form = new FormSelectClassTeacher(0,0,0,0);
            form.Show();
            form.onSet += (s, e) =>
            {
                int clsID = form.ClassID;
                int teaId = form.TeacherID;

                string q = $@"
                    INSERT INTO {Constants.tbClassTeacher} (ClassID, TeacherID)
                    VALUES (@ClassID, @TeacherID);
                ";

                using SqlCommand cmd = new SqlCommand(q, conn.GetConnection());
                cmd.Parameters.AddWithValue("@ClassID", clsID);
                cmd.Parameters.AddWithValue("@TeacherID", teaId);
                cmd.ExecuteNonQuery();

                mgmClassTeacher.loadDataFromDB(conn.GetConnection());
                paginator.AllData = mgmClassTeacher.classTeachers;

            };
        }

        public void update()
        {
            int clsID = (dgv.CurrentRow.Tag as ClassTeacher).ClassID;
            int teaID = (dgv.CurrentRow.Tag as ClassTeacher).TeacherID;
            FormSelectClassTeacher form = new FormSelectClassTeacher(0,(dgv.CurrentRow.Tag as ClassTeacher).ClassTeacherID, clsID,teaID);
            form.Show();
            form.onSet += (s, e) =>
            {
                int clsID = form.ClassID;
                int teaId = form.TeacherID;
                int ctID = form.ClassTeacherID;

                string q = $@"
                    UPDATE {Constants.tbClassTeacher}
                    SET TeacherID = @TeacherID, ClassID = @ClassID
                    WHERE ClassTeacherID = @ClassTeacherID;
                ";

                using SqlCommand cmd = new SqlCommand(q, conn.GetConnection());
                cmd.Parameters.AddWithValue("@ClassID", clsID);
                cmd.Parameters.AddWithValue("@TeacherID", teaId);
                cmd.Parameters.AddWithValue("@ClassTeacherID",ctID);
                cmd.ExecuteNonQuery();
                mgmClassTeacher.loadDataFromDB(conn.GetConnection());
                paginator.AllData = mgmClassTeacher.classTeachers;
            };
           

        }
        public void delete()
        {
            //int clsID = (dgv.CurrentRow.Tag as ClassTeacher).ClassID;
            //FormSelectClassTeacher form = new FormSelectClassTeacher(clsID, 0);
            //form.Show();
            //form.onSet += (s, e) =>
            //{
            //    int clsID = form.ClassID;
            //    int teaId = form.TeacherID;

            //    string q = $@"
            //        UPDATE {Constants.tbClassTeacher}
            //        SET IsActive =0;
            //        WHERE ClassID = @ClassID;
            //    ";

            //    using SqlCommand cmd = new SqlCommand(q, conn.GetConnection());
            //    cmd.Parameters.AddWithValue("@ClassID", clsID);
            //    cmd.Parameters.AddWithValue("@TeacherID", teaId);
            //    cmd.ExecuteNonQuery();
            //    mgmClassTeacher.loadDataFromDB(conn.GetConnection());
            //    paginator.AllData = mgmClassTeacher.classTeachers;
            //};
        }

        public void search(string data)
        {
            var matches = mgmClassTeacher.classTeachers
            .Where(t => t.Teacher.NameEng.Contains(data, StringComparison.OrdinalIgnoreCase) || t.Teacher.NameKh.Contains(data, StringComparison.OrdinalIgnoreCase))
            .ToList();

            paginator.AllData = matches;
        }
    }
}
