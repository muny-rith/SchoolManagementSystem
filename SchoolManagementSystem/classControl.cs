using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolManagementSystem
{
    public partial class classControl : baseUserControl, IStrategyUserControl<Class>
    {
        PaginationMgm<Class> paginator;
        mgmClass mgmClass = new mgmClass();
        mgmLanguage mgmLanguage = new mgmLanguage();
        DatabaseConnection con = DatabaseConnection.Instance;
        public classControl()
        {
            mgmClass.loadDataFromDB(con.GetConnection());
            InitializeComponent();
            this.Title = "Class";
            ListTilte = "List Classes";
            this.txtInsert = "Add New";
            this.btnFilter.Visible = false;
            this.btnExport.Visible = false;
            this.dgv.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn
                {
                    Name = "colClassId",
                    HeaderText = "ID",
                    FillWeight = 49.19786F,
                    MinimumWidth = 6,
                    ReadOnly = true
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "colClassName",
                    HeaderText = "Class Name",
                    FillWeight = 116.934036F,
                    MinimumWidth = 6,
                    ReadOnly = true
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "colClassType",
                    HeaderText = "Class Type",
                    FillWeight = 116.934036F,
                    MinimumWidth = 6,
                    ReadOnly = true
                }, 
                new DataGridViewTextBoxColumn
                {
                    Name = "colClassOther",
                    HeaderText = "Other",
                    FillWeight = 116.934036F,
                    MinimumWidth = 6,
                    ReadOnly = true
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "colClassPrice",
                    HeaderText = "Price",
                    FillWeight = 116.934036F,
                    MinimumWidth = 6,
                    ReadOnly = true
                },

            });

            paginator = new PaginationMgm<Class>(mgmClass.Classes,viewDgv);
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

            LB2 = mgmClass.Classes.Count().ToString("D2");
            LB4 = mgmClass.Classes.Where(e => e.Language.Title == "English").ToList().Count().ToString("D2");
            LB6 = mgmClass.Classes.Where((e) => e.Language.Title == "Chinese").Count().ToString("D2");

            txtSearch.TextChanged += (s, e) => search(txtSearch.Text);
            btnInformation.Click += (s, e) => information();
            btnInsert.Click += (s, e) => insert();
            btnUpdate.Click  += (s, e) => update();
            btnDelete.Click += (s, e) => delete();


        }
        //public void viewDgv() { Console.Write(""); }
        public void viewDgv(List<Class> pageData)
        {
            dgv.Rows.Clear();
            int i = ((paginator.CurrentPage)-1)*15 +1;
            foreach (var c in pageData)
            {
                int index = dgv.Rows.Add(i, c.Name, c.Language.Title, c.Other, c.Price);
                // ទំនាក់ទំនង Row និង Student
                dgv.Rows[index].Tag = c; //ស្គាល់Row គេដឹងយកGradeមួយណាមកបង្ហាញ
                c.Tag = dgv.Rows[index]; //ស្គាល់Student គេដឹងយកRowមួយណា
                i++;
            }
            lbCurrentPage.Text = paginator.CurrentPage.ToString("D2");
            btnPre.Enabled = paginator.CurrentPage > 1;
            btnNext.Enabled = paginator.CurrentPage < paginator.TotalPages;
        }
        public void information()
        {
            if (dgv.CurrentRow?.Tag is Class selected)
            {
                var form = new FormUpadateClass(selected);
                form.showInfor();
                form.doReadOnly();
                form.Show();
            }
        }
        public void insert()
        {
            // Validate BEFORE creating the form
            mgmLanguage.loadDataFromDB(con.GetConnection());
            if (mgmLanguage.languages.Count == 0)
            {
                MessageBox.Show("Please insert Language first!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Class newClass = null;

            using (var form = new FormUpadateClass(null))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    newClass = form.gradeData;
                }
            }

            if (newClass == null)
                return;

            try
            {
                string query = $@"
            INSERT INTO {Constants.tbClass} (Name, LanguageID, Price, Other)
            VALUES (@Name, @LanguageID, @Price, @Other);
        ";

                using (SqlCommand cmd = new SqlCommand(query, con.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@Name", newClass.Name);
                    cmd.Parameters.AddWithValue("@LanguageID", newClass.Language.LanguageID);
                    cmd.Parameters.AddWithValue("@Price", newClass.Price);
                    cmd.Parameters.AddWithValue("@Other", newClass.Other ?? string.Empty);
                    cmd.ExecuteNonQuery();
                }

                mgmClass.addGrade(newClass);
                paginator.LoadPage(paginator.TotalPages);
                updateFile(mgmClass, Constants.GradeFile);
            }
            finally
            {
                con.CloseConnection();
            }
        }
        public void update()
        {
            if(dgv.CurrentRow?.Tag is Class selected)
            {
                var form = new FormUpadateClass(selected);
                if(form.ShowDialog() == DialogResult.OK)
                {
                    Class cls = form.gradeData;

                    string query = $@"
                            UPDATE {Constants.tbClass} 
                            SET Name = @Name, LanguageID = @LanguageID, Price = @Price,
                                Other = @Other
                            WHERE ClassID = @ClassID
                        ";

                    using (SqlCommand cmd = new SqlCommand(query, con.GetConnection()))
                    {
                        cmd.Parameters.AddWithValue("@Name", cls.Name);
                        cmd.Parameters.AddWithValue("@LanguageID", cls.LanguageID);
                        cmd.Parameters.AddWithValue("@Price", cls.Price);
                        cmd.Parameters.AddWithValue("@ClassID", cls.ClassID);
                        cmd.Parameters.AddWithValue("@Other", cls.Other);

                        cmd.ExecuteNonQuery();
                    }

                    con.CloseConnection();

                    mgmClass.updateGrade(cls);
                    updateFile(mgmClass,Constants.GradeFile);
                    paginator.LoadPage(paginator.CurrentPage);
                }
            }
        }
        public void delete()
        {
            if (dgv.CurrentRow?.Tag is Class selected)
            {
                var form = new FormUpadateClass(selected);
                form.doReadOnly();
                if(form.ShowDialog() == DialogResult.OK)
                {
                    Class cls = form.gradeData;

                    string query = $"Update {Constants.tbClass} set IsActive = 0 WHERE ClassID='{cls.ClassID}'";
                    SqlCommand cmd = new SqlCommand(query, con.GetConnection());
                    cmd.ExecuteNonQuery();
                    con.CloseConnection();

                    mgmClass.deleteGrade(cls);

                    updateFile(mgmClass,Constants.GradeFile);
                    paginator.LoadPage(paginator.CurrentPage);
                }
            }
        }
        public void search(string data)
        {
            paginator.AllData = mgmClass.searchGrades(data);
            //paginator.LoadPage(1);
        }
        #region method do on form
        private void updateFile(mgmClass grades, string fileName)
        {
            string[] lines = new string[grades.Classes.Count];
            for (int i = 0; i < grades.Classes.Count; i++)
            {
                lines[i] = grades.Classes[i].getInfoForFile;
            }
            File.Delete(fileName);
            File.WriteAllLines(fileName, lines);
        }
        #endregion

    }
}
