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
    public partial class teacherControl : baseUserControl,IStrategyUserControl<Teacher>
    {
        mgmTeacher mgmTeacher = new mgmTeacher();
        DatabaseConnection conn = DatabaseConnection.Instance;
        PaginationMgm<Teacher> paginator;
        public teacherControl()
        {
            mgmTeacher.loadDataFromDB(conn.GetConnection());
            InitializeComponent();
            this.dgv.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn
                {
                    Name = "colTeacherId",
                    HeaderText = "ID",
                    Width = 50,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                    ReadOnly = true
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "colTeacherNameEng",
                    HeaderText = "NameEng",
                    Width = 50,
                    ReadOnly = true
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "colTeacherNameKh",
                    HeaderText = "NameKh",
                    Width = 50,
                    ReadOnly = true
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "colTeacherSex",
                    HeaderText = "Sex",
                    Width = 100,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                    ReadOnly = true
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "colTeacherAddress",
                    HeaderText = "Address",
                    Width = 50,
                    ReadOnly = true
                }
            });
            Title = "Teacher";
            ListTilte = "List Teacher";
            txtInsert = "Insert";

            txtSearch.TextChanged += (s, e) => search(txtSearch.Text);
            btnInformation.Click += (s, e) => { if (dgv.CurrentRow != null) information(); };
            btnInsert.Click += (s,e) => { insert(); };
            btnUpdate.Click += (s,e) => { if (dgv.CurrentRow != null) update(); };
            btnDelete.Click += (s,e) => { if (dgv.CurrentRow != null) delete(); };


            paginator = new PaginationMgm<Teacher>(mgmTeacher.teachers,viewDgv);
            paginator.LoadPage();
            btnNext.Click += (s, e) => paginator.NextPage();
            btnPre.Click += (s, e) => paginator.PreviousPage();

            LB1 = "Total:";
            LB2 = mgmTeacher.teachers.Count.ToString("D2");
            LB3 = "Male";
            LB4 = mgmTeacher.teachers.Where(e => e.Sex.Contains("male", StringComparison.OrdinalIgnoreCase)).Count().ToString("D2");
            LB5 = "Female";
            LB6 = mgmTeacher.teachers.Where(e => e.Sex.Contains("fe", StringComparison.OrdinalIgnoreCase)).Count().ToString("D2");

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
        public void viewDgv(List<Teacher> pageData)
        {
            dgv.Rows.Clear();
            int i = (paginator.CurrentPage - 1) * 15 + 1;

            foreach (Teacher t in pageData)
            {
                int index = dgv.Rows.Add(i,t.NameEng,t.NameKh,t.Sex,t.Address);
                dgv.Rows[index].Tag = t;
                t.Tag = dgv.Rows[index];
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
            formUpdateTeacher form = new formUpdateTeacher();
            if(form.ShowDialog() == DialogResult.OK)
            {
                Teacher teacher = new Teacher();
                teacher = form.teacherData;

                string sourcePath = teacher.Photo;
                //MessageBox.Show(sourcePath);


                string insertQuery = @"
                        INSERT INTO Teacher (NameEng, NameKh, Sex, Address, Phone, Photo)
                        VALUES (@NameEng, @NameKh,@Sex,@Address,@Phone, @Photo);
                        SELECT SCOPE_IDENTITY();";

                using (SqlCommand cmd = new SqlCommand(insertQuery, conn.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@NameEng", teacher.NameEng);
                    cmd.Parameters.AddWithValue("@NameKh", teacher.NameKh);
                    cmd.Parameters.AddWithValue("@Sex",teacher.Sex);
                    cmd.Parameters.AddWithValue("@Address",teacher.Address);
                    cmd.Parameters.AddWithValue("@Phone",teacher.Phone);

                    // temporary photo name (you will fix it after getting ID)
                    cmd.Parameters.AddWithValue("@Photo", "temp.jpg");

                    // Execute and get the new ID
                    teacher.TeacherID = Convert.ToInt32(cmd.ExecuteScalar());

                    // Generate photo filename like: 123_JohnDoe.jpg
                    string photoFileName = $"{teacher.TeacherID}_{teacher.NameEng}.jpg";

                    // Update Teacher record with new photo name
                    string updatePhotoQuery = "UPDATE Teacher SET Photo = @Photo WHERE TeacherID = @TeacherID";
                    using (SqlCommand updateCmd = new SqlCommand(updatePhotoQuery, conn.GetConnection()))
                    {
                        updateCmd.Parameters.AddWithValue("@Photo", photoFileName);
                        updateCmd.Parameters.AddWithValue("@TeacherID", teacher.TeacherID);
                        updateCmd.ExecuteNonQuery();
                    }

                }
                //copy photo
                string safeName = teacher.NameEng.Replace(" ", "_"); // "Sok_Dara"
                string fileName = $"{teacher.TeacherID}_{safeName}.jpg";
                teacher.Photo = fileName;

                string folderPath = Path.Combine(Application.StartupPath, "Data", "Teacher");
                Directory.CreateDirectory(folderPath); // safe even if already exists

                string destinationPath = Path.Combine("Data/Teacher/", fileName);
                File.Copy(sourcePath, destinationPath, true);

                mgmTeacher.teachers.Add(teacher);
                paginator.LoadPage();

            }
        }
        public void update() 
        {
            if(dgv.CurrentRow?.Tag is Teacher selected)
            {
                formUpdateTeacher form = new formUpdateTeacher(selected);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    Teacher teacher = form.teacherData;
                    string currentFilename = Path.GetFileName(teacher.Photo); // removes path if any

                    if (!string.Equals(currentFilename?.Trim(), selected.Photo?.Trim(), StringComparison.OrdinalIgnoreCase))
                    {
                        // File has changed
                        string sourcePath = teacher.Photo;
                        string safeName = teacher.NameEng.Replace(" ", "_");
                        string fileName = $"{teacher.TeacherID}_{safeName}.jpg";
                        teacher.Photo = fileName;

                        string folderPath = Path.Combine(Application.StartupPath, "Data", "Teacher");
                        Directory.CreateDirectory(folderPath);

                        string destinationPath = Path.Combine(folderPath, fileName);
                        File.Copy(sourcePath, destinationPath, true);
                    }
                    else teacher.Photo = currentFilename;

                    string query = $@"
                                UPDATE {Constants.tbTeacher} SET
                                NameEng = @NameEng,
                                NameKh = @NameKh,
                                Sex = @Sex,
                                Address = @Address,
                                Phone = @Phone,
                                Photo = @Photo
                            WHERE TeacherID = @TeacherID;
                    ";
                    SqlCommand cmd = new SqlCommand(query,conn.GetConnection());
                    cmd.Parameters.AddWithValue("@NameEng",teacher.NameEng);
                    cmd.Parameters.AddWithValue("@Namekh", teacher.NameKh);
                    cmd.Parameters.AddWithValue("@Sex", teacher.Sex);
                    cmd.Parameters.AddWithValue("@Address", teacher.Address);
                    cmd.Parameters.AddWithValue("@Phone", teacher.Phone);
                    cmd.Parameters.AddWithValue("@Photo", teacher.Photo);
                    cmd.Parameters.AddWithValue("@TeacherID",teacher.TeacherID);
                    cmd.ExecuteNonQuery();
                    conn.CloseConnection();
                    int index = mgmTeacher.teachers.FindIndex(t => t.TeacherID == teacher.TeacherID);
                    if (index != -1)
                    {
                        mgmTeacher.teachers[index] = teacher;
                    }
                    paginator.LoadPage();
                }
            }

        }
        public void delete()
        {
            if(dgv.CurrentRow?.Tag is Teacher selected)
            {
                formUpdateTeacher form = new formUpdateTeacher(selected);
                if(form.DialogResult == DialogResult.OK)
                {
                    Teacher teacher = form.teacherData;
                    string query = $@"
                                UPDATE {Constants.tbTeacher} SET
                                IsActive = 0,
                            WHERE TeacherID = @TeacherID;
                    ";
                    SqlCommand cmd = new SqlCommand(query, conn.GetConnection());
                    cmd.ExecuteNonQuery();
                    conn.CloseConnection();
                    mgmTeacher.teachers.Remove(teacher);
                    paginator.LoadPage();
                }
            }
        }

        public void search(string data)
        {
            var matches = mgmTeacher.teachers
            .Where(t => t.NameEng.Contains(data, StringComparison.OrdinalIgnoreCase) || t.NameKh.Contains(data, StringComparison.OrdinalIgnoreCase))
            .ToList();
            
            paginator.AllData = matches;
        }
    }
}
