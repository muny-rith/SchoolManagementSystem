using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Mysqlx.Notice;
using Org.BouncyCastle.Bcpg.OpenPgp;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Utilities.Encoders;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using ZstdSharp.Unsafe;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace SchoolManagementSystem
{
    public partial class classTeacherStudentsControls: baseUserControl, IStrategyUserControl<StudentClassTeacher>
    {
        DatabaseConnection conn = DatabaseConnection.Instance;
        PaginationMgm<StudentClassTeacher> paginator;
        mgmStudentClassTeacher mgmStudentClassTeacher = new mgmStudentClassTeacher();
        CheckBox headerCheckBox = new CheckBox();
        mgmTeacher mgmTeacher = new mgmTeacher();
        mgmClass mgmClass = new mgmClass();

        Dictionary<string,object?> parameters = new Dictionary<string, object?>();
        public classTeacherStudentsControls()
        {
            mgmStudentClassTeacher.loadDataFromDB(conn.GetConnection());
            //mgmClass.loadDataFromDB(conn.GetConnection());
            //mgmTeacher.loadDataFromDB(conn.GetConnection());
            
            //if (mgmStudentClassTeacher.studentClassTeachers.Any(e => e.ClassTeacher != null)) { }

            InitializeComponent();
            doDesignDgv();
            addItemToCombo();
            #region add item to cb

            cbClass.SelectedIndexChanged += cbSelectClass_SelectedIndexChanged;


            //foreach(Class cls in mgmClass.Classes)
            //{
            //    cbClass.Items.Add(new Item { ID = cls.ClassID, Name = cls.Name});
            //}
            //cbClass.SelectedIndex = 0;
            //foreach (Teacher t in mgmTeacher.teachers)
            //{
            //    cbTeacher.Items.Add(new Item { ID = t.TeacherID, Name = t.NameEng });
            //}
            //cbTeacher.SelectedIndex = 0;
            #endregion
            #region tes
            headerCheckBox.Size = new Size(20, 20);
            headerCheckBox.BackColor = Color.Transparent;

            dgv.Controls.Add(headerCheckBox);

            dgv.CellPainting += (s, e) =>
            {
                if (e.RowIndex == -1 && e.ColumnIndex == 0)
                {
                    e.PaintBackground(e.ClipBounds, false);

                    //Rectangle rect = dgv.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                    headerCheckBox.Location = new Point(4,2);
                    headerCheckBox.Visible = true;
                }
            };
            headerCheckBox.CheckedChanged += (s, e) =>
            {
                bool check = headerCheckBox.Checked;

                paginator.SelectAll(check);
                paginator.LoadPage();
                //LoadPage(paginator.);
            };
            dgv.CellValueChanged += (s, e) =>
            {
                if (e.ColumnIndex == dgv.Columns["colChk"].Index && e.RowIndex >= 0)
                {
                    dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    // Do something with the changed row
                }
            };
            dgv.CurrentCellDirtyStateChanged += (s, e) =>
            {
                if (dgv.CurrentCell is DataGridViewCheckBoxCell)
                {
                    dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            };


            dgv.CellClick += (s, e) =>
            {
                if (e.RowIndex >= 0 && e.RowIndex < dgv.Rows.Count && e.ColumnIndex ==0)
                {
                    var row = dgv.Rows[e.RowIndex];

                    // Toggle checkbox value
                    DataGridViewCheckBoxCell chkCell = (DataGridViewCheckBoxCell)row.Cells["colChk"];
                    bool isChecked = Convert.ToBoolean(chkCell.Value ?? false);
                    chkCell.Value = !isChecked;

                    // Update the IsSelected property in your data model
                    if (row.Tag is StudentClassTeacher sct)
                    {
                        sct.IsSelected = !isChecked;
                    }
                }
            };

            //dgv.EditMode = DataGridViewEditMode.EditOnEnter;


            #endregion


            Title = "Class & Teahcer";
            ListTilte = $"list Students";
            txtInsert = "Add Student";

            LB1 = "Total:";
            LB2 = mgmStudentClassTeacher.studentClassTeachers.Count().ToString("D2");
            LB3 = "Male:";
            LB4 = mgmStudentClassTeacher.studentClassTeachers.Where(e => e.Student.Sex == "male").Count().ToString("D2");
            LB5 = "Female:";
            LB6 = mgmStudentClassTeacher.studentClassTeachers.Where(e => e.Student.Sex == "female").Count().ToString("D2");

            paginator = new PaginationMgm<StudentClassTeacher>(mgmStudentClassTeacher.studentClassTeachers, viewDgv);
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
            doFilter();

            btnInformation.Visible = false;
            btnFilter.Visible = false;

            btnInsert.Click += (s, e) => insert();
            btnUpdate.Click += (s, e) =>
            {
                if (dgv.CurrentRow?.Tag is StudentClassTeacher sct)
                {
                    sct.IsSelected = true;
                    update(new List<StudentClassTeacher> { sct });
                    return;
                }

                var selectedItems = mgmStudentClassTeacher.studentClassTeachers
                    .Where(e => e.IsSelected)
                    .ToList();

                if (selectedItems.Any())
                {
                    update(selectedItems);
                }
                else
                {
                    MessageBox.Show("Please select at least one item to update.",
                        "No Selection",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            };

            btnDelete.Click += (s, e) =>
            {
                if (dgv.CurrentRow?.Tag is StudentClassTeacher sct)
                {
                    sct.IsSelected = true;
                    delete();
                }
                else
                {
                    var selectedItems = paginator.AllData?.Where(item => item.IsSelected).ToList();

                    if (selectedItems != null && selectedItems.Any())
                    {
                        delete();
                    }
                    else
                    {
                        MessageBox.Show("Please select at least one item to delete.",
                            "No Selection",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                }
            };

            //btnDalete.Click += (s, e) => { if (!paginator.AllData.IsNullOrEmpty() && paginator.AllData.Where(e => e.IsSelected).Count() > 0) || (dgv.CurrentRow is StudentClassTeacher)) delete(); };
            txtSearch.TextChanged += (s, e) => search(txtSearch.Text);

            //ContextMenuStrip menuTypeGrade = new ContextMenuStrip();
            //menuTypeGrade.Items.Add("Khmer", null, MenuItemTypeGrade_Click);
            //menuTypeGrade.Items.Add("English", null, MenuItemTypeGrade_Click);
            //btnFilter.Click += (s, ev) =>
            //{
            //    menuTypeGrade.Show(btnFilter, new Point(0, btnFilter.Height+5));
            //};

            //btnExport.Click += (s, e) => { if (mgmStudentClassTeacher.studentClassTeachers.Where(e => e.ClassTeacher.ClassTeacherID == getClassTeacherID(cbClass.SelectedValue,cbTeacher.SelectedValue) { doPrint(mgmStudent.Students.Where(e => e.GradeName == option).ToList()); } };
            btnExport.Click += (s, e) => doChoiceExport();
            btnExport.Click += (s, e) =>
            {

            };


            cbClass.SelectedIndexChanged += (s, e) => doFilter();
            cbTeacher.SelectedIndexChanged += (s, e) => doFilter();
        }

        private void doDesignDgv()
        {
            this.dgv.Columns.AddRange(new DataGridViewColumn[]
            {
            new DataGridViewCheckBoxColumn
            {
                Name = "colChk",
                HeaderText = "",
                Width = 25,
                ReadOnly = false,
                TrueValue = true,
                FalseValue = false,
                Frozen = false,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,

            },
            //new DataGridViewTextBoxColumn
            //{
            //    Name = "colStuId",
            //    HeaderText = "ID",
            //    Width = 50,
            //    AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
            //    ReadOnly = true
            //},
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
                Name = "colStuClass",
                HeaderText = "Class",
                ReadOnly = true,
                Width = 200
            },
            new DataGridViewTextBoxColumn
            {
                Name = "colStuTeacher",
                HeaderText = "Teacher",
                ReadOnly = true,
                Width = 200
            },
            new DataGridViewTextBoxColumn
            {
                Name = "colStuPhone",
                HeaderText = "Phone",
                ReadOnly = true,
                Width = 200
            },

            });

        }

        private void addItemToCombo(int clsID = 0, int teaID = 0)
        {
            cbClass.Items.Clear();

            var parameters = new Dictionary<string, object>();
            string query;

            cbClass.DisplayMember = "Name";
            cbClass.ValueMember = "ID";
            cbTeacher.DisplayMember = "Name";
            cbTeacher.ValueMember = "ID";
            cbClass.Items.Add(new Item { ID = 0, Name = "All" });
            query = $@"
                        SELECT DISTINCT c.ClassID, c.Name, c.LanguageID, c.Price, l.Title , c.Other
                        FROM {Constants.tbClass} c 
                        JOIN {Constants.tbClassTeacher} ct ON ct.ClassID = c.ClassID 
                        JOIN {Constants.tbLanguage} l ON c.LanguageID = l.LanguageID 
                        ORDER BY l.Title DESC, c.Price
                        ";
            // Load all classes
            mgmClass.loadDataFromDB(conn.GetConnection(),query);

            // Add classes to cbClass
            foreach (Class c in mgmClass.Classes)
            {
                cbClass.Items.Add(new Item { ID = c.ClassID, Name = c.Name+"   "+c.Other });
            }

            // Select class
            if (clsID != 0)
            {
                foreach (Item item in cbClass.Items)
                {
                    if (item.ID == clsID)
                    {
                        cbClass.SelectedItem = item;
                        break;
                    }
                }
            }
            else if (cbClass.Items.Count > 0)
            {
                cbClass.SelectedIndex = 0;
            }

            // Get selected classID
            int selectedClassID = 0;
            if (cbClass.SelectedItem is Item selectedClassItem)
            {
                selectedClassID = selectedClassItem.ID;
                parameters["@ClassID"] = selectedClassID;
            }

            // Load teachers for selected class
            if (parameters.ContainsKey("@ClassID"))
            {
                query = $@"
                SELECT t.*
                FROM {Constants.tbTeacher} t
                JOIN {Constants.tbClassTeacher} ct ON t.TeacherID = ct.TeacherID
                WHERE ct.ClassID = @ClassID";

                mgmTeacher.loadDataFromDB(conn.GetConnection(), query, parameters);
                cbTeacher.Items.Clear();
                cbTeacher.Items.Add(new Item { ID = 0, Name = "All"});
                // Add teachers to cbTeacher
                foreach (Teacher t in mgmTeacher.teachers)
                {
                    cbTeacher.Items.Add(new Item { ID = t.TeacherID, Name = t.NameEng });
                }
            }

            cbTeacher.SelectedIndex = 0;

            //// Select teacher
            //if (teaID != 0)
            //{
            //    foreach (Item item in cbTeacher.Items)
            //    {
            //        if (item.ID == teaID)
            //        {
            //            cbTeacher.SelectedItem = item;
            //            break;
            //        }
            //    }
            //}
            //else if (cbTeacher.Items.Count > 0)
            //{
            //}
        }


        private void doFilter()
        {
            int? classID = (cbClass.SelectedItem is Item classItem) ? classItem.ID : (int?)null;
            //cbTeacher.SelectedIndex = 0;
            int? teacherID = (cbTeacher.SelectedItem is Item teacherItem) ? teacherItem.ID : (int?)null;

            parameters = new Dictionary<string, object?>();

            if (classID.HasValue && classID >= 0)
                parameters["@ClassID"] = classID;

            if (teacherID.HasValue && teacherID >= 0)
                parameters["@TeacherID"] = teacherID;

            // Add Year/Month if needed:
            // parameters["@Year"] = selectedYear;
            // parameters["@Month"] = selectedMonth;

            using (conn.GetConnection())
            {
                paginator.AllData = mgmStudentClassTeacher.loadDataFromDB(conn.GetConnection(),null, parameters);
                LB2 = paginator.AllData.Count().ToString("D2");
                LB4 = paginator.AllData.Where(e => e.Student.Sex == "male").Count().ToString("D2");
                LB6 = paginator.AllData.Where(e => e.Student.Sex == "female").Count().ToString("D2");
                // Bind list to your DataGridView or control
                //dgv.DataSource = list; // or whatever control you use
            }
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


        #region export 

        private string nbsp(int num)
        {
            if (num <= 0) return string.Empty;

            // Use string constructor + Replace for efficiency
            return new string(' ', num).Replace(" ", "&nbsp;");
        }
        string[] op = { "List Student", "Certificate","Award" };

        private void doChoiceExport()
        {
            ContextMenuStrip option = new ContextMenuStrip();

            // Attach handlers using lambdas
            option.Items.Add(op[0], null, (s, e) => doExport(op[0], s, e));
            option.Items.Add(op[1], null, (s, e) => doExport(op[1], s, e));
            option.Items.Add(op[2], null, (s, e) => doExport(op[2], s, e));

            option.Show(btnExport, new Point(0, btnExport.Height + 5));
        }
        private void doExport(string op, object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(op))
            {
                MessageBox.Show("No option selected.");
            }
            else
            {
                List<StudentClassTeacher> selected = mgmStudentClassTeacher.studentClassTeachers.Where(e => e.IsSelected).ToList();
                var itemsToPrint = selected.Any()
                                    ? selected
                                    : (dgv.CurrentRow?.Tag is StudentClassTeacher sct
                                        ? new List<StudentClassTeacher> { sct }
                                        : null);
                switch (op)
                {
                    case "List Student":
                        // Your export logic for student list
                        int classID = 0; int teacherID = 0;
                        if (cbClass.SelectedItem is Item selectedItem && cbTeacher.SelectedItem is Item selectedItemTeacher)
                        {
                            classID = selectedItem.ID;
                            teacherID = selectedItemTeacher.ID;
                            // Get or create the ClassTeacherID from selected class and teacher
                            int selectedClassTeacherID = getClassTeacherID(classID, teacherID);

                            // Get all matching StudentClassTeacher entries
                            var selectedSCTs = mgmStudentClassTeacher.studentClassTeachers
                                .Where(sct => sct.ClassTeacher.ClassTeacherID == selectedClassTeacherID)
                                .ToList();

                            // If found, print associated students
                            if (selectedSCTs.Count > 0)
                            {
                                doPrint(selectedSCTs, this.op[0]); // Your printing method
                            }
                            else
                            {
                                MessageBox.Show("No students found for this class and teacher.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please select Class and Teacher","Warning!",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        }
                    break;
                    case "Certificate":
                        if (itemsToPrint != null)
                        {

                            doPrint(itemsToPrint, this.op[1]);
                        }
                        else
                        {
                            MessageBox.Show("Please select!!");
                        }
                        break;
                    case "Award":
                        if (itemsToPrint != null)
                        {

                            doPrint(itemsToPrint, this.op[2]);
                        }
                        else
                        {
                            MessageBox.Show("Please select!!");
                        }
                        break;
                }
            }
        }
        public string SanitizeFileName(string fileName)
        {
            // Remove invalid characters
            var invalidChars = Path.GetInvalidFileNameChars();
            string sanitized = string.Concat(fileName.Split(invalidChars));

            // Also remove any whitespace characters like newlines, tabs
            sanitized = sanitized.Replace("\n", "").Replace("\r", "").Replace("\t", "");

            return sanitized.Trim();
        }

        private void doPrint(List<StudentClassTeacher> allData,string option)
        {



            if (option == op[0]) // print list
            {
                string folderPath = Path.Combine(System.Windows.Forms.Application.StartupPath, "Data", option);
                Directory.CreateDirectory(folderPath); // Safe: creates only if not exists
                //string safeDate = p.PayDate.ToString("yyyyMMdd");
                string fileName = $"{allData.First().ClassTeacher.Class.Name}_{allData.First().ClassTeacher.Teacher.NameEng}_list.html";
                string fullPath = Path.Combine(folderPath, fileName);

                File.WriteAllText(fullPath, GenerateListHtml(allData));

                Process.Start(new ProcessStartInfo(fullPath)
                {
                    UseShellExecute = true
                });
            }
            else if(option == op[1]) // print certificae
            {
                string folderPath = Path.Combine(System.Windows.Forms.Application.StartupPath, "Data", option, $"{DateTime.Now.ToString("yyyy")}");

                foreach (StudentClassTeacher s in allData)
                {
                    string fileName = $"{s.Student.NameEng}_{s.ClassTeacher.Teacher.NameEng}_{s.ClassTeacher.Class.Name}_Certificate.html";
                    fileName = SanitizeFileName(fileName);

                    //select title's language to cho print Chi or Eng
                    int lgID = s.ClassTeacher.Class.LanguageID;
                    string q = "SELECT Title FROM Language WHERE LanguageID = @LanguageID";
                    string title = string.Empty;
                    using (SqlCommand cmd = new SqlCommand(q, conn.GetConnection()))
                    {
                        cmd.Parameters.AddWithValue("@LanguageID", lgID);
                        var result = cmd.ExecuteScalar();
                        title = result?.ToString() ?? string.Empty;
                    }


                    if (title.ToLower().Contains("chi"))
                    {
                        var folderPathFinal = Path.Combine(folderPath,"Chinese");
                        Directory.CreateDirectory(folderPathFinal); // Safe: creates only if not exists

                        string fullPath = Path.Combine(folderPathFinal, fileName);

                        File.WriteAllText(fullPath, GenerateCertificateChHtml(s));

                        Process.Start(new ProcessStartInfo(fullPath)
                        {
                            UseShellExecute = true
                        });
                    }
                    else if (title.ToLower().Contains("eng"))
                    {
                        var folderPathFinal = Path.Combine(folderPath,"English");
                        Directory.CreateDirectory(folderPathFinal); // Safe: creates only if not exists

                        string fullPath = Path.Combine(folderPathFinal, fileName);

                        File.WriteAllText(fullPath, GenerateCertificateEngHtml(s));

                        Process.Start(new ProcessStartInfo(fullPath)
                        {
                            UseShellExecute = true
                        });
                    }

                }
            }
            //Print award
            else if(option == op[2])
            {
                string folderPath = Path.Combine(System.Windows.Forms.Application.StartupPath, "Data", option,$"{DateTime.Now.ToString("yyyy")}");
                foreach (StudentClassTeacher s in allData)
                {
                    formInputNumber form = new formInputNumber(s.Student.NameEng);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        int number = form.number;
                        // Use the number here
                        //if()
                        s.Tag = number;

                    }

                }
                foreach(StudentClassTeacher s in allData)
                {
                    //select title's language to cho print Chi or Eng
                    int lgID = s.ClassTeacher.Class.LanguageID;
                    string q = "SELECT Title FROM Language WHERE LanguageID = @LanguageID";
                    string title = string.Empty;
                    using (SqlCommand cmd = new SqlCommand(q, conn.GetConnection()))
                    {
                        cmd.Parameters.AddWithValue("@LanguageID", lgID);
                        var result = cmd.ExecuteScalar();
                        title = result?.ToString() ?? string.Empty;
                    }

                    string fileName = $"{s.Student.NameEng}_{s.ClassTeacher.Teacher.NameEng}_{s.ClassTeacher.Class.Name}_CertificateAward.html";
                    fileName = SanitizeFileName(fileName);


                    if (title.ToLower().Contains("eng"))
                    {
                        var folderPathFinal = Path.Combine(folderPath, "English");
                        Directory.CreateDirectory(folderPathFinal); // Safe: creates only if not exists
                        string fullPath = Path.Combine(folderPathFinal, fileName);

                        File.WriteAllText(fullPath, GenerateCertificateAwardEngHtml(s, (int)s.Tag));

                        Process.Start(new ProcessStartInfo(fullPath)
                        {
                            UseShellExecute = true
                        });
                    }
                    else if (title.ToLower().Contains("chi"))
                    {
                        var folderPathFinal = Path.Combine(folderPath, "Chinese");
                        Directory.CreateDirectory(folderPathFinal); // Safe: creates only if not exists
                        string fullPath = Path.Combine(folderPathFinal, fileName);

                        File.WriteAllText(fullPath, GenerateCertificateAwardChiHtml(s, (int)s.Tag));

                        Process.Start(new ProcessStartInfo(fullPath)
                        {
                            UseShellExecute = true
                        });
                    }

                }
            }


        }

        private string RomanPattern = @"\b(I|II|III|IV|V|VI)\s+Book\b";

        public string NormalizeClassNameEng(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return name;

            // Check if it starts with "Super Kids" and contains a number
            if (name.StartsWith("Super Kids "))
            {
                // Replace "Super Kids [number]" with "Super Kids Book [number]"
                return name.Replace("Super Kids ", "Super Kids Book ");
            }
            else
            {
                name = "New Headway " + name + " Book";
            }

            return name;
        }
        private string GenerateCertificateEngHtml(StudentClassTeacher s)
        {
            #region POB
            string pobEng;
            string pobKh;
            if (Enum.TryParse<Provinces>(s.Student.POB, out Provinces pobEnum))
            {
                pobKh = KhmerUtils.ProvinceEngKh[pobEnum];
                pobEng = KhmerUtils.FormatProvince(pobEnum);

                if (pobKh == KhmerUtils.ProvinceEngKh[Provinces.PhnomPenh])
                {
                    pobKh = "រាជធានី " + pobKh;
                    pobEng = pobEng + " City";
                }
                else
                {
                    pobKh = "ខេត្ត " + pobKh;
                    pobEng = pobEng + " Province";
                }
            }
            else
            {
                pobEng = s.Student.POB;
                pobKh = s.Student.POB; // Keep old format like "P.P"
                
            }
            #endregion

            #region nameClass


            string nameClass = s.ClassTeacher.Class.Name;

            nameClass = NormalizeClassNameEng(nameClass);
            //if (!nameClass.ToLower().Contains("super"))
            //{

            //}
            #endregion

            string html = $$$""""
                <!DOCTYPE html>
                <html lang="en">
                <head>
                    <meta charset="UTF-8">
                    <meta name="viewport" content="width=device-width, initial-scale=1.0">
                    <title>Document</title>
                    <!-- <link rel="stylesheet" href="style.css"> -->
                    <style>
                        :root {
                  --main-bg-color: #f0f0f0;
                  --primary-text-color: #333333;
                  --accent-color: #007bff;
                  --font-size-small: 13.8px;
                  --font-size-large: 18px;
                }

                *{
                    padding: 0px;
                    margin: 0px;
                    box-sizing: border-box;
                }
                body {
                    width: 297mm; /* /2  A4 width at 96 DPI */
                    height: 210mm; /* A4 height at 96 DPI */
                    display: flex;
                    /* border: 1px solid black; */

                }
                @font-face {
                  font-family: khMoulFont;
                  src: url('../../font/kh/KhmerOSmuollight.ttf');
                  /* font-size: 10px; */
                }
                @font-face {
                  font-family: khFont;
                  src: url('../../font/kh/KhmerOSbattambang.ttf');
                  /* font-size: 10px; */
                }
                @font-face {
                  font-family: khPenSurin;
                  src: url('../../font/kh/KhmerPenSurin.ttf');
                  /* font-size: 10px; */
                }
                @font-face {
                  font-family: engFont;
                  src: url('../../font/eng/times.ttf');
                }
                @font-face {
                  font-family: tacteingFont;
                  src: url('../../font/TACTENG.TTF');
                }
                .khmerMoul{
                    font-family: khMoulFont;
                    font-size: var(--font-size-small);
                    /* font-weight: 600; */
                }
                .khmerOS{
                    font-family: khFont;
                    font-size: var(--font-size-small);
                }
                .khmerPensurin{
                    font-family: khPensurin;
                    font-size: var(--font-size-small);
                }
                .english{
                    font-size: var(--font-size-small);
                    font-family: engFont;
                    line-height: 1.88; /* increases space between lines */
                }
                .tacteing{
                    font-size: 30px;
                    font-family: tacteingFont;
                }
                @media print {
                    body {
                        width: 297mm;
                        height: 210m;
                        margin: 0;
                        padding: 0;
                    }
                }
                .main{
                    width: 100%;
                    height: 100%;
                    /* border: 1px solid black; */
                    background-image: url("../../image/4B.png");
                    background-position: center;
                    background-repeat: no-repeat;
                    background-size: contain;
                    /* display: flex;
                    flex-direction: column;
                    align-items: center; */
                    padding: 70px 115px 100px 115px;

                    /* padding: 140px; */
                }

                .main  .contain{
                    display: flex;
                    flex-direction: column;
                    justify-content: center;
                    align-items: center;
                    width: 100%;
                    height: 100%;

                }
                .p1{
                    display: flex;
                    flex-direction: row;
                    justify-content: space-between;
                    width: 100%;
                    height: 15%;
                }
                .p1 .logo{
                    height: 100%;
                    width: 22%;
                    display: block;
                    flex-direction: column;
                    align-items: center;
                    justify-content: top;
                    text-align: center;
                }

                .p1 .logo .logoimage{
                    height: 60% ;
                    width: 100%;
                    background-image: url('../../image/logo.png');
                    background-repeat: no-repeat;
                    background-size:contain;
                    background-position: center;
                    /* margin-bottom: 5px; */
                } 

                .p1 .logo .logotitle{
                    height: 20%;
                    /* margin-bottom: ; */
                }
                .p1 .logo .no{
                    margin-top: 10px;
                }
                .p1 .kingdomcambodia{
                    width: 23%;
                    float: right;
                    text-align: center;
                }
                .p1 .kingdomcambodia .sign{

                }

                .p2{
                    text-align: center;
                    height: 15%;
                }
                .p3{
                    display: flex;
                    flex-direction: row;
                    height: 23%;
                    width: 100%;
                }
                .p3 .left{
                    width: 50%;
                }
                .p3 .right{
                    width: 50%;
                }
                .p4{
                    height: 9%;
                    text-align: center;
                }
                .p5{
                    padding-top: 10px;
                    width: 100%;
                    height:25%;
                    display: flex;
                    justify-content: space-between;
                    text-align: center;
                }
                .p5 .center{
                    display: flex;
                    justify-content: center;
                    align-items: center;
                    width: 20%;
                    height: 100%;
                }
                .p5 .center .box{
                    margin-top: 20px;
                    width: 50%;
                    height: 80%;
                    border: 1px solid black;
                }



                    </style>
                </head>
                <body>
                    <div class="main">
                        <div class="contain">
                            <div class="p1">
                                <div class="logo">
                                    <div class="logoimage" ></div>
                                    <div class="logotitle">
                                        <div class="khmerMoul" style="font-size: 9px ;color: rgb(255,109,00);">សាលារៀនអន្តរជាតិស្មាយលីង</div>
                                        <div class="english" style="font-size: 7.7px; font-weight: 700; color: rgb(255,109,00);">SMILING INTERNATIONAL SCHOOL</div>
                                    </div>
                                    <div class="no english" style="font-size: 12px;">No................SIS</div>
                                </div>
                                <div class="" style="width: auto;"></div>
                                <div class="kingdomcambodia">
                                    <div class="khmerMoul" style="font-size: 11px;">ព្រះរាជាណាចក្រកម្ពុជា</div>
                                    <div class="english" style="font-size: 11px; font-weight: bold; margin-bottom: 2px;">KINGDOM OF CAMBODIA</div>
                                    <div class="khmerMoul" style="font-size: 11px;">ជាតិ សាសនា ព្រះមហាក្សត្រ</div>
                                    <div class="english" style="font-size: 11px; font-weight: bold;">NATION RELIGION KING</div>
                                    <div class="tacteing">3</div>
                                </div>
                            </div>
                            <div class="p2">
                                <div class="khmerMoul">វិញ្ញាបនបត្របញ្ជាក់ការសិក្សា</div>
                                <div class="english" style="font-weight: bold;">CERTIFICATE OF ACHIEVEMENT</div>
                                <div class="khmerMoul">នាយក សាលារៀន អន្តរជាតិ ស្មាយលីង</div>
                            </div>
                            <div class="p3">
                                <div class="left">
                                    <div class="l1 khmerOS">សូមបញ្ជាក់ថា{{{nbsp(10)}}}:{{{nbsp(3)}}}<spans class="khmerMoul">{{{s.Student.NameKh}}}</span></div>
                                    <div class="l2 khmerOS">ភេទ{{{nbsp(27)}}}<span class="khmerMoul">{{{KhmerUtils.ToKhmerGender(s.Student.Sex)}}}</span>{{{nbsp(17)}}}សញ្ជាតិ{{{nbsp(12)}}}<span class="khmerMoul">ខ្មែរ</span></div>
                                    <div class="l3 khmerOS">ថ្ងៃខែឆ្នាំកំណើត	{{{nbsp(7)}}}:{{{nbsp(3)}}}{{{KhmerUtils.ToKhmerNumber(s.Student.DOB.Day,2)}}}{{{nbsp(20)}}}{{{KhmerUtils.ToKhmerMonth(s.Student.DOB.Month)}}}{{{nbsp(15)}}}{{{KhmerUtils.ToKhmerNumber(s.Student.DOB.Year)}}}</div>
                                    <div class="l4 khmerOS">ទីកន្លែងកំណើត	{{{nbsp(7)}}}:{{{nbsp(3)}}}<span class="khmerMoul">{{{pobKh}}}</span></div>
                                    <div class="l5 khmerOS" style="font-size: 12px;">បានចូលរៀន និងបញ្ចប់ដោយជោគជ័យក្នុងវគ្គសិក្សាភាសាអង់គ្លេសកម្រិត <span style="font-family: engFont; font-size: var(--font-size-small);">{{{s.ClassTeacher.Class.Name}}}</span></div>
                                </div>
                                <div class="right">
                                    <div class="r1 english">This is to certify that{{{nbsp(4)}}}:{{{nbsp(3)}}}<span style="font-weight: bold;">{{{s.Student.NameEng.ToUpper()}}}</span></div>
                                    <div class="r2 english">Sex{{{nbsp(35)}}}<span style="font-weight: bold;">{{{CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.Student.Sex.ToLower())}}}</span>{{{nbsp(25)}}}Nationality{{{nbsp(11)}}}<span style="font-weight: bold;">Khmer</span></div>
                                    <div class="r3 english" style="line-height: 1.9;">Date of Birth{{{nbsp(16)}}}:{{{nbsp(3)}}}{{{s.Student.DOB.ToString("MMMM") + "," + nbsp(15) + s.Student.DOB.Day.ToString("00") + nbsp(25) + s.Student.DOB.Year}}} </div>
                                    <div class="r4 english" style="line-height: 1.87;">Place of Birth{{{nbsp(15)}}}:{{{nbsp(3)}}}<span style="font-weight: bold;">{{{pobEng}}}</span></div>
                                    <div class="r5 english" style="font-size: 12px; line-height: 1.9;">Has studied and successfully completed of ELS, level {{{nameClass}}}</div>
                                </div>
                            </div>
                            <div class="p4">
                                <div class="khmerOS">វិញ្ញាបនបត្រនេះប្រគល់ជូនសាមីខ្លួនប្រើប្រាស់តាមការដែលអាចប្រើបាន។</div>
                                <div class="english">This is certificate of appreciation for being useful and worthy of what is right.</div>
                            </div>
                            <div class="p5">
                                <div class="left">
                                    <div class="khmerOS">ថ្ងៃ<span class="khmerPensurin"> ................... </span>ខែ<span class="khmerPensurin"> ......... </span> ឆ្នាំម្សាញ់ សប្តស័ក ព.ស. ២៥៦៩</div>
                                    <div class="khmerOS">ភ្នំពេញ ថ្ងៃទី<span class="khmerPensurin"> ....... </span>ខែ<span class="khmerPensurin"> ...... </span>ឆ្នាំ២០២៥</div>
                                    <div class="khmerMoul">នាយកសាលា</div>
                                </div>
                                <div class="center">
                                    <div class="box"></div>
                                </div>
                                <div class="right">
                                    <div class="khmerOS">ថ្ងៃ<span class="khmerPensurin"> ................... </span>ខែ<span class="khmerPensurin"> ......... </span> ឆ្នាំម្សាញ់ សប្តស័ក ព.ស. ២៥៦៩</div>
                                    <div class="khmerOS">ភ្នំពេញ ថ្ងៃទី<span class="khmerPensurin"> ....... </span>ខែ<span class="khmerPensurin"> ...... </span>ឆ្នាំ២០២៥</div>
                                    <div class="khmerMoul">គ្រូបន្ទុកថ្នាក់</div>
                                </div>
                            </div>
                        </div>

                    </div>
                </body>
                </html>
                """";
            return html;
        }
        private string GenerateCertificateChHtml(StudentClassTeacher s)
        {
            int getNum(string s)
            {
                return int.Parse(new string(s.Where(char.IsDigit).ToArray()));
            }
            #region POB
            int grade = getNum(s.ClassTeacher.Class.Name);
            string pobCh;
            string pobKh;
            if (Enum.TryParse<Provinces>(s.Student.POB, out Provinces pobEnum))
            {
                pobKh = KhmerUtils.ProvinceEngKh[pobEnum];
                pobCh = KhmerUtils.ProvinceEngCh[pobEnum];

                if (pobKh == KhmerUtils.ProvinceEngKh[Provinces.PhnomPenh])
                {
                    pobKh = "រាជធានី " + pobKh;
                    //pobEng = pobEng + " City";
                }
                else
                {
                    pobKh = "ខេត្ត " + pobKh;
                    //pobEng = pobEng + " Province";
                }
            }
            else
            {
                pobCh = s.Student.POB;
                pobKh = s.Student.POB; // Keep old format like "P.P"

            }
            #endregion

            #region nameClass


            string nameClass = s.ClassTeacher.Class.Name;

            nameClass = NormalizeClassNameEng(nameClass);
            //if (!nameClass.ToLower().Contains("super"))
            //{

            //}
            #endregion

            string html = $$$""""
                <!DOCTYPE html>
                <html lang="en">
                <head>
                    <meta charset="UTF-8">
                    <meta name="viewport" content="width=device-width, initial-scale=1.0">
                    <title>Document</title>
                    <!-- <link rel="stylesheet" href="style.css"> -->
                    <style>
                        :root {
                  --main-bg-color: #f0f0f0;
                  --primary-text-color: #333333;
                  --accent-color: #007bff;
                  --font-size-small: 13.8px;
                  --font-size-large: 18px;
                }

                *{
                    padding: 0px;
                    margin: 0px;
                    box-sizing: border-box;
                }
                body {
                    width: 297mm; /* /2  A4 width at 96 DPI */
                    height: 210mm; /* A4 height at 96 DPI */
                    display: flex;
                    /* border: 1px solid black; */

                }
                @font-face {
                  font-family: khMoulFont;
                  src: url('../../font/kh/KhmerOSmuollight.ttf');
                  /* font-size: 10px; */
                }
                @font-face {
                  font-family: khFont;
                  src: url('../../font/kh/KhmerOSbattambang.ttf');
                  /* font-size: 10px; */
                }
                @font-face {
                  font-family: khPenSurin;
                  src: url('../../font/kh/KhmerPenSurin.ttf');
                  /* font-size: 10px; */
                }
                @font-face {
                  font-family: engFont;
                  src: url('../../font/eng/times.ttf');
                }
                @font-face{
                    font-family: chFont;
                    src: url('../../font/ch/simsun.ttc');
                }
                @font-face {
                  font-family: tacteingFont;
                  src: url('../../font/TACTENG.TTF');
                }
                .khmerMoul{
                    font-family: khMoulFont;
                    font-size: var(--font-size-small);
                    /* font-weight: 600; */
                }
                .khmerOS{
                    font-family: khFont;
                    font-size: var(--font-size-small);
                }
                .khmerPensurin{
                    font-family: khPensurin;
                    font-size: var(--font-size-small);
                }
                .english{
                    font-size: var(--font-size-small);
                    font-family: engFont;
                    line-height: 1.88; /* increases space between lines */
                }
                .chinese{
                    font-size: var(--font-size-small);
                    font-family: chFont;
                    line-height: 1.88; /* increases space between lines */
                }
                .tacteing{
                    font-size: 30px;
                    font-family: tacteingFont;
                }
                @media print {
                    body {
                        width: 297mm;
                        height: 210m;
                        margin: 0;
                        padding: 0;
                    }
                }
                .main{
                    width: 100%;
                    height: 100%;
                    /* border: 1px solid black; */
                    background-image: url("../../image/4B.png");
                    background-position: center;
                    background-repeat: no-repeat;
                    background-size: contain;
                    /* display: flex;
                    flex-direction: column;
                    align-items: center; */
                    padding: 70px 115px 100px 115px;

                    /* padding: 140px; */
                }

                .main  .contain{
                    display: flex;
                    flex-direction: column;
                    justify-content: center;
                    align-items: center;
                    width: 100%;
                    height: 100%;

                }
                .p1{
                    display: flex;
                    flex-direction: row;
                    justify-content: space-between;
                    width: 100%;
                    height: 15%;
                }
                .p1 .logo{
                    height: 100%;
                    width: 22%;
                    display: block;
                    flex-direction: column;
                    align-items: center;
                    justify-content: top;
                    text-align: center;
                }

                .p1 .logo .logoimage{
                    height: 60% ;
                    width: 100%;
                    background-image: url('../../image/logo.png');
                    background-repeat: no-repeat;
                    background-size:contain;
                    background-position: center;
                    /* margin-bottom: 5px; */
                } 

                .p1 .logo .logotitle{
                    height: 20%;
                    /* margin-bottom: ; */
                }
                .p1 .logo .no{
                    margin-top: 10px;
                }
                .p1 .kingdomcambodia{
                    width: 23%;
                    float: right;
                    text-align: center;
                }
                .p1 .kingdomcambodia .sign{

                }

                .p2{
                    text-align: center;
                    height: 15%;
                }
                .p3{
                    display: flex;
                    flex-direction: row;
                    height: 23%;
                    width: 100%;
                }
                .p3 .left{
                    width: 50%;
                }
                .p3 .right{
                    width: 50%;
                }
                .p4{
                    height: 9%;
                    text-align: center;
                }
                .p5{
                    padding-top: 10px;
                    width: 100%;
                    height:25%;
                    display: flex;
                    justify-content: space-between;
                    text-align: center;
                }
                .p5 .center{
                    display: flex;
                    justify-content: center;
                    align-items: center;
                    width: 20%;
                    height: 100%;
                }
                .p5 .center .box{
                    margin-top: 20px;
                    width: 50%;
                    height: 80%;
                    border: 1px solid black;
                }



                    </style>
                </head>
                <body>
                    <div class="main">
                        <div class="contain">
                            <div class="p1">
                                <div class="logo">
                                    <div class="logoimage" ></div>
                                    <div class="logotitle">
                                        <div class="khmerMoul" style="font-size: 9px ;color: rgb(255,109,00);">សាលារៀនអន្តរជាតិស្មាយលីង</div>
                                        <div class="english" style="font-size: 7.7px; font-weight: 700; color: rgb(255,109,00);">SMILING INTERNATIONAL SCHOOL</div>
                                    </div>
                                    <div class="no english" style="font-size: 12px;">No................SIS</div>
                                </div>
                                <div class="" style="width: auto;"></div>
                                <div class="kingdomcambodia">
                                    <div class="khmerMoul" style="font-size: 11px;">ព្រះរាជាណាចក្រកម្ពុជា</div>
                                    <div class="english" style="font-size: 11px; font-weight: bold; margin-bottom: 2px;">KINGDOM OF CAMBODIA</div>
                                    <div class="khmerMoul" style="font-size: 11px;">ជាតិ សាសនា ព្រះមហាក្សត្រ</div>
                                    <div class="english" style="font-size: 11px; font-weight: bold;">NATION RELIGION KING</div>
                                    <div class="tacteing">3</div>
                                </div>
                            </div>
                            <div class="p2">
                                <div class="khmerMoul">វិញ្ញាបនបត្របញ្ជាក់ការសិក្សា</div>
                                <div class="chinese" style="font-weight: bold;">学 习 证 明 书</div>
                                <div class="khmerMoul">នាយក សាលារៀន អន្តរជាតិ ស្មាយលីង</div>
                            </div>
                            <div class="p3">
                                <div class="left">
                                    <div class="l1 khmerOS">សូមបញ្ជាក់ថា{{{nbsp(10)}}}:{{{nbsp(3)}}}<spans class="khmerMoul">{{{s.Student.NameKh}}}</span></div>
                                    <div class="l2 khmerOS">ភេទ{{{nbsp(27)}}}<span class="khmerMoul">{{{KhmerUtils.ToKhmerGender(s.Student.Sex)}}}</span>{{{nbsp(17)}}}សញ្ជាតិ{{{nbsp(12)}}}<span class="khmerMoul">ខ្មែរ</span></div>
                                    <div class="l3 khmerOS">ថ្ងៃខែឆ្នាំកំណើត	{{{nbsp(7)}}}:{{{nbsp(3)}}}{{{KhmerUtils.ToKhmerNumber(s.Student.DOB.Day, 2)}}}{{{nbsp(20)}}}{{{KhmerUtils.ToKhmerMonth(s.Student.DOB.Month)}}}{{{nbsp(15)}}}{{{KhmerUtils.ToKhmerNumber(s.Student.DOB.Year)}}}</div>
                                    <div class="l4 khmerOS">ទីកន្លែងកំណើត	{{{nbsp(7)}}}:{{{nbsp(3)}}}<span class="khmerMoul">{{{pobKh}}}</span></div>
                                    <div class="l5 khmerOS">បានចូលរៀន និងបញ្ចប់ដោយជោគជ័យក្នុងវគ្គសិក្សាភាសាចិនកម្រិត <span">{{{KhmerUtils.ToKhmerNumber(grade)}}}</span></div>
                                </div>
                                <div class="right">
                                    <div class="r1 chinese">这是为了证明{{{nbsp(1)}}}:{{{nbsp(1)}}}<span class="english" style="font-weight: bold;">{{{s.Student.NameEng.ToUpper()}}}</span></div>
                                    <div class="r2 chinese">性别{{{nbsp(6)}}}<span class="english">{{{nbsp(2)}}}</span><span style="font-weight: bold;">{{{KhmerUtils.ToChinaGender(s.Student.Sex.ToLower())}}}</span>{{{nbsp(7)}}}国籍{{{nbsp(5)}}}<span class="english">{{{nbsp(1)}}}</span><span style="font-weight: bold;">柬埔寨</span></div>
                                    <div class="r3 chinese">出生日期{{{nbsp(3)}}}:{{{nbsp(1)}}}<span class="english">{{{s.Student.DOB.ToString("MMMM") + "," + nbsp(15) + s.Student.DOB.Day.ToString("00") + nbsp(25) + s.Student.DOB.Year}}}</span></div>
                                    <div class="r4 chinese">出生地址{{{nbsp(3)}}}:{{{nbsp(1)}}}<span style="font-weight: bold;">{{{pobCh}}}</span></div>
                                    <div class="r5 chinese">成 功 参 加 并 完 {{{KhmerUtils.ToChinaNumber(grade)}}} 级 中 文 课 程。</div>
                                </div>
                            </div>
                            <div class="p4">
                                <div class="khmerOS">វិញ្ញាបនបត្រនេះប្រគល់ជូនសាមីខ្លួនប្រើប្រាស់តាមការដែលអាចប្រើបាន។</div>
                                <div class="chinese">这 个 证 明 书 是 给 申 请 者 盝 可 能 多 地 使 用 的。</div>
                            </div>
                            <div class="p5">
                                <div class="left">
                                    <div class="khmerOS">ថ្ងៃ<span class="khmerPensurin"> ................... </span>ខែ<span class="khmerPensurin"> ......... </span> ឆ្នាំម្សាញ់ សប្តស័ក ព.ស. ២៥៦៩</div>
                                    <div class="khmerOS">ភ្នំពេញ ថ្ងៃទី<span class="khmerPensurin"> ....... </span>ខែ<span class="khmerPensurin"> ...... </span>ឆ្នាំ២០២៥</div>
                                    <div class="khmerMoul">នាយកសាលា</div>
                                </div>
                                <div class="center">
                                    <div class="box"></div>
                                </div>
                                <div class="right">
                                    <div class="khmerOS">ថ្ងៃ<span class="khmerPensurin"> ................... </span>ខែ<span class="khmerPensurin"> ......... </span> ឆ្នាំម្សាញ់ សប្តស័ក ព.ស. ២៥៦៩</div>
                                    <div class="khmerOS">ភ្នំពេញ ថ្ងៃទី<span class="khmerPensurin"> ....... </span>ខែ<span class="khmerPensurin"> ...... </span>ឆ្នាំ២០២៥</div>
                                    <div class="khmerMoul">គ្រូបន្ទុកថ្នាក់</div>
                                </div>
                            </div>
                        </div>

                    </div>
                </body>
                </html>
                """";
            return html;
        }

        private string GenerateCertificateAwardEngHtml(StudentClassTeacher s , int number)
        {
            #region POB
            string pobEng;
            string pobKh;
            if (Enum.TryParse<Provinces>(s.Student.POB, out Provinces pobEnum))
            {
                pobKh = KhmerUtils.ProvinceEngKh[pobEnum];
                pobEng = KhmerUtils.FormatProvince(pobEnum);

                if (pobKh == KhmerUtils.ProvinceEngKh[Provinces.PhnomPenh])
                {
                    pobKh = "រាជធានី " + pobKh;
                    pobEng = pobEng + " City";
                }
                else
                {
                    pobKh = "ខេត្ត " + pobKh;
                    pobEng = pobEng + " Province";
                }
            }
            else
            {
                pobEng = s.Student.POB;
                pobKh = s.Student.POB; // Keep old format like "P.P"

            }
            #endregion

            #region nameClass


            string nameClass = s.ClassTeacher.Class.Name;

            //nameClass = NormalizeClassName(nameClass);
            //if (!nameClass.ToLower().Contains("super"))
            //{

            //}
            #endregion

            string html = $$$""""
                <!DOCTYPE html>
                <html lang="en">
                <head>
                    <meta charset="UTF-8">
                    <meta name="viewport" content="width=device-width, initial-scale=1.0">
                    <title>Document</title>
                    <style>
                        *{
                            padding: 0px;
                            margin: 0px;
                            box-sizing: border-box;
                        }
                        body {
                            width: 297mm; /* /2  A4 width at 96 DPI */
                            height: 210mm; /* A4 height at 96 DPI */
                            display: flex;
                            /* border: 1px solid black; */

                        }
                                @media print {
                            body {
                                width: 297mm;
                                height: 210m;
                                margin: 0;
                                padding: 0;
                            }
                        }
                        @font-face {
                            font-family: khMoulFont;
                            src: url('../../font/kh/KhmerOSmuollight.ttf');
                            /* font-size: 10px; */
                        }
                        @font-face {
                            font-family: khFont;
                            src: url('../../font/kh/KhmerOSbattambang.ttf');
                            /* font-size: 10px; */
                        }
                        @font-face {
                            font-family: khPenSurin;
                            src: url('../../font/kh/KhmerPenSurin.ttf');
                            /* font-size: 10px; */
                        }
                        @font-face {
                            font-family: engFont;
                            src: url('../../font/eng/times.ttf');
                        }
                        @font-face{
                            font-family: chFont;
                            src: url('../../font/ch/simsun.ttc');
                        }
                        @font-face {
                            font-family: tacteingFont;
                            src: url('../../font/TACTENG.TTF');
                        }
                        .khmerMoul{
                            font-family: khMoulFont;
                            font-size: var(--font-size-small);
                            /* font-weight: 600; */
                        }
                        .khmerOS{
                            font-family: khFont;
                            font-size: var(--font-size-small);
                        }
                        .khmerPensurin{
                            font-family: khPensurin;
                            font-size: var(--font-size-small);
                        }
                        .english{
                            font-size: var(--font-size-small);
                            font-family: engFont;
                            line-height: 1.88; /* increases space between lines */
                        }
                        .chinese{
                            font-size: var(--font-size-small);
                            font-family: chFont;
                        }
                        .tacteing{
                            font-size: 30px;
                            font-family: tacteingFont;
                        }
                        .yellow{
                            color: rgb(254, 109, 3);
                        }
                        .main{

                            background-image: url('../../image/background.png');
                            background-repeat: no-repeat;
                            background-size: contain;
                            width: 100%;
                            height: 100%;
                            display: flex;
                            flex-direction: column;
                            justify-content: center;
                            align-items: center;
                        }
                        .contain{
                            width: 100%;
                            height: 100%;
                            /* background-color: rgba(255, 255, 0, 0.13) ; */
                            display: flex;
                            flex-direction: column;
                            /* justify-content: center; */
                            align-items: center;
                        }
                        .wrap1{
                            width: 100%;
                            height: 20%;
                            display: flex;
                            /* text-align: end;
                             */
                            /* float: right; */
                            .block1{
                                width: 77.4%;
                            }
                            .block2{
                                width: 10%;
                                height: 100%;
                                text-align: center;
                                font-size: 120px;
                                font-weight: 900;
                                color: rgb(254, 109, 3);
                            }
                        }
                        .wrap2{
                            text-align: center;
                            display: inline-block;
                            font-size: 48px;
                            font-weight: 500;
                            color: rgb(254, 109, 3);
                            line-height: 1;
                            border-bottom: 2px solid rgb(254, 109, 3);
                        }
                        .wrap3{
                            margin-top: 18px;
                            text-align: center;
                            display: inline-block;
                            font-size: 26px;
                            color: rgb(30,54,83);
                            line-height: 1.2;

                        }
                    </style>
                </head>
                <body>
                    <div class="main">
                        <div class="contain">
                            <div style="height: 1%;"></div>
                            <div class="wrap1">
                                <div class="block1"></div>
                                <div class="block2 english">{{{number}}}</div>
                                <div class="block3"></div>
                            </div>
                            <div style="height: 25%; width: 10%;"></div>

                            <div class="wrap2 english">{{{s.Student.NameEng.ToUpper()}}}</div>
                            <div class="wrap3 english">In recognition of his/her efforts and achievement in completing for<br>one year ESL program, level <span class="yellow">{{{nameClass}}}</span>. Academy year <span class="yellow">2024-2025</span>.
                </div>
                        </div>
                    </div>
                </body>
                </html>
                """";
            return html;
        }
        private string GenerateCertificateAwardChiHtml(StudentClassTeacher s, int number)
        {
            #region nameClass
            string nameClass = s.ClassTeacher.Class.Name;
            int getNum(string s)
            {
                return int.Parse(new string(s.Where(char.IsDigit).ToArray()));
            }
            int grade = getNum(nameClass);
            #endregion

            string html = $$$""""
                <!DOCTYPE html>
                <html lang="en">
                <head>
                    <meta charset="UTF-8">
                    <meta name="viewport" content="width=device-width, initial-scale=1.0">
                    <title>Document</title>
                    <style>
                        *{
                            padding: 0px;
                            margin: 0px;
                            box-sizing: border-box;
                        }
                        body {
                            width: 297mm; /* /2  A4 width at 96 DPI */
                            height: 210mm; /* A4 height at 96 DPI */
                            display: flex;
                            /* border: 1px solid black; */

                        }
                                @media print {
                            body {
                                width: 297mm;
                                height: 210m;
                                margin: 0;
                                padding: 0;
                            }
                        }
                        @font-face {
                            font-family: khMoulFont;
                            src: url('../../font/kh/KhmerOSmuollight.ttf');
                            /* font-size: 10px; */
                        }
                        @font-face {
                            font-family: khFont;
                            src: url('../../font/kh/KhmerOSbattambang.ttf');
                            /* font-size: 10px; */
                        }
                        @font-face {
                            font-family: khPenSurin;
                            src: url('../../font/kh/KhmerPenSurin.ttf');
                            /* font-size: 10px; */
                        }
                        @font-face {
                            font-family: engFont;
                            src: url('../../font/eng/times.ttf');
                        }
                        @font-face{
                            font-family: chFont;
                            src: url('../../font/ch/simsun.ttc');
                        }
                        @font-face{
                            font-family: chBoldFont;
                            src: url('../../font/ch/NotoSansSC-Regular.ttf');
                        }
                        @font-face {
                            font-family: tacteingFont;
                            src: url('../../font/TACTENG.TTF');
                        }
                        .khmerMoul{
                            font-family: khMoulFont;
                            font-size: var(--font-size-small);
                            /* font-weight: 600; */
                        }
                        .khmerOS{
                            font-family: khFont;
                            font-size: var(--font-size-small);
                        }
                        .khmerPensurin{
                            font-family: khPensurin;
                            font-size: var(--font-size-small);
                        }
                        .english{
                            font-size: var(--font-size-small);
                            font-family: engFont;
                            line-height: 1.88; /* increases space between lines */
                        }
                        .chinese{
                            font-size: var(--font-size-small);
                            font-family: chFont;
                        }
                        .chineseBold{
                            font-size: var(--font-size-small);
                            font-family: chBoldFont;
                        }
                        .tacteing{
                            font-size: 30px;
                            font-family: tacteingFont;
                        }
                        .yellow{
                            color: rgb(254, 109, 3);
                        }
                        .main{

                            background-image: url('../../image/backgroundch.png');
                            background-repeat: no-repeat;
                            background-size: contain;
                            width: 100%;
                            height: 100%;
                            display: flex;
                            flex-direction: column;
                            justify-content: center;
                            align-items: center;
                        }
                        .contain{
                            width: 100%;
                            height: 100%;
                            /* background-color: rgba(255, 255, 0, 0.13) ; */
                            display: flex;
                            flex-direction: column;
                            /* justify-content: center; */
                            align-items: center;
                        }
                        .wrap1{
                            width: 100%;
                            height: 20%;
                            display: flex;
                            /* text-align: end;
                             */
                            /* float: right; */
                            .block1{
                                width: 77.4%;
                            }
                            .block2{
                                width: 10%;
                                height: 100%;
                                text-align: center;
                                font-size: 120px;
                                font-weight: 900;
                                color: rgb(254, 109, 3);
                            }
                        }
                        .wrap2{
                            text-align: center;
                            display: inline-block;
                            font-size: 48px;
                            font-weight: 500;
                            color: rgb(254, 109, 3);
                            line-height: 1;
                            border-bottom: 2px solid rgb(254, 109, 3);
                        }
                        .wrap3{
                            margin-top: 18px;
                            text-align: center;
                            display: inline-block;
                            font-size: 26px;
                            color: rgb(30,54,83);
                            line-height: 1.2;

                        }
                    </style>
                </head>
                <body>
                    <div class="main">
                        <div class="contain">
                            <div style="height: 1%;"></div>
                            <div class="wrap1">
                                <div class="block1"></div>
                                <div class="block2 english">{{{number}}}</div>
                                <div class="block3"></div>
                            </div>
                            <div style="height: 22%; width: 10%;"></div>

                            <div class="wrap2 english">{{{s.Student.NameEng.ToUpper()}}}</div>
                            <div class="wrap3 chinese" style="line-height:1.7; font-weight:600; font-size:31px">为表彰他在完成为期一年的汉语通试学习课程中<br>所付出的努力和取得的成就, 汉语水平<span class="yellow">{{{KhmerUtils.ToChinaNumber(grade)}}}</span>年级。<span class="yellow">2024-2025</span> 学年。</div>
                        </div>
                    </div>
                </body>
                </html>
                """";
            return html;
        }


        private string? GenerateListHtml(List<StudentClassTeacher> allData)
        {
            string logoPath = Path.Combine(System.Windows.Forms.Application.StartupPath, "Resource\\System\\logo.png");
            string tbodydata = "";
            int i = 1;
            
            foreach(StudentClassTeacher sct in allData)
            {
                tbodydata += $$""""
                                <tr>
                                    <td>{{i}}</td>
                                    <td>{{sct.Student.NameEng}}</td>
                                    <td>{{sct.Student.Sex.ToUpper().First()}}</td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td>{{sct.Student.Phone}}</td>
                                </tr>
                    """";
                i++;
            }
            string html = $$"""
                <!DOCTYPE html>
                <html lang="en">

                <head>
                    <meta charset="UTF-8">
                    <title>Attendance Sheet</title>

                    <link
                        rel="stylesheet">
                    <style>
                        *{
                            margin: 0;
                            padding: 0;
                            box-sizing: border-box;
                        }
                        body {
                            width: 297mm; /* /2  A4 width at 96 DPI */
                            height: 210mm; /* A4 height at 96 DPI */
                            font-family: Arial, sans-serif;
                            font-size: 12px;
                            padding: 20px;
                        }
                        @media print {
                            body {
                                width: 297mm;
                                height: 210m;
                            }
                        }

                        .header {
                            display: flex;
                            justify-content: space-between;

                        }

                        strong {
                            font-size: larger;
                            font-family: "Moul", serif;
                        }

                        .school {
                            display: flex;
                            align-items: center;
                        }

                        .info {
                            margin: 10px 0;
                        }

                        .info span {
                            margin-right: 20px;
                        }

                        table {
                            border-collapse: collapse;
                            width: 100%;
                            text-align: center;
                        }

                        th,
                        td {
                            border: 1px solid #000;
                        }

                        th.rotate {
                            height: 100px;
                            white-space: nowrap;
                        }

                        th.rotate>div {
                            transform:
                                translate(10px, 30px) rotate(-90deg);
                            width: 20px;
                        }
                        th.week-day
                        {
                            width: 20px; /* or any value that works for your layout */
                        text-align: center;
                        }
                    </style>
                </head>

                <body>

                    <div class="header">
                        <div class="school">
                            <div class="logo">
                                <img src="{{logoPath}}"
                                    alt="logo" width="100px">
                            </div>
                            <div class="school-name" style="padding-left: 10px;">
                                <strong>សាលារៀន អន្ដរជាតិ ស្មាលីង</strong> <br>
                                <strong>Smiling International School</strong>
                            </div>
                        </div>
                        <div style="text-align: center;">
                            <strong>ព្រះរាជាណាចក្រកម្ពុជា</strong><br>
                            <strong>ជាតិ សាសនា ព្រះមហាក្សត្រ</strong>
                        </div>
                    </div>

                    <div class="info">
                        <span style="padding-right: 30px;">Class: {{allData.First().ClassTeacher.Class.Name}}</span>
                        <span style="padding-right: 100px;">Name: {{allData.First().ClassTeacher.Teacher.NameEng}}</span>
                        <span style="padding-right: 30px;">Sex: {{allData.First().ClassTeacher.Teacher.Sex}}</span>
                        <span style="padding-right: 100px;">Phone: {{allData.First().ClassTeacher.Teacher.Phone}}</span>
                        <span>Month: {{DateTime.Now.ToString("MMMM")}}</span>
                    </div>

                    <table>
                        <thead>
                            <tr>
                                <th rowspan="3">N</th>
                                <th rowspan="3">Name</th>
                                <th rowspan="3">Sex</th>
                                <th colspan="7">Week 1</th>
                                <th colspan="7">Week 2</th>
                                <th colspan="7">Week 3</th>
                                <th colspan="7">Week 4</th>
                                <th colspan="3">Week 5</th>
                                <th rowspan="2" style="width: 170px;">Other</th>

                            </tr>
                            <tr>
                                <!-- Week Dates Row -->
                                <th class="week-day">1</th>
                                <th class="week-day">2</th>
                                <th class="week-day">3</th>
                                <th class="week-day">4</th>
                                <th class="week-day">5</th>
                                <th class="week-day">6</th>
                                <th class="week-day">7</th>
                                <th class="week-day">8</th>
                                <th class="week-day">9</th>
                                <th class="week-day">10</th>
                                <th class="week-day">11</th>
                                <th class="week-day">12</th>
                                <th class="week-day">13</th>
                                <th class="week-day">14</th>
                                <th class="week-day">15</th>
                                <th class="week-day">16</th>
                                <th class="week-day">17</th>
                                <th class="week-day">18</th>
                                <th class="week-day">19</th>
                                <th class="week-day">20</th>
                                <th class="week-day">21</th>
                                <th class="week-day">22</th>
                                <th class="week-day">23</th>
                                <th class="week-day">24</th>
                                <th class="week-day">25</th>
                                <th class="week-day">26</th>
                                <th class="week-day">27</th>
                                <th class="week-day">28</th>
                                <th class="week-day">29</th>
                                <th class="week-day">30</th>
                                <th class="week-day">31</th>
                            </tr>
                        </thead>
                        <tbody>
                            {{tbodydata}}
                        </tbody>
                    </table>

                </body>

                </html>
                """;
            return html;
        }
        #endregion



        public void viewDgv(List<StudentClassTeacher> pageData)
        {
            dgv.Rows.Clear();
            foreach (StudentClassTeacher sct in pageData)
            {
                int index = dgv.Rows.Add(sct.IsSelected, sct.Student.NameEng, sct.Student.NameKh, sct.Student.Sex, sct.ClassTeacher.Class.Name, sct.ClassTeacher.Teacher.NameEng, sct.Student.Phone);
                dgv.Rows[index].Tag = sct;
                sct.Tag = dgv.Rows[index];
            }

            lbCurrentPage.Text = paginator.CurrentPage.ToString("D2");
            btnPre.Enabled = paginator.CurrentPage > 1;
            btnNext.Enabled = paginator.CurrentPage < paginator.TotalPages;
        }
        public void search(string data)
        {
            //mgmStudent listStudents = new mgmStudent();
            //listStudents.Students = mgmStudent.Students.Where(e => e.GradeName == option).ToList();
            paginator.AllData = mgmStudentClassTeacher.studentClassTeachers
            .Where(e =>
                (!string.IsNullOrEmpty(e.Student.NameEng) && e.Student.NameEng.Contains(data, StringComparison.OrdinalIgnoreCase)) ||
                (!string.IsNullOrEmpty(e.Student.NameKh) && e.Student.NameKh.Contains(data, StringComparison.OrdinalIgnoreCase))
            )
            .ToList();

            //paginator.AllData = mgmStudentClassTeacher.studentClassTeachers.Where(e => e.Student.NameEng == data && e.Student.NameKh == data).ToList();
            //paginator.LoadPage(1);
        }
        public void information()
        {

        }
        public void insert()
        {
            using (formSelectStudent form = new formSelectStudent())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    Student Student = form.S;
                    DateTime startDate = form.StartDate.Date;
                    int classID = 0;
                    int teacherID = 0;


                    if (cbClass.SelectedItem is Item itemCls) classID = itemCls.ID;
                    if (cbTeacher.SelectedItem is Item itemTea) teacherID = itemTea.ID;
                    //query = $"Insert into (StudentID,ClassTeacherID) values ({newStudent.StudentID},{classID},{teacherID}) FROM {Constants.tbStudentClassTeacher}";

                    int classTeacherID;

                    // 1. Get ClassTeacherID from ClassID and TeacherID
                    string getClassTeacherQuery = @"
                        SELECT ClassTeacherID 
                        FROM ClassTeacher 
                        WHERE ClassID = @ClassID AND TeacherID = @TeacherID";

                    using (SqlCommand cmd = new SqlCommand(getClassTeacherQuery, conn.GetConnection()))
                    {
                        cmd.Parameters.AddWithValue("@ClassID", classID);
                        cmd.Parameters.AddWithValue("@TeacherID", teacherID);

                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            classTeacherID = Convert.ToInt32(result);
                            // 2. Insert into StudentClassTeacher
                            int newSCTID;

                            // First check if record exists with IsActive = 0
                            string checkQuery = @"
                                                SELECT StudentClassTeacherID 
                                                FROM StudentClassTeacher
                                                WHERE StudentID = @StudentID 
                                                  AND ClassTeacherID = @ClassTeacherID
                                            ";

                            using (var checkCmd = new SqlCommand(checkQuery, conn.GetConnection()))
                            {
                                checkCmd.Parameters.AddWithValue("@StudentID", Student.StudentID);
                                checkCmd.Parameters.AddWithValue("@ClassTeacherID", classTeacherID);

                                object resultClassTeacher = checkCmd.ExecuteScalar();

                                if (resultClassTeacher != null)
                                {
                                    // Record exists → just update IsActive and dates

                                    if (MessageBox.Show(
                                        "This student is already in the class but inactive.\nDo you want to activate them again?",
                                        "Confirm Reactivation",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        // Do your update logic here

                                        string updateQuery = @"
                                            UPDATE StudentClassTeacher
                                            SET IsActive = 1,
                                                StartDate = @StartDate,
                                                EndDate = @EndDate
                                            WHERE StudentClassTeacherID = @SCTID
                                        ";
                                        using (var updateCmd = new SqlCommand(updateQuery, conn.GetConnection()))
                                        {
                                            updateCmd.Parameters.AddWithValue("@StartDate", startDate);
                                            updateCmd.Parameters.AddWithValue("@EndDate", startDate);
                                            updateCmd.Parameters.AddWithValue("@SCTID", (int)result);

                                            updateCmd.ExecuteNonQuery();
                                        }

                                        newSCTID = (int)result; // Keep same ID
                                    }

                                }
                                else
                                {
                                    // Record doesn’t exist → insert new
                                    string insertQuery = @"
                                        INSERT INTO StudentClassTeacher (StudentID, ClassTeacherID, StartDate, EndDate, IsActive)
                                        VALUES (@StudentID, @ClassTeacherID, @StartDate, @EndDate, 1);
                                        SELECT CAST(SCOPE_IDENTITY() AS INT);
                                    ";

                                    using (var insertCmd = new SqlCommand(insertQuery, conn.GetConnection()))
                                    {
                                        insertCmd.Parameters.AddWithValue("@StudentID", Student.StudentID);
                                        insertCmd.Parameters.AddWithValue("@ClassTeacherID", classTeacherID);
                                        insertCmd.Parameters.AddWithValue("@StartDate", startDate);
                                        insertCmd.Parameters.AddWithValue("@EndDate", startDate);

                                        newSCTID = (int)insertCmd.ExecuteScalar();
                                    }

                                    mgmStudentClassTeacher mgmStudentClassTeacher = new mgmStudentClassTeacher();
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

                                    Dictionary<string, object?> parameters = new Dictionary<string, object?>();
                                    parameters.Add("@SCTID", newSCTID);

                                    mgmStudentClassTeacher.loadDataFromDB(conn.GetConnection(), null, parameters);


                                    using (FormUpdatePayment formPay = new FormUpdatePayment(mgmStudentClassTeacher.studentClassTeachers.First(), 0))
                                    {
                                        if (formPay.ShowDialog() == DialogResult.OK)
                                        {
                                            Payment payment = formPay.dataPayment;
                                            string insertQueryPay = $@"
                                        INSERT INTO {Constants.tbPayment}
                                            (StudentClassTeacherID, PayDate, Duration, StartDate, EndDate, Bill, Discount, Amount)
                                        VALUES
                                            (@StudentClassTeacherID, @PayDate, @Duration, @StartDate, @EndDate, @Bill, @Discount, @Amount);
                                    ";

                                            string updateQueryPay = $@"
                                        UPDATE {Constants.tbStudentClassTeacher}
                                        SET EndDate = @EndDate
                                        WHERE StudentClassTeacherID = @StudentClassTeacherID;
                                    ";

                                            using (SqlConnection connection = conn.GetConnection())
                                            {
                                                using (SqlTransaction transaction = connection.BeginTransaction())
                                                {
                                                    try
                                                    {
                                                        using (SqlCommand insertCmd = new SqlCommand(insertQueryPay, connection, transaction))
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

                                                        using (SqlCommand updateCmd = new SqlCommand(updateQueryPay, connection, transaction))
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
                                }
                            }

                        }
                        else
                        {
                            // Not found → handle error
                            //throw new Exception("ClassTeacher not found.");
                            MessageBox.Show("Please select Class and Teacher!!!","warning!!",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        }
                    }




                    paginator.AllData = mgmStudentClassTeacher.loadDataFromDB(conn.GetConnection(), null, parameters);

                }
            }
        }
        public void update() { }
        public void update(List<StudentClassTeacher> selected)
        {
            var form = new FormSelectClassTeacher(1,0,0); //to get claID and teaID for update students
            form.Show();
            form.onSet += (s, e) =>
            {
                int teaID = form.TeacherID;
                int clsID = form.ClassID;


                // Get or create the new ClassTeacherID (you must already have this)
                int newClassTeacherID = getClassTeacherID(clsID, teaID); // Replace with your actual logic

                string query = $@"
                        UPDATE {Constants.tbStudentClassTeacher}
                        SET ClassTeacherID = @ClassTeacherID
                        WHERE StudentClassTeacherID = @StudentClassTeacherID";

                foreach (StudentClassTeacher sct in selected)
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn.GetConnection()))
                    {
                        cmd.Parameters.AddWithValue("@ClassTeacherID", newClassTeacherID);
                        cmd.Parameters.AddWithValue("@StudentClassTeacherID", sct.StudentClassTeacherID);

                        cmd.ExecuteNonQuery();
                    }
                }
                headerCheckBox.Checked = false;

                addItemToCombo(clsID,teaID);

                LB2 = paginator.AllData.Count().ToString("D2");
                LB4 = paginator.AllData.Where(e => e.Student.Sex == "male").Count().ToString("D2");
                LB6 = paginator.AllData.Where(e => e.Student.Sex == "female").Count().ToString("D2");
            };

        }
        int getClassTeacherID(int classID, int teacherID)
        {
            using (conn.GetConnection())
            {

                // Try to find existing
                string select = $@"
                    SELECT ClassTeacherID FROM {Constants.tbClassTeacher}
                    WHERE ClassID = @ClassID AND TeacherID = @TeacherID";

                using (SqlCommand cmd = new SqlCommand(select, conn.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@ClassID", classID);
                    cmd.Parameters.AddWithValue("@TeacherID", teacherID);

                    object result = cmd.ExecuteScalar();
                    if (result != null)
                        return Convert.ToInt32(result);
                    else return 0;
                }
                // Not found → insert new
                //string insert = $@"
                //    INSERT INTO {Constants.tbClassTeacher} (ClassID, TeacherID)
                //    OUTPUT INSERTED.ClassTeacherID
                //    VALUES (@ClassID, @TeacherID)";

                //using (SqlCommand cmd = new SqlCommand(insert, conn.GetConnection()))
                //{
                //    cmd.Parameters.AddWithValue("@ClassID", classID);
                //    cmd.Parameters.AddWithValue("@TeacherID", teacherID);
                //    return Convert.ToInt32(cmd.ExecuteScalar());
                //}
            }
        }

        public void delete()
        {
            var result = MessageBox.Show("Are you sure you want to delete?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                List<StudentClassTeacher> selected = mgmStudentClassTeacher.studentClassTeachers.Where(e => e.IsSelected == true).ToList();

                string query = $@"
                        UPDATE {Constants.tbStudentClassTeacher}
                        SET IsActive =0 
                        WHERE StudentClassTeacherID = @StudentClassTeacherID";

                foreach (StudentClassTeacher sct in selected)
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn.GetConnection()))
                    {
                        cmd.Parameters.AddWithValue("@StudentClassTeacherID", sct.StudentClassTeacherID);

                        cmd.ExecuteNonQuery();
                    }
                }
                headerCheckBox.Checked = false;
                paginator.AllData = mgmStudentClassTeacher.loadDataFromDB(conn.GetConnection(), null, parameters);

                LB2 = paginator.AllData.Count().ToString("D2");
                LB4 = paginator.AllData.Where(e => e.Student.Sex == "male").Count().ToString("D2");
                LB6 = paginator.AllData.Where(e => e.Student.Sex == "female").Count().ToString("D2");

            }
        }
        private void cbSelectClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbClass.SelectedIndex >= 0)
            {
                //cbSelectTeacher.SelectedIndexChanged -= cbSelectTeacher_SelectedIndexChanged;
                //int classID = Convert.ToInt32(cbSelectClass.SelectedValue);

                string query = $@"
                    SELECT t.*
                    FROM {Constants.tbTeacher} t
                    JOIN {Constants.tbClassTeacher} ct ON t.TeacherID = ct.TeacherID
                    WHERE ct.ClassID = @ClassID";
                var parameters = new Dictionary<string, object>();
                //if (cbSelectClass.SelectedItem is Item selectedItem)
                //{
                //    int classID = selectedItem.ID;
                //    parameters.Add("@ClassID", classID);

                //}
                if (cbClass.SelectedItem is Item item)
                    parameters.Add("@ClassID", item.ID);

                //parameters.Add("@ClassID", ((Item)cbSelectClass.SelectedItem).ID);


                mgmTeacher.loadDataFromDB(conn.GetConnection(), query, parameters);
                cbTeacher.Items.Clear();
                cbTeacher.Items.Add(new Item { ID = 0, Name = "All" });
                foreach (Teacher t in mgmTeacher.teachers)
                {
                    cbTeacher.Items.Add(new Item { ID = t.TeacherID, Name = t.NameEng });
                }
                if (cbTeacher.Items.Count >= 0) cbTeacher.SelectedIndex = 0;
                //cbSelectTeacher.SelectedIndexChanged += cbSelectTeacher_SelectedIndexChanged;

            }
        }
    }
}
